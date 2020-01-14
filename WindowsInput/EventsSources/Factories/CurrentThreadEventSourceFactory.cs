using System;

namespace WindowsInput.Events.Sources {
    public class CurrentThreadHookSource : EventSourceFactory {
        protected override IKeyboardEventSource KeyboardInternal() {
            return new CurrentThreadKeyboardEventSource();
        }

        protected override IMouseEventSource MouseInternal() {
            return new CurrentThreadMouseEventSource();
        }

    }

}
