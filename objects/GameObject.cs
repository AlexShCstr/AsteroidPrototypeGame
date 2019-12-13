using System;
using System.Drawing;

namespace AsteroidGamePrototypeApp
{
    public abstract class GameObject : IDrawable, IBoundObject
    {
        public delegate void Log(String message);

        private Log _logger;

        public abstract void Draw();

        public abstract void Update();

        public abstract Rectangle GetBounds();

        public virtual int GetPoints()
        {
            return 0;
        }

        public Log log
        {
            get
            {
                return _logger!=null?_logger:message => { };
            }
            set { _logger = value; }
        }

        protected bool CollisionsWith(GameObject gameObject)
        {
            if (gameObject == this)
            {
                return false;
            }

            var bounds = gameObject.GetBounds();
            var rect = GetBounds();
            if (bounds.X <= rect.X + rect.Width && rect.X <= bounds.X + bounds.Width &&
                bounds.Y <= rect.Y + rect.Height)
                return rect.Y <= bounds.Y + bounds.Height;
            return false;
        }
    }
}