namespace WindowsInput.Native {
    public enum HookType : int {

        /// <summary>
        ///     Installs a hook procedure that monitors keystroke messages. For more information, see the KeyboardProc hook
        ///     procedure.
        /// </summary>
        AppKeyboard = 2,

        /// <summary>
        ///     Installs a hook procedure that monitors mouse messages. For more information, see the MouseProc hook procedure.
        /// </summary>
        AppMouse = 7,

        /// <summary>
        ///     Windows NT/2000/XP/Vista/7: Installs a hook procedure that monitors low-level keyboard  input events.
        /// </summary>
        GlobalKeyboard = 13,

        /// <summary>
        ///     Windows NT/2000/XP/Vista/7: Installs a hook procedure that monitors low-level mouse input events.
        /// </summary>
        GlobalMouse = 14,


    }
}