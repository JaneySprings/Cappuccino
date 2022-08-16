// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Cappuccino.App.iOS.UI.Common
{
	partial class HeaderViewCell
	{
		[Outlet]
		Cappuccino.App.iOS.UI.Styles.UIStylableLabel caption { get; set; }

		[Outlet]
		UIKit.UIView divider { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (caption != null) {
				caption.Dispose ();
				caption = null;
			}

			if (divider != null) {
				divider.Dispose ();
				divider = null;
			}
		}
	}
}
