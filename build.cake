#addin nuget:?package=Cake.FileHelpers&version=5.0.0
#addin nuget:?package=Cake.Xamarin&version=4.0.0

string target = Argument("target", "BuildAll");
string configuration = Argument("configuration", "debug");
string version = "1.0";
string root = ".";


Task("Prepare")
    .Does(() => {
        Information("Cleaning binaries folder ...");
        CleanDirectory($"{root}/Binaries");

        Information($"Updating versions to {version} ...");
        ReplaceTextInFiles("./**/*", "{" + "CP_VERSION" + "}", version);
    });

Task("BuildNetwork")
    .IsDependentOn("Prepare")
    .Does(() => {
        DotNetBuild($"{root}/Cappuccino.Core.Network/Cappuccino.Core.Network.csproj", new DotNetBuildSettings {
            Configuration = configuration
        });
    });

Task("BuildIOS")
    .IsDependentOn("BuildNetwork")
    .Does(() => {
        NuGetRestore($"{root}/Cappuccino.App.iOS/Cappuccino.App.iOS.csproj");
        BuildiOSIpa($"{root}/Cappuccino.App.iOS/Cappuccino.App.iOS.csproj", configuration);
        Zip($"{root}/Binaries/iOS/Cappuccino.App.iOS.app", $"{root}/Binaries/Cappuccino-iOS.ipa");
    });

Task("BuildAndroid")
    .IsDependentOn("BuildNetwork")
    .Does(() => {
        
    });

Task("BuildAll")
    .IsDependentOn("BuildIOS")
    .IsDependentOn("BuildAndroid");


RunTarget(target);