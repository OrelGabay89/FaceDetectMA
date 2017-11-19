using AForge.Video;
using AForge.Video.DirectShow;
using ObjectDetectMA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceDetectMA
{
    public partial class frmMain : Form
    {
        #region Variables
        FilterInfoCollection videoDevices;
        VideoCaptureDevice videoSource;
        float sensivity = 0.2f;
        #endregion

        public frmMain()
        {
            InitializeComponent();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            if (cbDriver.SelectedIndex < 0)
            {
                MessageBox.Show("Please select driver!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bStop.Enabled = true;
            bStart.Enabled = false;

            videoSource = new VideoCaptureDevice(videoDevices[cbDriver.SelectedIndex].MonikerString);
            videoSource.DesiredFrameSize = new Size(320, 240);
            videoSource.NewFrame += videoSource_NewFrame;
            videoSource.Start();
        }

        void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            var _eventFrame = eventArgs.Frame;
            Bitmap _imageOriginal = (Bitmap)_eventFrame.Clone();
            Bitmap _image = (Bitmap)_eventFrame.Clone();
            Bitmap _imageSobel = (Bitmap)_eventFrame.Clone();
            _image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            _imageSobel.RotateFlip(RotateFlipType.RotateNoneFlipX);

            #region Face Detect Normal
            ODSobelEdgeMA.Detect(
                bmp: _image,
                sobelEdgeDetectEvent: (int x, int y, ODColorMA color) =>
                {
                    return Color.White;
                },
                rectDetectEvent: (int x, int y, ODColorMA color) =>
                {
                    byte _red = color.R;
                    byte _green = color.G;
                    byte _blue = color.B;
                    
                    _red = (byte)(200 - _red);
                    _green = (byte)(200 - _green);
                    _blue = (byte)(200 - _blue);

                    return new ODColorMA(_red, _green, _blue);
                },
                otherPixelDetectEvent: (int x, int y, ODColorMA color) =>
                {
                    byte _red = color.R;
                    byte _green = color.G;
                    byte _blue = color.B;
                    _red = (byte)Math.Max(_red - 120, 0);
                    _green = (byte)Math.Max(_green - 120, 0);
                    _blue = (byte)Math.Max(_blue - 120, 0);

                    return new ODColorMA(_red, _green, _blue);
                },
                filterRectEvent: (rects) =>
                {
                    return rects.OrderByDescending(o => o.CountDetect).Take(1).ToList();
                },
                diffDatas: new List<ODColorDiffMA>()
                {
                    bFilterColor.BackColor
                    //new ODColorDiffMA(<RED>, <GREEN>, <BLUE>)
                },
                sensivity: sensivity,
                boxRangeWidthPercent: 4,
                boxRangeHeightPercent: 4,
                detectRangeWidthPercent: 10,
                detectRangeHeightPercent: 10);
            #endregion

            #region Face Detect Sobel
            ODSobelEdgeMA.Detect(
                bmp: _imageSobel,
                sobelEdgeDetectEvent: (int x, int y, ODColorMA color) =>
                {
                    return Color.White;
                },
                rectDetectEvent: (int x, int y, ODColorMA color) =>
                {
                    return Color.Black;
                },
                otherPixelDetectEvent: (int x, int y, ODColorMA color) =>
                {
                    return Color.Red;
                },
                filterRectEvent: (rects) =>
                {
                    return rects.OrderByDescending(o => o.CountDetect).Take(1).ToList();
                },
                diffDatas: new List<ODColorDiffMA>()
                {
                    bFilterColor.BackColor
                    //new ODColorDiffMA(<RED>, <GREEN>, <BLUE>)
                },
                sensivity: sensivity,
                boxRangeWidthPercent: 4,
                boxRangeHeightPercent: 4,
                detectRangeWidthPercent: 10,
                detectRangeHeightPercent: 10);
            #endregion

            var _beforeImage = pbCamera.Image;
            var _beforeImageSobel = pbCameraSobel.Image;
            var _beforeImageOriginal = pbCamera.Tag == null ? null : (Bitmap)pbCamera.Tag;

            pbCamera.Image = _image;
            pbCameraSobel.Image = _imageSobel;
            pbCamera.Tag = _imageOriginal;

            if (_beforeImageSobel != null)
                _beforeImageSobel.Dispose();
            if (_beforeImage != null)
                _beforeImage.Dispose();
            if (_beforeImageOriginal != null)
                _beforeImageOriginal.Dispose();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            bStop.Enabled = false;
            bStart.Enabled = true;

            videoSource.Stop();
            pbCamera.Image = null;
            pbCamera.Invalidate();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (FilterInfo device in videoDevices)
                cbDriver.Items.Add(device.Name);

            videoSource = new VideoCaptureDevice();

            bFilterColor.BackColor = Color.FromArgb(148, 111, 109);
            tbSensivity.Value = (int)(sensivity * 100);
        }

        private void bFilterColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog _cd = new ColorDialog())
            {
                if (_cd.ShowDialog() == DialogResult.OK)
                {
                    bFilterColor.BackColor = _cd.Color;
                }
            }
        }

        private void pbCamera_MouseDown(object sender, MouseEventArgs e)
        {
            if (pbCamera.Image == null)
                return;
            var _bmp = (Bitmap)((Bitmap)pbCamera.Tag).Clone();
            var _pbWidthRatio = (float)_bmp.Width / pbCamera.Width;
            var _pbHeightRatio = (float)_bmp.Height / pbCamera.Height;
            var _pixel = _bmp.GetPixel((int)(_bmp.Width - (e.X * _pbWidthRatio)), (int)(e.Y * _pbHeightRatio));
            bFilterColor.BackColor = _pixel;
            _bmp.Dispose();
        }

        private void tbSensivity_ValueChanged(object sender, EventArgs e)
        {
            sensivity = tbSensivity.Value / 100f;
            lSensivity.Text = "Sensivity " + sensivity.ToString();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
                videoSource.Stop();
        }
    }
}
