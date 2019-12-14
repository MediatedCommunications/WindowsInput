using System.Threading.Tasks;

namespace WindowsInput.Events {
    public interface IEvent {
        Task<bool> Invoke(InvokeOptions Options);
    }

}
