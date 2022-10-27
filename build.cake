#addin nuget:?package=Cake.FileHelpers&version=5.0.0
#load "env.cake"

string target           =  Argument("target"        , "clean");
string configuration    =  Argument("configuration" , "debug");
string version          =  Argument("up-version"    , "1.0");
string signkey          =  Argument("signkey"       , "null");
string apikey           =  Argument("apikey"        , "null");


//////////////////////////////////////////////////////////////////////
// COMMON
//////////////////////////////////////////////////////////////////////

Task("clean").Does(() => {
    CleanDirectories("./**/bin");
    CleanDirectories("./**/obj");
    CleanDirectory($"{ArtifactsDirectory}/iOS");
    CleanDirectory($"{ArtifactsDirectory}/Android");
    CleanDirectory(PublishDirectory);
});

//////////////////////////////////////////////////////////////////////
// NETWORK
//////////////////////////////////////////////////////////////////////

Task("network-build")
    .IsDependentOn("clean")
    .Does(() => {
        var options = System.Text.RegularExpressions.RegexOptions.None;
        var pattern = "\\<[Vv]ersion\\>[^,]*?\\</[Vv]ersion\\>";

        foreach (var match in FindRegexMatchesInFile(ProjectCorePath, pattern, options)) 
            ReplaceTextInFiles(ProjectCorePath, match, $"<Version>{version}</Version>");
        foreach (var match in FindRegexMatchesInFile(NuSpecPath, pattern, options)) 
            ReplaceTextInFiles(NuSpecPath, match, $"<version>{version}</version>");
    })
    .Does(() => {
        DotNetPublish(ProjectCorePath, DotNetPublishSettings($"{ArtifactsDirectory}/Common", signkey));
        NuGetPack(NuSpecPath, new NuGetPackSettings { OutputDirectory = PublishDirectory });
    })
    .Does(() => MoveFile(
        $"{ArtifactsDirectory}/Common/msbuild.binlog", 
        $"{PublishDirectory}/Cappuccino.Core.Network.{version}.binlog"
    ));

Task("network-test").Does(() => DotNetTest(ProjectCoreTestsPath, new DotNetTestSettings {  
    Configuration = configuration,
    Verbosity = DotNetVerbosity.Quiet,
    ResultsDirectory = ArtifactsDirectory,
    Loggers = new[] { "trx" }
}));

Task("network-publish").Does(() => NuGetPush(NuGetPackagePath, new NuGetPushSettings { 
    Source = "https://api.nuget.org/v3/index.json",
    ApiKey = apikey
}));

//////////////////////////////////////////////////////////////////////
// iOS
//////////////////////////////////////////////////////////////////////

Task("ios-build")
    .IsDependentOn("clean")
    .Does(() => {
        var options = System.Text.RegularExpressions.RegexOptions.None;
        var pattern = "\\<key\\>CFBundle(Short)?Version(String)?\\</key\\>[^,]*?\\<string\\>[^,]*?\\</string\\>";
        var plist = $"{RootiOSDirectory}/Info.plist";
        var replace = $"\t<string>{version}</string>";

        foreach (var match in FindRegexMatchesInFile(plist, pattern, options)) {
            var position = match.LastIndexOf('\t');
            var patch = match.Remove(position, match.Length - position).Insert(position, replace);
            ReplaceTextInFiles(plist, match, patch);
        }
    })
    .Does(() => DotNetPublish(ProjectiOSPath, DotNetPublishSettings($"{ArtifactsDirectory}/iOS", signkey, "ios-arm64")))
    .Does(() => {
        Zip($"{BundleiOSPath}.dSYM", $"{PublishDirectory}/Cappuccino.App.iOS.dSYM.zip");
        MoveFile(BundleiOSPath, $"{PublishDirectory}/Cappuccino.App.iOS.{version}.ipa");
        MoveFile(
            $"{ArtifactsDirectory}/iOS/msbuild.binlog",
            $"{PublishDirectory}/Cappuccino.App.iOS.{version}.binlog"
        );
    });

//////////////////////////////////////////////////////////////////////
// ANDROID
//////////////////////////////////////////////////////////////////////



RunTarget(target);