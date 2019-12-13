using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidGamePrototypeApp
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var form = new Form {Width = 800, Height = 600};
            Game.ExecuteAndShow(form);
            Application.Run(form);
        }
    }
}

