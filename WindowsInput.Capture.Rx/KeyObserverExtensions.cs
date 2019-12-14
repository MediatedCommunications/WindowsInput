// This code is distributed under MIT license. 
// Copyright (c) 2010-2018 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using srx = System.Reactive.Linq;

namespace WindowsInput.Capture.Rx {
    public static class KeyObserverExtensions
    {
        public static IObservable<KeyCode> KeyDownObservable(this IKeyboardEvents source)
        {
            return Observable
                .FromEventPattern<InputEventArgs<KeyInput>>(source, "KeyDown")
                .Select(ep => ep.EventArgs.Data.Key)
                ;
        }

        public static IObservable<KeyCode> KeyUpObservable(this IKeyboardEvents source)
        {
            return Observable
                .FromEventPattern<InputEventArgs<KeyInput>>(source, "KeyDown")
                .Select(ep => ep.EventArgs.Data.Key)
                ;
        }


        public static IObservable<KeyEvent> UpDownEvents(this IKeyboardEvents source)
        {
            return source
                .KeyDownObservable()
                .Select(key => key.Down())
                .Merge(source
                    .KeyUpObservable()
                    .Select(key => key.Down()));
        }


        public static IObservable<KeyWithState> WithState(this IObservable<KeyCode> source)
        {
            return source
                .Select(evt => new KeyWithState(evt, KeyboardState.Current()));
        }

        public static IObservable<Combination> Matching(this IObservable<KeyCode> source, IEnumerable<Combination> triggers)
        {
            return source
                .WithState()
                .SelectMany(se => triggers.Where(se.Matches));
        }

        public static IObservable<Combination> MatchingLongest(this IObservable<KeyCode> source, IEnumerable<Combination> triggers)
        {
            var sortedTriggers = triggers
                .GroupBy(t => t.TriggerKey)
                .Select(group => new KeyValuePair<KeyCode, IEnumerable<Combination>>(group.Key, group.OrderBy(t => -t.ChordLength)))
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            return source
                .Where(keyCode => sortedTriggers.ContainsKey(keyCode))
                .WithState()
                .Select(se => sortedTriggers[se.KeyCode].First(se.Matches));
        }

    }
}