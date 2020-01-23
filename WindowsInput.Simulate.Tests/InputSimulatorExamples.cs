using NUnit.Framework;
using System.Threading.Tasks;
using WindowsInput.Events;
namespace WindowsInput.Tests
{
    [TestFixture]
    public class InputSimulatorExamples
    {
        [Test]
        [Explicit]
        public async Task OpenWindowsExplorer()
        {
            var sim = await EventBuilder.Create()
                .ClickChord(KeyCode.LWin, KeyCode.E)
                .Invoke()
                ;
        }

        [Test]
        [Explicit]
        public async Task SayHello() {
            var sim = Simulate.Events()
               .ClickChord(KeyCode.LWin, KeyCode.R)
               .Wait(1000)
               .Click("notepad")
               .Wait(1000)
               .Click(KeyCode.Return)
               .Wait(1000)
               .Click("These are your orders if you choose to accept them...")
               .Click("This message will self destruct in 5 seconds.")
               .Wait(5000)
               .Click(KeyCode.Alt, KeyCode.Space)
               .Click(KeyCode.Down)
               .Click(KeyCode.Return)
               ;


            for (int i = 0; i < 10; i++) {
                sim
                    .Click(KeyCode.Down)
                    .Wait(100)
                    ;
            }


            sim
                .Click(KeyCode.Return)
                .Wait(1000)
                .ClickChord(KeyCode.Alt, KeyCode.F4)
                .Click(KeyCode.N)
                ;

            await sim.Invoke();

        }

        [Test]
        [Explicit]
        public async Task AnotherTest()
        {
            var sim = new EventBuilder()
                .Click(KeyCode.Space)
                .ClickChord(KeyCode.LWin, KeyCode.R)
                .Wait(1000)
                .Click("mspaint")
                .Wait(1000)
                .Click(KeyCode.Return)
                .Wait(1000)
                .Hold(ButtonCode.Left)
                .MoveToVirtual(65535 / 2, 65535 / 2)
                .Release(ButtonCode.Left)
                ;

            await sim.Invoke();

        }

        [Test]
        [Explicit]
        public async Task TestMouseMoveTo() {
            var sim = new EventBuilder();
            sim
                .MoveTo(0, 0)
                .Wait(1000)
                .MoveTo(65535, 65535)
                .Wait(1000)
                .MoveTo(65535 / 2, 65535 / 2)
                ;

            await sim.Invoke();

        }

        [Test]
        [Explicit]
        public async Task TestMoves() {
            for (int i = 0; i < 100; i+=1) {
                await WindowsInput.Simulate.Events()
                    .MoveBy(i, i)
                    .Wait(100)
                    .MoveBy(-i, -i)
                    .Invoke()
                    ;
            }

        }



    }
}