using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Threading.Tasks {
    internal static class TaskExtensions {
        private const bool __DefaultAwait = false;

        public static ConfiguredTaskAwaitable<T> DefaultAwait<T>(this Task<T> This) {
            return This.ConfigureAwait(__DefaultAwait);
        }

        public static ConfiguredTaskAwaitable DefaultAwait(this Task This) {
            return This.ConfigureAwait(__DefaultAwait);
        }

    }
}