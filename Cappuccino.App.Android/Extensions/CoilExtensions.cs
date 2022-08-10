using Android.Graphics;
using Android.Widget;
using Xamarin.Coil;
using Xamarin.Coil.Request;

namespace Cappuccino.App.Android.Extensions {
    public static class CoilExtensions {
        public static IDisposable Load(this ImageView imageView, string? uri) {
            ImageRequest? request = new ImageRequest.Builder(imageView.Context)
                .Data(uri)
                .Target(imageView)
                .Build();
            return Coil.ImageLoader(imageView.Context).Enqueue(request);
        }
        public static IDisposable Load(this ImageView imageView, Bitmap? uri) {
            ImageRequest? request = new ImageRequest.Builder(imageView.Context)
                .Data(uri)
                .Target(imageView)
                .Build();
            return Coil.ImageLoader(imageView.Context).Enqueue(request);
        }
    }

}