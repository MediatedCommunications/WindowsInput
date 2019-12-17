using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using WindowsInput.Events;

namespace WindowsInput.Events {
    
    public partial class EventBuilder {
        /// <summary>
        /// Add an event to this <see cref="EventBuilder"/>
        /// </summary>
        /// <param name="Value">The Event to add.  Null values will silently be ignored.</param>
        /// <returns></returns>
        public EventBuilder Add(IEvent Value) {
            if (Value != null) {
                this.Events.Add(Value);
            }

            return this;
        }

        /// <summary>
        /// Hold the specifed keyboard key.
        /// </summary>
        public EventBuilder Hold(KeyCode Key, bool? Extended = default) {
            return Add(new KeyDown(Key, Extended));
        }

        /// <summary>
        /// Release the specifed keyboard key.
        /// </summary>
        public EventBuilder Release(KeyCode Key, bool? Extended = default) {
            return Add(new KeyUp(Key, Extended));
        }

        /// <summary>
        /// Hold and release the specifed keyboard key.
        /// </summary>
        public EventBuilder Click(KeyCode Key, bool? Extended = default) {
            return Add(new KeyClick(Key, Extended));
        }

        /// <summary>
        /// Hold and release the specified character.
        /// </summary>
        public EventBuilder Click(char Key) {
            return Add(new CharacterClick(Key));
        }

        /// <summary>
        /// Hold and release the specified keycodes.  Keys will be held in order and released in reverse order.
        /// </summary>
        public EventBuilder ClickChord(params KeyCode[] Keys) {
            return Add(new ChordClick(Keys));
        }

        /// <summary>
        /// Hold and release the specified keycodes.
        /// </summary>
        public EventBuilder Click(params KeyCode[] Keys) {
            return Add(new SequenceClick(Keys));
        }

        /// <summary>
        /// Hold and release the characters associated with the specified text while optionally waiting the specified time between characters.
        /// </summary>
        public EventBuilder Click(IEnumerable<char> Keys, IEvent BetweenCharacters = default) {
            return Add(new TextClick(Keys, BetweenCharacters));
        }

        /// <summary>
        /// Hold the specified mouse button.
        /// </summary>
        public EventBuilder Hold(ButtonCode Button) {
            return Add(new ButtonDown(Button));
        }

        /// <summary>
        /// Release the specified mouse button.
        /// </summary>
        public EventBuilder Release(ButtonCode Button) {
            return Add(new ButtonUp(Button));
        }

        /// <summary>
        /// Hold and release the specified mouse button.
        /// </summary>
        public EventBuilder Click(ButtonCode Button) {
            return Add(new ButtonClick(Button));
        }

        /// <summary>
        /// Hold and release the specified mouse button twice.
        /// </summary>
        public EventBuilder DoubleClick(ButtonCode Button) {
            return Add(new ButtonDoubleClick(Button));
        }

        /// <inheritdoc cref="Scroll(ButtonCode, ButtonScrollDirection, int)"/>
        public EventBuilder Scroll(ButtonCode Button, int Offset) {
            return Add(new ButtonScroll(Button, Offset));
        }

        /// <inheritdoc cref="Scroll(ButtonCode, ButtonScrollDirection, int)"/>
        public EventBuilder Scroll(ButtonCode Button, ButtonScrollDirection Direction) {
            return Add(new ButtonScroll(Button, Direction));
        }

        /// <summary>
        /// Scroll the specified mouse button inthe specified direction by the given amount.
        /// </summary>
        /// <param name="Button">The button to scroll</param>
        /// <param name="Direction">The direction to scroll</param>
        /// <param name="Offset">The amount to scroll</param>
        /// <returns></returns>
        public EventBuilder Scroll(ButtonCode Button, ButtonScrollDirection Direction, int Offset) {
            return Add(new ButtonScroll(Button, Direction, Offset));
        }

        /// <summary>
        /// Move the mouse to the specified absolute coordinates
        /// </summary>
        public EventBuilder MoveTo(int x, int y) {
            return Add(new MouseMoveAbsolute(x, y));
        }

        /// <summary>
        /// Move the mouse to the specified virtual coordinates
        /// </summary>
        public EventBuilder MoveToVirtual(int x, int y) {
            return Add(new MouseMoveVirtual(x, y));
        }

        /// <summary>
        /// Move the mouse by the specified relative coordinates
        /// </summary>
        public EventBuilder MoveBy(int x, int y) {
            return Add(new MouseMoveRelative(x, y));
        }

        /// <summary>
        /// Wait for the specified duration.
        /// </summary>
        public EventBuilder Wait(TimeSpan Duration) {
            return Add(new Wait(Duration));
        }

        /// <summary>
        /// Wait for the specified duration.
        /// </summary>
        public EventBuilder Wait(double DurationInMs) {
            return Add(new Wait(DurationInMs));
        }

        /// <summary>
        /// Wait until the specified window becomes responsive.
        /// </summary>
        public EventBuilder WaitUntilResponsive(IntPtr hWnd, TimeSpan Timeout) {
            return Add(new WaitUntilResponsive(hWnd, Timeout));
        }

        /// <summary>
        /// Wait long enough to prevent two consecutive clicks from triggering a doubleclick event.
        /// </summary>
        public EventBuilder WaitToPreventDoubleClicking() {
            return Add(new WaitToPreventDoubleClicking());
        }

        /// <summary>
        /// Simulate a drag and drop operation between two locations.
        /// </summary>
        /// <param name="Start">The initial location the cursor should be positioned.</param>
        /// <param name="Button">The button to perfom the drag with.</param>
        /// <param name="Stop">The final location of the cursor where the button should be released.</param>
        /// <returns></returns>
        public EventBuilder DragDrop(MouseMove Start, ButtonCode Button, MouseMove Stop) {
            return Add(new DragDrop(Start, Button, Stop));
        }


    }

}
