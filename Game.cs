using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AsteroidGamePrototypeApp.helper;
using AsteroidGamePrototypeApp.objects;

namespace AsteroidGamePrototypeApp
{
    static class Game
    {
        private const int SpaceObjectsCount = 100;
        private const int StarsCount = 200;
        private const int NearestObjectsBufferSize = 4;

        private static GameObject[] _spaceObjects, _backGroundStars;
        private static BufferedGraphics _buffer;
        private static Rectangle bounds;
        private static Graphics _graphics;
        private static Timer _timer;

        public static int Width { get; set; }
        public static int Height { get; set; }


        static Game()
        {
        }

        private static void Load()
        {
            _spaceObjects = new GameObject[SpaceObjectsCount];
            _backGroundStars = new GameObject[StarsCount];
            var random = new Random();

            FillWithBackGroundStars(StarsCount);
            for (var i = 0; i < _spaceObjects.Length; i++)
            {
                _spaceObjects[i] = SpaceObjectsFactory.Create(new Point(random.Next(2, Width), i * random.Next(0, 15)),
                    () => _buffer.Graphics, () => bounds);
            }
        }

        private static void FillWithBackGroundStars(in int starsCount)
        {
            var random = new Random();
            for (var i = 0; i < starsCount; i++)
            {
                _backGroundStars[i] = new BackGroundStar(
                    new Point(random.Next(2, Width), random.Next(2, Height)),
                    () => _buffer.Graphics, () => bounds);
            }
        }

        public static void ExecuteAndShow(Form form)
        {
            _graphics = form.CreateGraphics();
            form.ClientSizeChanged += FormOnClientSizeChanged;
            Init(form, _graphics);

            InitDrawTimer();
            Load();
            form.Show();
            Draw();
        }

        private static void Init(Form form, Graphics graphics)
        {
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            bounds = new Rectangle(new Point(0, 0), new Size(Width, Height));
            _buffer = BufferedGraphicsManager
                .Current
                .Allocate(graphics, form.DisplayRectangle);
        }

        private static void FormOnClientSizeChanged(object sender, EventArgs e)
        {
            _timer.Stop();
            try
            {
                Init((Form) sender, _graphics);
                FillWithBackGroundStars(StarsCount);
            }
            finally
            {
                _timer.Start();
            }
        }

        private static void InitDrawTimer()
        {
            _timer = new Timer {Interval = 100};
            _timer.Start();
            _timer.Tick += Timer_Tick;
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
            {
                obj.SetNearestObjects(GetNearestObjects(_spaceObjects, obj));
                obj.Update();
            }
        }

        private static List<GameObject> GetNearestObjects(IEnumerable<GameObject> spaceObjects,
            IBoundObject drawableObject)
        {
            var distanceList = spaceObjects
                .Where(obj => obj != drawableObject)
                .ToDictionary(obj => obj, obj => SpaceUtils.CalcDistance(obj.GetBounds(), drawableObject.GetBounds()))
                .ToList();
            distanceList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            return distanceList.GetRange(0, NearestObjectsBufferSize).Select(pair => pair.Key).ToList();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }
    }
}