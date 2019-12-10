using System;

namespace AsteroidGamePrototypeApp.exceptions
{
    public class StarIsKilledException : Exception
    {
        public StarIsKilledException(string message) : base(message)
        {
        }
    }
}