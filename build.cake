#addin nuget:?package=Cake.FileHelpers&version=5.0.0
#load "env.cake"

string target           =  Argument("target"        , "clean");
string configuration    =  Argument("configuration" , "debug");
string version          =  Argument("up-version"    , "1.0");
string signkey          =  Argument("signkey"       , "null");
string apikey           =  Argument("apikey"        , "null");
string device           =  Argument("device"        , "");


//////////////////////////////////////////////////////////////////////
// COMMON
//////////////////////////////////////////////////////////////////////

Task("clean").Does(() => {
    CleanDirectories("./**/bin");
    CleanDirectories("./**/obj");

    if (!DirectoryExists(ArtifactsDirectory)) {
        CreateDirectory(ArtifactsDirectory);
        return;
    }

    foreach (var file in System.IO.Directory.GetFiles(ArtifactsDirectory)) 
        DeleteFile(file);
    foreach (var directory in System.IO.Directory.GetDirectories(ArtifactsDirectory))
        if (!directory.Equals($"{ArtifactsDirectory}/Common"))
            DeleteDirectory(directory, new DeleteDirectorySettings { Recursive = true });
    
    CreateDirectory(PublishDirectory);
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
        NuGetPack(NuSpecCorePath, new NuGetPackSettings { OutputDirectory = PublishDirectory });

        foreach (var file in System.IO.Directory.GetFiles($"{ArtifactsDirectory}/Common")) 
            if (!file.Contains("Cappuccino.Core.Network"))
                DeleteFile(file);
    });

Task("network-test").Does(() => DotNetTest(ProjectCoreTestsPath, new DotNetTestSettings {  
    Configuration = configuration,
    Verbosity = DotNetVerbosity.Quiet,
    Logger = $"trx;LogFileName={TestsResultPath}"
}));

Task("network-publish").Does(() => NuGetPush(NugetCorePath, new NuGetPushSettings { 
    Source = "https://api.nuget.org/v3/index.json", 
    ApiKey = apikey 
}));

//////////////////////////////////////////////////////////////////////
// CAPPUCCINO iOS
//////////////////////////////////////////////////////////////////////

Task("ios")
    .IsDependentOn("clean")
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

        DotNetPublish(ProjectiOSPath, DotNetPublishSettings($"{ArtifactsDirectory}/iOS", "ios-arm64"));
        MoveFile(BundleiOSPath, $"{PublishDirectory}/Cappuccino.App.iOS.{version}.ipa");
        Zip($"{BundleiOSPath}.dSYM", $"{PublishDirectory}/Cappuccino.App.iOS.dSYM.zip");
    });

//////////////////////////////////////////////////////////////////////
// CAPPUCCINO ANDROID
//////////////////////////////////////////////////////////////////////


//////////////////////////////////////////////////////////////////////
// DEBUG
//////////////////////////////////////////////////////////////////////

Task("ios-run")
    .Does(() => {
        DotNetBuild(ProjectiOSPath, new DotNetBuildSettings { Configuration = configuration });
        StartProcess("open", "/Applications/Xcode.app/Contents/Developer/Applications/Simulator.app");
        StartProcess("dotnet", "xharness apple run"
            + $" --app {ArtifactsDirectory}/iOS/iossimulator-x64/Cappuccino.App.iOS.app" 
            + $" --device {device}"
            + $" --output-directory {ArtifactsDirectory}"
            +  " --target ios-simulator-64"
        );
    });


RunTarget(target);