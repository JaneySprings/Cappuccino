public string RootDirectory => MakeAbsolute(Directory("./")).ToString();

public string ArtifactsDirectory => $"{RootDirectory}/Artifacts";
public string PublishDirectory => $"{ArtifactsDirectory}/Publish";

public string BundleAndroidPath => $"{ArtifactsDirectory}/Android/Cappuccino.App.Android.apk";
public string BundleiOSPath => $"{ArtifactsDirectory}/iOS/Cappuccino.App.iOS.ipa";

public string RootAndroidDirectory => $"{RootDirectory}/Cappuccino.App.Android";
public string RootiOSDirectory => $"{RootDirectory}/Cappuccino.App.iOS";

public string ProjectCorePath => $"{RootDirectory}/Cappuccino.Core.Network/Cappuccino.Core.Network.csproj";
public string ProjectCoreTestsPath => $"{RootDirectory}/Cappuccino.Core.Network.Tests/Cappuccino.Core.Network.Tests.csproj";
public string ProjectAndroidPath => $"{RootAndroidDirectory}/Cappuccino.App.Android.csproj";
public string ProjectiOSPath => $"{RootiOSDirectory}/Cappuccino.App.iOS.csproj";

public string NuSpecPath => $"{RootDirectory}/Cappuccino.Core.Network/Nuget/Cappuccino.Core.Network.nuspec";
public string NuGetPackagePath => $"{ArtifactsDirectory}/Cappuccino.Core.Network.*.nupkg";


public DotNetPublishSettings DotNetPublishSettings(string output, string key, string runtimeIdentifier = null) {
    var settings = new DotNetMSBuildSettings {
        BinaryLogger = new MSBuildBinaryLoggerSettings {
            FileName = $"{output}/msbuild.binlog",
            Enabled = true
        }
    };
    settings.WithProperty("SignAssembly", key.Equals("null") ? "false" : "true");
    settings.WithProperty("AssemblyOriginatorKeyFile", key);

    if (runtimeIdentifier != null)
        settings.WithProperty("RuntimeIdentifier", runtimeIdentifier);

    return new DotNetPublishSettings {
        Verbosity = DotNetVerbosity.Minimal,
        MSBuildSettings = settings,
        OutputDirectory = output,
        Configuration = "release"
    };
}