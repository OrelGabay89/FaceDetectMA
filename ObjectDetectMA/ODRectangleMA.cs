using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDetectMA
{
    public class ODRectangleMA
    {
        #region Constructs
        public ODRectangleMA(float x, float y, float width, float height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.CountDetect = 1;
        }
        #endregion

        #region Variables
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public int CountDetect { get; set; }

        public float X2
        {
            get
            {
                return X + Width;
            }
            set
            {
                Width = value - X;
            }
        }
        public float Y2
        {
            get
            {
                return Y + Height;
            }
            set
            {
                Height = value - Y;
            }
        }
        #endregion

        #region Methods
        #region Implicit
        public static implicit operator RectangleF(ODRectangleMA value)
        {
            return new RectangleF(value.X, value.Y, value.Width, value.Height);
        }
        #endregion
        #endregion
    }
}
