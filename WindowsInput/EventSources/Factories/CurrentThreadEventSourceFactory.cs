using System;

namespace WindowsInput.EventSources {
    public class CurrentThreadHookSource : EventSourceFactory {
        protected override IKeyboardEventSource KeyboardInternal() {
            return new CurrentThreadKeyboardEventSource();
        }

        protected override IMouseEventSource MouseInternal() {
            //return new CurrentThreadMouseEventSource();
            throw new NotImplementedException();
        }

    }

}
