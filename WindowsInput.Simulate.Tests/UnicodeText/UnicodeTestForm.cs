using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsInput.Tests.UnicodeText
{
    public partial class UnicodeTestForm : Form
    {

        public UnicodeTestForm()
        {
            InitializeComponent();
            this.Shown += this.UnicodeTestForm_Shown;
        }

        public UnicodeTestForm(string Name, string Expected) : this() {
            this.Text = $@"{Name} - {Expected.Length} Long";
            
            this.SetExpected(Expected);
        }

        public bool Ready { get; private set; }

        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(IntPtr hWnd);

        public void BringToForeground() {

            this.Invoke(new Action(() => {
                SetForegroundWindow(this.Handle);
            }));
           
        }

        private void UnicodeTestForm_Shown(object sender, EventArgs e) {
            BringToForeground();

            this.Ready = true;
        }


        public void SetExpected(string Value) {
            this.Expected = Value;
            this.ExpectedTextBox.Text = Value;
        }


        public string Expected { get; private set; } = "";
        public string Recieved { get; private set; } = "";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RecievedTextBox.Focus();
        }

        private void RecievedTextBox_TextChanged(object sender, EventArgs e) {
            Recieved = RecievedTextBox.Text;
        }
    }
}
