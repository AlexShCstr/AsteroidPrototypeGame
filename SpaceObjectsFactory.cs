using System;
using System.Drawing;

namespace AsteroidGamePrototypeApp
{
    internal static class SpaceObjectsFactory
    {
        private static readonly Random Random = new Random();

        public static SpaceObject Create(Point position, Graphics graphics)
        {
            var size = next();
            // Туманности должны быть довольно редкими
            if (Random.Next(0, 10) == 3)
                return new Nebula(
                    new Point(RandomXPos(), RandomYPos()),
                    new Point(Random.Next(1, 3), 0),
                    graphics);
            var objectSize = new Size(size, size);
            return Random.Next(1, 3) switch
            {
                1 => new SpaceObject(position, new Point(-next(), -next()), objectSize, graphics),
                2 => new Star(position, new Point(next(), 0), objectSize, graphics),
                _ => new SpaceObject(position, new Point(-next(), -next()), objectSize, graphics)
            };
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