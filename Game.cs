using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Windows.Forms;
using AsteroidGamePrototypeApp.objects;

namespace AsteroidGamePrototypeApp
{
    static class Game
    {
        private const int SpaceObjectsCount = 50;
        private const int StarsCount = 200;
        private const int NearestObjectsBufferSize = 4;

        private static GameObject[] _backGroundStars;
        private static BufferedGraphics _buffer;
        private static Rectangle bounds;
        private static Graphics _graphics;
        private static Timer _timer, _burstTimer;
        private static DefaultGameContext GameContext;
        private static SpaceShip _ship;
        private static int _points = 0;

        public static int Width { get; set; }
        public static int Height { get; set; }

        static Game()
        {
            GameContext = new DefaultGameContext();
        }

        private static void Load()
        {
            _backGroundStars = new GameObject[StarsCount];
            var random = new Random();

            FillWithBackGroundStars(StarsCount);
            for (var i = 0; i < SpaceObjectsCount; i++)
            {
                var gameObject = GameContext.AddGameObject(SpaceObjectsFactory.Create(
                    new Point(random.Next(2, Width), i * random.Next(0, 15)),
                    GameContext, GameObjectDestroyed));
                gameObject.log = Console.WriteLine;
            }

            _ship = (SpaceShip) GameContext.AddGameObject(new SpaceShip(new Point(10, 200), GameContext,
                ShipDestroyedEvent));
        }

        private static void GameObjectDestroyed(GameObject gameobject)
        {
            _points += gameobject.GetPoints();
        }

        private static void ShipDestroyedEvent(GameObject gameobject)
        {
            _timer.Stop();
            _burstTimer.Stop();
            throw new GameOverException();
        }

        private static void FillWithBackGroundStars(in int starsCount)
        {
            var random = new Random();
            for (var i = 0; i < starsCount; i++)
            {
                _backGroundStars[i] = new BackGroundStar(
                    new Point(random.Next(2, Width), random.Next(2, Height)),
                    GameContext);
            }
        }

        public static void ExecuteAndShow(Form form)
        {
            _graphics = form.CreateGraphics();
            form.ClientSizeChanged += FormOnClientSizeChanged;
            form.KeyDown += Form_KeyDown;
            Init(form, _graphics);
            InitDrawTimer();
            InitBurstTimer();
            Load();
            form.Show();
            Draw();
        }

        private static void InitBurstTimer()
        {
            _burstTimer = new Timer {Interval = 3000};
            _burstTimer.Start();
            _burstTimer.Tick += BurstTimer_Tick;
        }

        private static void BurstTimer_Tick(object sender, EventArgs e)
        {
            GameContext.AddGameObject(SpaceObjectsFactory.CreateBurst(GameContext, GameContext.RemoveGameObject));
        }

        private static void Init(Form form, Graphics graphics)
        {
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            bounds = new Rectangle(new Point(0, 0), new Size(Width, Height));
            GameContext.Bounds = bounds;
            _buffer = BufferedGraphicsManager
                .Current
                .Allocate(graphics, form.DisplayRectangle);
            GameContext.Graphics = _buffer.Graphics;
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
            foreach (var obj in GameContext.GetAllObjects())
                obj.Draw();
            _buffer.Graphics.DrawString("Points: " + _points, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            _buffer.Render();
        }

        private static void Update()
        {
            foreach (var obj in _backGroundStars)
                obj.Update();
            foreach (var obj in GameContext.GetAllObjects())
                obj.Update();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    GameContext.AddGameObject(_ship.Shoot(GameContext.RemoveGameObject));
                    break;
                case Keys.Up:
                    _ship.Up();
                    break;
                case Keys.Down:
                    _ship.Down();
                    break;
            }
        }
    }

    internal class DefaultGameContext : IGameContext
    {
        private List<GameObject> _gameObjects;
        internal Graphics Graphics { set; get; }
        internal Rectangle Bounds { set; get; }

        public DefaultGameContext()
        {
            _gameObjects = new List<GameObject>();
        }

        public Graphics GetGraphics()
        {
            return Graphics;
        }

        public Rectangle GetBounds()
        {
            return Bounds;
        }

        public IEnumerable<GameObject> GetAllObjects()
        {
            return _gameObjects.ToImmutableList();
        }

        public GameObject AddGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
            return gameObject;
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
        }
    }
}