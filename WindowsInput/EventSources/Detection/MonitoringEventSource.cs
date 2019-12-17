namespace WindowsInput.EventSources {
    public abstract class MonitoringEventSource<TMonitor> : EventSourceBase where TMonitor : IEventSource {
        protected TMonitor Monitor { get; private set; }

        public bool Reset_On_EnabledChanged { get; set; } = true;
        public bool Reset_On_Parent_EnabledChanged { get; set; } = true;

        public MonitoringEventSource(TMonitor Monitor) {
            this.Monitor = Monitor;
        }

        protected override void Enable() {
            if (Reset_On_EnabledChanged) {
                Reset();
            }
            Monitor.EnabledChanged += Monitor_EnabledChanged;
            
        }
        protected override void Disable() {
            if (Reset_On_EnabledChanged) {
                Reset();
            }
            Monitor.EnabledChanged -= Monitor_EnabledChanged;
        }

        private void Monitor_EnabledChanged(object sender, EnabledChangedEventArgs e) {
            if (Reset_On_Parent_EnabledChanged) {
                Reset();
            }
        }

        protected virtual void Reset() {

        }


    }

}
