using System.Drawing;
using System.Linq;

namespace AsteroidGamePrototypeApp
{
    public abstract class InteractingObject : SpaceObject,IInteracting
    {
        protected InteractingObject(Point pos, Point dir, Size size, IGameContext gameContext) : base(pos, dir, size, gameContext)
        {
        }

        public override void Update()
        {
            foreach (var obj in _gameContext.GetAllObjects().Where(CollisionsWith))
            {
                switch (obj)
                {
                    case IInteracting interacting:
                        Clash(interacting);
                        break;
                }
            }
        }

        public abstract void Clash(IInteracting obj);

        public abstract int GetDamage();
    }
}