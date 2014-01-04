// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the AppDelegate type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CodeFramework.iOS;

namespace CodeBucket.iOS
{
	using Cirrious.CrossCore;
	using Cirrious.MvvmCross.Touch.Platform;
	using Cirrious.MvvmCross.ViewModels;
	using MonoTouch.Foundation;
	using MonoTouch.UIKit;

	/// <summary>
	/// The UIApplicationDelegate for the application. This class is responsible for launching the 
	/// User Interface of the application, as well as listening (and optionally responding) to 
	/// application events from iOS.
	/// </summary>
	[Register("AppDelegate")]
	public class AppDelegate : MvxApplicationDelegate
	{
		/// <summary>
		/// The window.
		/// </summary>
		private UIWindow window;

		/// <summary>
		/// This is the main entry point of the application.
		/// </summary>
		/// <param name="args">The args.</param>
		public static void Main(string[] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main(args, null, "AppDelegate");
		}

		private class UserVoiceStyleSheet : UserVoice.UVStyleSheet
		{
			public override UIColor NavigationBarTextColor
			{
				get
				{
					return UIColor.White;
				}
			}

			public override UIColor NavigationBarTintColor
			{
				get
				{
					return UIColor.White;
				}
			}
		}

		/// <summary>
		/// Finished the launching.
		/// </summary>
		/// <param name="app">The app.</param>
		/// <param name="options">The options.</param>
		/// <returns>True or false.</returns>
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			var iRate = MTiRate.iRate.SharedInstance;
			iRate.AppStoreID = 551531422;

			UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
			UINavigationBar.Appearance.TintColor = UIColor.White;
			UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(45,80,148);
			UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.White, Font = UIFont.SystemFontOfSize(18f) });
			CodeFramework.iOS.Utils.Hud.BackgroundTint = UIColor.FromRGBA(228, 228, 228, 128);

			UserVoice.UVStyleSheet.StyleSheet = new UserVoiceStyleSheet();

			UISegmentedControl.Appearance.TintColor = UIColor.FromRGB(45,80,148);
			UITableViewHeaderFooterView.Appearance.TintColor = UIColor.FromRGB(228, 228, 228);
			UILabel.AppearanceWhenContainedIn(typeof(UITableViewHeaderFooterView)).TextColor = UIColor.FromRGB(136, 136, 136);
			UILabel.AppearanceWhenContainedIn(typeof(UITableViewHeaderFooterView)).Font = UIFont.SystemFontOfSize(13f);

			UIToolbar.Appearance.BarTintColor = UIColor.FromRGB(245, 245, 245);

			UIBarButtonItem.AppearanceWhenContainedIn(typeof(UISearchBar)).SetTitleTextAttributes(new UITextAttributes()
				{
					TextColor = UIColor.White,
				}, UIControlState.Normal);

			this.window = new UIWindow(UIScreen.MainScreen.Bounds);

			// Setup theme
			Theme.Setup();

			var presenter = new TouchViewPresenter(this.window);

			var setup = new Setup(this, presenter);
			setup.Initialize();

			Mvx.Resolve<CodeFramework.Core.Services.IAnalyticsService>().Init("UA-44040302-1", "CodeHub");

			var startup = Mvx.Resolve<IMvxAppStart>();
			startup.Start();

			this.window.MakeKeyAndVisible();

			return true;
		}

		public override bool HandleOpenURL(UIApplication application, NSUrl url)
		{
			if (url == null)
				return false;
			var uri = new System.Uri(url.ToString());

//			if (Slideout != null)
//			{
//				if (!string.IsNullOrEmpty(uri.Host))
//				{
//					string username = uri.Host;
//					string repo = null;
//
//					if (uri.Segments.Length > 1)
//						repo = uri.Segments[1].Replace("/", "");
//
//					if (repo == null)
//						Slideout.SelectView(new CodeBucket.ViewControllers.ProfileViewController(username));
//					else
//						Slideout.SelectView(new CodeBucket.ViewControllers.RepositoryInfoViewController(username, repo, repo));
//				}
//			}

			return true;
		}
	}
}