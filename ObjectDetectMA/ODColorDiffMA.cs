using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDetectMA
{
    public class ODColorDiffMA
    {
        #region Constructs
        public ODColorDiffMA(int red, int green, int blue)
        {
            float _redDivideGreen;
            float _redDivideBlue;
            float _greenDivideBlue;
            Calc(red, green, blue, out _redDivideGreen, out _redDivideBlue, out _greenDivideBlue);
            this.RedDivideGreen = _redDivideGreen;
            this.RedDivideBlue = _redDivideBlue;
            this.GreenDivideBlue = _greenDivideBlue;
        }
        #endregion

        #region Variables
        public float RedDivideGreen { get; set; }
        public float RedDivideBlue { get; set; }
        public float GreenDivideBlue { get; set; }
        #endregion

        #region Methods
        public bool Control(int red, int green, int blue, float diff)
        {
            float _redDivideGreen;
            float _redDivideBlue;
            float _greenDivideBlue;
            Calc(red, green, blue, out _redDivideGreen, out _redDivideBlue, out _greenDivideBlue);

            var _diffRedDivideGreen = Math.Abs(this.RedDivideGreen - _redDivideGreen);
            var _diffRedDivideBlue = Math.Abs(this.RedDivideBlue - _redDivideBlue);
            var _diffGreenDivideBlue = Math.Abs(this.GreenDivideBlue - _greenDivideBlue);

            var _ctrl =
                            _diffRedDivideGreen <= diff &&
                            _diffRedDivideBlue <= diff &&
                            _diffGreenDivideBlue <= diff;
            return _ctrl;
        }
        public static void Calc(
            float red,
            float green,
            float blue,
            out float redDivideGreen,
            out float redDivideBlue,
            out float greenDivideBlue
            )
        {
            red++;
            green++;
            blue++;
            redDivideGreen = (float)Math.Round(red / green, 3, MidpointRounding.AwayFromZero);
            redDivideBlue = (float)Math.Round(red / blue, 3, MidpointRounding.AwayFromZero);
            greenDivideBlue = (float)Math.Round(green / blue, 3, MidpointRounding.AwayFromZero);
        }

        #region Implicit
        public static implicit operator ODColorDiffMA(Color color)
        {
            return new ODColorDiffMA(color.R, color.G, color.B);
        }
        public static implicit operator ODColorDiffMA(ODColorMA color)
        {
            return new ODColorDiffMA(color.R, color.G, color.B);
        }
        #endregion
        #endregion
    }
}
