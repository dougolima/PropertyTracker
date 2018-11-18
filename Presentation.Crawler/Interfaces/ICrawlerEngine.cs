using System.Threading.Tasks;
using Unity;

namespace PropertyCrawler.Interfaces
{
    public interface ICrawlerEngine
    {
        Task Process(UnityContainer container);
    }
}