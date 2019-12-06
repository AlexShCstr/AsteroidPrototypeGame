using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security;

namespace AsteroidGamePrototypeApp
{
    internal class SpaceObject
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;
        protected readonly Graphics Graphics;

        public SpaceObject(Point pos, Point dir, Size size, Graphics graphics)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
            Graphics = graphics;
        }

        public virtual void Draw()
        {
            Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public virtual void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
    }

    internal class BackGroundStar : SpaceObject
    {
        private static readonly Random Random=new Random();
        public BackGroundStar(Point pos, Graphics graphics) : base(pos, new Point(0,0), new Size(1,1), graphics)
        {
        }

        public override void Draw()
        {
            var color = Color.FromArgb(Random.Next(0, 255), Random.Next(0, 255), Random.Next(0, 255));
            Graphics.DrawEllipse(new Pen(color), Pos.X, Pos.Y, Size.Width, Size.Height);
        }
    }

    internal class Nebula : SpaceObject
    {
        private static readonly Image ImageFile = Image.FromFile("nebula.png");

        public Nebula(Point pos, Point dir, Graphics graphics) : base(pos, dir, Size.Empty, graphics)
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

    internal class Star : SpaceObject
    {
        public Star(Point pos, Point dir, Size size, Graphics graphics) : base(pos, dir, size, graphics)
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