#addin nuget:?package=Cake.FileHelpers&version=5.0.0
#load "env.cake"

string target           =  Argument("target"        , "clean");
string configuration    =  Argument("configuration" , "debug");
string version          =  Argument("up-version"    , "1.0");
string sign             =  Argument("sign"          , "false");
string apikey           =  Argument("apikey"        , "");
string device           =  Argument("device"        , "");


//////////////////////////////////////////////////////////////////////
// COMMON
//////////////////////////////////////////////////////////////////////

Task("clean").Does(() => {
    CleanDirectories("./**/bin");
    CleanDirectories("./**/obj");
    CleanDirectory(ArtifactsDirectory);
});

//////////////////////////////////////////////////////////////////////
// CAPPUCCINO NETWORK
//////////////////////////////////////////////////////////////////////

Task("network-build")
    .IsDependentOn("clean")
    .Does(() => {
        var options = System.Text.RegularExpressions.RegexOptions.None;
        var pattern = "\\<[Vv]ersion\\>[^,]*?\\</[Vv]ersion\\>";

        foreach (var match in FindRegexMatchesInFile(ProjectCorePath, pattern, options)) 
            ReplaceTextInFiles(ProjectCorePath, match, $"<Version>{version}</Version>");
        foreach (var match in FindRegexMatchesInFile(NuSpecCorePath, pattern, options)) 
            ReplaceTextInFiles(NuSpecCorePath, match, $"<version>{version}</version>");

        DotNetPublish(ProjectCorePath, DotNetPublishSettings($"{ArtifactsDirectory}/Common"));

        foreach (var file in System.IO.Directory.GetFiles($"{ArtifactsDirectory}/Common")) 
            if (!file.Contains("Cappuccino.Core.Network"))
                DeleteFile(file);
    });

Task("network-test").Does(() => DotNetTest(ProjectCoreTestsPath, new DotNetTestSettings {  
    Configuration = configuration,
    Verbosity = DotNetVerbosity.Quiet,
    Logger = $"trx;LogFileName={TestsResultPath}"
}));

Task("network-publish").Does(() => {
    NuGetPack(NuSpecCorePath, new NuGetPackSettings { 
        OutputDirectory = ArtifactsDirectory 
    });
    NuGetPush(NugetCorePath, new NuGetPushSettings { 
        Source = "https://api.nuget.org/v3/index.json", 
        ApiKey = apikey 
    });
});

//////////////////////////////////////////////////////////////////////
// CAPPUCCINO iOS
//////////////////////////////////////////////////////////////////////

Task("ios")
//    .IsDependentOn("clean")
    .Does(() => {
        var options = System.Text.RegularExpressions.RegexOptions.None;
        var pattern = "\\<key\\>CFBundle[A-z]*Version[A-z]*\\</key\\>[^,]*?\\<string\\>[^,]*?\\</string\\>";
        var plist = $"{RootiOSDirectory}/Info.plist";
        var tag = $"\t<string>{version}</string>";

        foreach (var match in FindRegexMatchesInFile(plist, pattern, options)) {
            var position = match.LastIndexOf('\t');
            var patch = match.Remove(position, match.Length - position).Insert(position, tag);
            ReplaceTextInFiles(plist, match, patch);
        }

        DotNetPublish(ProjectiOSPath, DotNetPublishSettings($"{ArtifactsDirectory}/iOS"));
        CopyFile(BundleiOSPath, $"{ArtifactsDirectory}/Cappuccino.App.iOS.{version}.ipa");
    });

Task("ios-run")
    .IsDependentOn("ios")
    .Does(() => {
        StartProcess("dotnet", "xharness apple install"
            + $" --app {ArtifactsDirectory}/iOS/ios-arm64/Cappuccino.App.iOS.app" 
            + $" --device {device}"
            + $" --output-directory {ArtifactsDirectory}"
            + " --target ios-device"
        );
    });

//////////////////////////////////////////////////////////////////////
// CAPPUCCINO ANDROID
//////////////////////////////////////////////////////////////////////


RunTarget(target);