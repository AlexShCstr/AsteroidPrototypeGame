using System;
using System.Drawing;
using AsteroidGamePrototypeApp.objects;

namespace AsteroidGamePrototypeApp
{
    internal static class SpaceObjectsFactory
    {
        private static readonly Random Random = new Random();

        public static GameObject CreateBurst(IGameContext gameContext, GameEvents.DestructEvent destructEvent)
        {
            return new MedicPack(new Point(gameContext.GetBounds().Width, RandomYPos()), new Point(10, 0), gameContext,
                destructEvent);
        }

        public static GameObject Create(Point position, IGameContext gameContext, GameEvents.DestructEvent destructEvent)
        {
            var size = next();
            // Туманности должны быть довольно редкими
            if (Random.Next(0, 10) == 4)
                return new Nebula(
                    RandomPoint(),
                    new Point(Random.Next(1, 3), 0),
                    gameContext);
            var objectSize = new Size(size, size);
            return Random.Next(1, 3) switch
            {
                1 => (SpaceObject) new SimpleAsteroid(position, new Point(-next(), -next()), objectSize, gameContext,destructEvent),
                2 => new Star(position, new Point(next(), 0), new Size(4,4), gameContext),
                _ => new SimpleAsteroid(position, new Point(-next(), -next()), objectSize, gameContext,destructEvent)
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
            return next(10, 20);
        }

        private static int next(int start, int end)
        {
            return Random.Next(start, end);
        }
    }
}