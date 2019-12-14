// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Runtime.InteropServices;
using WindowsInput.Native;
using WindowsInput;
using WindowsInput.Events;

namespace WindowsInput.Native
{
    /// <summary>
    ///     Provides extended data for the MouseClickExt and MouseMoveExt events.
    /// </summary>
    public class MouseInput
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MouseInput" /> class.
        /// </summary>
        /// <param name="buttons">One of the MouseButtons values indicating which mouse button was pressed.</param>
        /// <param name="clicks">The number of times a mouse button was pressed.</param>
        /// <param name="point">The x and y coordinate of a mouse click, in pixels.</param>
        /// <param name="scrolldelta">A signed count of the number of detents the wheel has rotated.</param>
        /// <param name="timestamp">The system tick count when the event occurred.</param>
        /// <param name="isMouseButtonDown">True if event signals mouse button down.</param>
        /// <param name="isMouseButtonUp">True if event signals mouse button up.</param>
        public MouseInput(ButtonCode buttons, POINT point, int scrolldelta, ButtonStatus status) {
            this.Button = buttons;
            this.X = point.X;
            this.Y = point.Y;

            this.ScrollOffset = scrolldelta;
            this.ButtonStatus = status;
        }

        //
        // Summary:
        //     Gets which mouse button was pressed.
        //
        // Returns:
        //     One of the System.Windows.Forms.MouseButtons values.
        public ButtonCode Button { get; private set; }

        //
        // Summary:
        //     Gets the x-coordinate of the mouse during the generating mouse event.
        //
        // Returns:
        //     The x-coordinate of the mouse, in pixels.
        public int X { get; private set; }

        //
        // Summary:
        //     Gets the y-coordinate of the mouse during the generating mouse event.
        //
        // Returns:
        //     The y-coordinate of the mouse, in pixels.
        public int Y { get; private set; }


        //
        // Summary:
        //     Gets a signed count of the number of detents the mouse wheel has rotated, multiplied
        //     by the WHEEL_DELTA constant. A detent is one notch of the mouse wheel.
        //
        // Returns:
        //     A signed count of the number of detents the mouse wheel has rotated, multiplied
        //     by the WHEEL_DELTA constant.
        public int ScrollOffset { get; private set; }


        public ButtonStatus ButtonStatus { get; }

        /// <summary>
        ///     The system tick count of when the event occurred.
        /// </summary>
        public int Timestamp { get; }

    }

}