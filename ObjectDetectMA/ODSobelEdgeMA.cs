using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDetectMA
{
    public abstract class ODSobelEdgeMA
    {
        public delegate ODColorMA DetectEvent(int x, int y, ODColorMA color);
        public delegate List<ODRectangleMA> FilterRect(List<ODRectangleMA> rects);
        public static unsafe List<ODRectangleMA> Detect(Bitmap bmp,
            DetectEvent sobelEdgeDetectEvent,
            DetectEvent rectDetectEvent,
            DetectEvent otherPixelDetectEvent,
            FilterRect filterRectEvent,
            List<ODColorDiffMA> diffDatas,
            float sensivity = 0.1f,
            byte boxRangeWidthPercent = 4,
            byte boxRangeHeightPercent = 4,
            byte detectRangeWidthPercent = 10,
            byte detectRangeHeightPercent = 10)
        {
            #region Variables
            int _width = bmp.Width;
            int _height = bmp.Height;

            //Sobel Algorithm
            sbyte[,] _sobelX = new sbyte[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            sbyte[,] _sobelY = new sbyte[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
            int _limit = 128 * 128;

            //For pixel search codes
            BitmapData _bmpData = bmp.LockBits(new Rectangle(0, 0, _width, _height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            byte* _pixel = (byte*)_bmpData.Scan0;
            int _pixelPerByte = Image.GetPixelFormatSize(_bmpData.PixelFormat) / 8;

            //Pixels Colors
            byte[,] _allPixR = new byte[_width, _height];
            byte[,] _allPixG = new byte[_width, _height];
            byte[,] _allPixB = new byte[_width, _height];

            List<ODColorDiffMA> _faceDatas = diffDatas;
            //{
            //    //new ColorDiffMA(157, 100, 116),
            //    new ColorDiffMA(138, 99, 99)
            //};
            var _sensivity = sensivity;

            //Clear Rect
            ODRectangleMA _faceRectFirst = new ODRectangleMA(-1, 0, 1, 1);
            List<ODRectangleMA> _faceRects = new List<ODRectangleMA>()
            {
                _faceRectFirst
            };
            List<RectangleF> _faceRectsDotNet = new List<RectangleF>();
            int _faceRectControlRangeX = (int)((bmp.Width / 100f) * boxRangeWidthPercent);
            int _faceRectControlRangeY = (int)((bmp.Height / 100f) * boxRangeHeightPercent);

            int _faceControlWidth = (int)((bmp.Width / 100f) * detectRangeWidthPercent);
            int _faceControlWidthRight = bmp.Width - _faceControlWidth;
            int _faceControlHeight = (int)((bmp.Height / 100f) * detectRangeHeightPercent);
            int _faceControlHeightBottom = bmp.Height - _faceControlHeight;
            #endregion

            #region Clear Face
            float _diffX = 0, _diffX2 = 0;
            float _diffY = 0, _diffY2 = 0;
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    byte _redPixel = _pixel[2];
                    byte _greenPixel = _pixel[1];
                    byte _bluePixel = _pixel[0];

                    if (y >= _faceControlHeight && y <= _faceControlHeightBottom && x >= _faceControlHeight && x <= _faceControlWidthRight)
                    {
                        if (_faceDatas.Any(o => o.Control(_redPixel, _greenPixel, _bluePixel, _sensivity)))
                        {
                            if (_faceRectFirst.X == -1)
                            {
                                _faceRectFirst.X = x;
                                _faceRectFirst.Y = y;
                            }

                            var _existsRange = false;
                            foreach (var item in _faceRects)
                            {
                                _diffX = Math.Abs(item.X - x);
                                _diffY = Math.Abs(item.Y - y);
                                _diffX2 = Math.Abs(item.X2 - x);
                                _diffY2 = Math.Abs(item.Y2 - y);
                                if (
                                    (
                                        _diffY2 <= _faceRectControlRangeY ||
                                        _diffY <= _faceRectControlRangeY
                                    )
                                    &&
                                    (
                                        _diffX <= _faceRectControlRangeX ||
                                        _diffX2 <= _faceRectControlRangeX
                                    ))
                                {
                                    item.CountDetect++;
                                    _existsRange = true;
                                    if (item.X > x)
                                        item.X = x;

                                    if (item.Y > y)
                                        item.Y = y;

                                    if (item.X2 < x)
                                        item.X2 = x;
                                    if (item.Y2 < y)
                                        item.Y2 = y;
                                    break;
                                }
                            }

                            if (!_existsRange)
                            {
                                var _rect = new ODRectangleMA(x, y, 1, 1);
                                _faceRects.Add(_rect);
                            }
                        }
                    }

                    _allPixR[x, y] = _redPixel;
                    _allPixG[x, y] = _greenPixel;
                    _allPixB[x, y] = _bluePixel;

                    _pixel += _pixelPerByte;
                }
            }
            List<ODRectangleMA> _faceRectanglesFilter = new List<ODRectangleMA>();
            List<int> _filterIndexes = new List<int>();
            int _rectIndex1 = 0;
            int _rectIndex2 = 0;
            foreach (var rect1 in _faceRects)
            {
                if (_filterIndexes.Any(o => o == _rectIndex1))
                {
                    _rectIndex1++;
                    continue;
                }

                _filterIndexes.Add(_rectIndex1);
                _rectIndex2 = 0;
                ODRectangleMA _netRect = new ODRectangleMA(rect1.X, rect1.Y, rect1.Width, rect1.Height);
                _netRect.CountDetect += rect1.CountDetect;
                foreach (var rect2 in _faceRects)
                {
                    if (_rectIndex1 == _rectIndex2 || _filterIndexes.Any(o => o == _rectIndex2))
                    {
                        _rectIndex2++;
                        continue;
                    }

                    var _rect1 = (RectangleF)_netRect;
                    var _rect2 = (RectangleF)rect2;
                    if (_rect1.IntersectsWith(_rect2))
                    {
                        var _minX = Math.Min(_netRect.X, rect2.X);
                        var _minY = Math.Min(_netRect.Y, rect2.Y);
                        var _maxX = Math.Max(_netRect.Width, rect2.Width);
                        var _maxY = Math.Max(_netRect.Height, rect2.Height);

                        _netRect.X = _minX;
                        _netRect.Y = _minY;
                        _netRect.Width = _maxX;
                        _netRect.Height = _maxY;
                        _netRect.CountDetect += rect2.CountDetect;

                        _filterIndexes.Add(_rectIndex2);
                    }
                    _rectIndex2++;
                }
                _faceRectanglesFilter.Add(_netRect);
                _rectIndex1++;
            }

            _faceRects = filterRectEvent(_faceRectanglesFilter);
            _faceRectsDotNet = _faceRects.Select(o => (RectangleF)o).ToList();
            #endregion

            int _sobelNewRedX = 0, _sobelNewRedY = 0;
            int _sobelNewGreenX = 0, _sobelNewGreenY = 0;
            int _sobelNewBlueX = 0, _sobelNewBlueY = 0;
            byte _sobelRed, _sobelGreen, _sobelBlue;
            _pixel = (byte*)_bmpData.Scan0;
            byte _sobelPixelColorR = 0;
            byte _sobelPixelColorG = 0;
            byte _sobelPixelColorB = 0;

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    var _otherPixel = true;
                    if (y > 0 && y < bmp.Height - 1 && x > 0 && x < bmp.Width - 1)
                    {
                        var _rect = new RectangleF(x, y, 1, 1);
                        if (_faceRectsDotNet.Any(o => o.IntersectsWith(_rect)))
                        {
                            _otherPixel = false;

                            #region Sobel Algorithm
                            _sobelNewRedX = 0;
                            _sobelNewRedY = 0;

                            _sobelNewGreenX = 0;
                            _sobelNewGreenY = 0;
                            _sobelNewBlueX = 0;
                            _sobelNewBlueY = 0;
                            _sobelRed = 0;
                            _sobelGreen = 0;
                            _sobelBlue = 0;


                            for (int wi = -1; wi < 2; wi++)
                            {
                                for (int hw = -1; hw < 2; hw++)
                                {
                                    _sobelRed = _allPixR[x + hw, y + wi];
                                    _sobelNewRedX += _sobelX[wi + 1, hw + 1] * _sobelRed;
                                    _sobelNewRedY += _sobelY[wi + 1, hw + 1] * _sobelRed;

                                    _sobelGreen = _allPixG[x + hw, y + wi];
                                    _sobelNewGreenX += _sobelX[wi + 1, hw + 1] * _sobelGreen;
                                    _sobelNewGreenY += _sobelY[wi + 1, hw + 1] * _sobelGreen;

                                    _sobelBlue = _allPixB[x + hw, y + wi];
                                    _sobelNewBlueX += _sobelX[wi + 1, hw + 1] * _sobelBlue;
                                    _sobelNewBlueY += _sobelY[wi + 1, hw + 1] * _sobelBlue;

                                    if (wi == 0 && hw == 0)
                                    {
                                        _sobelPixelColorR = _sobelRed;
                                        _sobelPixelColorG = _sobelGreen;
                                        _sobelPixelColorB = _sobelBlue;
                                    }
                                }
                            }
                            var _sobelControl =
                                _sobelNewRedX * _sobelNewRedX + _sobelNewRedY * _sobelNewRedY > _limit ||
                                _sobelNewGreenX * _sobelNewGreenX + _sobelNewGreenY * _sobelNewGreenY > _limit ||
                                _sobelNewBlueX * _sobelNewBlueX + _sobelNewBlueY * _sobelNewBlueY > _limit;
                            #endregion

                            if (_sobelControl)
                            {
                                var _eventGetColor = sobelEdgeDetectEvent(x, y, new ODColorMA(_sobelPixelColorR, _sobelPixelColorG, _sobelPixelColorB));
                                if (_eventGetColor != null)
                                {
                                    _pixel[2] = _eventGetColor.R;
                                    _pixel[1] = _eventGetColor.G;
                                    _pixel[0] = _eventGetColor.B;
                                }
                            }
                            else
                            {
                                var _eventGetColor = rectDetectEvent(x, y, new ODColorMA(_sobelPixelColorR, _sobelPixelColorG, _sobelPixelColorB));
                                if (_eventGetColor != null)
                                {
                                    _pixel[2] = _eventGetColor.R;
                                    _pixel[1] = _eventGetColor.G;
                                    _pixel[0] = _eventGetColor.B;
                                }
                            }
                        }
                    }

                    if (_otherPixel)
                    {
                        _sobelPixelColorR = _allPixR[x, y];
                        _sobelPixelColorG = _allPixG[x, y];
                        _sobelPixelColorB = _allPixB[x, y];

                        var _eventGetColor = otherPixelDetectEvent(x, y, new ODColorMA(_sobelPixelColorR, _sobelPixelColorG, _sobelPixelColorB));
                        if (_eventGetColor != null)
                        {
                            _pixel[2] = _eventGetColor.R;
                            _pixel[1] = _eventGetColor.G;
                            _pixel[0] = _eventGetColor.B;
                        }
                    }

                    _pixel += _pixelPerByte;
                }
            }

            bmp.UnlockBits(_bmpData);

            return _faceRects;
        }
    }
}
