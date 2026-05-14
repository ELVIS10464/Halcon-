using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using HalconDotNet;

namespace AlgorithmProcess
{
    public class HalconImageConverter
    {
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);


        public Bitmap HimageToBitmap(HImage himg, bool isGray = false)
        {
            //轉換成灰階影像
            HTuple channelTup = himg.CountChannels();
            HImage grayImg;

            if (0 != channelTup.Length && 1 < channelTup.I && !isGray)
            {
                return ConvertRGBBitmap(himg);
            }
            else
            {
                grayImg = himg.Rgb1ToGray();

                return ConvertGrayBitmap(grayImg);
            }
        }

        public Bitmap ConvertRGBBitmap(HImage himg)
        {

            HTuple typeTup, wTup, hTup;

            HTuple rPtr, gPtr, bPtr;

            HOperatorSet.GetImagePointer3(himg as HObject, out rPtr, out gPtr, out bPtr, out typeTup, out wTup, out hTup);

            Bitmap resBmp = new Bitmap(wTup.I, hTup.I, PixelFormat.Format24bppRgb);

            BitmapData resBmpData = resBmp.LockBits(new Rectangle(0, 0, wTup.I, hTup.I), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            IntPtr bmpImgPtr = resBmpData.Scan0;

            int PixelSize = Bitmap.GetPixelFormatSize(resBmpData.PixelFormat) / 8;

            unsafe
            {
                byte* bptr = (byte*)bmpImgPtr;
                byte* r = ((byte*)rPtr.L);
                byte* g = ((byte*)gPtr.L);
                byte* b = ((byte*)bPtr.L);

                for (int i = 0; i < hTup.I; ++i)
                {
                    for (int j = 0; j < wTup.I; ++j)
                    {
                        byte* data = bptr + i * resBmpData.Stride + j * PixelSize;

                        *data = *b;
                        *(data + 1) = *g;
                        *(data + 2) = *r;

                        r++;
                        g++;
                        b++;
                    }
                }
            }

            //HOperatorSet.WriteImage(hImage, "bmp", 0, "D:\\HalconTemp.bmp");
            resBmp.UnlockBits(resBmpData);

            return resBmp;
        }

        public Bitmap ConvertGrayBitmap(HImage himg)
        {
            HTuple pointer, type, width, height;

            try
            {
                //轉換成灰階影像
                HTuple channelTup = himg.CountChannels();
                HImage grayImg;

                if (1 < channelTup.I)
                    grayImg = himg.Rgb1ToGray();
                else
                    grayImg = himg;

                pointer = grayImg.GetImagePointer1(out type, out width, out height);

                Bitmap img = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
                ColorPalette pal = img.Palette;

                for (int i = 0; i <= 255; i++)
                {
                    pal.Entries[i] = Color.FromArgb(255, i, i, i);
                }

                img.Palette = pal;
                Rectangle rect = new Rectangle(0, 0, width, height);
                BitmapData bitmapData = img.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                int PixelSize = Bitmap.GetPixelFormatSize(bitmapData.PixelFormat) / 8;

                if (width % 4 == 0)
                {
                    CopyMemory(bitmapData.Scan0, new IntPtr(pointer.L), (uint)(width * height * PixelSize));
                    //CopyMemory(ptr[0], ptr[1], width * height * PixelSize);
                }
                else
                {
                    for (int i = 0; i < height - 1; i++)
                    {
                        pointer.L += width;
                        //CopyMemory(ptr[0], ptr[1], width * PixelSize);
                        CopyMemory(bitmapData.Scan0, new IntPtr(pointer.L), (uint)(width * PixelSize));
                        bitmapData.Scan0 += bitmapData.Stride;
                    }
                }
                img.UnlockBits(bitmapData);

                //img.Save("D:\\BitmapTemp.bmp");

                grayImg.Dispose();

                return img;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public bool Bitmap2HImage(Bitmap bImage, out HImage hImage)
        {
            BitmapData bmData = null;
            Rectangle rect;
            IntPtr pBitmapPtr;

            int bmpW = bImage.Width;
            int bmpH = bImage.Height;
            hImage = new HImage("byte", bmpW, bmpH);

            try
            {

                rect = new Rectangle(0, 0, bmpW, bmpH);

                bmData = bImage.LockBits(rect, ImageLockMode.ReadOnly, bImage.PixelFormat);
                pBitmapPtr = bmData.Scan0;
                int PixelSize = Bitmap.GetPixelFormatSize(bmData.PixelFormat) / 8;

                if (PixelFormat.Format8bppIndexed == bImage.PixelFormat)
                {
                    HTuple typeTup, wTup, hTup;

                    HTuple ptrTup = hImage.GetImagePointer1(out typeTup, out wTup, out hTup);

                    if (0 != bmpW % 4)
                    {
                        for (int i = 0; i < bmpH - 1; i++)
                        {
                            ptrTup.L += bmpW;
                            CopyMemory(new IntPtr(ptrTup.L), bmData.Scan0, (uint)(bmpW * PixelSize));
                            bmData.Scan0 += bmData.Stride;
                        }
                    }
                    else
                    {
                        CopyMemory(new IntPtr(ptrTup.L), bmData.Scan0, (uint)(bmpW * bmpH));
                    }

                    //HOperatorSet.WriteImage(hImage, "bmp", 0, "D:\\HalconTemp.bmp");
                    bImage.UnlockBits(bmData);
                }
                else
                {
                    if (0 != bmpW % 4)
                    {
                        HTuple typeTup, wTup, hTup;

                        HTuple ptrTup = hImage.GetImagePointer1(out typeTup, out wTup, out hTup);

                        unsafe
                        {
                            byte* bptr = (byte*)pBitmapPtr;
                            byte* ptr = ((byte*)ptrTup.L);

                            for (int i = 0; i < bmData.Height; ++i)
                            {
                                for (int j = 0; j < bmData.Width; ++j)
                                {
                                    byte* data = bptr + i * bmData.Stride + j * PixelSize;

                                    byte b = *data;
                                    byte g = *(data + 1);
                                    byte r = *(data + 2);
                                    byte gray = (byte)(0.299F * (float)r + 0.587F * (float)g + 0.114F * (float)b);

                                    *ptr = gray;

                                    ptr++;
                                }
                            }
                        }
                    }
                    else
                    {
                        HImage colorImg = new HImage();
                        colorImg.GenImageInterleaved(pBitmapPtr, "bgr", bmpW, bmpH, 0, "byte", bmpW, bmpH, 0, 0, -1, 0);

                        hImage = colorImg;
                    }

                    //HOperatorSet.WriteImage(hImage, "bmp", 0, "D:\\HalconTemp.bmp");
                    bImage.UnlockBits(bmData);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ConvertHImage(Bitmap bImage, out HImage hImage)
        {
            hImage = new HImage();

            try
            {
                Rectangle rect = new Rectangle(0, 0, bImage.Width, bImage.Height);

                if (bImage.PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    BitmapData srcBmpData = bImage.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);

                    hImage.GenImage1("byte", bImage.Width, bImage.Height, srcBmpData.Scan0);

                    bImage.UnlockBits(srcBmpData);
                }
                else if (bImage.PixelFormat == PixelFormat.Format24bppRgb)
                {
                    BitmapData srcBmpData = bImage.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

                    hImage.GenImageInterleaved(srcBmpData.Scan0, "bgr", bImage.Width, bImage.Height, -1, "byte", 0, 0, 0, 0, -1, 0);

                    bImage.UnlockBits(srcBmpData);
                }
                else if (bImage.PixelFormat == PixelFormat.Format32bppArgb)
                {
                    BitmapData srcBmpData = bImage.LockBits(rect, ImageLockMode.ReadOnly, bImage.PixelFormat);

                    hImage.GenImageInterleaved(srcBmpData.Scan0, "bgrx", bImage.Width, bImage.Height, -1, "byte", bImage.Width, bImage.Height, 0, 0, -1, 0);

                    bImage.UnlockBits(srcBmpData);
                }

                else // default: trans to color image
                {
                    BitmapData srcBmpData = null;

                    Bitmap MetaBmp = new Bitmap(bImage.Width, bImage.Height, PixelFormat.Format24bppRgb);

                    Graphics g = Graphics.FromImage(MetaBmp);

                    g.DrawImage(bImage, rect);

                    g.Dispose();

                    srcBmpData = MetaBmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

                    hImage.GenImageInterleaved(srcBmpData.Scan0, "bgr", bImage.Width, bImage.Height, -1, "byte", 0, 0, 0, 0, -1, 0);

                    MetaBmp.UnlockBits(srcBmpData);

                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
