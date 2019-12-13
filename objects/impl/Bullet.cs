using System.Drawing;

namespace AsteroidGamePrototypeApp
{
    public class Bullet : DestructableObject
    {
        private IInteracting _clashedObject;
        private readonly int _damage;
        

        public Bullet(Point pos, Point dir, IGameContext gameContext, GameEvents.DestructEvent destructEvent, int damage) :
            base(pos, dir, new Size(10, 2), gameContext,destructEvent)
        {
            _damage = damage;
        }

        public override void Update()
        {
            Pos.X += Dir.X;
            base.Update();
        }

        public override void Draw()
        {
            Graphics.DrawRectangle(new Pen(Color.Aqua), Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Clash(IInteracting obj)
        {
            if (_clashedObject == obj) return;
            _clashedObject = obj;
            obj.Clash(this);
            _clashedObject = null;
            Destruct();
        }

        public override int GetDamage()
        {
            return _damage;
        }

        protected override bool IsOutOfSurface()
        {
            return Pos.X + Size.Width >= SurfaceBounds.Width;
        }
    }
}