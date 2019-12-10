using System;
using System.Drawing;
using System.Linq;
using AsteroidGamePrototypeApp.exceptions;
using AsteroidGamePrototypeApp.objects;

namespace AsteroidGamePrototypeApp
{
    public class Bullet : SpaceObject, IInteracting
    {
        private IInteracting _clashedObject;
        private static Random _random = new Random();

        public Bullet(Point pos, Point dir, Func<Graphics> graphicsSupplier,
            Func<Rectangle> surfaceBoundsSupplier) :
            base(pos, dir, new Size(10, 2), graphicsSupplier, surfaceBoundsSupplier)
        {
        }

        public override void Update()
        {
            if (IsOutOfSurface())
            {
                Rebirth();
            }

            Pos.X += Dir.X;
            foreach (var obj in NearestObjects.Where(IntersectsWith))
            {
                switch (obj)
                {
                    case IInteracting interacting:
                        Clash(interacting);
                        break;
                    case Star _:
                        throw new StarIsKilledException("OHH NO!!! Star is killed!!!!");
                }
            }
        }

        private bool IsOutOfSurface()
        {
            return Pos.X + Size.Width >= SurfaceBounds.Width;
        }

        private void Rebirth()
        {
            Pos.Y = _random.Next(10, SurfaceBounds.Height - 10);
            Pos.X = 0;
            Dir.X = _random.Next(10, 30);
        }

        public override void Draw()
        {
            Graphics.DrawRectangle(new Pen(Color.Aqua), Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public void Clash(IInteracting obj)
        {
            if (_clashedObject == obj) return;
            _clashedObject = obj;
            obj.Clash(this);
            _clashedObject = null;
            Rebirth();
        }
    }
}