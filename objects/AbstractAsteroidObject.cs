using System;
using System.Drawing;

namespace AsteroidGamePrototypeApp
{
    public abstract class AbstractAsteroidObject : SpaceObject, IInteracting
    {
        protected AbstractAsteroidObject(Point pos, Point dir, Size size, Func<Graphics> graphicsSupplier,
            Func<Rectangle> surfaceBoundsSupplier) :
            base(pos, dir, size, graphicsSupplier, surfaceBoundsSupplier)
        {
        }

        public override void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
            if (Pos.X < 0 && Dir.X < 0)
                Dir.X = -Dir.X;
            if (Pos.X > SurfaceBounds.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0 && Dir.Y < 0)
                Dir.Y = -Dir.Y;
            if (Pos.Y > SurfaceBounds.Height) Dir.Y = -Dir.Y;
        }

        public void Clash(IInteracting obj)
        {
            RebirthObject();
        }

        private void RebirthObject()
        {
            Pos.X = SurfaceBounds.Width;
            Pos.Y = SurfaceBounds.Height % 2;
        }
    }
}