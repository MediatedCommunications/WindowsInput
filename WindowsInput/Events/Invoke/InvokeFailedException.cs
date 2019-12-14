namespace WindowsInput.Events {
    public class InvokeFailedException : System.Exception {

        private const string MESSAGE = "Invoking the event failed for unknown reasons.";

        public InvokeFailedException() : this(MESSAGE) {

        }

        public InvokeFailedException(string Message) : base(Message) {

        }

    }

}
