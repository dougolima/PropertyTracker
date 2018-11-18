using Topshelf;

namespace PropertyTracker.Presentation.Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<ServiceManagement>();
            });
        }
    }
}
