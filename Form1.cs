using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LearnSkiaSharp
{
    public partial class Form1 : Form
    {
        private List<IDrawable> shapes = new List<IDrawable>();
        private SKColor backgroundColor = new SKColor(47, 49, 54); // main screen color

        private SKMatrix matrix = SKMatrix.Identity;
        private SKMatrix inverseMatrix = SKMatrix.Identity;

        private List<SKPoint> currentPolylinePoints = new List<SKPoint>();
        private SKPoint previousMouseLocation;

        private float zoomFactor = 1.0f;
        public Form1()
        {
            InitializeComponent();
            InitializeShapes();
        }

        private void InitializeShapes()
        {
            var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.White,
                StrokeWidth = 5
            };
            //Add Circle
            shapes.Add(new Circle { Center = new SKPoint(100, 100), Radius = 50, Paint = paint });
            //Add Line
            shapes.Add(new Line { StartPoint = new SKPoint(200, 200), EndPoint = new SKPoint(300, 300), Paint = paint });
            //Add Polyline
            shapes.Add(new Polyline
            {
                Points = new List<SKPoint>
                {
                    new SKPoint(400,400),
                    new SKPoint(450,450),
                    new SKPoint(500,400)
                },
                Paint = paint
            });

            //Add Polygon
            shapes.Add(new Polygon
            {
                Points = new List<SKPoint> {
                    new SKPoint(600, 100),
                    new SKPoint(650, 200),
                    new SKPoint(700, 400),
                    new SKPoint(650, 150) },
                Paint = paint
            });
            skControl1.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void skControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(backgroundColor);

            canvas.SetMatrix(matrix);
            DrawGrid(canvas, e.Info.Width, e.Info.Height);
            foreach (var shape in shapes)
            {
                shape.Draw(canvas);
            }
        }
        private void DrawGrid(SKCanvas canvas, int width, int height)
        {
            using (var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Gray,
                StrokeWidth = 1
            })
            {
                int gridSize = 50;
                float scaleFactor = matrix.ScaleX;
                // Adjust grid size based on the zoom level
                int adjustedGridSize = (int)(gridSize / scaleFactor);
                if (adjustedGridSize < 10) adjustedGridSize = 10;

                //Calulate the visible bounds
                var leftTop = new SKPoint(0, 0);
                SKPoint[] corners = new SKPoint[]
                {
                    new SKPoint(0,0),
                    new SKPoint(width,0),
                    new SKPoint(0,height),
                    new SKPoint(width,height)
                };
                matrix.MapPoints(corners);
                float minX = Math.Min(Math.Min(corners[0].X, corners[1].X), Math.Min(corners[2].X, corners[3].X));
                float maxX = Math.Max(Math.Max(corners[0].X, corners[1].X), Math.Max(corners[2].X, corners[3].X));
                float minY = Math.Min(Math.Min(corners[0].Y, corners[1].Y), Math.Min(corners[2].Y, corners[3].Y));
                float maxY = Math.Max(Math.Max(corners[0].Y, corners[1].Y), Math.Max(corners[2].Y, corners[3].Y));

                // Calculate the origin to ensure (0,0) is at the center
                var origin = new SKPoint(width / 2, height / 2);
                matrix.MapPoints(new[] { origin });

                float startX = minX - (minX % adjustedGridSize);
                float startY = minY - (minY % adjustedGridSize);

                for (float x = startX; x < maxX; x += adjustedGridSize)
                {
                    canvas.DrawLine(x, minY, x, maxY, paint);
                }

                for (float y = startY; y < maxY; y += adjustedGridSize)
                {
                    canvas.DrawLine(minX, y, maxX, y, paint);
                }
            }

        }

        private void skControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentPolylinePoints.Add(new SKPoint(e.X, e.Y));
                skControl1.Invalidate();
            }
        }

        private void skControl1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void skControl1_MouseMove(object sender, MouseEventArgs e)
        {

        }

    }
}
