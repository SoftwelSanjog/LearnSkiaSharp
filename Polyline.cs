using SkiaSharp;
using System.Collections.Generic;

namespace LearnSkiaSharp
{
    public class Polyline : IDrawable

    {
        public List<SKPoint> Points { get; set; }
        public SKPaint Paint { get; set; }
        public void Draw(SKCanvas canvas)
        {
            if (Points.Count > 0)
            {
                for (int i = 0; i < Points.Count - 1; i++)
                {
                    canvas.DrawLine(Points[i], Points[i + 1], Paint);
                }
            }
        }
    }
}
