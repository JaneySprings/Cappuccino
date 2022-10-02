using Cappuccino.Core.Network.Polling;

namespace Cappuccino.App.iOS;


[Register ("SceneDelegate")]
public class SceneDelegate : UIResponder, IUIWindowSceneDelegate {

	[Export ("window")]
	public UIWindow? Window { get; set; }


	[Export ("scene:willConnectToSession:options:")]
	public void WillConnect (UIScene scene, UISceneSession session, UISceneConnectionOptions connectionOptions) {}

	[Export ("sceneDidDisconnect:")]
	public void DidDisconnect (UIScene scene) {}

	[Export ("sceneDidBecomeActive:")]
	public void DidBecomeActive (UIScene scene) {
		LongPollManager.Instance.StartExecution();
	}

	[Export ("sceneWillResignActive:")]
	public void WillResignActive (UIScene scene) {
		LongPollManager.Instance.StopExecution();
	}

	[Export ("sceneWillEnterForeground:")]
	public void WillEnterForeground (UIScene scene) {}

	[Export ("sceneDidEnterBackground:")]
	public void DidEnterBackground (UIScene scene) {}
}
