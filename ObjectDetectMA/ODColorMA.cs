using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDetectMA
{
    public class ODColorMA
    {
        #region Constructs
        public ODColorMA(byte r, byte g, byte b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }
        #endregion

        #region Variables
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        #endregion

        #region Methods


        #region Implicit
        public static implicit operator ODColorMA(Color color)
        {
            return new ODColorMA(color.R, color.G, color.B);
        }
        #endregion
        #endregion
    }
}
