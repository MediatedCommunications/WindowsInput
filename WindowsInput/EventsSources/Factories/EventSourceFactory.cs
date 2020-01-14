using WindowsInput.Events.Sources;

namespace WindowsInput.Events.Sources {
    public abstract class EventSourceFactory {
        protected abstract IKeyboardEventSource KeyboardInternal();
        
        /// <summary>
        /// Retrieve an <see cref="IKeyboardEventSource"/> that processes messages on the calling thread's message loop.
        /// </summary>
        /// <param name="Enable">Whether it should start listening for events by default</param>
        /// <returns></returns>
        public IKeyboardEventSource Keyboard(bool Enable = true) {
            var ret = KeyboardInternal();
            ret.Enabled = Enable;

            return ret;
        }

        protected abstract IMouseEventSource MouseInternal();

        /// <summary>
        /// Retrieve an <see cref="IMouseEventSource"/> that processes messages on the calling thread's message loop.
        /// </summary>
        /// <param name="Enable">Whether it should start listening for events by default</param>
        /// <returns></returns>
        public IMouseEventSource Mouse(bool Enable = true) {
            var ret = MouseInternal();
            ret.Enabled = Enable;

            return ret;
        }

    }

}
