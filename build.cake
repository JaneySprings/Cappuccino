#addin nuget:?package=Cake.FileHelpers&version=5.0.0
#load "env.cake"

string target = Argument("target", "Clean");
string version = Argument("cp-version", "1.0");
string configuration = Argument("configuration", "debug");
//Debug
string device = Argument("device", "");


Task("Clean")
    .Does(() => {
        CleanDirectories("./**/bin");
        CleanDirectories("./**/obj");
        CleanDirectory(ArtifactsDirectory);
    });

Task("Network")
    .IsDependentOn("Clean")
    .Does(() => {
        var settings = new DotNetBuildSettings { 
            Configuration = configuration,
            Verbosity = DotNetVerbosity.Quiet 
        };
        DotNetBuild("./Cappuccino.Core.Network", settings);
    });

Task("iOS")
//    .IsDependentOn("Clean")
    .Does(() => {
        var settings = new DotNetPublishSettings { 
            Framework = "net6.0-ios",
            Configuration = configuration,
            Verbosity = DotNetVerbosity.Quiet 
        };
        DotNetPublish("./Cappuccino.App.iOS", settings);
    });



Task("iOS_Run")
    .IsDependentOn("iOS")
    .Does(() => {
        StartProcess("dotnet", "xharness apple install"
            + $" --app {iOSBundlePath}" 
            + $" --device {device}"
            + $" --output-directory {ArtifactsDirectory}"
            + " --target ios-device"
        );
    });


RunTarget(target);