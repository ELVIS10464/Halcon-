using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgorithmProcess
{
    public class MappingProcess
    {
        private readonly ImageConverter _ImageConverter = new ImageConverter();
        private HalconImageConverter HC = null;
        private AlgorithmModule AM = null;
        public void OnMappingForTwoCamera(string Imagepath, ROIList ROI)
        {
            HC = new HalconImageConverter();
            AM = new AlgorithmModule();

            for (int i = 20; i > 0; i--)
            {
                string LImagePath = Imagepath + "\\Image\\Slot" + i.ToString("00") + "_Left.bmp";
                string RImagePath = Imagepath + "\\Image\\Slot" + i.ToString("00") + "_Right.bmp";

                Bitmap Lbmp = (Bitmap)_ImageConverter.ConvertFrom(File.ReadAllBytes(LImagePath));
                Bitmap Rbmp = (Bitmap)_ImageConverter.ConvertFrom(File.ReadAllBytes(RImagePath));

                Task.Run(() =>
                {
                    HImage _hImageLeft = new HImage(), _hImageRight = new HImage();

                    HC.Bitmap2HImage((Bitmap)Lbmp.Clone(), out _hImageLeft);
                    HC.Bitmap2HImage((Bitmap)Rbmp.Clone(), out _hImageRight);

                    AlgorithmModule AM_Clone = AM.Clone();


                });
            }
        }
        public void OnMappingForThreeCamera(string Imagepath, ROIList ROI)
        {

        }
    }
}
