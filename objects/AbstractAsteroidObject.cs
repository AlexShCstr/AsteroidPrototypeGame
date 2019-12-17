using System.Drawing;
using AsteroidGamePrototypeApp.helper;

namespace AsteroidGamePrototypeApp
{
    public abstract class AbstractAsteroidObject : DestructableObject
    {
        private int _damage = 1;
        private int _energy;
        public int Energy => _energy;
        public int EnergyIndicatorSize => (int) (Size.Width * ((double) Energy / Size.Width));

        protected AbstractAsteroidObject(Point pos, Point dir, Size size, IGameContext gameContext,
            GameEvents.DestructEvent destructEvent)
            : base(pos, dir, size, gameContext, destructEvent)
        {
            _energy = GetDefaultEnergyValue();
        }

        public override void Draw()
        {
            base.Draw();
            if (IsNoFullEnergy())
            {
                DrawUtils.DrawEnergyIndicator(_gameContext.GetGraphics(), Pos, Size.Width, EnergyIndicatorSize);
            }
        }

        public override void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
            CheckBounds();
            log($"Object {Id} changed position to {Pos.X}:{Pos.Y}");
        }

        private void CheckBounds()
        {
            if (Pos.X < 0 && Dir.X < 0) Dir.X = -Dir.X;
            if (Pos.X > SurfaceBounds.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0 && Dir.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > SurfaceBounds.Height) Dir.Y = -Dir.Y;
        }

        public override int GetPoints()
        {
            return Size.Width;
        }

        public override void Clash(IInteracting obj)
        {
            LowEnergy(obj.GetDamage());
        }

        private void LowEnergy(int damage)
        {
            _energy -= damage;
            if (Energy <= 0)
            {
                RebirthObject();
            }
        }

        private void RebirthObject()
        {
            _energy = Size.Height;
            Pos.X = SurfaceBounds.Width;
            Pos.Y = SurfaceBounds.Height % 2;
            Destruct();
        }

        public override int GetDamage()
        {
            return CalcDamage();
        }

        protected virtual int CalcDamage()
        {
            return _damage;
        }

        private bool IsNoFullEnergy()
        {
            return _energy < GetDefaultEnergyValue();
        }

        private int GetDefaultEnergyValue()
        {
            return Size.Height;
        }
    }
}