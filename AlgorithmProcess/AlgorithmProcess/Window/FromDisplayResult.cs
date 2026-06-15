using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgorithmProcess.Window
{
    public partial class FromDisplayResult : UserControl
    {

        // =============================== 事件與委託 ===============================

        public delegate void CallBackReturnLog(string type, string msg);
        public event CallBackReturnLog CallBackLog;

        // =============================== 事件與委託 ===============================

        // =============================== function建立 ===============================

        private readonly ImageConverter _ImageConverter = new ImageConverter();

        private ReadROIINI RRI = null;

        private ReadAlgorithmParameterINI RAPI = null;

        private HalconImageConverter HC = null;

        private MappingModule MM = null;

        // =============================== function建立 ===============================

        // =============================== 全域變數宣告 ===============================

        private SettingInfo settingInfo = null;

        private ROIList rOIList = null;

        private List<string> CameraLocation = new List<string>() { "Left", "Middle", "Right" };

        private int CameraMode = 0;

        private static readonly string[] ClassNames = { "Absence", "Presence", "Slant", "Stack" };

        // =============================== 全域變數宣告 ===============================

        public FromDisplayResult()
        {
            InitializeComponent();
            InitializeImageLoad(settingInfo); // 初始化圖片載入
        }

        public void InitializeImageLoad(SettingInfo settingInfo)
        {
            this.settingInfo = settingInfo;
        }

        private void runalgorithm_btn_Click(object sender, EventArgs e)
        {
            RRI = new ReadROIINI();

            string[] files = Directory.GetFiles(settingInfo.DirImagePath, $"*Middle*", SearchOption.TopDirectoryOnly);

            if (files.Length > 0)
            {
                CameraMode = 3;

                rOIList = RRI.ReadRecipeINI(settingInfo.RecipePath + "\\ROI.ini", 3);

            }
            else
            {
                CameraMode = 2;

                rOIList = RRI.ReadRecipeINI(settingInfo.RecipePath + "\\ROI.ini", 2);

                OnMappingTwoCamera();
            }
        }

        private void OnMappingTwoCamera()
        {
            string[] FilePath = Directory.GetFiles(this.settingInfo.DirImagePath, "*.bmp");

            CallBackLog?.Invoke("INFO", $"開始處理影像，總共找到 {FilePath.Length} 張圖片。");

            for (int i = settingInfo.SlotNumber; i > 0; i--)
            {

                string LImagePath = settingInfo.DirImagePath + "\\Slot" + i.ToString("00") + "_Left.bmp";

                string RImagePath = settingInfo.DirImagePath + "\\Slot" + i.ToString("00") + "_Right.bmp";

                //Bitmap Lbmp = (Bitmap)_ImageConverter.ConvertFrom(File.ReadAllBytes(LImagePath));

                //Bitmap Rbmp = (Bitmap)_ImageConverter.ConvertFrom(File.ReadAllBytes(RImagePath));

                string onnxModelPath = "C:\\Users\\elvis\\Desktop\\me\\Company\\VisionMapping\\VisionMappingDemo\\AIModel\\image_classifier.onnx";

                // 修正：改回舊版 C# 7.3 支援的傳統 using 區塊結構
                using (Mat imgLeft = Cv2.ImRead(LImagePath))
                using (Mat imgRight = Cv2.ImRead(RImagePath))
                {
                    if (imgLeft.Empty() || imgRight.Empty())
                    {
                        CallBackLog?.Invoke("ERROR", $"影像讀取失敗！請確認路徑: {LImagePath} 和 {RImagePath}");
                        return;
                    }

                    // 高度對齊
                    if (imgLeft.Rows != imgRight.Rows)
                    {
                        double scale = (double)imgLeft.Rows / imgRight.Rows;
                        int newWidth = (int)(imgRight.Cols * scale);
                        // 修正：明確指定 OpenCvSharp.Size 解決模稜兩可的衝突
                        Cv2.Resize(imgRight, imgRight, new OpenCvSharp.Size(newWidth, imgLeft.Rows));
                    }

                    using (Mat combined = new Mat())
                    {
                        Cv2.HConcat(new Mat[] { imgLeft, imgRight }, combined);

                        // 2. 執行影像前處理
                        var result = BlurGy(combined);

                        // 3. 縮放到 256x256
                        float[,] blurResized = ResizeArray(result.blur, result.h, result.w, 256, 256);
                        float[,] gyResized = ResizeArray(result.gy, result.h, result.w, 256, 256);

                        // 4. 打包成 Tensor
                        int[] dimensions = new int[] { 1, 2, 256, 256 };
                        DenseTensor<float> inputTensor = new DenseTensor<float>(dimensions);

                        for (int y = 0; y < 256; y++)
                        {
                            for (int x = 0; x < 256; x++)
                            {
                                inputTensor[0, 0, y, x] = blurResized[y, x]; // Channel 0: Blur
                                inputTensor[0, 1, y, x] = gyResized[y, x];   // Channel 1: Gy
                            }
                        }

                        // 強制指定使用 CPU 推理，避免環境核心相容性卡死
                        SessionOptions options = new SessionOptions();
                        options.AppendExecutionProvider_CPU(0);

                        // 5. 啟動 ONNX 推理
                        using (InferenceSession session = new InferenceSession(onnxModelPath))
                        {
                            List<NamedOnnxValue> inputs = new List<NamedOnnxValue>
                            {
                                NamedOnnxValue.CreateFromTensor("input", inputTensor)
                             };

                            using (IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = session.Run(inputs))
                            {
                                Tensor<float> outputTensor = results.First().AsTensor<float>();
                                List<float> outputList = outputTensor.ToList();
                                // 確保真的有輸出數值，防止空參照
                                if (outputList != null && outputList.Count > 0)
                                {
                                    int maxIndex = outputList.IndexOf(outputList.Max());

                                    // 🔥 修正點 1：將 Callback 移入大括號內。此時 maxIndex 與 outputList 還活著！
                                    CallBackLog?.Invoke("INFO", $"C# 模型預測結果: {ClassNames[maxIndex]} (原始分數: {outputList[maxIndex]:F4})");

                                    /* 💡 備註：因為 ONNX 模型輸出的通常是未經 Softmax 的 Raw Logits（原始分數），
                                       如果是負數或大於 1 的數，使用 :P2 (百分比) 顯示會變得很奇怪（例如 -500.00%）。
                                       建議先用 :F4 (浮點數四位) 觀察，若需要百分比，稍後我們要在 C# 補一個 Softmax 函數。 */
                                }
                                else
                                {
                                    CallBackLog?.Invoke("ERROR", "模型推理成功，但輸出的 Tensor 內容為空。");
                                }
                            }
                        }
                    }
                }
            }

        }

        // ====== BlurGy 前處理 ======
        public static (float[,] blur, float[,] gy, double grayMean, double grayStd, int h, int w) BlurGy(Mat bgr)
        {
            using (Mat gray = new Mat())
            using (Mat grayF = new Mat())
            using (Mat blur = new Mat())
            using (Mat gy = new Mat())
            {
                Cv2.CvtColor(bgr, gray, ColorConversionCodes.BGR2GRAY);
                gray.ConvertTo(grayF, MatType.CV_32F);
                Cv2.MeanStdDev(grayF, out Scalar mean, out Scalar sd);

                // 修正：明確指定 OpenCvSharp.Size 解決模稜兩可的衝突
                Cv2.GaussianBlur(grayF, blur, new OpenCvSharp.Size(3, 3), 0);
                Cv2.Sobel(blur, gy, MatType.CV_32F, 0, 1, 3);

                int h = bgr.Rows, w = bgr.Cols;
                return (ToArray(blur, h, w), ToArray(gy, h, w), mean.Val0, sd.Val0, h, w);
            }
        }

        // 將 Mat 轉換為二維陣列
        private static float[,] ToArray(Mat mat, int h, int w)
        {
            float[,] arr = new float[h, w];
            float[] totalArr = new float[h * w];
            Marshal.Copy(mat.Data, totalArr, 0, totalArr.Length);

            // 將一維陣列排入二維陣列
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    arr[i, j] = totalArr[i * w + j];
                }
            }
            return arr;
        }

        // 修正：重寫二維陣列 Resize 邏輯，避免使用不合法的 Mat 建構子
        private static float[,] ResizeArray(float[,] source, int originalH, int originalW, int targetH, int targetW)
        {
            // 先將二維陣列拍平成一維
            float[] flatten = new float[originalH * originalW];
            for (int i = 0; i < originalH; i++)
            {
                for (int j = 0; j < originalW; j++)
                {
                    flatten[i * originalW + j] = source[i, j];
                }
            }

            // 修正核心：改用 FromPixelData 安全載入資料，不觸碰內部保護層級建構子
            using (Mat mat = Mat.FromPixelData(originalH, originalW, MatType.CV_32F, flatten))
            using (Mat resizedMat = new Mat())
            {
                Cv2.Resize(mat, resizedMat, new OpenCvSharp.Size(targetW, targetH), 0, 0, InterpolationFlags.Linear);
                return ToArray(resizedMat, targetH, targetW);
            }
        }
    }
}
