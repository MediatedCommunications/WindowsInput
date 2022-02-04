using System;

namespace WindowsInput.Events {

    public class InvokeOptions {
        public InvokeCancellationOptions Cancellation { get; } = new InvokeCancellationOptions();
        public InvokeFailureOptions Failure { get; } = new InvokeFailureOptions();
        public SendInputOptions SendInput { get; } = new SendInputOptions();
    }

    public class SendInputOptions {
        public int BatchSize { get; set; } = 5000;
        public TimeSpan BatchDelay { get; set; } = TimeSpan.FromSeconds(1);

    }

}
