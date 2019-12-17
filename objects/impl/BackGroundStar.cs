using System;
using System.Drawing;

namespace AsteroidGamePrototypeApp.objects
{
    public class BackGroundStar : AbstractAsteroidObject
    {
        private static readonly Random Random=new Random();


        public BackGroundStar(Point pos, IGameContext gameContext) 
            : base(pos, new Point(0,0), new Size(1,1), gameContext,gameObject => { })
        {
        }

        public override void Draw()
        {
            var color = Color.FromArgb(Random.Next(0, 255), Random.Next(0, 255), Random.Next(0, 255));
            Graphics.DrawEllipse(new Pen(color), Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        protected override bool IsOutOfSurface()
        {
            return false;
        }
    }
}
