// This code is distributed under MIT license.
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using System.Linq;
using WindowsInput.Native;
using WindowsInput.Events;

namespace WindowsInput.Events.Sources {
    public class MouseEventSourceState {
        public int DragThresholdX { get; private set; } = SystemMetrics.Mouse.Drag.XThreshold.Value;
        public int DragThresholdY { get; private set; } = SystemMetrics.Mouse.Drag.YThreshold.Value;
        public TimeSpan DoubleClickDuration { get; private set; } = SystemMetrics.Mouse.DoubleClick.Duration.Value;


        public EventSourceEventArgs<MouseInput> LastInput { get; private set; }

        public Dictionary<ButtonCode, EventSourceEventArgs<MouseInput>> LastButtonDownInput { get; private set; } = new Dictionary<ButtonCode, EventSourceEventArgs<MouseInput>>();
        public Dictionary<ButtonCode, EventSourceEventArgs<MouseInput>> LastButtonClickInput { get; private set; } = new Dictionary<ButtonCode, EventSourceEventArgs<MouseInput>>();
        
        public Dictionary<ButtonCode, EventSourceEventArgs<MouseInput>> ButtonsDownInput { get; private set; } = new Dictionary<ButtonCode, EventSourceEventArgs<MouseInput>>();
        public Dictionary<ButtonCode, MouseMoveAbsolute> ButtonsDownPosition { get; private set; } = new Dictionary<ButtonCode, MouseMoveAbsolute>();
        public Dictionary<ButtonCode, ButtonDown> ButtonsDownData { get; private set; } = new Dictionary<ButtonCode, ButtonDown>();
        public Dictionary<ButtonCode, DragStart> ButtonsDragging { get; private set; } = new Dictionary<ButtonCode, DragStart>();

        public Dictionary<ButtonCode, EventSourceEventArgs<MouseInput>> ButtonsDraggingInput { get; private set; } = new Dictionary<ButtonCode, EventSourceEventArgs<MouseInput>>();

        public MouseMoveAbsolute CurrentPosition { get; private set; }

        public EventSourceEventArgs<MouseEvent> GetEventArgs(EventSourceEventArgs<MouseInput> e) {
            var Wait = LastInput != default && e.Timestamp - LastInput.Timestamp is var Duration && Duration > TimeSpan.Zero
                ? new Wait(Duration)
                : null
                ;

            var Move = (LastInput?.Data.X != e.Data.X || LastInput?.Data.Y != e.Data.Y)
                ? new MouseMoveAbsolute(e.Data.X, e.Data.Y)
                : null
                ;

            CurrentPosition = Move ?? CurrentPosition;


            var Scroll = e.Data.ButtonStatus == ButtonStatus.Scrolled
                ? new ButtonScroll(e.Data.Button, e.Data.ScrollOffset)
                : null
                ;

            var Down = e.Data.ButtonStatus == ButtonStatus.Pressed
                ? new ButtonDown(e.Data.Button)
                : null
                ;

            if(Down is { }) {
                ButtonsDownInput[Down.Button] = e;
                ButtonsDownData[Down.Button] = Down;
                ButtonsDownPosition[Down.Button] = CurrentPosition;
            }

            var DragStarted = default(IReadOnlyCollection<DragStart>);
            {
                var DragStartedInternal = new List<DragStart>();

                foreach (var item in ButtonsDownInput.Where(x => !ButtonsDraggingInput.ContainsKey(x.Key))) {

                    if (Math.Abs(item.Value.Data.X - e.Data.X) > DragThresholdX || Math.Abs(item.Value.Data.Y - e.Data.Y) > DragThresholdY) {
                        ButtonsDraggingInput[item.Key] = item.Value;

                        if (ButtonsDownPosition.TryGetValue(item.Key, out var DragStartPosition) &&  ButtonsDownData.TryGetValue(item.Key, out var DragData)) {
                            var ThisDragData = new DragStart(DragStartPosition, DragData);
                            DragStartedInternal.Add(ThisDragData);

                            ButtonsDragging[item.Key] = ThisDragData;

                        }

                    }
                }

                if(DragStartedInternal.Count != 0) {
                    DragStarted = DragStartedInternal.AsReadOnly();
                }

            }


            var Up = e.Data.ButtonStatus == ButtonStatus.Released
                ? new ButtonUp(e.Data.Button)
                : null
                ;

            var DragFinished = default(IReadOnlyCollection<DragDrop>);
            if(Up is { }) {
                var DragFinishedInternal = new List<DragDrop>();

                if (ButtonsDragging.TryGetValue(Up.Button, out var DragStart)) {

                    var DragStop = new DragStop(CurrentPosition, Up);
                    var DragDrop = new DragDrop(DragStart, DragStop);

                    DragFinishedInternal.Add(DragDrop);
                }

                if (DragFinishedInternal.Count != 0) {
                    DragFinished = DragFinishedInternal.AsReadOnly();
                }
            }
            

            if(Up is { }) {
                ButtonsDownInput.Remove(Up.Button);
                ButtonsDownData.Remove(Up.Button);
                ButtonsDownPosition.Remove(Up.Button);

                ButtonsDraggingInput.Remove(Up.Button);
                ButtonsDragging.Remove(Up.Button);
            }

            var Click = true
                && Up != default 
                && LastButtonDownInput.TryGetValue(Up.Button, out var LastUp) 
                && Math.Abs(LastUp.Data.X - e.Data.X) < DragThresholdX 
                && Math.Abs(LastUp.Data.Y - e.Data.Y) < DragThresholdY
                ? new ButtonClick(e.Data.Button)
                : null
                ;

            var ClickHold = true
                && Down != default
                && LastButtonClickInput.TryGetValue(Down.Button, out var LastClick_ClickHold)
                && e.Timestamp - LastClick_ClickHold.Timestamp <= DoubleClickDuration
                && Math.Abs(LastClick_ClickHold.Data.X - e.Data.X) < DragThresholdX
                && Math.Abs(LastClick_ClickHold.Data.Y - e.Data.Y) < DragThresholdY
                ? new ButtonClickHold(e.Data.Button)
                : null
                ;

            var DoubleClick = true
                && Click != default 
                && LastButtonClickInput.TryGetValue(Up.Button, out var LastClick_DoubleClick) 
                && e.Timestamp - LastClick_DoubleClick.Timestamp <= DoubleClickDuration 
                && Math.Abs(LastClick_DoubleClick.Data.X - e.Data.X) < DragThresholdX 
                && Math.Abs(LastClick_DoubleClick.Data.Y - e.Data.Y) < DragThresholdY
                ? new ButtonDoubleClick(e.Data.Button)
                : null
                ;




            //SET VARIABLES FOR NEXT TIME (THE LAST VARIABLES)
            if(Move is { } || Scroll is { } || Down is { } || Up is { } || Click is { } || DoubleClick is { } || DragStarted is { } || DragFinished is { }) {
                //Only set the LastInput if we have some event other than Wait
                LastInput = e;
            } else {
                //If all we have is a "Wait", don't return it as it will get aggregated in our next call.
                Wait = default;
            }
            

            if (Down is { }) {
                LastButtonDownInput[Down.Button] = e;
            }

            if (Click is { }) {
                LastButtonClickInput[Click.Button] = e;
            }

            var Data = new MouseEvent(Wait, Move, Scroll, Down, Up, Click, ClickHold, DoubleClick, DragStarted, DragFinished);

            var ret = new EventSourceEventArgs<MouseEvent>(e.Timestamp, Data, e.RawData);

            return ret;
        }
    }


}