public string ArtifactsDirectory => MakeAbsolute(Directory("./Artifacts")).ToString();
public string iOSBundlePath => Directory($"{ArtifactsDirectory}/iOS/ios-arm64/Cappuccino.App.iOS.app").ToString();