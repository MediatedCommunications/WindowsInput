# WindowsInput
## Capture and Simulate Keyboard and Mouse Input
WindowsInput provides simple .NET (C#) classes to capture and simulate Keyboard and mouse input using Win32's SetWindowsHook and SendInput.  All of the interop is done for you and there is a simple programming model for everything.



## NuGet [![nuget][nuget-badge]][nuget-url] 

 [nuget-badge]: https://img.shields.io/badge/nuget-v6.0.0-blue.svg
 [nuget-url]: https://www.nuget.org/packages/WindowsInput

````
Install-Package WindowsInput
````

## Prerequisites

 - **Windows:**
   .Net 4.6.1+

## Samples

### Run Notepad using the Keyboard...
````csharp
public async Task RunNotepad() {
    await WindowsInput.Simulate.Events()
        //Hold Windows Key+R
        .ClickChord(KeyCode.LWin, KeyCode.R).Wait(1000)

        //Type "notepad"
        .Click("notepad").Wait(1000)

        //Press Enter
        .Click(KeyCode.Return).Wait(1000)

        //Type out our message.
        .Click("These are your orders if you choose to accept them...")
        .Click("This message will self destruct in 5 seconds.").Wait(5000)

        //Hold Alt+F4
        .ClickChord(KeyCode.Alt, KeyCode.F4).Wait(1000)

        //Press Tab then Enter.
        .Click(KeyCode.Tab, KeyCode.Return)

        //Do it!
        .Invoke()
        ;
````

### Capture Keys from the Keyboard and disable the 'a' key:
````csharp

public static void Main(){
    using (var Keyboard = WindowsInput.Capture.Global.KeyboardAsync()) {
        //Capture all events from the keyboard
        Keyboard.KeyEvent += Keyboard_KeyEvent;
        Console.ReadLine();
    }
}

private static void Keyboard_KeyEvent(object sender, EventSourceEventArgs<KeyboardEvent> e) {
    
    if(e.Data?.KeyDown?.Key == WindowsInput.Events.KeyCode.A || e.Data?.KeyUp?.Key == WindowsInput.Events.KeyCode.A) {
        e.Next_Hook_Enabled = false;
    }

}
````

### Text Snippet Replacement
This example waits for someone to type 'aaa' (three A's in a row) and then replaces
the text with 'Always Ask Albert'.
````csharp
public static Task Do() {
    using (var Keyboard = WindowsInput.Capture.Global.KeyboardAsync()) {

        var Listener = new WindowsInput.EventSources.TextSequenceEventSource(Keyboard, new WindowsInput.Events.TextClick("aaa"));
        Listener.Triggered += (x, y) => Listener_Triggered(Keyboard, x, y); ;
        Listener.Enabled = true;

        Console.WriteLine("The keyboard is now listening for sequences.  Try typing 'aaa' in notepad.");

        Console.WriteLine("Press enter to quit...");
        Console.ReadLine();

    }
                
    return Task.CompletedTask;
}

private static async void Listener_Triggered(IKeyboardEventSource Keyboard, object sender, WindowsInput.EventSources.TextSequenceEventArgs e) {
    e.Input.Next_Hook_Enabled = false;

    var ToSend = WindowsInput.Simulate.Events();
    for (int i = 1; i < e.Sequence.Text.Length; i++) {
        ToSend.Click(WindowsInput.Events.KeyCode.Backspace);
    }

    ToSend.Click("Always ask albert!");

    //We suspend keyboard events because we don't want to accidently trigger a recursive loop if our
    //sending text actually had 'aaa' in it.
    using (Keyboard.Suspend()) {
        await ToSend.Invoke();
    }
}
````

### Trigger text on a special key combination
````csharp
public static Task Do() {
    using (var Keyboard = WindowsInput.Capture.Global.KeyboardAsync()) {

        var Listener = new WindowsInput.EventSources.KeyChordEventSource(Keyboard, new ChordClick(KeyCode.Control, KeyCode.Alt, KeyCode.Shift));
        Listener.Triggered += (x, y) => Listener_Triggered(Keyboard, x, y);
        Listener.Reset_On_Parent_EnabledChanged = false;
        Listener.Enabled = true;

        Console.WriteLine("The keyboard is now listening for chords.  Try typing 'CONTROL+ALT+SHIFT' in notepad.");

        Console.WriteLine("Press enter to quit...");
        Console.ReadLine();

    }

    return Task.CompletedTask;
}

private static async void Listener_Triggered(IKeyboardEventSource Keyboard, object sender, WindowsInput.EventSources.KeyChordEventArgs e) {
    var ToSend = WindowsInput.Simulate.Events();
            
    ToSend.Click("You pressed the magic keys.");

    using (Keyboard.Suspend()) {
        await ToSend.Invoke();
    }
}


````


## Credits
----
This work is a tightly unified library that was built on the backs of the following giants:
- https://github.com/michaelnoonan/inputsimulator
- https://github.com/gmamaladze/globalmousekeyhook
