using System;
using System.Drawing;

namespace AsteroidGamePrototypeApp
{
    public class SimpleAsteroid : AbstractAsteroidObject
    {
        public SimpleAsteroid(Point pos, Point dir, Size size, IGameContext gameContext,
            GameEvents.DestructEvent destructEvent)
            : base(pos, dir, size, gameContext, destructEvent)
        {
        }

        protected override int CalcDamage()
        {
            return Size.Height;
        }

        protected override bool IsOutOfSurface()
        {
            return false;
        }
    }
}