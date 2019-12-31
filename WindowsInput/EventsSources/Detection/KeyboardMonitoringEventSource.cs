namespace WindowsInput.EventSources {
    public class KeyboardMonitoringEventSource : MonitoringEventSource<IKeyboardEventSource> {
        public KeyboardMonitoringEventSource(IKeyboardEventSource Monitor) : base(Monitor) { 
        
        }
    }

}
