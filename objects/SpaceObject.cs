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
        protected readonly IGameContext _gameContext;

        protected Graphics Graphics => _gameContext.GetGraphics();
        protected Rectangle SurfaceBounds => _gameContext.GetBounds();

        protected SpaceObject(Point pos, Point dir, Size size, IGameContext gameContext)
        {
            Id = _idInc++;
            Pos = pos;
            Dir = dir;
            Size = size;
            _gameContext = gameContext;
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