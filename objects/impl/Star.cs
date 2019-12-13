using System;
using System.Drawing;

namespace AsteroidGamePrototypeApp.objects
{
    public class Star : SpaceObject
    {
        public Star(Point pos, Point dir, Size size, IGameContext gameContext) : base(pos, dir, size, gameContext)
        {
        }

        public override void Draw()
        {
            Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }

        public override void Update()
        {
            Pos.X -= Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }

      
    }
}