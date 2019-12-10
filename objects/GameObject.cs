using System;
using System.Collections.Generic;
using System.Drawing;

namespace AsteroidGamePrototypeApp
{
    public abstract class GameObject : IDrawable,IBoundObject
    {
        protected List<GameObject> NearestObjects;

        protected GameObject()
        {
            NearestObjects=new List<GameObject>();
        }

        public abstract void Draw();

        public abstract void Update();

        public abstract Rectangle GetBounds();

        public bool IntersectsWith(GameObject gameObject)
        {
            var bounds = gameObject.GetBounds();
            var rect = GetBounds();
            if (bounds.X <= rect.X + rect.Width && rect.X <= bounds.X + bounds.Width && bounds.Y <= rect.Y + rect.Height)
                return rect.Y <= bounds.Y + bounds.Height;
            return false;
        }

        public void SetNearestObjects(List<GameObject> objects)
        {
            if (objects == null) throw new ArgumentNullException(nameof(objects));
            if (NearestObjects == null)
            {
                NearestObjects = new List<GameObject>();
            }

            NearestObjects.Clear();
            NearestObjects.AddRange(objects);
        }
    }
}