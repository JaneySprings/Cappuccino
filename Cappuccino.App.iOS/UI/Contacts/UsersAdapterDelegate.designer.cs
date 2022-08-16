// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Cappuccino.App.iOS.UI.Contacts
{
	partial class UserViewCell
	{
		[Outlet]
		Cappuccino.App.iOS.UI.Styles.UIStylableLabel caption { get; set; }

		[Outlet]
		Cappuccino.App.iOS.UI.Styles.UIStylableLabel name { get; set; }

		[Outlet]
		Cappuccino.App.iOS.UI.Styles.UIRoundedImageView photo { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (name != null) {
				name.Dispose ();
				name = null;
			}

			if (caption != null) {
				caption.Dispose ();
				caption = null;
			}

			if (photo != null) {
				photo.Dispose ();
				photo = null;
			}
		}
	}
}
