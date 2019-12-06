using System;
using System.Drawing;
using System.Windows.Forms;

namespace AsteroidGamePrototypeApp
{
    static class Game
    {
        private const int SpaceObjectsCount = 50;
        private const int StarsCount = 200;

        private static SpaceObject[] _spaceObjects, _backGroundStars;
        private static BufferedGraphics _buffer;

        public static int Width { get; set; }
        public static int Height { get; set; }

        static Game()
        {
        }

        private static void Load()
        {
            _spaceObjects = new SpaceObject[SpaceObjectsCount];
            _backGroundStars = new SpaceObject[StarsCount];
            var random = new Random();

            FillWithBackGroundStars(StarsCount);
            for (var i = 0; i < _spaceObjects.Length; i++)
            {
                _spaceObjects[i] = SpaceObjectsFactory.Create(new Point(random.Next(2, Width), i * random.Next(0, 15)),
                    _buffer.Graphics);
            }
        }

        private static void FillWithBackGroundStars(in int starsCount)
        {
            var random = new Random();
            for (var i = 0; i < starsCount; i++)
            {
                _backGroundStars[i] = new BackGroundStar(
                    new Point(random.Next(2, Width), random.Next(2, Height)),
                    _buffer.Graphics);
            }
        }

        public static void ExecuteAndShow(Form form)
        {
            var graphics = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            _buffer = BufferedGraphicsManager
                .Current
                .Allocate(graphics, form.DisplayRectangle);

            InitDrawTimer();
            Load();
            form.Show();
            Draw();
        }

        private static void InitDrawTimer()
        {
            var timer = new Timer {Interval = 100};
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private static void Draw()
        {
            _buffer.Graphics.Clear(Color.Black);
            foreach (var obj in _backGroundStars)
                obj.Draw();
            foreach (var obj in _spaceObjects)
                obj.Draw();
            _buffer.Render();
        }

        private static void Update()
        {
            foreach (var obj in _backGroundStars)
                obj.Update();
            foreach (var obj in _spaceObjects)
                obj.Update();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
    }
}