using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using WindowsInput;
using WindowsInput.Events;
using WindowsInput.Native;

namespace WindowsInput.Tests {
    [TestFixture]
    public class ToggleTests {
        [Test]
        public async Task ToggleAsync1() {

            for (var i = 0; i < 5; i++) {
                
                var Before = KeyboardState.GetAsyncKeyState(KeyCode.LShift);

                if (Before != KeyboardKeyState.Default) {
                    await Simulate.Events().Release(KeyCode.LShift).Invoke();
                } else {
                    await Simulate.Events().Hold(KeyCode.LShift).Invoke();
                }

                var After1 = KeyboardState.GetAsyncKeyState(KeyCode.LShift);
                var After2 = KeyboardState.GetAsyncKeyState(KeyCode.LShift);

                Console.WriteLine($@"Test {i}");
                Console.WriteLine($@"Before: {Before}");
                Console.WriteLine($@"After1: {After1}");
                Console.WriteLine($@"After2: {After2}");
                Console.WriteLine();

            }

		}

        [Test]
        public async Task ToggleAsync2() {

            for (var i = 0; i < 5; i++) {

                var Before = KeyboardState.Current()[KeyCode.LShift];

                if (Before != KeyboardKeyState.Default) {
                    await Simulate.Events().Release(KeyCode.LShift).Invoke();
                } else {
                    await Simulate.Events().Hold(KeyCode.LShift).Invoke();
                }

                var After1 = KeyboardState.Current()[KeyCode.LShift];
                var After2 = KeyboardState.Current()[KeyCode.LShift];

                Console.WriteLine($@"Test {i}");
                Console.WriteLine($@"Before: {Before}");
                Console.WriteLine($@"After1: {After1}");
                Console.WriteLine($@"After2: {After2}");
                Console.WriteLine();

            }

        }

    }
}
