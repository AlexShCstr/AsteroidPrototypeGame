using System.Drawing;

namespace AsteroidGamePrototypeApp
{
    public abstract class DestructableObject : InteractingObject
    {
        protected readonly GameEvents.DestructEvent DestructEvent;
        
        protected DestructableObject(Point pos, Point dir, Size size, IGameContext gameContext, GameEvents.DestructEvent destructEvent) : base(pos, dir, size, gameContext)
        {
            DestructEvent = destructEvent;
        }

        public virtual void Destruct()
        {
            DestructEvent?.Invoke(this);
        }

        public override void Update()
        {
            if (IsOutOfSurface())
            {
                Destruct();
            }
            base.Update();
        }

        protected abstract bool IsOutOfSurface();
    }

    
}