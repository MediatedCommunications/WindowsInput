using System;

namespace WindowsInput.Events {
    public class InvokeDispatcherException : Exception {

        const string MESSAGE = "" 
            + "A simulated input command was not sent successfully.  "
            + "This generally occurs because of Windows security features including User Interface Privacy Isolation (UIPI).  " 
            + "Your application can only send commands to applications of the same or lower elevation.  " 
            + "Similarly certain commands are restricted to Accessibility/UIAutomation applications.  " 
            + "Please refer to the project home page and the code samples for more information."
            ;

        public InvokeDispatcherException() : base(MESSAGE) { }

    }


}
