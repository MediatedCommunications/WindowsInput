namespace WindowsInput.Events {
    public enum ButtonScrollDirection {
        Backwards = 1,
        None = 0,
        Forwards = -1,

        /// <summary>
        /// An alias for <seealso cref="Backwards"/>
        /// </summary>
        Up = 1,

        /// <summary>
        /// An alias for <see cref="Forwards"/>
        /// </summary>
        Down = -1,

        /// <summary>
        /// An alias for <seealso cref="Backwards"/>
        /// </summary>
        Left = 1,

        /// <summary>
        /// An alias for <see cref="Forwards"/>
        /// </summary>
        Right = -1,

    }

}
