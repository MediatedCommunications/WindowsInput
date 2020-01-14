using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Events.Sources;

namespace WindowsInput {

    public static class Capture {

        /// <summary>
        /// Listen to global mouse and keyboard events
        /// </summary>
        public static GlobalEventSourceFactory Global { get; private set; } = new GlobalEventSourceFactory();

        /// <summary>
        /// Listen to mouse and keyboard events for the current application
        /// </summary>
        public static CurrentThreadHookSource CurrentThread { get; private set; } = new CurrentThreadHookSource();

    }



}
