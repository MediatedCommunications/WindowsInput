// This code is distributed under MIT license.
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System.Collections.Generic;

namespace System.Collections.Generic {
    internal static class CollectionExtensions {
        public static void Add<T>(this ICollection<T> This, IEnumerable<T>? Items) {
            if (Items is { }) {
                foreach (var item in Items) {
                    This.Add(item);
                }
            }
        }
    }
}