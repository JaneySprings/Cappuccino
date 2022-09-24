public string RootDirectory => MakeAbsolute(Directory("./")).ToString();

public string ArtifactsDirectory => $"{RootDirectory}/Artifacts";

public string CoreAssemblyPath => $"{ArtifactsDirectory}/Common/Cappuccino.Core.Network.dll";
public string iOSBundlePath => $"{ArtifactsDirectory}/iOS/ios-arm64/Cappuccino.App.iOS.app";
public string AndroidBundlePath => $"{ArtifactsDirectory}/Android/Cappuccino.App.Android.apk";

public string CoreProjectPath => $"{RootDirectory}/Cappuccino.Core.Network/Cappuccino.Core.Network.csproj";
public string AndroidProjectPath => $"{RootDirectory}/Cappuccino.App.Android/Cappuccino.App.Android.csproj";
public string iOSProjectPath => $"{RootDirectory}/Cappuccino.App.iOS/Cappuccino.App.iOS.csproj";