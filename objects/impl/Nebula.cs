using System;
using System.Drawing;

namespace AsteroidGamePrototypeApp.objects
{
    public class Nebula : SpaceObject
    {
        private static readonly Image ImageFile = Image.FromFile("nebula.png");

        public Nebula(Point pos, Point dir, IGameContext gameContext) : base(pos, dir, Size.Empty, gameContext)
        {
        }

        public override void Draw()
        {
            Graphics.DrawImage(ImageFile, base.Pos);
        }

        public override void Update()
        {
            Pos.X -= Dir.X;
            if (Pos.X + ImageFile.Width < 0) Pos.X = Game.Width + Size.Width;
        }
    }
}