using System.Threading;

namespace WindowsInput.Events {
    public class InvokeCancellationOptions {
        public CancellationToken Token { get; set; } = CancellationToken.None;
        public bool ThowWhenCanceled { get; set; } = false;
    }

}
