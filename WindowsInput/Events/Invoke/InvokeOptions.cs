namespace WindowsInput.Events {
    public class InvokeOptions {
        public InvokeCancellationOptions Cancellation { get; private set; } = new InvokeCancellationOptions();
        public InvokeFailureOptions Failure { get; private set; } = new InvokeFailureOptions();

    }

}
