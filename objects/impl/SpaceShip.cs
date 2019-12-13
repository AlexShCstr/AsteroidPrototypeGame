using System.Drawing;
using System.Linq;
using AsteroidGamePrototypeApp.helper;

namespace AsteroidGamePrototypeApp
{
    public class SpaceShip : DestructableObject
    {
        private const int MaxEnergy = 100;
        private const int IndicatorMargin = 10;
        private const int IndicatorHeight = 5;
        private const int DefaultDamage = 10;

        private int _energy = MaxEnergy;

        public int Energy => _energy;

        private int EnergyIndicatorSize => (int) (Size.Width * ((double) Energy / MaxEnergy));
        public int IndicatorHeightSize => IndicatorMargin + IndicatorHeight;

        public SpaceShip(Point pos, IGameContext gameContext,
            GameEvents.DestructEvent destructEvent)
            : base(pos, new Point(0,10), new Size(36,24), gameContext, destructEvent)
        {
        }

        public override void Draw()
        {
            _gameContext.GetGraphics().FillEllipse(new SolidBrush(Color.Brown), Pos.X, Pos.Y, Size.Width, Size.Height);
            DrawUtils.DrawEnergyIndicator(_gameContext.GetGraphics(),Pos,Size.Width,EnergyIndicatorSize);
        }

        public override void Update()
        {
            foreach (var gameObject in _gameContext.GetAllObjects().Where(CollisionsWith))
            {
                switch (gameObject)
                {
                    case IInteracting interacting:
                        Clash(interacting);
                        break;
                }
            }
        }

        protected override bool IsOutOfSurface()
        {
            return false;
        }

        public void Up()
        {
            if (Pos.Y + IndicatorHeightSize > 0) Pos.Y -= CalcCurrentMovementSpeed();
        }

        private int CalcCurrentMovementSpeed()
        {
            return 3;
        }

        public void Down()
        {
            if (Pos.Y + Size.Height < Game.Height) Pos.Y += CalcCurrentMovementSpeed();
        }

        public Bullet Shoot(GameEvents.DestructEvent destructEvent)
        {
            return new Bullet(GetBulletPosition(), CalcCurrentBulletSpeed(), _gameContext,
                destructEvent, GetBulletDamage());
        }

        private int GetBulletDamage()
        {
            return DefaultDamage;
        }

        private Point GetBulletPosition()
        {
            return new Point(Pos.X + Size.Width + 1, Pos.Y + Size.Height / 2);
        }

        private Point CalcCurrentBulletSpeed()
        {
            return new Point(20, 0);
        }

        public override void Clash(IInteracting obj)
        {
            LowEnergyValue(obj.GetDamage());
            obj.Clash(this);
            if (obj is DestructableObject destructableObject)
            {
                destructableObject.Destruct();
            }
        }

        public override int GetDamage()
        {
            return 100;
        }

        private void LowEnergyValue(in int value)
        {
            _energy -= value;
            if (_energy > MaxEnergy)
            {
                _energy = MaxEnergy;
            }

            if (_energy <= 0)
            {
                Destruct();
            }
        }
    }
}