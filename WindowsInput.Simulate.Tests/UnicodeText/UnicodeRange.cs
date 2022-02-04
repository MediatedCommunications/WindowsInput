using System.Linq;
using System.Text;

namespace WindowsInput.Tests.UnicodeText
{
    public class UnicodeRange
    {
        public string Name { get; }
        public int Low { get; }
        public int High { get; }

        public string Characters
        {
            get
            {
                var Valid = Enumerable.Range(Low, High + 1 - Low)
                    .Where(x => (x >= 0 && x <= 0x10ffff) && !(x >= 0x00d800 && x <= 0x00dfff))
                    .Select(x => char.ConvertFromUtf32(x))
                    .ToArray()
                    ;

                var ret = string.Join("", Valid);

                return ret;
            }
        }

        public UnicodeRange(string name, int low, int high)
        {
            Name = name;
            Low = low;
            High = high;
        }
    }
}