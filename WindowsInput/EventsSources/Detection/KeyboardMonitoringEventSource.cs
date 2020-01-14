namespace WindowsInput.Events.Sources {
    public class KeyboardMonitoringEventSource : MonitoringEventSource<IKeyboardEventSource> {
        public KeyboardMonitoringEventSource(IKeyboardEventSource Monitor) : base(Monitor) { 
        
        }
    }

}
