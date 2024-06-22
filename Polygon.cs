using SkiaSharp;
using System.Collections.Generic;

namespace LearnSkiaSharp
{
    public class Polygon : IDrawable
    {
        public List<SKPoint> Points { get; set; }
        public SKPaint Paint { get; set; }
        public void Draw(SKCanvas canvas)
        {
            if (Points.Count > 0)
            {
                using (var path = new SKPath())
                {
                    path.MoveTo(Points[0]);
                    for (int i = 0; i < Points.Count; i++)
                    {
                        path.LineTo(Points[i]);
                    }
                    path.Close();
                    canvas.DrawPath(path, Paint);
                }
            }
        }
    }
}
