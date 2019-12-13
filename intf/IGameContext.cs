using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace AsteroidGamePrototypeApp
{
    public interface IGameContext
    {
        Graphics GetGraphics();
        Rectangle GetBounds();
        IEnumerable<GameObject> GetAllObjects();
    }
}