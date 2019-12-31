namespace WindowsInput.Events {

    public class InvokeOptions {
        public InvokeCancellationOptions Cancellation { get; private set; } = new InvokeCancellationOptions();
        public InvokeFailureOptions Failure { get; private set; } = new InvokeFailureOptions();
        public SendInputOptions SendInput { get; private set; } = new SendInputOptions();
    }

    public class SendInputOptions {
        public bool Aggregate { get; set; } = true;
    }

}
