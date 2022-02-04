using System;
using System.Runtime.InteropServices;

namespace WindowsInput.Native
{

    public static class SystemMetrics {

        public static class Screen {

            //This value is a constant.  I just like it here.
            public static CachedMetric<int> ScaleFactor { get; } = CachedMetric.Create(() => 65536);

            public static class Primary {
                public static CachedMetric<int> Width { get; } = CachedMetric.Create(SYSTEMMETRIC.PrimaryScreen_X);
                public static CachedMetric<int> Height { get; } = CachedMetric.Create(SYSTEMMETRIC.PrimaryScreen_Y);

            }

            public static class Virtual {
                public static CachedMetric<int> Width { get; } = CachedMetric.Create(SYSTEMMETRIC.VirtualScreen_X);
                public static CachedMetric<int> Height { get; } = CachedMetric.Create(SYSTEMMETRIC.VirtualScreen_Y);
            }

        }

        public static class Mouse {
            public static class Drag {
                public static CachedMetric<int> XThreshold { get; } = CachedMetric.Create(SYSTEMMETRIC.Drag_X);
                public static CachedMetric<int> YThreshold { get; } = CachedMetric.Create(SYSTEMMETRIC.Drag_Y);
            }


            public static class DoubleClick {

                [DllImport("user32")]
                private static extern int GetDoubleClickTime();

                private static TimeSpan GetDoubleClickDuration() {
                    return TimeSpan.FromMilliseconds(GetDoubleClickTime());
                }

                public static CachedMetric<TimeSpan> Duration { get; } = CachedMetric.Create(() => GetDoubleClickDuration());
            }
        }

        

    }

    public static class CachedMetric {

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(SYSTEMMETRIC index);

        public static CachedMetric<int> Create(SYSTEMMETRIC Metric){
            return new CachedMetric<int>(() => GetSystemMetrics(Metric));
        }

        public static CachedMetric<T> Create<T>(Func<T> Value) where T:struct {
            return new CachedMetric<T>(Value);
        }

    }

    public class CachedMetric<T> where T : struct {
        private T? __LastValue;
        public T LastValue {
            get {
                var ret = __LastValue ?? GetValue();
                __LastValue = ret;

                return ret;
            }
        }

        public T Value {
            get {
                var ret = GetValue();
                __LastValue = ret;
                return ret;
            }
        }

        public CachedMetric(Func<T> Value) {
            GetValue = Value;
        }


        protected Func<T> GetValue { get; }
    }

    public enum SYSTEMMETRIC : int {
        PrimaryScreen_X = 0,
        PrimaryScreen_Y = 1,

        DoubleClick_X = 36,
        DoubleClick_Y = 37,

        Drag_X = 68,
        Drag_Y = 69,

        VirtualScreen_X = 76,
        VirtualScreen_Y = 77,

    }

}