// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System.Globalization;
using System.Runtime.InteropServices;

namespace WindowsInput.Native
{

    /// <summary>Represents an ordered pair of integer x- and y-coordinates that defines a point in a two-dimensional plane.</summary>
    /// <filterpriority>1</filterpriority>
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT {
        public int X { get; set; }
        public int Y { get; set; }
    }


}