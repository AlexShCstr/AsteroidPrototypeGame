namespace AsteroidGamePrototypeApp
{
    public interface IInteracting : IBoundObject
    {
        void Clash(IInteracting obj);
    }
}