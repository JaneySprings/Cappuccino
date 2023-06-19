using Cappuccino.Core.Network.Config;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Models;

namespace Cappuccino.App.Multiplatform;

public partial class App : Application {
	public App() {
		InitializeComponent();
		TokenExpiredHandler.Expired += TokenExpired;
		MainPage = CredentialsManager.IsInternalTokenValid() ? new RootPage() : new AuthPage();
	}

	protected override void CleanUp() {
		TokenExpiredHandler.Expired -= TokenExpired;
        base.CleanUp();
    }

    private void TokenExpired(ErrorResponse response) {
		MainPage = new AuthPage();
    }
}
