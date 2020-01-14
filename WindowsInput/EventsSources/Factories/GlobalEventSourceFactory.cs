using WindowsInput.Events.Sources;

namespace WindowsInput.Events.Sources {
    public class GlobalEventSourceFactory : EventSourceFactory {
        protected override IKeyboardEventSource KeyboardInternal() {
            return new GlobalKeyboardEventSource();
        }

        protected override IMouseEventSource MouseInternal() {
            return new GlobalMouseEventSource();
        }

        /// <summary>
        /// Retrieve an <see cref="IKeyboardEventSource"/> that processes messages on a dedicated thread.
        /// </summary>
        /// <param name="Enable">Whether it should start listening for events by default</param>
        /// <returns></returns>
        public IKeyboardEventSource KeyboardAsync(bool Enable = true) {
            return new AsyncKeyboardEventSource(() => Keyboard(Enable));
        }

        /// <summary>
        /// Retrieve an <see cref="IKeyboardEventSource"/> that processes messages on a dedicated thread.
        /// </summary>
        /// <param name="Enable">Whether it should start listening for events by default</param>
        /// <returns></returns>
        public IMouseEventSource MouseAsync(bool Enable = true) {
            return new AsyncMouseEventSource(() => Mouse(Enable));
        }

    }

}
