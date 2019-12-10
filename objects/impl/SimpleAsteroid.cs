using System;
using System.Drawing;

namespace AsteroidGamePrototypeApp
{
    public class SimpleAsteroid : AbstractAsteroidObject
    {
        public SimpleAsteroid(Point pos, Point dir, Size size, Func<Graphics> graphicsSupplier,
            Func<Rectangle> surfaceBoundsSupplier) : base(pos, dir, size, graphicsSupplier, surfaceBoundsSupplier)
        {
        }
       
    }
}