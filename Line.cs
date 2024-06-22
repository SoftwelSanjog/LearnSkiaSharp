using SkiaSharp;

namespace LearnSkiaSharp
{
    public class Line : IDrawable
    {
        public SKPoint StartPoint { get; set; }
        public SKPoint EndPoint { get; set; }
        public SKPaint Paint { get; set; }
        public void Draw(SKCanvas canvas)
        {
            canvas.DrawLine(StartPoint, EndPoint, Paint);
        }
    }
}
