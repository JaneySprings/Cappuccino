using System;
using Android.Graphics;

namespace Cappuccino.App.Android.Utils {
    public class PaintUtils {
        private const int BitmapSize = 100;
        private const float TextSize = 40f;
        private const float TextStroke = 2f;
        private static readonly Color[] Gradient = new[] { Color.ParseColor("#53baff"), Color.ParseColor("#238aef") };

        public static Bitmap CreateCover(string title) {
            Bitmap bitmap = Bitmap.CreateBitmap(BitmapSize, BitmapSize, Bitmap.Config.Argb8888!)!;
            Canvas canvas = new Canvas(bitmap);
            Paint paint = new Paint(PaintFlags.AntiAlias);
            string text = title != String.Empty ? title[..1] : "-";

            paint.AntiAlias = true;
            paint.SetShader(new LinearGradient(0f, 0f, BitmapSize, BitmapSize, Gradient[0], Gradient[1], Shader.TileMode.Mirror!));
            canvas.DrawPaint(paint);
            paint.SetShader(null);

            paint.TextSize = TextSize;
            paint.StrokeWidth = TextStroke;
            paint.SetStyle(Paint.Style.FillAndStroke);
            paint.Color = Color.White;
            canvas.DrawText(text, BitmapSize/2f - paint.MeasureText(text)/2f, 64f, paint);

            return bitmap;
        }
    }
}