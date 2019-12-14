using System;
using System.Linq;
using NUnit.Framework;
using WindowsInput;
using WindowsInput.Events;

namespace WindowsInput.Tests {
    [TestFixture]
    public class InputBuilderTests {
        [Test]
        public void AddKeyDown() {


            var builder = new EventBuilder();
            Assert.That(builder.Events.ToArray(), Is.Empty);
            builder.Hold(KeyCode.A);
            Assert.That(builder.Events.Count(), Is.EqualTo(1));


        }
    }
}
