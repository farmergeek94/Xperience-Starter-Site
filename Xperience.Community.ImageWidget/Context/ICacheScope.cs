namespace Xperience.Community.ImageWidget.Context
{
    public interface ICacheScope
    {
        void Add(IEnumerable<string> dependancies);
        void Add(string dependancy);
        void Add(string[] dependancies);
        void Begin();
        void BeginWidget();
        string[] End();
    }
}