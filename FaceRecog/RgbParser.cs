using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace FaceRecog
{
    public delegate void PixelAnalyzed(int x, int y, byte yd, byte cr, byte cb);

    class SkinScanner
    {
        public void Analyse(Bitmap bmp, PixelAnalyzed callback)
        {
            var width = bmp.Width;
            var height = bmp.Height;
            //var yData = new byte[width, height];
            //var bData = new byte[width, height];
            //var rData = new byte[width, height];

            unsafe
            {
                BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                int heightInPixels = bitmapData.Height;
                int widthInBytes = width * 3;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

                //Convert to YCbCr
                for (int y = 0; y < heightInPixels; y++)
                {
                    byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < width; x++)
                    {
                        int xPor3 = x * 3;
                        float b = currentLine[xPor3++];
                        float g = currentLine[xPor3++];
                        float r = currentLine[xPor3];

                        byte yd;
                        byte cb;
                        byte cr;
                        yd = (byte)(0.299 * r + 0.587 * g + 0.114 * b);
                        cb = (byte)(128 + (int)(-0.16874 * r - 0.33126 * g + 0.5 * b));
                        cr = (byte)(128 + (int)(0.5 * r - 0.418688 * g - 0.081312 * b));

                        //yData[x, y] = yd;
                        //bData[x, y] = cb;
                        //rData[x, y] = cr;

                        callback(x, y, yd, cr, cb);
                    }
                }
                bmp.UnlockBits(bitmapData);
            }
        }

        public Tuple<byte, byte> AnalysePixel(Bitmap bmp, int x, int y)
        {
            var width = bmp.Width;
            var height = bmp.Height;

            unsafe
            {
                BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                int heightInPixels = bitmapData.Height;
                int widthInBytes = width * 3;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

                //Convert to YCbCr
                byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                int xPor3 = x * 3;
                float b = currentLine[xPor3++];
                float g = currentLine[xPor3++];
                float r = currentLine[xPor3];

                byte yd;
                byte cb;
                byte cr;
                yd = (byte)(0.299 * r + 0.587 * g + 0.114 * b);
                cb = (byte)(128 + (int)(-0.16874 * r - 0.33126 * g + 0.5 * b));
                cr = (byte)(128 + (int)(0.5 * r - 0.418688 * g - 0.081312 * b));

                bmp.UnlockBits(bitmapData);
                return new Tuple<byte, byte>(cr, cb);
            }
        }
    }
}