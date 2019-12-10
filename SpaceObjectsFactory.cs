using System;
using System.Drawing;
using AsteroidGamePrototypeApp.objects;

namespace AsteroidGamePrototypeApp
{
    internal static class SpaceObjectsFactory
    {
        private static readonly Random Random = new Random();
        private static Bullet _bullet;

        public static GameObject Create(Point position, Func<Graphics> graphicsSupplier,
            Func<Rectangle> surfaceBoundsSupplier)
        {
            var size = next();
            if (_bullet == null)
            {
                _bullet = new Bullet(new Point(0, RandomYPos()), new Point(Random.Next(5, 10), 0), 
                    graphicsSupplier,surfaceBoundsSupplier);
                return _bullet;
            }

            // Туманности должны быть довольно редкими
            if (Random.Next(0, 10) == 4)
                return new Nebula(
                    RandomPoint(),
                    new Point(Random.Next(1, 3), 0),
                    graphicsSupplier,surfaceBoundsSupplier);
            var objectSize = new Size(size, size);
            return Random.Next(1, 3) switch
            {
                1 => (SpaceObject) new SimpleAsteroid(position, new Point(-next(), -next()), objectSize, graphicsSupplier,surfaceBoundsSupplier),
                2 => new Star(position, new Point(next(), 0), objectSize, graphicsSupplier,surfaceBoundsSupplier),
                _ => new SimpleAsteroid(position, new Point(-next(), -next()), objectSize, graphicsSupplier,surfaceBoundsSupplier)
            };
        }

        private static Point RandomPoint()
        {
            return new Point(RandomXPos(), RandomYPos());
        }

        private static int RandomYPos()
        {
            return Game.Height * Random.Next(1, 100) / 100;
        }

        private static int RandomXPos()
        {
            return Game.Width * Random.Next(1, 100) / 100;
        }

        private static int next()
        {
            return next(2, 10);
        }

        private static int next(int start, int end)
        {
            return Random.Next(start, end);
        }
    }
}