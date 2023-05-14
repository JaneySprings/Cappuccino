public string RootDirectory => MakeAbsolute(Directory("./")).ToString();

public string RootCoreDirectory => $"{RootDirectory}/Cappuccino.Core.Network";
public string RootCoreTestsDirectory => $"{RootDirectory}/Cappuccino.Core.Network";
public string RootAndroidDirectory => $"{RootDirectory}/Cappuccino.App.Android";
public string RootAppleDirectory => $"{RootDirectory}/Cappuccino.App.iOS";

public string ProjectCorePath => $"{RootCoreDirectory}/Cappuccino.Core.Network.csproj";
public string ProjectCoreTestsPath => $"{RootCoreTestsDirectory}/Cappuccino.Core.Network.Tests.csproj";
public string ProjectAndroidPath => $"{RootAndroidDirectory}/Cappuccino.App.Android.csproj";
public string ProjectApplePath => $"{RootAppleDirectory}/Cappuccino.App.iOS.csproj";

public string ArtifactsDirectory => $"{RootDirectory}/Artifacts";

public string ArtifactsCoreDirectory => $"{ArtifactsDirectory}/Common";
public string ArtifactsAndroidDirectory => $"{ArtifactsDirectory}/Android";
public string ArtifactsAppleDirectory => $"{ArtifactsDirectory}/Apple";

public string BundleCoreName => "Cappuccino.Core.Network";
public string BundleAndroidName => "Cappuccino.App.Android";
public string BundleAppleName => "Cappuccino.App.iOS";

public string NuSpecPath => $"{RootCoreDirectory}/Nuget/Cappuccino.Core.Network.nuspec";


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