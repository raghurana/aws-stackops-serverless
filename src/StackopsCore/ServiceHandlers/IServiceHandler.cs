using System.Threading.Tasks;
using StackopsCore.Models;

namespace StackopsCore.ServiceHandlers
{
    public interface IServiceHandler
    {
        bool CanHandle(Stack stack);

        Task<int> StartStackService(Stack stack);

        Task<int> StopStackService(Stack stack);
    }
}