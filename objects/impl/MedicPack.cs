using System.Drawing;

namespace AsteroidGamePrototypeApp
{
    class MedicPack : DestructableObject
    {
        private const int DefaultCureValue = -10;

        public MedicPack(Point pos, Point dir, IGameContext gameContext,
            GameEvents.DestructEvent destructEvent)
            : base(pos, dir, new Size(16, 16), gameContext, destructEvent)
        {
        }

        public override void Draw()
        {
            Graphics.FillEllipse(new SolidBrush(Color.White), Pos.X, Pos.Y, Size.Width, Size.Height);
            Graphics.DrawLine(new Pen(Color.Green, 3), Pos.X + Size.Width / 2, Pos.Y + 2, Pos.X + Size.Width / 2,
                Pos.Y + Size.Height - 2);
            Graphics.DrawLine(new Pen(Color.Green, 3), Pos.X + 2, Pos.Y + Size.Height / 2, Pos.X + Size.Width - 2,
                Pos.Y + Size.Height / 2);
        }

        public override void Update()
        {
             Pos.X -= Dir.X;
            base.Update();
        }

        protected override bool IsOutOfSurface()
        {
            return Pos.X - Size.Width <= 0;
        }

        public override void Clash(IInteracting obj)
        {
           
        }

        public override int GetDamage()
        {
            return DefaultCureValue;
        }
    }
}