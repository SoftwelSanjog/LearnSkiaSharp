using SkiaSharp;

namespace LearnSkiaSharp
{
    public class Circle : IDrawable
    {
        public SKPoint Center { get; set; }
        public float Radius { get; set; }
        public SKPaint Paint { get; set; }

        public void Draw(SKCanvas canvas)
        {
            canvas.DrawCircle(Center, Radius, Paint);
        }
    }
}
