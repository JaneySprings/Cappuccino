using System.Globalization;
using Cappuccino.Core.Network.Config;
using DotNet.Meteor.HotReload.Plugin;
using Scope = Cappuccino.Core.Network.Auth.Permissions;

namespace Cappuccino.App.Multiplatform;

public static class MauiProgram {
	private const int APP_ID = 7317599;
	private const int LONGPOLL_VERSION = 12;
	private const string API_VERSION = "5.131";

	public static MauiApp CreateMauiApp() {
		CredentialsManager.ApplyConfiguration(new ApiConfiguration.Builder()
			.WithApiLanguage(CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
            .WithAppId(APP_ID)
            .WithApiVersion(API_VERSION)
            .WithLongPollVersion(LONGPOLL_VERSION)
            .WithTokenStorageHandler(new TokenStorageProvider())
#if DEBUG
            .WithPermissions(new[] { Scope.Friends, Scope.Messages })
#else
            .WithPermissions(new[] { Scope.Friends, Scope.Messages, Scope.Offline })
#endif
			.Build()
		);

		return MauiApp.CreateBuilder()
			.UseMauiApp<App>()
#if DEBUG
			.EnableHotReload()
#endif
			.ConfigureFonts(fonts => {
				fonts.AddFont("VKSansDisplay-DemiBold.ttf", "VKDemiBold");
				fonts.AddFont("VKSansDisplay-Medium.ttf", "VKMedium");
				fonts.AddFont("VKSansDisplay-Bold.ttf", "VKBold");
			})
			.Build();
	}
}
