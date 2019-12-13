using System.Drawing;

namespace AsteroidGamePrototypeApp.helper
{
    public static class DrawUtils
    {
        public static void DrawEnergyIndicator(Graphics graphics, Point position, int width, int value)
        {
            const int indicatorHeight = 5;
            const int indicatorMargin = 5;
            const int indicatorHeightSize = indicatorHeight+indicatorMargin;
            graphics.FillRectangle(Brushes.Chartreuse, position.X, position.Y - indicatorHeightSize,
                value, indicatorHeight);
            graphics.DrawRectangle(Pens.Chartreuse, position.X, position.Y - indicatorHeightSize, width, indicatorHeight);
        }
    }
}