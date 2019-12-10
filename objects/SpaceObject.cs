using System;
using System.Drawing;
using System.Linq;

namespace AsteroidGamePrototypeApp
{
    public abstract class SpaceObject : GameObject
    {
        private static long _idInc;
        // Just for debug
        protected long Id;
        protected Point Pos;
        protected Point Dir;
        protected Size Size;
        protected readonly Func<Graphics> GraphicsSupplier;
        protected readonly Func<Rectangle> SurfaceBoundsSupplier;

        protected Graphics Graphics => GraphicsSupplier.Invoke();
        protected Rectangle SurfaceBounds => SurfaceBoundsSupplier.Invoke();

        protected SpaceObject(Point pos, Point dir, Size size, Func<Graphics> graphicsSupplier, Func<Rectangle> surfaceBoundsSupplier)
        {
            Id = _idInc++;
            Pos = pos;
            Dir = dir;
            Size = size;
            GraphicsSupplier = graphicsSupplier;
            SurfaceBoundsSupplier = surfaceBoundsSupplier;
        }

        public override void Draw()
        {
            Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override Rectangle GetBounds()
        {
            return new Rectangle(Pos, Size);
        }
    }
}