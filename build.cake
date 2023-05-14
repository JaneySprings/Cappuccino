#addin nuget:?package=Cake.FileHelpers&version=5.0.0
#load "env.cake"

string configuration = Argument("configuration", "debug");
string version = Argument("up-version", "1.0");
string target = Argument("target", "build");
string key = Argument("sign-key", "null");


//////////////////////////////////////////////////////////////////////
// COMMON
//////////////////////////////////////////////////////////////////////

Task("clean").Does(() => {
    CleanDirectories("./**/bin");
    CleanDirectories("./**/obj");
    CleanDirectory($"{ArtifactsDirectory}");
});

Task("archive").Does(() => {
    EnsureDirectoryExists(ArtifactsDirectory);
    Zip(RootDirectory, $"{ArtifactsDirectory}/source-code.{version}.zip");
});

Task("build")
    .IsDependentOn("clean")
    .IsDependentOn("archive")
    .IsDependentOn("network-build")
    .IsDependentOn("ios-build");

//////////////////////////////////////////////////////////////////////
// NETWORK
//////////////////////////////////////////////////////////////////////

Task("network-build")
    .Does(() => CleanDirectory($"{ArtifactsCoreDirectory}"))
    .Does(() => {
        var options = System.Text.RegularExpressions.RegexOptions.None;
        var pattern = "\\<[Vv]ersion\\>[^,]*?\\</[Vv]ersion\\>";

        foreach (var match in FindRegexMatchesInFile(ProjectCorePath, pattern, options)) 
            ReplaceTextInFiles(ProjectCorePath, match, $"<Version>{version}</Version>");
        foreach (var match in FindRegexMatchesInFile(NuSpecPath, pattern, options)) 
            ReplaceTextInFiles(NuSpecPath, match, $"<version>{version}</version>");
    })
    .Does(() => {
        DotNetPublish(ProjectCorePath, DotNetPublishSettings(ArtifactsCoreDirectory, key));
        NuGetPack(NuSpecPath, new NuGetPackSettings { OutputDirectory = ArtifactsCoreDirectory });
    })
    .Does(() => MoveFile(
        $"{ArtifactsCoreDirectory}/msbuild.binlog", 
        $"{ArtifactsCoreDirectory}/{BundleCoreName}.{version}.binlog"
    ));

Task("network-test").Does(() => DotNetTest(ProjectCoreTestsPath, new DotNetTestSettings {  
    Configuration = configuration,
    Verbosity = DotNetVerbosity.Quiet,
    ResultsDirectory = ArtifactsCoreDirectory,
    Loggers = new[] { "trx" }
}));

//////////////////////////////////////////////////////////////////////
// iOS
//////////////////////////////////////////////////////////////////////

Task("ios-build")
    .Does(() => CleanDirectory($"{ArtifactsAppleDirectory}"))
    .Does(() => {
        var options = System.Text.RegularExpressions.RegexOptions.None;
        var pattern = "\\<key\\>CFBundle(Short)?Version(String)?\\</key\\>[^,]*?\\<string\\>[^,]*?\\</string\\>";
        var plist = $"{RootAppleDirectory}/Info.plist";
        var replace = $"\t<string>{version}</string>";

        foreach (var match in FindRegexMatchesInFile(plist, pattern, options)) {
            var position = match.LastIndexOf('\t');
            var patch = match.Remove(position, match.Length - position).Insert(position, replace);
            ReplaceTextInFiles(plist, match, patch);
        }
    })
    .Does(() => DotNetPublish(ProjectApplePath, DotNetPublishSettings(ArtifactsAppleDirectory, key, "ios-arm64")))
    .Does(() => {
        // Zip($"{BundleApplePath}.dSYM", $"{PublishDirectory}/Cappuccino.App.iOS.dSYM.zip");
        MoveFile(
            $"{ArtifactsAppleDirectory}/msbuild.binlog", 
            $"{ArtifactsAppleDirectory}/{BundleAppleName}.{version}.binlog"
        );
        MoveFile(
            $"{ArtifactsAppleDirectory}/{BundleAppleName}.ipa", 
            $"{ArtifactsAppleDirectory}/{BundleAppleName}.{version}.ipa"
        );
    });

//////////////////////////////////////////////////////////////////////
// ANDROID
//////////////////////////////////////////////////////////////////////



RunTarget(target);