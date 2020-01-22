using System;

namespace WindowsInput.Events {

    public class InvokeOptions {
        public InvokeCancellationOptions Cancellation { get; private set; } = new InvokeCancellationOptions();
        public InvokeFailureOptions Failure { get; private set; } = new InvokeFailureOptions();
        public SendInputOptions SendInput { get; private set; } = new SendInputOptions();
    }

    public class SendInputOptions {
        public int BatchSize { get; set; } = 5000;
        public TimeSpan BatchDelay { get; set; } = TimeSpan.FromSeconds(1);

    }

}
