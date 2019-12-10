using System;
using System.Drawing;

namespace AsteroidGamePrototypeApp.helper
{
    internal static class SpaceUtils
    {
        public static long CalcDistance(Rectangle bounds1, Rectangle bounds2)
        {
            int x1 = bounds1.Left;
            int y1 = bounds1.Top;
            int x2 = bounds2.Right;
            int y2 = bounds2.Bottom;
            if (bounds1.Right < bounds2.Left)
            {
                x1 = bounds1.Right;
                x2 = bounds2.Left;
            }

            if (bounds1.Bottom < bounds2.Top)
            {
                y1 = bounds1.Bottom;
                y2 = bounds2.Top;
            }

            var distance = (x1 - x2) ^ 2 + (y1 - y2) ^ 2;
            return Math.Abs(distance);
        }
    }
}