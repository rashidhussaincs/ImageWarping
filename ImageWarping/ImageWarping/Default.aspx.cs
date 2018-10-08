using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ImageWarping
{
    public class ProductData
    {
        public int ProductID { get; set; }

        public string MannequinType { get; set; }

        public string ProductType { get; set; }

        public string ProductSleevesType { get; set; }

        public string ProductSection { get; set; }

        public double[,] ProductCoordinates { get; set; }
    }

    public class MemberData
    {
        public int MemberID { get; set; }

        public double[,] MemberCoordinates { get; set; }

        public List<ProductData> ProductCoordinatesData { get; set; }
    }

    public partial class _Default : Page
    {
        List<MemberData> members = new List<MemberData>();

        [ThreadStatic]
        double[,] mannequinCoordinates = new double[,] { };

        double[,] mannequinLaurieCoordinates = new double[70, 2] { { 1780, 540 }, { 1950, 696 }, { 1980, 763 }, { 1991, 818 }, { 1950, 852 }, { 1913, 956 }, { 1921, 1015 }, { 2153, 1145 }, { 2333, 1520 }, { 2392, 1858 }, { 2418, 2474 }, { 2340, 2839 }, { 2327, 2668 }, { 2336, 2462 }, { 2218, 1950 }, { 2166, 1520 }, { 2151, 1423 }, { 2099, 1472 }, { 2073, 1883 }, { 2110, 2026 }, { 2154, 2164 }, { 2177, 2314 }, { 2158, 2758 }, { 2095, 3194 }, { 2086, 3374 }, { 2104, 3711 }, { 2058, 4227 }, { 2151, 4641 }, { 2101, 4700 }, { 1980, 4641 }, { 1952, 4374 }, { 1945, 4227 }, { 1870, 3711 }, { 1854, 3375 }, { 1828, 2758 }, { 1804, 2600 }, { 1787, 2758 }, { 1748, 3375 }, { 1752, 3711 }, { 1705, 4227 }, { 1702, 4361 }, { 1676, 4639 }, { 1568, 4702 }, { 1518, 4639 }, { 1590, 4227 }, { 1520, 3711 }, { 1531, 3375 }, { 1523, 3194 }, { 1453, 2758 }, { 1436, 2314 }, { 1455, 2164 }, { 1492, 2026 }, { 1525, 1883 }, { 1477, 1472 }, { 1446, 1423 }, { 1420, 1523 }, { 1386, 1973 }, { 1271, 2500 }, { 1267, 2707 }, { 1264, 2826 }, { 1189, 2500 }, { 1208, 1909 }, { 1258, 1522 }, { 1429, 1150 }, { 1676, 1015 }, { 1676, 956 }, { 1631, 852 }, { 1594, 822 }, { 1624, 763 }, { 1627, 696 } };
        //double[,] mannequinLaurieCoordinates = new double[70, 2] { { 1715, 204 }, { 1881, 354 }, { 1911, 418 }, { 1921, 470 }, { 1881, 502 }, { 1845, 602 }, { 1852, 656 }, { 2078, 782 }, { 2250, 1142 }, { 2310, 1466 }, { 2333, 2056 }, { 2259, 2407 }, { 2246, 2242 }, { 2255, 2045 }, { 2141, 1555 }, { 2088, 1142 }, { 2076, 1049 }, { 2033, 1094 }, { 2000, 1490 }, { 2034, 1627 }, { 2079, 1760 }, { 2100, 1904 }, { 2084, 2327 }, { 2021, 2746 }, { 2013, 2918 }, { 2030, 3242 }, { 1983, 3736 }, { 2075, 4133 }, { 2027, 4190 }, { 1910, 4132 }, { 1883, 3878 }, { 1877, 3736 }, { 1802, 3242 }, { 1788, 2920 }, { 1761, 2329 }, { 1740, 2179 }, { 1725, 2329 }, { 1688, 2920 }, { 1688, 3242 }, { 1646, 3737 }, { 1640, 3865 }, { 1616, 4133 }, { 1512, 4192 }, { 1463, 4130 }, { 1533, 3736 }, { 1464, 3242 }, { 1475, 2920 }, { 1467, 2746 }, { 1403, 2329 }, { 1382, 1905 }, { 1400, 1758 }, { 1436, 1628 }, { 1472, 1490 }, { 1424, 1097 }, { 1392, 1049 }, { 1368, 1145 }, { 1335, 1575 }, { 1224, 2080 }, { 1221, 2281 }, { 1216, 2395 }, { 1146, 2080 }, { 1164, 1514 }, { 1212, 1144 }, { 1375, 788 }, { 1617, 659 }, { 1616, 602 }, { 1572, 502 }, { 1537, 474 }, { 1566, 417 }, { 1569, 353 } };

        double[,] mannequinDollyCoordinates = new double[70, 2] { { 1788, 438 }, { 1946, 615 }, { 1938, 687 }, { 1969, 758 }, { 1940, 848 }, { 1890, 917 }, { 1890, 1082 }, { 2146, 1190 }, { 2305, 1700 }, { 2327, 1851 }, { 2355, 2491 }, { 2316, 2823 }, { 2292, 2654 }, { 2259, 2491 }, { 2168, 1871 }, { 2147, 1726 }, { 2138, 1439 }, { 2120, 1563 }, { 2045, 1925 }, { 2105, 2079 }, { 2133, 2153 }, { 2151, 2244 }, { 2160, 2611 }, { 2086, 3085 }, { 2066, 3224 }, { 2071, 3539 }, { 1984, 4152 }, { 2025, 4476 }, { 1917, 4595 }, { 1839, 4476 }, { 1851, 4308 }, { 1852, 4152 }, { 1852, 3539 }, { 1852, 3222 }, { 1812, 2611 }, { 1782, 2495 }, { 1747, 2610 }, { 1724, 3222 }, { 1734, 3539 }, { 1721, 4154 }, { 1728, 4308 }, { 1750, 4476 }, { 1708, 4597 }, { 1563, 4478 }, { 1593, 4154 }, { 1520, 3539 }, { 1511, 3222 }, { 1478, 3085 }, { 1403, 2610 }, { 1418, 2246 }, { 1437, 2153 }, { 1465, 2079 }, { 1515, 1925 }, { 1444, 1563 }, { 1427, 1440 }, { 1413, 1724 }, { 1392, 1864 }, { 1314, 2485 }, { 1296, 2671 }, { 1268, 2821 }, { 1214, 2489 }, { 1238, 1839 }, { 1262, 1698 }, { 1413, 1190 }, { 1665, 1082 }, { 1661, 917 }, { 1647, 878 }, { 1611, 769 }, { 1629, 697 }, { 1629, 615 } };

        float srcX1, srcX2, srcX3, srcX4, srcY1, srcY2, srcY3, srcY4, XAdditional, YAdditional, dstX1, dstX2, dstX3, dstX4, dstY1, dstY2, dstY3, dstY4 = 0;

        int minSrcX, maxSrcX, maxDstX, minSrcY, maxSrcY, maxDstY, dstWidth, dstHeight = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            DataConfiguration();
            //DataConfiguration13828();

            //RunFor13724();


            //RunForMembers();
            //RunForMembersCopy();
            //RunForMembersComplete();

            //Image<Bgra, byte> mainSrcImg = new Image<Bgra, byte>("c:\\projects\\569_slice_leftshoulder.png");
            //FindContours(mainSrcImg.Convert<Bgr, byte>(), "569_slice_leftshoulder");
            generateMeshNew();
        }

        void RunFor13724()
        {
            //////--------------LEFT SHOULDER to FOREARM WARPING--------------------///////////////
            Image<Bgra, byte> img_s = new Image<Bgra, byte>("c:\\projects\\712_slice_leftshoulder.png");

            PointF[] src = new PointF[] {
                new PointF { X = 0, Y = 0 },
                new PointF { X = 1368, Y = 786 },
                new PointF { X = 1204, Y = 1140 },
                new PointF { X = 1368, Y = 1140 }
                };

            PointF[] dst = new PointF[] {
                new PointF { X = 0, Y = 0 },
                new PointF { X = 1368, Y = 786 },
                new PointF { X = 1179, Y = 1169 },
                new PointF { X = 1357, Y = 1213 }
                };

            Mat img_d = new Mat();
            Mat warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
            CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(img_s.Width, Convert.ToInt32(dst.Max(a => a.Y))), Inter.Linear, Warp.Default, BorderType.Transparent);

            img_d.Bitmap.Save("c:\\projects\\712_slice_leftshoulder_warpPerspective.png");

            img_s.Dispose();

            //////--------------FOREARM TO ARM WARPING--------------------///////////////
            img_s = new Image<Bgra, byte>("c:\\projects\\712_slice_leftarmToForeArm.png");

            src = new PointF[] {
                new PointF { X = 1205, Y = 0 },
                new PointF { X = 1372, Y = 0 },
                new PointF { X = 1157, Y = 441 },
                new PointF { X = 1340, Y = 441 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 57, Y = img_s.Height },
                //new PointF { X = 296, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                // note even with a 4th term in here and dst, same error arises....
                };

            dst = new PointF[] {
                new PointF { X = 1199, Y = 0 },
                new PointF { X = 1199 + dst[3].X - dst[2].X, Y = 45 }, //1372
                new PointF { X = 1148, Y = 283 },
                new PointF { X = 1305, Y = 353 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 67, Y = img_s.Height },
                //new PointF { X = 286, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                };

            img_d = new Mat();
            warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
            CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(img_s.Width, Convert.ToInt32(dst.Max(a => a.Y))), Inter.Linear, Warp.Default, BorderType.Transparent);

            img_d.Bitmap.Save("c:\\projects\\712_slice_leftarmToForeArm_warpPerspective.png");

            img_s.Dispose();

            //////--------------ARM TO WRIST WARPING--------------------///////////////
            img_s = new Image<Bgra, byte>("c:\\projects\\712_slice_leftForeArmToWrist.png");

            src = new PointF[] {
                new PointF { X = 1155, Y = 0 },
                new PointF { X = 1338, Y = 0 },
                new PointF { X = 1139, Y = 532 },
                new PointF { X = 1247, Y = 552 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 57, Y = img_s.Height },
                //new PointF { X = 296, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                // note even with a 4th term in here and dst, same error arises....
                };

            dst = new PointF[] {
                new PointF { X = 1155, Y = 0 },
                new PointF { X = 1155 + dst[3].X - dst[2].X, Y = 69 }, //1313
                new PointF { X = 1073, Y = 550 },
                new PointF { X = 1159, Y = 563 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 67, Y = img_s.Height },
                //new PointF { X = 286, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                };

            img_d = new Mat();
            warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
            CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(img_s.Width, Convert.ToInt32(dst.Max(a => a.Y))), Inter.Linear, Warp.Default, BorderType.Transparent);

            img_d.Bitmap.Save("c:\\projects\\712_slice_leftForeArmToWrist_warpPerspective.png");

            img_s.Dispose();

            //////--------------RIGHT SHOULDER to FOREARM WARPING--------------------///////////////
            img_s = new Image<Bgra, byte>("c:\\projects\\712_slice_rightshoulder.png");

            src = new PointF[] {
                new PointF { X = 0, Y = 780 },
                new PointF { X = 183, Y = 780 },
                new PointF { X = 0, Y = 1147 },
                new PointF { X = 185, Y = 1147 }
                };

            dst = new PointF[] {
                new PointF { X = 0, Y = 780 },
                new PointF { X = 190, Y = 780 },
                new PointF { X = 0, Y = 1188 },
                new PointF { X = 178, Y = 1124 }
                };

            img_d = new Mat();
            warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
            CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(img_s.Width, Convert.ToInt32(dst.Max(a => a.Y))), Inter.Linear, Warp.Default, BorderType.Transparent);

            img_d.Bitmap.Save("c:\\projects\\712_slice_rightshoulder_warpPerspective.png");

            img_s.Dispose();

            //////--------------FOREARM TO ARM WARPING--------------------///////////////
            img_s = new Image<Bgra, byte>("c:\\projects\\712_slice_rightarmToForeArm.png");

            src = new PointF[] {
                new PointF { X = 0, Y = 0 },
                new PointF { X = 185, Y = 0 },
                new PointF { X = 41, Y = 411 },
                new PointF { X = 241, Y = 411 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 57, Y = img_s.Height },
                //new PointF { X = 296, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                // note even with a 4th term in here and dst, same error arises....
                };

            dst = new PointF[] {
                new PointF { X = 0, Y = 63 },
                new PointF { X = 0 + dst[3].X - dst[2].X, Y = 0 }, //189
                new PointF { X = 28, Y = 405 },
                new PointF { X = 210, Y = 330 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 67, Y = img_s.Height },
                //new PointF { X = 286, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                };

            img_d = new Mat();
            warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
            CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(img_s.Width, Convert.ToInt32(dst.Max(a => a.Y))), Inter.Linear, Warp.Default, BorderType.Transparent);

            img_d.Bitmap.Save("c:\\projects\\712_slice_rightarmToForeArm_warpPerspective.png");

            img_s.Dispose();

            //////--------------ARM TO WRIST WARPING--------------------///////////////
            img_s = new Image<Bgra, byte>("c:\\projects\\712_slice_rightForeArmToWrist.png");

            src = new PointF[] {
                new PointF { X = 4, Y = 0 },
                new PointF { X = 202, Y = 0 },
                new PointF { X = 98, Y = 543 },
                new PointF { X = 210, Y = 564 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 57, Y = img_s.Height },
                //new PointF { X = 296, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                // note even with a 4th term in here and dst, same error arises....
                };

            dst = new PointF[] {
                new PointF { X = 18, Y = 72 },
                new PointF { X = 18 + dst[3].X - dst[2].X, Y = 0 }, //199
                new PointF { X = 127, Y = 568 },
                new PointF { X = 228, Y = 566 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 67, Y = img_s.Height },
                //new PointF { X = 286, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                };

            img_d = new Mat();
            warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
            CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(img_s.Width, Convert.ToInt32(dst.Max(a => a.Y))), Inter.Linear, Warp.Default, BorderType.Transparent);

            img_d.Bitmap.Save("c:\\projects\\712_slice_rightForeArmToWrist_warpPerspective.png");

            img_s.Dispose();


            //////--------------SHOLDERS TO CHEST WARPING--------------------///////////////
            img_s = new Image<Bgra, byte>("c:\\projects\\712_slice_SholuderToChest.png");

            src = new PointF[] {
                new PointF { X = 0, Y = 790 },
                new PointF { X = 709, Y = 782 },
                new PointF { X = 0, Y = 1093 },
                new PointF { X = 709, Y = 1093 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 57, Y = img_s.Height },
                //new PointF { X = 296, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                // note even with a 4th term in here and dst, same error arises....
                };

            dst = new PointF[] {
                new PointF { X = 32, Y = 790 },
                new PointF { X = 657, Y = 804 },
                new PointF { X = 25, Y = 1093 },
                new PointF { X = 655, Y = 1093 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 67, Y = img_s.Height },
                //new PointF { X = 286, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                };

            img_d = new Mat();
            warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
            CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(Convert.ToInt32(dst.Max(a => a.X)), Convert.ToInt32(dst.Max(a => a.Y))), Inter.Linear, Warp.Default, BorderType.Transparent);

            img_d.Bitmap.Save("c:\\projects\\712_slice_SholuderToChest_warpPerspective.png");

            img_s.Dispose();

            //////--------------CHEST TO WAIST WARPING--------------------///////////////
            img_s = new Image<Bgra, byte>("c:\\projects\\712_slice_ChestToWaist.png");

            src = new PointF[] {
                new PointF { X = 0, Y = 0 },
                new PointF { X = 702, Y = 0 },
                new PointF { X = 40, Y = 396 },
                new PointF { X = 688, Y = 396 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 57, Y = img_s.Height },
                //new PointF { X = 296, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                // note even with a 4th term in here and dst, same error arises....
                };

            dst = new PointF[] {
                new PointF { X = 28, Y = 0 },
                new PointF { X = 28 + dst[3].X - dst[2].X, Y = 0 }, //660
                new PointF { X = 59, Y = 386 },
                new PointF { X = 644, Y = 386 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 67, Y = img_s.Height },
                //new PointF { X = 286, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                };

            img_d = new Mat();
            warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
            CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(Convert.ToInt32(dst.Max(a => a.X)), Convert.ToInt32(dst.Max(a => a.Y))), Inter.Linear, Warp.Default, BorderType.Transparent);

            img_d.Bitmap.Save("c:\\projects\\712_slice_ChestToWaist_warpPerspective.png");

            img_s.Dispose();

            //////--------------WAIST TO ABDOMEN WARPING--------------------///////////////
            img_s = new Image<Bgra, byte>("c:\\projects\\712_slice_WaistToAbdomen.png");

            src = new PointF[] {
                new PointF { X = 39, Y = 0 },
                new PointF { X = 690, Y = 0 },
                new PointF { X = 33, Y = 137 },
                new PointF { X = 702, Y = 137 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 57, Y = img_s.Height },
                //new PointF { X = 296, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                // note even with a 4th term in here and dst, same error arises....
                };

            dst = new PointF[] {
                new PointF { X = 80, Y = 0 },
                new PointF { X = 80 + dst[3].X - dst[2].X, Y = 0 }, //660
                new PointF { X = 66, Y = 106 },
                new PointF { X = 683, Y = 108 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 67, Y = img_s.Height },
                //new PointF { X = 286, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                };

            img_d = new Mat();
            warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
            CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(Convert.ToInt32(dst.Max(a => a.X)), Convert.ToInt32(dst.Max(a => a.Y))), Inter.Linear, Warp.Default, BorderType.Transparent);

            img_d.Bitmap.Save("c:\\projects\\712_slice_WaistToAbdomen_warpPerspective.png");

            img_s.Dispose();

            //////--------------ABDOMEN TO HIGH HIP WARPING--------------------///////////////
            img_s = new Image<Bgra, byte>("c:\\projects\\712_slice_AbdomenToHighHip.png");

            src = new PointF[] {
                new PointF { X = 30, Y = 0 },
                new PointF { X = 699, Y = 0 },
                new PointF { X = 14, Y = 133 },
                new PointF { X = 714, Y = 133 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 57, Y = img_s.Height },
                //new PointF { X = 296, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                // note even with a 4th term in here and dst, same error arises....
                };

            dst = new PointF[] {
                new PointF { X = 30, Y = 0 },
                new PointF { X = 30 + dst[3].X - dst[2].X, Y = 0 }, //660
                new PointF { X = 0, Y = 102 },
                new PointF { X = 685, Y = 103 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 67, Y = img_s.Height },
                //new PointF { X = 286, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                };

            img_d = new Mat();
            warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
            CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(Convert.ToInt32(dst.Max(a => a.X)), Convert.ToInt32(dst.Max(a => a.Y))), Inter.Linear, Warp.Default, BorderType.Transparent);

            img_d.Bitmap.Save("c:\\projects\\712_slice_AbdomenToHighHip_warpPerspective.png");

            img_s.Dispose();

            //////--------------HIGH HIP TO LOW HIP WARPING--------------------///////////////
            img_s = new Image<Bgra, byte>("c:\\projects\\712_slice_HighHipToLowHip.png");

            src = new PointF[] {
                new PointF { X = 37, Y = 0 },
                new PointF { X = 737, Y = 0 },
                new PointF { X = 27, Y = 145 },
                new PointF { X = 755, Y = 145 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 57, Y = img_s.Height },
                //new PointF { X = 296, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                // note even with a 4th term in here and dst, same error arises....
                };

            dst = new PointF[] {
                new PointF { X = 45, Y = 0 },
                new PointF { X = 37 + dst[3].X - dst[2].X, Y = 0 }, //660
                new PointF { X = 6, Y = 210 },
                new PointF { X = 747, Y = 212 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 67, Y = img_s.Height },
                //new PointF { X = 286, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                };

            img_d = new Mat();
            warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
            CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(Convert.ToInt32(dst.Max(a => a.X)), Convert.ToInt32(dst.Max(a => a.Y))), Inter.Linear, Warp.Default, BorderType.Transparent);

            img_d.Bitmap.Save("c:\\projects\\712_slice_HighHipToLowHip_warpPerspective.png");

            img_s.Dispose();

            //////--------------LOW HIP TO HIGH THIGH WARPING--------------------///////////////
            img_s = new Image<Bgra, byte>("c:\\projects\\712_slice_LowHipToHighThigh.png");

            src = new PointF[] {
                new PointF { X = 26, Y = 0 },
                new PointF { X = 756, Y = 0 },
                new PointF { X = 25, Y = 38 },
                new PointF { X = 759, Y = 41 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 57, Y = img_s.Height },
                //new PointF { X = 296, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                // note even with a 4th term in here and dst, same error arises....
                };

            dst = new PointF[] {
                new PointF { X = 18, Y = 0 },
                new PointF { X = 18 + dst[3].X - dst[2].X, Y = 0 }, //660
                new PointF { X = 15, Y = 76 },
                new PointF { X = 765, Y = 79 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 67, Y = img_s.Height },
                //new PointF { X = 286, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                };

            img_d = new Mat();
            warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
            CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(Convert.ToInt32(dst.Max(a => a.X)), Convert.ToInt32(dst.Max(a => a.Y))), Inter.Linear, Warp.Default, BorderType.Transparent);

            img_d.Bitmap.Save("c:\\projects\\712_slice_LowHipToHighThigh_warpPerspective.png");

            img_s.Dispose();
        }

        void RunForMembers()
        {
            foreach (MemberData md in members)
            {
                List<ProductData> pds = md.ProductCoordinatesData.Where(a => a.ProductID == 712).ToList();

                #region "LEFT SHOULDER to FOREARM WARPING"

                ProductData pd = pds.Where(a => a.ProductSection == "Left Shoulder").SingleOrDefault();

                //////--------------LEFT SHOULDER to FOREARM WARPING--------------------///////////////

                //pd.ProductCoordinates[0, 0] += 72;
                //pd.ProductCoordinates[1, 0] += 72;
                //pd.ProductCoordinates[2, 0] += 72;
                //pd.ProductCoordinates[3, 0] += 72;
                //pd.ProductCoordinates[0, 1] += 450.91;
                //pd.ProductCoordinates[1, 1] += 450.91;
                //pd.ProductCoordinates[2, 1] += 450.91;
                //pd.ProductCoordinates[3, 1] += 450.91;

                Image<Bgra, byte> img_s = new Image<Bgra, byte>("c:\\projects\\" + pd.ProductID + "_slice_leftshoulder.png");

                srcX1 = (float)pd.ProductCoordinates[0, 0];
                srcX2 = (float)pd.ProductCoordinates[3, 0];
                srcX3 = (float)pd.ProductCoordinates[2, 0];
                srcX4 = (float)pd.ProductCoordinates[3, 0];
                srcY1 = (float)pd.ProductCoordinates[0, 1];
                srcY2 = (float)pd.ProductCoordinates[0, 1];
                srcY3 = (float)pd.ProductCoordinates[2, 1];
                srcY4 = (float)pd.ProductCoordinates[3, 1];

                PointF[] src = new PointF[] {
                new PointF { X = srcX1, Y = srcY1 },
                new PointF { X = srcX2, Y = srcY2 },
                new PointF { X = srcX3, Y = srcY3 },
                new PointF { X = srcX4, Y = srcY4 }
                };

                float XAdditional1 = ((float)md.MemberCoordinates[63, 0] - srcX2);
                float XAdditional2 = ((float)md.MemberCoordinates[62, 0] - 12 - srcX1);
                float XAdditional3 = ((float)md.MemberCoordinates[55, 0] + 12 - srcX3);
                float YAdditional1 = ((float)md.MemberCoordinates[63, 1] - srcY2);
                float YAdditional2 = ((float)md.MemberCoordinates[62, 1] - srcY1);
                float YAdditional3 = ((float)md.MemberCoordinates[55, 1] - srcY3);

                md.MemberCoordinates[63, 0] -= XAdditional1;
                md.MemberCoordinates[62, 0] -= XAdditional1;
                md.MemberCoordinates[55, 0] -= XAdditional1;
                md.MemberCoordinates[63, 1] -= YAdditional1;
                md.MemberCoordinates[62, 1] -= YAdditional1;
                md.MemberCoordinates[55, 1] -= YAdditional1;

                dstX1 = srcX3;
                dstX2 = srcX2;

                //if strectch/shrink applies
                if ((((float)md.MemberCoordinates[55, 0] + 12 - (float)md.MemberCoordinates[62, 0] - 12) - (srcX4 - srcX3)) == 0)
                {
                    dstX3 = srcX3;
                    dstX4 = srcX4;
                    //dstX2 = dstX4;
                }
                else if (((float)md.MemberCoordinates[55, 0] + 12 - (float)md.MemberCoordinates[62, 0] - 12 > (srcX4 - srcX3)))
                {
                    //dstX3 = (srcX3 - ((((float)md.MemberCoordinates[8, 0] + 12 - (float)md.MemberCoordinates[15, 0] - 12) - (srcX4 - srcX3)) / 2));
                    //dstX4 = (srcX4 + ((((float)md.MemberCoordinates[8, 0] + 12 - (float)md.MemberCoordinates[15, 0] - 12) - (srcX4 - srcX3)) / 2));
                    ////dstX2 = srcX2 + ((((float)md.MemberCoordinates[8, 0] + 12 - (float)md.MemberCoordinates[15, 0] - 12) - (srcX4 - srcX3)));
                    //dstX1 = srcX1 - ((((float)md.MemberCoordinates[8, 0] + 12 - (float)md.MemberCoordinates[15, 0] - 12) - (srcX4 - srcX3)));

                    dstX3 = (srcX3) + ((float)md.MemberCoordinates[62, 0] - 12 - (srcX3));
                    dstX4 = (srcX4) + ((float)md.MemberCoordinates[55, 0] + 12 - (srcX4));
                }
                else if ((srcX4 - srcX3) > ((float)md.MemberCoordinates[55, 0] + 12 - (float)md.MemberCoordinates[62, 0] - 12))
                {
                    dstX3 = (srcX3 + ((((srcX4 - srcX3) - ((float)md.MemberCoordinates[62, 0] - 12 - (float)md.MemberCoordinates[55, 0] + 12))) / 2));
                    dstX4 = (srcX4 - ((((srcX4 - srcX3) - ((float)md.MemberCoordinates[62, 0] - 12 - (float)md.MemberCoordinates[55, 0] + 12))) / 2));
                    //dstX2 = srcX2 - ((((float)md.MemberCoordinates[8, 0] + 12 - (float)md.MemberCoordinates[15, 0] - 12) - (srcX4 - srcX3)));
                    dstX1 = srcX1 + ((((srcX4 - srcX3) - ((float)md.MemberCoordinates[62, 0] - 12 - (float)md.MemberCoordinates[55, 0] + 12))));
                }
                else
                {
                    dstX3 = srcX3;
                    dstX4 = srcX4;
                    //dstX2 = dstX4;
                }

                //if strectch/shrink applies
                //if ((((float)md.MemberCoordinates[7, 0] - (float)md.MemberCoordinates[15, 0] - 12) - (srcX4 - srcX3)) == 0)
                //{
                //    dstX4 = srcX4;
                //}
                //else if ((float)md.MemberCoordinates[7, 0] > (float)md.MemberCoordinates[15, 0] - 12)
                //{
                //    dstX4 = (srcX4 - ((float)md.MemberCoordinates[7, 0] - (float)md.MemberCoordinates[15, 0] - 12));
                //}
                //else if ((float)md.MemberCoordinates[15, 0] - 12 > (float)md.MemberCoordinates[7, 0])
                //{
                //    dstX4 = (srcX4 + ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[7, 0]));
                //}
                //else
                //{
                //    dstX4 = srcX4;
                //}

                dstY1 = srcY1;
                dstY2 = srcY2;

                //if strectch/shrink applies
                if (
                    (((float)md.MemberCoordinates[62, 1] - (float)md.MemberCoordinates[63, 1]) - (srcY4 - srcY2)) == 0
                    &&
                    (((float)md.MemberCoordinates[55, 1] - (float)md.MemberCoordinates[63, 1]) - (srcY3 - srcY1)) == 0
                    )
                {
                    dstY3 = srcY3;
                    dstY4 = srcY4;
                }
                else
                {
                    if (((float)md.MemberCoordinates[62, 1] - (float)md.MemberCoordinates[63, 1] > (srcY3 - srcY1)))
                    {
                        //dstY3 = (srcY3 + ((((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]) - (srcY3 - srcY1))));

                        dstY3 = (srcY3) - ((srcY3) - (float)md.MemberCoordinates[62, 1]);
                    }
                    else if ((srcY3 - srcY1) > ((float)md.MemberCoordinates[62, 1] - (float)md.MemberCoordinates[63, 1]))
                    {
                        dstY3 = (srcY3 - ((((srcY3 - srcY1) - ((float)md.MemberCoordinates[62, 1] - (float)md.MemberCoordinates[63, 1])))));
                    }
                    else
                    {
                        dstY3 = srcY3;
                    }

                    if (((float)md.MemberCoordinates[55, 1] - (float)md.MemberCoordinates[63, 1] > (srcY4 - srcY2)))
                    {
                        //dstY4 = (srcY4 + ((((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]) - (srcY4 - srcY2))));

                        dstY4 = (srcY4) - ((srcY4) - (float)md.MemberCoordinates[55, 1]);
                    }
                    else if ((srcY4 - srcY2) > ((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]))
                    {
                        dstY4 = (srcY4 - ((((srcY4 - srcY2) - ((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1])))));
                    }
                    else
                    {
                        dstY4 = srcY4;
                    }
                }

                //if strectch/shrink applies
                //if ((((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]) - (srcY3 - srcY1)) == 0)
                //{
                //    dstY3 = srcY3;
                //}
                //else if (((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1] > (srcY3 - srcY1)))
                //{
                //    dstY3 = (srcY3 + ((((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1])) - (srcY3 - srcY1)));
                //}
                //else if ((srcY3 - srcY1) > ((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]))
                //{
                //    dstY3 = (srcY3 - (((srcY3 - srcY1) - ((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]))));
                //}
                //else
                //{
                //    dstY3 = srcY3;
                //}

                ////if strectch/shrink applies
                //if ((((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]) - (srcY4 - srcY2)) == 0)
                //{
                //    dstY4 = srcY4;
                //}
                //else if (((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1] > (srcY4 - srcY2)))
                //{
                //    dstY4 = (srcY4 + ((((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1])) - (srcY4 - srcY2)));
                //}
                //else if ((srcY4 - srcY2) > ((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]))
                //{
                //    dstY4 = (srcY4 - (((srcY4 - srcY2) - ((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]))));
                //}
                //else
                //{
                //    dstY4 = srcY4;
                //}

                PointF[] dst = new PointF[] {
                new PointF { X = dstX1, Y = dstY1 }, //0, 0
                new PointF { X = dstX2, Y = dstY2 }, //1368, 786
                new PointF { X = dstX3, Y = dstY3 }, //1110, 1145
                new PointF { X = dstX4, Y = dstY4 }, //1326, 1230
                //new PointF { X = ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[8, 0] + 12) > (src[3].X - src[2].X) ? (src[2].X - ((float)md.MemberCoordinates[8, 0] + 12 - src[2].X)) : src[2].X, Y = 1145 }, //1110
                //new PointF { X = ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[8, 0] + 12) > (src[3].X - src[2].X) ? (src[3].X - ((float)md.MemberCoordinates[15, 0] - 12 - src[3].X)) : src[3].X, Y = 1230 } //1326
                };

                minSrcX = Convert.ToInt32(src.Min(a => a.X));
                maxSrcX = Convert.ToInt32(src.Max(a => a.X));
                maxDstX = Convert.ToInt32(dst.Max(a => a.X));
                minSrcY = Convert.ToInt32(src.Min(a => a.Y));
                maxSrcY = Convert.ToInt32(src.Max(a => a.Y));
                maxDstY = Convert.ToInt32(dst.Max(a => a.Y));
                dstWidth = maxDstX;
                dstHeight = maxDstY;
                //dstWidth = (maxDstX - minSrcX) > (maxSrcX - minSrcX) ? img_s.Width + (maxDstX - maxSrcX) : img_s.Width;
                //dstHeight = (maxDstY - minSrcY) > (maxSrcY - minSrcY) ? img_s.Height + (maxDstY - maxSrcY) : img_s.Height;

                Mat img_d = new Mat();
                Mat warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
                CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(dstWidth, dstHeight), Inter.Linear, Warp.Default, BorderType.Transparent);

                img_d.Bitmap.Save("c:\\projects\\" + md.MemberID + "_" + pd.ProductID + "_slice_leftshoulder_warpPerspective.png");

                md.MemberCoordinates[63, 0] += XAdditional1;
                md.MemberCoordinates[62, 0] += XAdditional1;
                md.MemberCoordinates[55, 0] += XAdditional1;
                md.MemberCoordinates[63, 1] += YAdditional1;
                md.MemberCoordinates[62, 1] += YAdditional1;
                md.MemberCoordinates[55, 1] += YAdditional1;

                ResetCoordinates();

                img_s.Dispose();

                #endregion

                #region "LEFT FOREARM TO ARM WARPING"
                //////--------------FOREARM TO ARM WARPING--------------------///////////////
                pd = pds.Where(a => a.ProductSection == "Left Arm").SingleOrDefault();

                //pd.ProductCoordinates[0, 0] += 72;
                //pd.ProductCoordinates[1, 0] += 72;
                //pd.ProductCoordinates[2, 0] += 72;
                //pd.ProductCoordinates[3, 0] += 72;
                //pd.ProductCoordinates[0, 1] += 450.91;
                //pd.ProductCoordinates[1, 1] += 450.91;
                //pd.ProductCoordinates[2, 1] += 450.91;
                //pd.ProductCoordinates[3, 1] += 450.91;

                img_s = new Image<Bgra, byte>("c:\\projects\\" + pd.ProductID + "_slice_leftarmToForeArm.png");

                srcX1 = (float)pd.ProductCoordinates[0, 0];
                srcX2 = (float)pd.ProductCoordinates[1, 0];
                srcX3 = (float)pd.ProductCoordinates[2, 0];
                srcX4 = (float)pd.ProductCoordinates[3, 0];
                srcY1 = (float)pd.ProductCoordinates[0, 1];
                srcY2 = (float)pd.ProductCoordinates[1, 1];
                srcY3 = (float)pd.ProductCoordinates[2, 1];
                srcY4 = (float)pd.ProductCoordinates[3, 1];

                src = new PointF[] {
                new PointF { X = srcX1, Y = srcY1 },
                new PointF { X = srcX2, Y = srcY2 },
                new PointF { X = srcX3, Y = srcY3 },
                new PointF { X = srcX4, Y = srcY4 }
                };

                XAdditional1 = ((float)md.MemberCoordinates[55, 0] + 12 - srcX2); //((float)md.MemberCoordinates[62, 0] - 12 - srcX1);
                YAdditional1 = ((float)md.MemberCoordinates[55, 1] - srcY2); //((float)md.MemberCoordinates[62, 1] - srcY1);

                md.MemberCoordinates[62, 0] -= XAdditional1;
                md.MemberCoordinates[55, 0] -= XAdditional1;
                md.MemberCoordinates[61, 0] -= XAdditional1;
                md.MemberCoordinates[56, 0] -= XAdditional1;
                md.MemberCoordinates[62, 1] -= YAdditional1;
                md.MemberCoordinates[55, 1] -= YAdditional1;
                md.MemberCoordinates[61, 1] -= YAdditional1;
                md.MemberCoordinates[56, 1] -= YAdditional1;

                //if strectch/shrink applies
                if (
                    (((float)md.MemberCoordinates[55, 0] + 12 - (float)md.MemberCoordinates[62, 0] - 12) - (srcX2 - srcX1)) == 0
                    &&
                    (((float)md.MemberCoordinates[56, 0] + 12 - (float)md.MemberCoordinates[61, 0] - 12) - (srcX4 - srcX3)) == 0
                    )
                {
                    dstX1 = srcX1;
                    dstX2 = srcX2;
                    dstX3 = srcX3;
                    dstX4 = srcX4;
                    //dstX2 = dstX4;
                }
                else
                {
                    if (((dstX4 - dstX3) > (srcX2 - srcX1)) || (srcX2 - srcX1) > (dstX4 - dstX3))
                    {
                        //dstX1 = (srcX1 - (((dstX4 - dstX3) - (srcX2 - srcX1)) / 2));
                        //dstX2 = (srcX2 + (((dstX4 - dstX3) - (srcX2 - srcX1)) / 2));

                        dstX1 = srcX1; //((float)md.MemberCoordinates[62, 0] - 12 - (srcX1));

                        if (((float)md.MemberCoordinates[55, 0] + 12 - (float)md.MemberCoordinates[62, 0] - 12) > (srcX2 - srcX1))
                            dstX2 = ((srcX2) + (((float)md.MemberCoordinates[55, 0] + 12 - (float)md.MemberCoordinates[62, 0] - 12) - (srcX2 - srcX1))); //((float)md.MemberCoordinates[55, 0] + 12 - srcX2)
                        else
                            dstX2 = ((srcX2) + (((float)md.MemberCoordinates[55, 0] + 12 - (float)md.MemberCoordinates[62, 0] - 12) - (srcX2 - srcX1)));
                    }
                    else if ((srcX2 - srcX1) > (dstX4 - dstX3))
                    {
                        dstX1 = (srcX1 + (((srcX2 - srcX1) - (dstX4 - dstX3)) / 2));
                        dstX2 = (srcX2 - (((srcX2 - srcX1) - (dstX4 - dstX3)) / 2));
                    }
                    else
                    {
                        dstX1 = srcX1;
                        dstX2 = srcX2;
                    }

                    if ((((float)md.MemberCoordinates[56, 0] + 12 - (float)md.MemberCoordinates[61, 0] - 12) > (srcX4 - srcX3)))
                    {
                        dstX3 = (srcX3 - ((((float)md.MemberCoordinates[56, 0] + 12 - (float)md.MemberCoordinates[61, 0] - 12) - (srcX4 - srcX3)) / 2));
                        dstX4 = (srcX4 + ((((float)md.MemberCoordinates[56, 0] + 12 - (float)md.MemberCoordinates[61, 0] - 12) - (srcX4 - srcX3)) / 2));
                    }
                    else if ((srcX4 - srcX3) > ((float)md.MemberCoordinates[56, 0] + 12 - (float)md.MemberCoordinates[61, 0] - 12))
                    {
                        //dstX3 = (srcX3 + ((((srcX4 - srcX3) - ((float)md.MemberCoordinates[56, 0] + 12 - (float)md.MemberCoordinates[61, 0] - 12))) / 2));
                        //dstX4 = (srcX4 - ((((srcX4 - srcX3) - ((float)md.MemberCoordinates[56, 0] + 12 - (float)md.MemberCoordinates[61, 0] - 12))) / 2));
                        //dstX3 = srcX3 + ((float)md.MemberCoordinates[61, 0] - 12 - (float)md.MemberCoordinates[62, 0] - 12) - (srcX3 - srcX1);
                        //dstX4 = srcX4 + ((float)md.MemberCoordinates[56, 0] + 12 - (float)md.MemberCoordinates[61, 0] - 12) + (dstX3 - srcX3);

                        if (((float)md.MemberCoordinates[62, 0] - 12 - (float)md.MemberCoordinates[61, 0] - 12) > (srcX1 - srcX3))
                        {
                            dstX3 = (srcX3) - (((float)md.MemberCoordinates[62, 0] - 12 - (float)md.MemberCoordinates[61, 0] - 12) - (srcX1 - srcX3)); //((float)md.MemberCoordinates[61, 0] - 12 - (srcX3));
                        }
                        else
                        {
                            dstX3 = (srcX3) + (((float)md.MemberCoordinates[62, 0] - 12 - (float)md.MemberCoordinates[61, 0] - 12) - (srcX1 - srcX3));
                        }

                        if (((float)md.MemberCoordinates[55, 0] + 12 - (float)md.MemberCoordinates[56, 0] + 12) > (srcX2 - srcX4))
                        {
                            dstX4 = (srcX4) - (((float)md.MemberCoordinates[55, 0] + 12 - (float)md.MemberCoordinates[56, 0] + 12) - (srcX2 - srcX4)); //((float)md.MemberCoordinates[56, 0] + 12 - (srcX4))
                        }
                        else
                        {
                            dstX4 = (srcX4) + (((float)md.MemberCoordinates[55, 0] + 12 - (float)md.MemberCoordinates[56, 0] + 12) - (srcX2 - srcX4));
                        }
                    }
                    else
                    {
                        dstX3 = srcX3;
                        dstX4 = srcX4;
                    }
                }

                //if strectch/shrink applies
                //if ((((float)md.MemberCoordinates[7, 0] - (float)md.MemberCoordinates[62, 0] - 12) - (srcX4 - srcX3)) == 0)
                //{
                //    dstX4 = srcX4;
                //}
                //else if ((float)md.MemberCoordinates[7, 0] > (float)md.MemberCoordinates[62, 0] - 12)
                //{
                //    dstX4 = (srcX4 - ((float)md.MemberCoordinates[7, 0] - (float)md.MemberCoordinates[62, 0] - 12));
                //}
                //else if ((float)md.MemberCoordinates[62, 0] - 12 > (float)md.MemberCoordinates[7, 0])
                //{
                //    dstX4 = (srcX4 + ((float)md.MemberCoordinates[62, 0] - 12 - (float)md.MemberCoordinates[7, 0]));
                //}
                //else
                //{
                //    dstX4 = srcX4;
                //}

                if ((float)md.MemberCoordinates[62, 1] > (float)md.MemberCoordinates[55, 1])
                {
                    dstY1 = srcY1;
                    dstY2 = ((srcY2) - ((float)md.MemberCoordinates[62, 1] - (float)md.MemberCoordinates[55, 1]));
                }
                else
                {
                    dstY1 = (srcY1 - ((float)md.MemberCoordinates[55, 1] - (float)md.MemberCoordinates[62, 1]));
                    dstY2 = srcY2;
                }


                //if strectch/shrink applies
                if (
                    (((float)md.MemberCoordinates[56, 1] - (float)md.MemberCoordinates[55, 1]) - (srcY4 - srcY2)) == 0
                    &&
                    (((float)md.MemberCoordinates[61, 1] - (float)md.MemberCoordinates[62, 1]) - (srcY3 - srcY1)) == 0
                    )
                {
                    dstY3 = srcY3;
                    dstY4 = srcY4;
                }
                else
                {
                    if (((float)md.MemberCoordinates[61, 1] - (float)md.MemberCoordinates[62, 1] > (srcY3 - srcY1)))
                    {
                        dstY3 = (srcY3 + ((((float)md.MemberCoordinates[61, 1] - (float)md.MemberCoordinates[62, 1]) - (srcY3 - srcY1))));
                    }
                    else if ((srcY3 - srcY1) > ((float)md.MemberCoordinates[61, 1] - (float)md.MemberCoordinates[62, 1]))
                    {
                        //dstY3 = (srcY3 - ((((srcY3 - srcY1) - ((float)md.MemberCoordinates[61, 1] - (float)md.MemberCoordinates[62, 1])))));
                        dstY3 = (srcY3) - ((srcY3) - (float)md.MemberCoordinates[61, 1]);
                    }
                    else
                    {
                        dstY3 = srcY3;
                    }

                    if (((float)md.MemberCoordinates[56, 1] - (float)md.MemberCoordinates[55, 1] > (srcY4 - dstY2)))
                    {
                        //dstY4 = (srcY4 + ((((float)md.MemberCoordinates[56, 1] - (float)md.MemberCoordinates[55, 1]) - (srcY4 - dstY2))));
                        dstY4 = (srcY4) - ((srcY4) - (float)md.MemberCoordinates[56, 1]);
                    }
                    else if ((srcY4 - dstY2) > ((float)md.MemberCoordinates[56, 1] - (float)md.MemberCoordinates[55, 1]))
                    {
                        dstY4 = (srcY4) - ((srcY4) - (float)md.MemberCoordinates[56, 1]);
                    }
                    else
                    {
                        dstY4 = srcY4;
                    }
                }

                //if strectch/shrink applies
                //if ((((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]) - (srcY3 - srcY1)) == 0)
                //{
                //    dstY3 = srcY3;
                //}
                //else if (((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1] > (srcY3 - srcY1)))
                //{
                //    dstY3 = (srcY3 + ((((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1])) - (srcY3 - srcY1)));
                //}
                //else if ((srcY3 - srcY1) > ((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]))
                //{
                //    dstY3 = (srcY3 - (((srcY3 - srcY1) - ((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]))));
                //}
                //else
                //{
                //    dstY3 = srcY3;
                //}

                ////if strectch/shrink applies
                //if ((((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]) - (srcY4 - srcY2)) == 0)
                //{
                //    dstY4 = srcY4;
                //}
                //else if (((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1] > (srcY4 - srcY2)))
                //{
                //    dstY4 = (srcY4 + ((((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1])) - (srcY4 - srcY2)));
                //}
                //else if ((srcY4 - srcY2) > ((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]))
                //{
                //    dstY4 = (srcY4 - (((srcY4 - srcY2) - ((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]))));
                //}
                //else
                //{
                //    dstY4 = srcY4;
                //}

                dst = new PointF[] {
                new PointF { X = dstX1, Y = dstY1 }, //0, 0
                new PointF { X = dstX2, Y = dstY2 }, //1368, 786
                new PointF { X = dstX3, Y = dstY3 }, //1110, 1145
                new PointF { X = dstX4, Y = dstY4 }, //1326, 1230
                //new PointF { X = ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[8, 0] + 12) > (src[3].X - src[2].X) ? (src[2].X - ((float)md.MemberCoordinates[8, 0] + 12 - src[2].X)) : src[2].X, Y = 1145 }, //1110
                //new PointF { X = ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[8, 0] + 12) > (src[3].X - src[2].X) ? (src[3].X - ((float)md.MemberCoordinates[15, 0] - 12 - src[3].X)) : src[3].X, Y = 1230 } //1326
                };

                minSrcX = Convert.ToInt32(src.Min(a => a.X));
                maxSrcX = Convert.ToInt32(src.Max(a => a.X));
                maxDstX = Convert.ToInt32(dst.Max(a => a.X));
                minSrcY = Convert.ToInt32(src.Min(a => a.Y));
                maxSrcY = Convert.ToInt32(src.Max(a => a.Y));
                maxDstY = Convert.ToInt32(dst.Max(a => a.Y));
                dstWidth = maxDstX;
                dstHeight = maxDstY;
                //dstWidth = (maxDstX - minSrcX) > (maxSrcX - minSrcX) ? img_s.Width + (maxDstX - maxSrcX) : img_s.Width;
                //dstHeight = (maxDstY - minSrcY) > (maxSrcY - minSrcY) ? img_s.Height + (maxDstY - maxSrcY) : img_s.Height;

                img_d = new Mat();
                warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
                CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(dstWidth, dstHeight), Inter.Linear, Warp.Default, BorderType.Transparent);

                img_d.Bitmap.Save("c:\\projects\\" + md.MemberID + "_" + pd.ProductID + "_slice_leftarmToForeArm_warpPerspective.png");

                md.MemberCoordinates[62, 0] += XAdditional1;
                md.MemberCoordinates[55, 0] += XAdditional1;
                md.MemberCoordinates[61, 0] += XAdditional1;
                md.MemberCoordinates[56, 0] += XAdditional1;
                md.MemberCoordinates[62, 1] += YAdditional1;
                md.MemberCoordinates[55, 1] += YAdditional1;
                md.MemberCoordinates[61, 1] += YAdditional1;
                md.MemberCoordinates[56, 1] += YAdditional1;

                ResetCoordinates();

                img_s.Dispose();

                #endregion

                #region "LEFT ARM TO WRIST WARPING"
                //////--------------ARM TO WRIST WARPING--------------------///////////////
                pd = pds.Where(a => a.ProductSection == "Left Wrist").SingleOrDefault();

                img_s = new Image<Bgra, byte>("c:\\projects\\" + pd.ProductID + "_slice_leftForeArmToWrist.png");

                srcX1 = (float)pd.ProductCoordinates[0, 0];
                srcX2 = (float)pd.ProductCoordinates[1, 0];
                srcX3 = (float)pd.ProductCoordinates[2, 0];
                srcX4 = (float)pd.ProductCoordinates[3, 0];
                srcY1 = (float)pd.ProductCoordinates[0, 1];
                srcY2 = (float)pd.ProductCoordinates[1, 1];
                srcY3 = (float)pd.ProductCoordinates[2, 1];
                srcY4 = (float)pd.ProductCoordinates[3, 1];

                src = new PointF[] {
                new PointF { X = srcX1, Y = srcY1 },
                new PointF { X = srcX2, Y = srcY2 },
                new PointF { X = srcX3, Y = srcY3 },
                new PointF { X = srcX4, Y = srcY4 }
                };

                XAdditional1 = ((float)md.MemberCoordinates[56, 0] + 12 - srcX1);
                YAdditional1 = ((float)md.MemberCoordinates[56, 1] - srcY1);

                md.MemberCoordinates[61, 0] -= XAdditional1;
                md.MemberCoordinates[56, 0] -= XAdditional1;
                md.MemberCoordinates[60, 0] -= XAdditional1;
                md.MemberCoordinates[57, 0] -= XAdditional1;
                md.MemberCoordinates[61, 1] -= YAdditional1;
                md.MemberCoordinates[56, 1] -= YAdditional1;
                md.MemberCoordinates[60, 1] -= YAdditional1;
                md.MemberCoordinates[57, 1] -= YAdditional1;

                //if strectch/shrink applies
                if (
                    (((float)md.MemberCoordinates[56, 0] + 12 - (float)md.MemberCoordinates[61, 0] - 12) - (srcX2 - srcX1)) == 0
                    &&
                    (((float)md.MemberCoordinates[57, 0] + 12 - (float)md.MemberCoordinates[60, 0] - 12) - (srcX4 - srcX3)) == 0
                    )
                {
                    dstX1 = srcX1;
                    dstX2 = srcX2;
                    dstX3 = srcX3;
                    dstX4 = srcX4;
                    //dstX2 = dstX4;
                }
                else
                {
                    if (((dstX4 - dstX3) > (srcX2 - srcX1)))
                    {
                        //dstX1 = (srcX1 - (((dstX4 - dstX3) - (srcX2 - srcX1)) / 2));
                        //dstX2 = (srcX2 + (((dstX4 - dstX3) - (srcX2 - srcX1)) / 2));
                        dstX1 = srcX1 + ((float)md.MemberCoordinates[61, 0] - 12 - (srcX1));
                        dstX2 = ((srcX2) + ((float)md.MemberCoordinates[56, 0] + 12 - srcX2));
                    }
                    else if ((srcX2 - srcX1) > (dstX4 - dstX3))
                    {
                        //dstX1 = (srcX1 + (((srcX2 - srcX1) - (dstX4 - dstX3)) / 2));
                        //dstX2 = (srcX2 - (((srcX2 - srcX1) - (dstX4 - dstX3)) / 2));

                        dstX1 = srcX1 + ((float)md.MemberCoordinates[61, 0] - 12 - (srcX1));
                        dstX2 = ((srcX2) + ((float)md.MemberCoordinates[56, 0] + 12 - srcX2));
                    }
                    else
                    {
                        dstX1 = srcX1;
                        dstX2 = srcX2;
                    }

                    if ((((float)md.MemberCoordinates[57, 0] + 12 - (float)md.MemberCoordinates[60, 0] - 12) > (srcX4 - srcX3)))
                    {
                        dstX3 = (srcX3 - ((((float)md.MemberCoordinates[9, 0] + 12 - (float)md.MemberCoordinates[14, 0] - 12) - (srcX4 - srcX3)) / 2));
                        dstX4 = (srcX4 + ((((float)md.MemberCoordinates[9, 0] + 12 - (float)md.MemberCoordinates[14, 0] - 12) - (srcX4 - srcX3)) / 2));
                    }
                    else if ((srcX4 - srcX3) > ((float)md.MemberCoordinates[57, 0] + 12 - (float)md.MemberCoordinates[60, 0] - 12))
                    {
                        //dstX3 = (srcX3 + ((((srcX4 - srcX3) - ((float)md.MemberCoordinates[9, 0] + 12 - (float)md.MemberCoordinates[14, 0] - 12))) / 2));
                        //dstX4 = (srcX4 - ((((srcX4 - srcX3) - ((float)md.MemberCoordinates[9, 0] + 12 - (float)md.MemberCoordinates[14, 0] - 12))) / 2));
                        //dstX3 = srcX3 + ((float)md.MemberCoordinates[14, 0] - 12 - (float)md.MemberCoordinates[15, 0] - 12) - (srcX3 - srcX1);
                        //dstX4 = srcX4 + ((float)md.MemberCoordinates[9, 0] + 12 - (float)md.MemberCoordinates[14, 0] - 12) + (dstX3 - srcX3);

                        //Loose Sleeves
                        if ((srcX4 - srcX3) > ((float)md.MemberCoordinates[57, 0] + 12 - (float)md.MemberCoordinates[60, 0] - 12) && ((srcX4 - srcX3) - ((float)md.MemberCoordinates[57, 0] + 12 - (float)md.MemberCoordinates[60, 0] - 12)) > 100)
                        {
                            dstX3 = (srcX3) - ((srcX4 - srcX3));
                            dstX4 = dstX3 + (srcX4 - srcX3);
                        }
                        else
                        {
                            dstX3 = (srcX3) + ((float)md.MemberCoordinates[60, 0] - 12 - (srcX3));
                            dstX4 = (srcX4) + ((float)md.MemberCoordinates[57, 0] + 12 - (srcX4));
                        }
                    }
                    else
                    {
                        dstX3 = srcX3;
                        dstX4 = srcX4;
                    }
                }

                //if strectch/shrink applies
                //if ((((float)md.MemberCoordinates[7, 0] - (float)md.MemberCoordinates[15, 0] - 12) - (srcX4 - srcX3)) == 0)
                //{
                //    dstX4 = srcX4;
                //}
                //else if ((float)md.MemberCoordinates[7, 0] > (float)md.MemberCoordinates[15, 0] - 12)
                //{
                //    dstX4 = (srcX4 - ((float)md.MemberCoordinates[7, 0] - (float)md.MemberCoordinates[15, 0] - 12));
                //}
                //else if ((float)md.MemberCoordinates[15, 0] - 12 > (float)md.MemberCoordinates[7, 0])
                //{
                //    dstX4 = (srcX4 + ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[7, 0]));
                //}
                //else
                //{
                //    dstX4 = srcX4;
                //}

                if ((float)md.MemberCoordinates[56, 1] > (float)md.MemberCoordinates[61, 1])
                {
                    dstY1 = ((srcY1) - ((float)md.MemberCoordinates[56, 1] - (float)md.MemberCoordinates[61, 1]));
                    dstY2 = srcY2;
                }
                else
                {
                    dstY1 = srcY1;
                    dstY2 = (srcY2 - ((float)md.MemberCoordinates[61, 1] - (float)md.MemberCoordinates[56, 1])); ;
                }


                //if strectch/shrink applies
                if (
                    (((float)md.MemberCoordinates[57, 1] - (float)md.MemberCoordinates[56, 1]) - (srcY4 - srcY2)) == 0
                    &&
                    (((float)md.MemberCoordinates[60, 1] - (float)md.MemberCoordinates[61, 1]) - (srcY3 - srcY1)) == 0
                    )
                {
                    dstY3 = srcY3;
                    dstY4 = srcY4;
                }
                else
                {
                    if (((float)md.MemberCoordinates[60, 1] - (float)md.MemberCoordinates[61, 1] > (srcY3 - srcY1)))
                    {
                        //dstY3 = (srcY3 + ((((float)md.MemberCoordinates[13, 1] - (float)md.MemberCoordinates[14, 1]) - (srcY3 - srcY1))));

                        //Short Sleeves
                        if ((srcY3 - srcY1) > ((float)md.MemberCoordinates[60, 1] - (float)md.MemberCoordinates[61, 1]))
                            dstY3 = (srcY3) - ((srcY3) - (float)md.MemberCoordinates[60, 1]);
                        else
                            dstY3 = dstY1 + (srcY3 - srcY1);
                    }
                    else if ((srcY3 - srcY1) > ((float)md.MemberCoordinates[60, 1] - (float)md.MemberCoordinates[61, 1]))
                    {
                        //dstY3 = (srcY3 - ((((srcY3 - srcY1) - ((float)md.MemberCoordinates[14, 1] - (float)md.MemberCoordinates[15, 1])))));

                        //Short Sleeves
                        if ((srcY3 - srcY1) > ((float)md.MemberCoordinates[60, 1] - (float)md.MemberCoordinates[61, 1]))
                            dstY3 = (srcY3) - ((srcY3) - (float)md.MemberCoordinates[60, 1]);
                        else
                            dstY3 = dstY1 + (srcY3 - srcY1);
                    }
                    else
                    {
                        dstY3 = srcY3;
                    }

                    if (((float)md.MemberCoordinates[57, 1] - (float)md.MemberCoordinates[56, 1] > (srcY4 - dstY2)))
                    {
                        //dstY4 = (srcY4 + ((((float)md.MemberCoordinates[9, 1] - (float)md.MemberCoordinates[8, 1]) - (srcY4 - dstY2))));

                        //Short Sleeves
                        if ((srcY4 - srcY2) > ((float)md.MemberCoordinates[57, 1] - (float)md.MemberCoordinates[56, 1]))
                            dstY4 = (srcY4) - ((srcY4) - (float)md.MemberCoordinates[57, 1]);
                        else
                            dstY4 = dstY2 + (srcY4 - srcY2);
                    }
                    else if ((srcY4 - dstY2) > ((float)md.MemberCoordinates[57, 1] - (float)md.MemberCoordinates[56, 1]))
                    {
                        //dstY4 = (srcY4) - ((srcY4) - (float)md.MemberCoordinates[10, 1]);

                        //Short Sleeves
                        if ((srcY4 - srcY2) < ((float)md.MemberCoordinates[57, 1] - (float)md.MemberCoordinates[56, 1]))
                            dstY4 = (srcY4) - ((srcY4) - (float)md.MemberCoordinates[57, 1]);
                        else
                            dstY4 = dstY2 + (srcY4 - srcY2);
                    }
                    else
                    {
                        dstY4 = srcY4;
                    }
                }

                //if strectch/shrink applies
                //if ((((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]) - (srcY3 - srcY1)) == 0)
                //{
                //    dstY3 = srcY3;
                //}
                //else if (((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1] > (srcY3 - srcY1)))
                //{
                //    dstY3 = (srcY3 + ((((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1])) - (srcY3 - srcY1)));
                //}
                //else if ((srcY3 - srcY1) > ((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]))
                //{
                //    dstY3 = (srcY3 - (((srcY3 - srcY1) - ((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]))));
                //}
                //else
                //{
                //    dstY3 = srcY3;
                //}

                ////if strectch/shrink applies
                //if ((((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]) - (srcY4 - srcY2)) == 0)
                //{
                //    dstY4 = srcY4;
                //}
                //else if (((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1] > (srcY4 - srcY2)))
                //{
                //    dstY4 = (srcY4 + ((((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1])) - (srcY4 - srcY2)));
                //}
                //else if ((srcY4 - srcY2) > ((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]))
                //{
                //    dstY4 = (srcY4 - (((srcY4 - srcY2) - ((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]))));
                //}
                //else
                //{
                //    dstY4 = srcY4;
                //}

                dst = new PointF[] {
                new PointF { X = dstX1, Y = dstY1 }, //0, 0
                new PointF { X = dstX2, Y = dstY2 }, //1368, 786
                new PointF { X = dstX3, Y = dstY3 }, //1110, 1145
                new PointF { X = dstX4, Y = dstY4 }, //1326, 1230
                //new PointF { X = ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[8, 0] + 12) > (src[3].X - src[2].X) ? (src[2].X - ((float)md.MemberCoordinates[8, 0] + 12 - src[2].X)) : src[2].X, Y = 1145 }, //1110
                //new PointF { X = ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[8, 0] + 12) > (src[3].X - src[2].X) ? (src[3].X - ((float)md.MemberCoordinates[15, 0] - 12 - src[3].X)) : src[3].X, Y = 1230 } //1326
                };

                minSrcX = Convert.ToInt32(src.Min(a => a.X));
                maxSrcX = Convert.ToInt32(src.Max(a => a.X));
                maxDstX = Convert.ToInt32(dst.Max(a => a.X));
                minSrcY = Convert.ToInt32(src.Min(a => a.Y));
                maxSrcY = Convert.ToInt32(src.Max(a => a.Y));
                maxDstY = Convert.ToInt32(dst.Max(a => a.Y));
                dstWidth = maxDstX;
                dstHeight = maxDstY;
                //dstWidth = (maxDstX - minSrcX) > (maxSrcX - minSrcX) ? img_s.Width + (maxDstX - maxSrcX) : img_s.Width;
                //dstHeight = (maxDstY - minSrcY) > (maxSrcY - minSrcY) ? img_s.Height + (maxDstY - maxSrcY) : img_s.Height;

                img_d = new Mat();
                warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
                CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(dstWidth, dstHeight), Inter.Linear, Warp.Default, BorderType.Transparent);

                img_d.Bitmap.Save("c:\\projects\\" + md.MemberID + "_" + pd.ProductID + "_slice_leftForeArmToWrist_warpPerspective.png");

                md.MemberCoordinates[61, 0] += XAdditional1;
                md.MemberCoordinates[56, 0] += XAdditional1;
                md.MemberCoordinates[60, 0] += XAdditional1;
                md.MemberCoordinates[57, 0] += XAdditional1;
                md.MemberCoordinates[61, 1] += YAdditional1;
                md.MemberCoordinates[56, 1] += YAdditional1;
                md.MemberCoordinates[60, 1] += YAdditional1;
                md.MemberCoordinates[57, 1] += YAdditional1;

                ResetCoordinates();

                img_s.Dispose();

                #endregion

                #region "RIGHT SHOULDER to FOREARM WARPING"
                //////--------------RIGHT SHOULDER to FOREARM WARPING--------------------///////////////
                pd = pds.Where(a => a.ProductSection == "Right Shoulder").SingleOrDefault();

                //pd.ProductCoordinates[0, 0] += 72;
                //pd.ProductCoordinates[1, 0] += 72;
                //pd.ProductCoordinates[2, 0] += 72;
                //pd.ProductCoordinates[3, 0] += 72;
                //pd.ProductCoordinates[0, 1] += 450.91;
                //pd.ProductCoordinates[1, 1] += 450.91;
                //pd.ProductCoordinates[2, 1] += 450.91;
                //pd.ProductCoordinates[3, 1] += 450.91;

                img_s = new Image<Bgra, byte>("c:\\projects\\" + pd.ProductID + "_slice_rightshoulder.png");

                srcX1 = (float)pd.ProductCoordinates[0, 0];
                srcX2 = (float)pd.ProductCoordinates[3, 0];
                srcX3 = (float)pd.ProductCoordinates[2, 0];
                srcX4 = (float)pd.ProductCoordinates[3, 0];
                srcY1 = (float)pd.ProductCoordinates[0, 1];
                srcY2 = (float)pd.ProductCoordinates[0, 1];
                srcY3 = (float)pd.ProductCoordinates[2, 1];
                srcY4 = (float)pd.ProductCoordinates[3, 1];

                src = new PointF[] {
                new PointF { X = srcX1, Y = srcY1 },
                new PointF { X = srcX2, Y = srcY2 },
                new PointF { X = srcX3, Y = srcY3 },
                new PointF { X = srcX4, Y = srcY4 }
                };

                XAdditional1 = ((float)md.MemberCoordinates[7, 0] - srcX1);
                XAdditional2 = ((float)md.MemberCoordinates[15, 0] - 12 - srcX2);
                XAdditional3 = ((float)md.MemberCoordinates[8, 0] + 12 - srcX3);
                YAdditional1 = ((float)md.MemberCoordinates[7, 1] - srcY1);
                YAdditional2 = ((float)md.MemberCoordinates[15, 1] - srcY2);
                YAdditional3 = ((float)md.MemberCoordinates[8, 1] - srcY3);

                md.MemberCoordinates[7, 0] -= XAdditional1;
                md.MemberCoordinates[15, 0] -= XAdditional1;
                md.MemberCoordinates[8, 0] -= XAdditional1;
                md.MemberCoordinates[7, 1] -= YAdditional1;
                md.MemberCoordinates[15, 1] -= YAdditional1;
                md.MemberCoordinates[8, 1] -= YAdditional1;

                dstX1 = srcX1;
                dstX2 = srcX4;

                //if strectch/shrink applies
                if ((((float)md.MemberCoordinates[8, 0] + 12 - (float)md.MemberCoordinates[15, 0] - 12) - (srcX4 - srcX3)) == 0)
                {
                    dstX3 = srcX3;
                    dstX4 = srcX4;
                    //dstX2 = dstX4;
                }
                else if (((float)md.MemberCoordinates[8, 0] + 12 - (float)md.MemberCoordinates[15, 0] - 12 > (srcX4 - srcX3)))
                {
                    //dstX3 = (srcX3 - ((((float)md.MemberCoordinates[8, 0] + 12 - (float)md.MemberCoordinates[15, 0] - 12) - (srcX4 - srcX3)) / 2));
                    //dstX4 = (srcX4 + ((((float)md.MemberCoordinates[8, 0] + 12 - (float)md.MemberCoordinates[15, 0] - 12) - (srcX4 - srcX3)) / 2));
                    ////dstX2 = srcX2 + ((((float)md.MemberCoordinates[8, 0] + 12 - (float)md.MemberCoordinates[15, 0] - 12) - (srcX4 - srcX3)));
                    //dstX1 = srcX1 - ((((float)md.MemberCoordinates[8, 0] + 12 - (float)md.MemberCoordinates[15, 0] - 12) - (srcX4 - srcX3)));

                    dstX3 = (srcX3) + ((float)md.MemberCoordinates[15, 0] - 12 - (srcX3));
                    dstX4 = (srcX4) + ((float)md.MemberCoordinates[8, 0] + 12 - (srcX4));
                }
                else if ((srcX4 - srcX3) > ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[8, 0] + 12))
                {
                    dstX3 = (srcX3 + ((((srcX4 - srcX3) - ((float)md.MemberCoordinates[8, 0] + 12 - (float)md.MemberCoordinates[15, 0] - 12))) / 2));
                    dstX4 = (srcX4 - ((((srcX4 - srcX3) - ((float)md.MemberCoordinates[8, 0] + 12 - (float)md.MemberCoordinates[15, 0] - 12))) / 2));
                    //dstX2 = srcX2 - ((((float)md.MemberCoordinates[8, 0] + 12 - (float)md.MemberCoordinates[15, 0] - 12) - (srcX4 - srcX3)));
                    dstX1 = srcX1 + ((((srcX4 - srcX3) - ((float)md.MemberCoordinates[8, 0] + 12 - (float)md.MemberCoordinates[15, 0] - 12))));
                }
                else
                {
                    dstX3 = srcX3;
                    dstX4 = srcX4;
                    //dstX2 = dstX4;
                }

                //if strectch/shrink applies
                //if ((((float)md.MemberCoordinates[7, 0] - (float)md.MemberCoordinates[15, 0] - 12) - (srcX4 - srcX3)) == 0)
                //{
                //    dstX4 = srcX4;
                //}
                //else if ((float)md.MemberCoordinates[7, 0] > (float)md.MemberCoordinates[15, 0] - 12)
                //{
                //    dstX4 = (srcX4 - ((float)md.MemberCoordinates[7, 0] - (float)md.MemberCoordinates[15, 0] - 12));
                //}
                //else if ((float)md.MemberCoordinates[15, 0] - 12 > (float)md.MemberCoordinates[7, 0])
                //{
                //    dstX4 = (srcX4 + ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[7, 0]));
                //}
                //else
                //{
                //    dstX4 = srcX4;
                //}

                dstY1 = srcY1;
                dstY2 = srcY2;

                //if strectch/shrink applies
                if (
                    (((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]) - (srcY4 - srcY2)) == 0
                    &&
                    (((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]) - (srcY3 - srcY1)) == 0
                    )
                {
                    dstY3 = srcY3;
                    dstY4 = srcY4;
                }
                else
                {
                    if (((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1] > (srcY3 - srcY1)))
                    {
                        //dstY3 = (srcY3 + ((((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]) - (srcY3 - srcY1))));

                        dstY3 = (srcY3) - ((srcY3) - (float)md.MemberCoordinates[15, 1]);
                    }
                    else if ((srcY3 - srcY1) > ((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]))
                    {
                        dstY3 = (srcY3 - ((((srcY3 - srcY1) - ((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1])))));
                    }
                    else
                    {
                        dstY3 = srcY3;
                    }

                    if (((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1] > (srcY4 - srcY2)))
                    {
                        //dstY4 = (srcY4 + ((((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]) - (srcY4 - srcY2))));

                        dstY4 = (srcY4) - ((srcY4) - (float)md.MemberCoordinates[8, 1]);
                    }
                    else if ((srcY4 - srcY2) > ((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]))
                    {
                        dstY4 = (srcY4 - ((((srcY4 - srcY2) - ((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1])))));
                    }
                    else
                    {
                        dstY4 = srcY4;
                    }
                }

                //if strectch/shrink applies
                //if ((((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]) - (srcY3 - srcY1)) == 0)
                //{
                //    dstY3 = srcY3;
                //}
                //else if (((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1] > (srcY3 - srcY1)))
                //{
                //    dstY3 = (srcY3 + ((((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1])) - (srcY3 - srcY1)));
                //}
                //else if ((srcY3 - srcY1) > ((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]))
                //{
                //    dstY3 = (srcY3 - (((srcY3 - srcY1) - ((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]))));
                //}
                //else
                //{
                //    dstY3 = srcY3;
                //}

                ////if strectch/shrink applies
                //if ((((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]) - (srcY4 - srcY2)) == 0)
                //{
                //    dstY4 = srcY4;
                //}
                //else if (((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1] > (srcY4 - srcY2)))
                //{
                //    dstY4 = (srcY4 + ((((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1])) - (srcY4 - srcY2)));
                //}
                //else if ((srcY4 - srcY2) > ((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]))
                //{
                //    dstY4 = (srcY4 - (((srcY4 - srcY2) - ((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]))));
                //}
                //else
                //{
                //    dstY4 = srcY4;
                //}

                dst = new PointF[] {
                new PointF { X = dstX1, Y = dstY1 }, //0, 0
                new PointF { X = dstX2, Y = dstY2 }, //1368, 786
                new PointF { X = dstX3, Y = dstY3 }, //1110, 1145
                new PointF { X = dstX4, Y = dstY4 }, //1326, 1230
                //new PointF { X = ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[8, 0] + 12) > (src[3].X - src[2].X) ? (src[2].X - ((float)md.MemberCoordinates[8, 0] + 12 - src[2].X)) : src[2].X, Y = 1145 }, //1110
                //new PointF { X = ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[8, 0] + 12) > (src[3].X - src[2].X) ? (src[3].X - ((float)md.MemberCoordinates[15, 0] - 12 - src[3].X)) : src[3].X, Y = 1230 } //1326
                };

                minSrcX = Convert.ToInt32(src.Min(a => a.X));
                maxSrcX = Convert.ToInt32(src.Max(a => a.X));
                maxDstX = Convert.ToInt32(dst.Max(a => a.X));
                minSrcY = Convert.ToInt32(src.Min(a => a.Y));
                maxSrcY = Convert.ToInt32(src.Max(a => a.Y));
                maxDstY = Convert.ToInt32(dst.Max(a => a.Y));
                dstWidth = maxDstX;
                dstHeight = maxDstY;
                //dstWidth = (maxDstX - minSrcX) > (maxSrcX - minSrcX) ? img_s.Width + (maxDstX - maxSrcX) : img_s.Width;
                //dstHeight = (maxDstY - minSrcY) > (maxSrcY - minSrcY) ? img_s.Height + (maxDstY - maxSrcY) : img_s.Height;

                img_d = new Mat();
                warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
                CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(dstWidth, dstHeight), Inter.Linear, Warp.Default, BorderType.Transparent);

                img_d.Bitmap.Save("c:\\projects\\" + md.MemberID + "_" + pd.ProductID + "_slice_rightshoulder_warpPerspective.png");

                md.MemberCoordinates[7, 0] += XAdditional1;
                md.MemberCoordinates[15, 0] += XAdditional1;
                md.MemberCoordinates[8, 0] += XAdditional1;
                md.MemberCoordinates[7, 1] += YAdditional1;
                md.MemberCoordinates[15, 1] += YAdditional1;
                md.MemberCoordinates[8, 1] += YAdditional1;

                ResetCoordinates();

                img_s.Dispose();

                #endregion

                #region "RIGHT FOREARM TO ARM WARPING"
                //////--------------FOREARM TO ARM WARPING--------------------///////////////
                pd = pds.Where(a => a.ProductSection == "Right Arm").SingleOrDefault();

                //pd.ProductCoordinates[0, 0] += 72;
                //pd.ProductCoordinates[1, 0] += 72;
                //pd.ProductCoordinates[2, 0] += 72;
                //pd.ProductCoordinates[3, 0] += 72;
                //pd.ProductCoordinates[0, 1] += 450.91;
                //pd.ProductCoordinates[1, 1] += 450.91;
                //pd.ProductCoordinates[2, 1] += 450.91;
                //pd.ProductCoordinates[3, 1] += 450.91;

                img_s = new Image<Bgra, byte>("c:\\projects\\" + pd.ProductID + "_slice_rightarmToForeArm.png");

                srcX1 = (float)pd.ProductCoordinates[0, 0];
                srcX2 = (float)pd.ProductCoordinates[1, 0];
                srcX3 = (float)pd.ProductCoordinates[2, 0];
                srcX4 = (float)pd.ProductCoordinates[3, 0];
                srcY1 = (float)pd.ProductCoordinates[0, 1];
                srcY2 = (float)pd.ProductCoordinates[1, 1];
                srcY3 = (float)pd.ProductCoordinates[2, 1];
                srcY4 = (float)pd.ProductCoordinates[3, 1];

                src = new PointF[] {
                new PointF { X = srcX1, Y = srcY1 },
                new PointF { X = srcX2, Y = srcY2 },
                new PointF { X = srcX3, Y = srcY3 },
                new PointF { X = srcX4, Y = srcY4 }
                };

                XAdditional1 = ((float)md.MemberCoordinates[15, 0] - 12 - srcX1);
                YAdditional1 = ((float)md.MemberCoordinates[15, 1] - srcY1);

                md.MemberCoordinates[15, 0] -= XAdditional1;
                md.MemberCoordinates[8, 0] -= XAdditional1;
                md.MemberCoordinates[14, 0] -= XAdditional1;
                md.MemberCoordinates[9, 0] -= XAdditional1;
                md.MemberCoordinates[15, 1] -= YAdditional1;
                md.MemberCoordinates[8, 1] -= YAdditional1;
                md.MemberCoordinates[14, 1] -= YAdditional1;
                md.MemberCoordinates[9, 1] -= YAdditional1;

                //if strectch/shrink applies
                if (
                    (((float)md.MemberCoordinates[8, 0] + 12 - (float)md.MemberCoordinates[15, 0] - 12) - (srcX2 - srcX1)) == 0
                    &&
                    (((float)md.MemberCoordinates[9, 0] + 12 - (float)md.MemberCoordinates[14, 0] - 12) - (srcX4 - srcX3)) == 0
                    )
                {
                    dstX1 = srcX1;
                    dstX2 = srcX2;
                    dstX3 = srcX3;
                    dstX4 = srcX4;
                    //dstX2 = dstX4;
                }
                else
                {
                    if (((dstX4 - dstX3) > (srcX2 - srcX1)))
                    {
                        //dstX1 = (srcX1 - (((dstX4 - dstX3) - (srcX2 - srcX1)) / 2));
                        //dstX2 = (srcX2 + (((dstX4 - dstX3) - (srcX2 - srcX1)) / 2));
                        dstX1 = srcX1 + ((float)md.MemberCoordinates[15, 0] - 12 - (srcX1));
                        dstX2 = ((srcX2) + ((float)md.MemberCoordinates[8, 0] + 12 - srcX2));
                    }
                    else if ((srcX2 - srcX1) > (dstX4 - dstX3))
                    {
                        dstX1 = (srcX1 + (((srcX2 - srcX1) - (dstX4 - dstX3)) / 2));
                        dstX2 = (srcX2 - (((srcX2 - srcX1) - (dstX4 - dstX3)) / 2));
                    }
                    else
                    {
                        dstX1 = srcX1;
                        dstX2 = srcX2;
                    }

                    if ((((float)md.MemberCoordinates[9, 0] + 12 - (float)md.MemberCoordinates[14, 0] - 12) > (srcX4 - srcX3)))
                    {
                        dstX3 = (srcX3 - ((((float)md.MemberCoordinates[9, 0] + 12 - (float)md.MemberCoordinates[14, 0] - 12) - (srcX4 - srcX3)) / 2));
                        dstX4 = (srcX4 + ((((float)md.MemberCoordinates[9, 0] + 12 - (float)md.MemberCoordinates[14, 0] - 12) - (srcX4 - srcX3)) / 2));
                    }
                    else if ((srcX4 - srcX3) > ((float)md.MemberCoordinates[9, 0] + 12 - (float)md.MemberCoordinates[14, 0] - 12))
                    {
                        //dstX3 = (srcX3 + ((((srcX4 - srcX3) - ((float)md.MemberCoordinates[9, 0] + 12 - (float)md.MemberCoordinates[14, 0] - 12))) / 2));
                        //dstX4 = (srcX4 - ((((srcX4 - srcX3) - ((float)md.MemberCoordinates[9, 0] + 12 - (float)md.MemberCoordinates[14, 0] - 12))) / 2));
                        //dstX3 = srcX3 + ((float)md.MemberCoordinates[14, 0] - 12 - (float)md.MemberCoordinates[15, 0] - 12) - (srcX3 - srcX1);
                        //dstX4 = srcX4 + ((float)md.MemberCoordinates[9, 0] + 12 - (float)md.MemberCoordinates[14, 0] - 12) + (dstX3 - srcX3);
                        dstX3 = (srcX3) + ((float)md.MemberCoordinates[14, 0] - 12 - (srcX3));
                        dstX4 = (srcX4) + ((float)md.MemberCoordinates[9, 0] + 12 - (srcX4));
                    }
                    else
                    {
                        dstX3 = srcX3;
                        dstX4 = srcX4;
                    }
                }

                //if strectch/shrink applies
                //if ((((float)md.MemberCoordinates[7, 0] - (float)md.MemberCoordinates[15, 0] - 12) - (srcX4 - srcX3)) == 0)
                //{
                //    dstX4 = srcX4;
                //}
                //else if ((float)md.MemberCoordinates[7, 0] > (float)md.MemberCoordinates[15, 0] - 12)
                //{
                //    dstX4 = (srcX4 - ((float)md.MemberCoordinates[7, 0] - (float)md.MemberCoordinates[15, 0] - 12));
                //}
                //else if ((float)md.MemberCoordinates[15, 0] - 12 > (float)md.MemberCoordinates[7, 0])
                //{
                //    dstX4 = (srcX4 + ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[7, 0]));
                //}
                //else
                //{
                //    dstX4 = srcX4;
                //}

                if ((float)md.MemberCoordinates[15, 1] > (float)md.MemberCoordinates[8, 1])
                {
                    dstY1 = srcY1;
                    dstY2 = ((srcY2) - ((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[8, 1]));
                }
                else
                {
                    dstY1 = (srcY1 - ((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[15, 1]));
                    dstY2 = srcY2;
                }


                //if strectch/shrink applies
                if (
                    (((float)md.MemberCoordinates[9, 1] - (float)md.MemberCoordinates[8, 1]) - (srcY4 - srcY2)) == 0
                    &&
                    (((float)md.MemberCoordinates[14, 1] - (float)md.MemberCoordinates[15, 1]) - (srcY3 - srcY1)) == 0
                    )
                {
                    dstY3 = srcY3;
                    dstY4 = srcY4;
                }
                else
                {
                    if (((float)md.MemberCoordinates[14, 1] - (float)md.MemberCoordinates[15, 1] > (srcY3 - srcY1)))
                    {
                        dstY3 = (srcY3 + ((((float)md.MemberCoordinates[14, 1] - (float)md.MemberCoordinates[15, 1]) - (srcY3 - srcY1))));
                    }
                    else if ((srcY3 - srcY1) > ((float)md.MemberCoordinates[14, 1] - (float)md.MemberCoordinates[15, 1]))
                    {
                        //dstY3 = (srcY3 - ((((srcY3 - srcY1) - ((float)md.MemberCoordinates[14, 1] - (float)md.MemberCoordinates[15, 1])))));
                        dstY3 = (srcY3) - ((srcY3) - (float)md.MemberCoordinates[14, 1]);
                    }
                    else
                    {
                        dstY3 = srcY3;
                    }

                    if (((float)md.MemberCoordinates[9, 1] - (float)md.MemberCoordinates[8, 1] > (srcY4 - dstY2)))
                    {
                        //dstY4 = (srcY4 + ((((float)md.MemberCoordinates[9, 1] - (float)md.MemberCoordinates[8, 1]) - (srcY4 - dstY2))));
                        dstY4 = (srcY4) - ((srcY4) - (float)md.MemberCoordinates[9, 1]);
                    }
                    else if ((srcY4 - dstY2) > ((float)md.MemberCoordinates[9, 1] - (float)md.MemberCoordinates[8, 1]))
                    {
                        dstY4 = (srcY4) - ((srcY4) - (float)md.MemberCoordinates[9, 1]);
                    }
                    else
                    {
                        dstY4 = srcY4;
                    }
                }

                //if strectch/shrink applies
                //if ((((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]) - (srcY3 - srcY1)) == 0)
                //{
                //    dstY3 = srcY3;
                //}
                //else if (((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1] > (srcY3 - srcY1)))
                //{
                //    dstY3 = (srcY3 + ((((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1])) - (srcY3 - srcY1)));
                //}
                //else if ((srcY3 - srcY1) > ((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]))
                //{
                //    dstY3 = (srcY3 - (((srcY3 - srcY1) - ((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]))));
                //}
                //else
                //{
                //    dstY3 = srcY3;
                //}

                ////if strectch/shrink applies
                //if ((((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]) - (srcY4 - srcY2)) == 0)
                //{
                //    dstY4 = srcY4;
                //}
                //else if (((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1] > (srcY4 - srcY2)))
                //{
                //    dstY4 = (srcY4 + ((((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1])) - (srcY4 - srcY2)));
                //}
                //else if ((srcY4 - srcY2) > ((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]))
                //{
                //    dstY4 = (srcY4 - (((srcY4 - srcY2) - ((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]))));
                //}
                //else
                //{
                //    dstY4 = srcY4;
                //}

                dst = new PointF[] {
                new PointF { X = dstX1, Y = dstY1 }, //0, 0
                new PointF { X = dstX2, Y = dstY2 }, //1368, 786
                new PointF { X = dstX3, Y = dstY3 }, //1110, 1145
                new PointF { X = dstX4, Y = dstY4 }, //1326, 1230
                //new PointF { X = ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[8, 0] + 12) > (src[3].X - src[2].X) ? (src[2].X - ((float)md.MemberCoordinates[8, 0] + 12 - src[2].X)) : src[2].X, Y = 1145 }, //1110
                //new PointF { X = ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[8, 0] + 12) > (src[3].X - src[2].X) ? (src[3].X - ((float)md.MemberCoordinates[15, 0] - 12 - src[3].X)) : src[3].X, Y = 1230 } //1326
                };

                minSrcX = Convert.ToInt32(src.Min(a => a.X));
                maxSrcX = Convert.ToInt32(src.Max(a => a.X));
                maxDstX = Convert.ToInt32(dst.Max(a => a.X));
                minSrcY = Convert.ToInt32(src.Min(a => a.Y));
                maxSrcY = Convert.ToInt32(src.Max(a => a.Y));
                maxDstY = Convert.ToInt32(dst.Max(a => a.Y));
                dstWidth = maxDstX;
                dstHeight = maxDstY;
                //dstWidth = (maxDstX - minSrcX) > (maxSrcX - minSrcX) ? img_s.Width + (maxDstX - maxSrcX) : img_s.Width;
                //dstHeight = (maxDstY - minSrcY) > (maxSrcY - minSrcY) ? img_s.Height + (maxDstY - maxSrcY) : img_s.Height;

                img_d = new Mat();
                warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
                CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(dstWidth, dstHeight), Inter.Linear, Warp.Default, BorderType.Transparent);

                img_d.Bitmap.Save("c:\\projects\\" + md.MemberID + "_" + pd.ProductID + "_slice_rightarmToForeArm_warpPerspective.png");

                md.MemberCoordinates[15, 0] += XAdditional1;
                md.MemberCoordinates[8, 0] += XAdditional1;
                md.MemberCoordinates[14, 0] += XAdditional1;
                md.MemberCoordinates[9, 0] += XAdditional1;
                md.MemberCoordinates[15, 1] += YAdditional1;
                md.MemberCoordinates[8, 1] += YAdditional1;
                md.MemberCoordinates[14, 1] += YAdditional1;
                md.MemberCoordinates[9, 1] += YAdditional1;

                ResetCoordinates();

                img_s.Dispose();

                #endregion

                #region "RIGHT ARM TO WRIST WARPING"
                //////--------------ARM TO WRIST WARPING--------------------///////////////
                pd = pds.Where(a => a.ProductSection == "Right Wrist").SingleOrDefault();

                img_s = new Image<Bgra, byte>("c:\\projects\\" + pd.ProductID + "_slice_rightForeArmToWrist.png");

                srcX1 = (float)pd.ProductCoordinates[0, 0];
                srcX2 = (float)pd.ProductCoordinates[1, 0];
                srcX3 = (float)pd.ProductCoordinates[2, 0];
                srcX4 = (float)pd.ProductCoordinates[3, 0];
                srcY1 = (float)pd.ProductCoordinates[0, 1];
                srcY2 = (float)pd.ProductCoordinates[1, 1];
                srcY3 = (float)pd.ProductCoordinates[2, 1];
                srcY4 = (float)pd.ProductCoordinates[3, 1];

                src = new PointF[] {
                new PointF { X = srcX1, Y = srcY1 },
                new PointF { X = srcX2, Y = srcY2 },
                new PointF { X = srcX3, Y = srcY3 },
                new PointF { X = srcX4, Y = srcY4 }
                };

                XAdditional1 = ((float)md.MemberCoordinates[14, 0] - 12 - srcX1);
                YAdditional1 = ((float)md.MemberCoordinates[14, 1] - srcY1);

                md.MemberCoordinates[14, 0] -= XAdditional1;
                md.MemberCoordinates[9, 0] -= XAdditional1;
                md.MemberCoordinates[13, 0] -= XAdditional1;
                md.MemberCoordinates[10, 0] -= XAdditional1;
                md.MemberCoordinates[14, 1] -= YAdditional1;
                md.MemberCoordinates[9, 1] -= YAdditional1;
                md.MemberCoordinates[13, 1] -= YAdditional1;
                md.MemberCoordinates[10, 1] -= YAdditional1;

                //if strectch/shrink applies
                if (
                    (((float)md.MemberCoordinates[9, 0] + 12 - (float)md.MemberCoordinates[14, 0] - 12) - (srcX2 - srcX1)) == 0
                    &&
                    (((float)md.MemberCoordinates[10, 0] + 12 - (float)md.MemberCoordinates[13, 0] - 12) - (srcX4 - srcX3)) == 0
                    )
                {
                    dstX1 = srcX1;
                    dstX2 = srcX2;
                    dstX3 = srcX3;
                    dstX4 = srcX4;
                    //dstX2 = dstX4;
                }
                else
                {
                    if (((dstX4 - dstX3) > (srcX2 - srcX1)))
                    {
                        //dstX1 = (srcX1 - (((dstX4 - dstX3) - (srcX2 - srcX1)) / 2));
                        //dstX2 = (srcX2 + (((dstX4 - dstX3) - (srcX2 - srcX1)) / 2));
                        dstX1 = srcX1 + ((float)md.MemberCoordinates[14, 0] - 12 - (srcX1));
                        dstX2 = ((srcX2) + ((float)md.MemberCoordinates[9, 0] + 12 - srcX2));
                    }
                    else if ((srcX2 - srcX1) > (dstX4 - dstX3))
                    {
                        //dstX1 = (srcX1 + (((srcX2 - srcX1) - (dstX4 - dstX3)) / 2));
                        //dstX2 = (srcX2 - (((srcX2 - srcX1) - (dstX4 - dstX3)) / 2));

                        dstX1 = srcX1 + ((float)md.MemberCoordinates[14, 0] - 12 - (srcX1));
                        dstX2 = ((srcX2) + ((float)md.MemberCoordinates[9, 0] + 12 - srcX2));
                    }
                    else
                    {
                        dstX1 = srcX1;
                        dstX2 = srcX2;
                    }

                    if ((((float)md.MemberCoordinates[10, 0] + 12 - (float)md.MemberCoordinates[13, 0] - 12) > (srcX4 - srcX3)))
                    {
                        dstX3 = (srcX3 - ((((float)md.MemberCoordinates[9, 0] + 12 - (float)md.MemberCoordinates[14, 0] - 12) - (srcX4 - srcX3)) / 2));
                        dstX4 = (srcX4 + ((((float)md.MemberCoordinates[9, 0] + 12 - (float)md.MemberCoordinates[14, 0] - 12) - (srcX4 - srcX3)) / 2));
                    }
                    else if ((srcX4 - srcX3) > ((float)md.MemberCoordinates[10, 0] + 12 - (float)md.MemberCoordinates[13, 0] - 12))
                    {
                        //dstX3 = (srcX3 + ((((srcX4 - srcX3) - ((float)md.MemberCoordinates[9, 0] + 12 - (float)md.MemberCoordinates[14, 0] - 12))) / 2));
                        //dstX4 = (srcX4 - ((((srcX4 - srcX3) - ((float)md.MemberCoordinates[9, 0] + 12 - (float)md.MemberCoordinates[14, 0] - 12))) / 2));
                        //dstX3 = srcX3 + ((float)md.MemberCoordinates[14, 0] - 12 - (float)md.MemberCoordinates[15, 0] - 12) - (srcX3 - srcX1);
                        //dstX4 = srcX4 + ((float)md.MemberCoordinates[9, 0] + 12 - (float)md.MemberCoordinates[14, 0] - 12) + (dstX3 - srcX3);

                        //Loose Sleeves
                        if ((srcX4 - srcX3) > ((float)md.MemberCoordinates[10, 0] + 12 - (float)md.MemberCoordinates[13, 0] - 12) && ((srcX4 - srcX3) - ((float)md.MemberCoordinates[10, 0] + 12 - (float)md.MemberCoordinates[13, 0] - 12)) > 100)
                        {
                            dstX3 = (srcX3) + ((srcX4 - srcX3) / 2);
                            dstX4 = dstX3 + (srcX4 - srcX3);
                        }
                        else
                        {
                            dstX3 = (srcX3) + ((float)md.MemberCoordinates[13, 0] - 12 - (srcX3));
                            dstX4 = (srcX4) + ((float)md.MemberCoordinates[10, 0] + 12 - (srcX4));
                        }
                    }
                    else
                    {
                        dstX3 = srcX3;
                        dstX4 = srcX4;
                    }
                }

                //if strectch/shrink applies
                //if ((((float)md.MemberCoordinates[7, 0] - (float)md.MemberCoordinates[15, 0] - 12) - (srcX4 - srcX3)) == 0)
                //{
                //    dstX4 = srcX4;
                //}
                //else if ((float)md.MemberCoordinates[7, 0] > (float)md.MemberCoordinates[15, 0] - 12)
                //{
                //    dstX4 = (srcX4 - ((float)md.MemberCoordinates[7, 0] - (float)md.MemberCoordinates[15, 0] - 12));
                //}
                //else if ((float)md.MemberCoordinates[15, 0] - 12 > (float)md.MemberCoordinates[7, 0])
                //{
                //    dstX4 = (srcX4 + ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[7, 0]));
                //}
                //else
                //{
                //    dstX4 = srcX4;
                //}

                if ((float)md.MemberCoordinates[14, 1] > (float)md.MemberCoordinates[9, 1])
                {
                    dstY1 = srcY1;
                    dstY2 = ((srcY2) - ((float)md.MemberCoordinates[14, 1] - (float)md.MemberCoordinates[9, 1]));
                }
                else
                {
                    dstY1 = (srcY1 - ((float)md.MemberCoordinates[9, 1] - (float)md.MemberCoordinates[14, 1]));
                    dstY2 = srcY2;
                }


                //if strectch/shrink applies
                if (
                    (((float)md.MemberCoordinates[10, 1] - (float)md.MemberCoordinates[9, 1]) - (srcY4 - srcY2)) == 0
                    &&
                    (((float)md.MemberCoordinates[13, 1] - (float)md.MemberCoordinates[14, 1]) - (srcY3 - srcY1)) == 0
                    )
                {
                    dstY3 = srcY3;
                    dstY4 = srcY4;
                }
                else
                {
                    if (((float)md.MemberCoordinates[13, 1] - (float)md.MemberCoordinates[14, 1] > (srcY3 - srcY1)))
                    {
                        //dstY3 = (srcY3 + ((((float)md.MemberCoordinates[13, 1] - (float)md.MemberCoordinates[14, 1]) - (srcY3 - srcY1))));

                        //Short Sleeves
                        if ((srcY3 - srcY1) > ((float)md.MemberCoordinates[13, 1] - (float)md.MemberCoordinates[14, 1]))
                            dstY3 = (srcY3) - ((srcY3) - (float)md.MemberCoordinates[13, 1]);
                        else
                            dstY3 = dstY1 + (srcY3 - srcY1);
                    }
                    else if ((srcY3 - srcY1) > ((float)md.MemberCoordinates[13, 1] - (float)md.MemberCoordinates[14, 1]))
                    {
                        //dstY3 = (srcY3 - ((((srcY3 - srcY1) - ((float)md.MemberCoordinates[14, 1] - (float)md.MemberCoordinates[15, 1])))));

                        //Short Sleeves
                        if ((srcY3 - srcY1) > ((float)md.MemberCoordinates[13, 1] - (float)md.MemberCoordinates[14, 1]))
                            dstY3 = (srcY3) - ((srcY3) - (float)md.MemberCoordinates[13, 1]);
                        else
                            dstY3 = dstY1 + (srcY3 - srcY1);
                    }
                    else
                    {
                        dstY3 = srcY3;
                    }

                    if (((float)md.MemberCoordinates[10, 1] - (float)md.MemberCoordinates[9, 1] > (srcY4 - dstY2)))
                    {
                        //dstY4 = (srcY4 + ((((float)md.MemberCoordinates[9, 1] - (float)md.MemberCoordinates[8, 1]) - (srcY4 - dstY2))));

                        //Short Sleeves
                        if ((srcY4 - srcY2) > ((float)md.MemberCoordinates[10, 1] - (float)md.MemberCoordinates[9, 1]))
                            dstY4 = (srcY4) - ((srcY4) - (float)md.MemberCoordinates[10, 1]);
                        else
                            dstY4 = dstY2 + (srcY4 - srcY2);
                    }
                    else if ((srcY4 - dstY2) > ((float)md.MemberCoordinates[10, 1] - (float)md.MemberCoordinates[9, 1]))
                    {
                        //dstY4 = (srcY4) - ((srcY4) - (float)md.MemberCoordinates[10, 1]);

                        //Short Sleeves
                        if ((srcY4 - srcY2) < ((float)md.MemberCoordinates[10, 1] - (float)md.MemberCoordinates[9, 1]))
                            dstY4 = (srcY4) - ((srcY4) - (float)md.MemberCoordinates[10, 1]);
                        else
                            dstY4 = dstY2 + (srcY4 - srcY2);
                    }
                    else
                    {
                        dstY4 = srcY4;
                    }
                }

                //if strectch/shrink applies
                //if ((((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]) - (srcY3 - srcY1)) == 0)
                //{
                //    dstY3 = srcY3;
                //}
                //else if (((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1] > (srcY3 - srcY1)))
                //{
                //    dstY3 = (srcY3 + ((((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1])) - (srcY3 - srcY1)));
                //}
                //else if ((srcY3 - srcY1) > ((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]))
                //{
                //    dstY3 = (srcY3 - (((srcY3 - srcY1) - ((float)md.MemberCoordinates[15, 1] - (float)md.MemberCoordinates[7, 1]))));
                //}
                //else
                //{
                //    dstY3 = srcY3;
                //}

                ////if strectch/shrink applies
                //if ((((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]) - (srcY4 - srcY2)) == 0)
                //{
                //    dstY4 = srcY4;
                //}
                //else if (((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1] > (srcY4 - srcY2)))
                //{
                //    dstY4 = (srcY4 + ((((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1])) - (srcY4 - srcY2)));
                //}
                //else if ((srcY4 - srcY2) > ((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]))
                //{
                //    dstY4 = (srcY4 - (((srcY4 - srcY2) - ((float)md.MemberCoordinates[8, 1] - (float)md.MemberCoordinates[7, 1]))));
                //}
                //else
                //{
                //    dstY4 = srcY4;
                //}

                dst = new PointF[] {
                new PointF { X = dstX1, Y = dstY1 }, //0, 0
                new PointF { X = dstX2, Y = dstY2 }, //1368, 786
                new PointF { X = dstX3, Y = dstY3 }, //1110, 1145
                new PointF { X = dstX4, Y = dstY4 }, //1326, 1230
                //new PointF { X = ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[8, 0] + 12) > (src[3].X - src[2].X) ? (src[2].X - ((float)md.MemberCoordinates[8, 0] + 12 - src[2].X)) : src[2].X, Y = 1145 }, //1110
                //new PointF { X = ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[8, 0] + 12) > (src[3].X - src[2].X) ? (src[3].X - ((float)md.MemberCoordinates[15, 0] - 12 - src[3].X)) : src[3].X, Y = 1230 } //1326
                };

                minSrcX = Convert.ToInt32(src.Min(a => a.X));
                maxSrcX = Convert.ToInt32(src.Max(a => a.X));
                maxDstX = Convert.ToInt32(dst.Max(a => a.X));
                minSrcY = Convert.ToInt32(src.Min(a => a.Y));
                maxSrcY = Convert.ToInt32(src.Max(a => a.Y));
                maxDstY = Convert.ToInt32(dst.Max(a => a.Y));
                dstWidth = maxDstX;
                dstHeight = maxDstY;
                //dstWidth = (maxDstX - minSrcX) > (maxSrcX - minSrcX) ? img_s.Width + (maxDstX - maxSrcX) : img_s.Width;
                //dstHeight = (maxDstY - minSrcY) > (maxSrcY - minSrcY) ? img_s.Height + (maxDstY - maxSrcY) : img_s.Height;

                img_d = new Mat();
                warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
                CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(dstWidth, dstHeight), Inter.Linear, Warp.Default, BorderType.Transparent);

                img_d.Bitmap.Save("c:\\projects\\" + md.MemberID + "_" + pd.ProductID + "_slice_rightForeArmToWrist_warpPerspective.png");

                md.MemberCoordinates[14, 0] += XAdditional1;
                md.MemberCoordinates[9, 0] += XAdditional1;
                md.MemberCoordinates[13, 0] += XAdditional1;
                md.MemberCoordinates[10, 0] += XAdditional1;
                md.MemberCoordinates[14, 1] += YAdditional1;
                md.MemberCoordinates[9, 1] += YAdditional1;
                md.MemberCoordinates[13, 1] += YAdditional1;
                md.MemberCoordinates[10, 1] += YAdditional1;

                ResetCoordinates();

                img_s.Dispose();

                #endregion

                #region "SHOULDERS TO CHEST WARPING"

                //////--------------SHOULDERS TO CHEST WARPING--------------------///////////////
                img_s = new Image<Bgra, byte>("c:\\projects\\" + pd.ProductID + "_slice_SholuderToChest.png");

                src = new PointF[] {
                new PointF { X = 0, Y = 790 },
                new PointF { X = 709, Y = 782 },
                new PointF { X = 0, Y = 1093 },
                new PointF { X = 709, Y = 1093 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 57, Y = img_s.Height },
                //new PointF { X = 296, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                // note even with a 4th term in here and dst, same error arises....
                };

                dst = new PointF[] {
                new PointF { X = 32, Y = 790 },
                new PointF { X = 657, Y = 804 },
                new PointF { X = 25, Y = 1093 },
                new PointF { X = 655, Y = 1093 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 67, Y = img_s.Height },
                //new PointF { X = 286, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                };

                img_d = new Mat();
                warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
                CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(Convert.ToInt32(dst.Max(a => a.X)), Convert.ToInt32(dst.Max(a => a.Y))), Inter.Linear, Warp.Default, BorderType.Transparent);

                img_d.Bitmap.Save("c:\\projects\\712_slice_SholuderToChest_warpPerspective.png");

                img_s.Dispose();

                #endregion

                #region "CHEST TO WAIST WARPING"

                //////--------------CHEST TO WAIST WARPING--------------------///////////////
                img_s = new Image<Bgra, byte>("c:\\projects\\712_slice_ChestToWaist.png");

                src = new PointF[] {
                new PointF { X = 0, Y = 0 },
                new PointF { X = 702, Y = 0 },
                new PointF { X = 40, Y = 396 },
                new PointF { X = 688, Y = 396 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 57, Y = img_s.Height },
                //new PointF { X = 296, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                // note even with a 4th term in here and dst, same error arises....
                };

                dst = new PointF[] {
                new PointF { X = 28, Y = 0 },
                new PointF { X = 28 + dst[3].X - dst[2].X, Y = 0 }, //660
                new PointF { X = 59, Y = 386 },
                new PointF { X = 644, Y = 386 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 67, Y = img_s.Height },
                //new PointF { X = 286, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                };

                img_d = new Mat();
                warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
                CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(Convert.ToInt32(dst.Max(a => a.X)), Convert.ToInt32(dst.Max(a => a.Y))), Inter.Linear, Warp.Default, BorderType.Transparent);

                img_d.Bitmap.Save("c:\\projects\\712_slice_ChestToWaist_warpPerspective.png");

                img_s.Dispose();

                #endregion


                //////--------------WAIST TO ABDOMEN WARPING--------------------///////////////
                img_s = new Image<Bgra, byte>("c:\\projects\\712_slice_WaistToAbdomen.png");

                src = new PointF[] {
                new PointF { X = 39, Y = 0 },
                new PointF { X = 690, Y = 0 },
                new PointF { X = 33, Y = 137 },
                new PointF { X = 702, Y = 137 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 57, Y = img_s.Height },
                //new PointF { X = 296, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                // note even with a 4th term in here and dst, same error arises....
                };

                dst = new PointF[] {
                new PointF { X = 80, Y = 0 },
                new PointF { X = 80 + dst[3].X - dst[2].X, Y = 0 }, //660
                new PointF { X = 66, Y = 106 },
                new PointF { X = 683, Y = 108 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 67, Y = img_s.Height },
                //new PointF { X = 286, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                };

                img_d = new Mat();
                warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
                CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(Convert.ToInt32(dst.Max(a => a.X)), Convert.ToInt32(dst.Max(a => a.Y))), Inter.Linear, Warp.Default, BorderType.Transparent);

                img_d.Bitmap.Save("c:\\projects\\712_slice_WaistToAbdomen_warpPerspective.png");

                img_s.Dispose();

                //////--------------ABDOMEN TO HIGH HIP WARPING--------------------///////////////
                img_s = new Image<Bgra, byte>("c:\\projects\\712_slice_AbdomenToHighHip.png");

                src = new PointF[] {
                new PointF { X = 30, Y = 0 },
                new PointF { X = 699, Y = 0 },
                new PointF { X = 14, Y = 133 },
                new PointF { X = 714, Y = 133 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 57, Y = img_s.Height },
                //new PointF { X = 296, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                // note even with a 4th term in here and dst, same error arises....
                };

                dst = new PointF[] {
                new PointF { X = 30, Y = 0 },
                new PointF { X = 30 + dst[3].X - dst[2].X, Y = 0 }, //660
                new PointF { X = 0, Y = 102 },
                new PointF { X = 685, Y = 103 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 67, Y = img_s.Height },
                //new PointF { X = 286, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                };

                img_d = new Mat();
                warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
                CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(Convert.ToInt32(dst.Max(a => a.X)), Convert.ToInt32(dst.Max(a => a.Y))), Inter.Linear, Warp.Default, BorderType.Transparent);

                img_d.Bitmap.Save("c:\\projects\\712_slice_AbdomenToHighHip_warpPerspective.png");

                img_s.Dispose();

                //////--------------HIGH HIP TO LOW HIP WARPING--------------------///////////////
                img_s = new Image<Bgra, byte>("c:\\projects\\712_slice_HighHipToLowHip.png");

                src = new PointF[] {
                new PointF { X = 37, Y = 0 },
                new PointF { X = 737, Y = 0 },
                new PointF { X = 27, Y = 145 },
                new PointF { X = 755, Y = 145 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 57, Y = img_s.Height },
                //new PointF { X = 296, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                // note even with a 4th term in here and dst, same error arises....
                };

                dst = new PointF[] {
                new PointF { X = 45, Y = 0 },
                new PointF { X = 37 + dst[3].X - dst[2].X, Y = 0 }, //660
                new PointF { X = 6, Y = 210 },
                new PointF { X = 747, Y = 212 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 67, Y = img_s.Height },
                //new PointF { X = 286, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                };

                img_d = new Mat();
                warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
                CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(Convert.ToInt32(dst.Max(a => a.X)), Convert.ToInt32(dst.Max(a => a.Y))), Inter.Linear, Warp.Default, BorderType.Transparent);

                img_d.Bitmap.Save("c:\\projects\\712_slice_HighHipToLowHip_warpPerspective.png");

                img_s.Dispose();

                //////--------------LOW HIP TO HIGH THIGH WARPING--------------------///////////////
                img_s = new Image<Bgra, byte>("c:\\projects\\712_slice_LowHipToHighThigh.png");

                src = new PointF[] {
                new PointF { X = 26, Y = 0 },
                new PointF { X = 756, Y = 0 },
                new PointF { X = 25, Y = 38 },
                new PointF { X = 759, Y = 41 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 57, Y = img_s.Height },
                //new PointF { X = 296, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                // note even with a 4th term in here and dst, same error arises....
                };

                dst = new PointF[] {
                new PointF { X = 18, Y = 0 },
                new PointF { X = 18 + dst[3].X - dst[2].X, Y = 0 }, //660
                new PointF { X = 15, Y = 76 },
                new PointF { X = 765, Y = 79 }
                //new PointF { X = 8, Y = img_s.Height },
                //new PointF { X = 67, Y = img_s.Height },
                //new PointF { X = 286, Y = img_s.Height },
                //new PointF { X = 344, Y = img_s.Height }
                };

                img_d = new Mat();
                warpMatrix = CvInvoke.GetPerspectiveTransform(src, dst);
                CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(Convert.ToInt32(dst.Max(a => a.X)), Convert.ToInt32(dst.Max(a => a.Y))), Inter.Linear, Warp.Default, BorderType.Transparent);

                img_d.Bitmap.Save("c:\\projects\\712_slice_LowHipToHighThigh_warpPerspective.png");

                img_s.Dispose();
            }
        }

        void RunForMembersCopy()
        {
            //foreach (MemberData md in members)
            //{

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            MemberData md = members.Where(a => a.MemberID == 13721).SingleOrDefault();

            List<ProductData> pds = md.ProductCoordinatesData.Where(a => a.ProductID == 626).ToList();

            Image<Bgra, byte> img_s = null;
            PointF[] src = null;
            PointF[] dst = null;

            using (Bitmap bmpfin = new Bitmap(3458, 4661))
            {
                //lock (bmpfin)
                //{
                using (Graphics g = Graphics.FromImage(bmpfin))
                {
                    for (int i = 0; i < 12; i++)
                    //Parallel.For(0, 11, (i, loopState) =>
                    {
                        #region "LEFT SHOULDER to FOREARM WARPING"

                        ProductData pd = pds.Where(a => a.ProductSection == "Left Shoulder").SingleOrDefault();

                        if (pd.MannequinType.ToLower().Contains("old"))
                            mannequinCoordinates = mannequinLaurieCoordinates;
                        else
                            mannequinCoordinates = mannequinDollyCoordinates;

                        //////--------------LEFT SHOULDER to FOREARM WARPING--------------------///////////////

                        //Thread for Left Shoulder
                        if (i == 0)
                        {
                            img_s = new Image<Bgra, byte>("c:\\projects\\" + pd.ProductID + "_slice_leftshoulder.png");
                            //img_s = mainSrcImg.Copy(mainSrcImg.ROI = new Rectangle());

                            srcX1 = (float)pd.ProductCoordinates[0, 0];
                            srcX2 = (float)pd.ProductCoordinates[1, 0];
                            srcX3 = (float)pd.ProductCoordinates[2, 0];
                            srcX4 = (float)pd.ProductCoordinates[3, 0];
                            srcY1 = (float)pd.ProductCoordinates[0, 1];
                            srcY2 = (float)pd.ProductCoordinates[1, 1];
                            srcY3 = (float)pd.ProductCoordinates[2, 1];
                            srcY4 = (float)pd.ProductCoordinates[3, 1];

                            src = new PointF[] {
                                new PointF { X = srcX1, Y = srcY1 },
                                new PointF { X = srcX2, Y = srcY2 },
                                new PointF { X = srcX3, Y = srcY3 },
                                new PointF { X = srcX4, Y = srcY4 }
                                    };

                            dst = new PointF[] {
                                new PointF { X = (float)md.MemberCoordinates[62, 0] - 12, Y = (float)md.MemberCoordinates[63, 1] - 3 }, //0, 0
                                new PointF { X = (float)md.MemberCoordinates[63, 0] + 6, Y = (float)md.MemberCoordinates[63, 1] - 3 }, //1368, 786
                                new PointF { X = (float)md.MemberCoordinates[62, 0] - 12, Y = (float)md.MemberCoordinates[62, 1] + 3 }, //1110, 1145
                                new PointF { X = (float)md.MemberCoordinates[55, 0] + (((float)md.MemberCoordinates[53, 0] - (float)md.MemberCoordinates[55, 0]) / 2) + 6, Y = (float)md.MemberCoordinates[55, 1] + 3 }, //1326, 1230
                                //new PointF { X = ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[8, 0] + 12) > (src[3].X - src[2].X) ? (src[2].X - ((float)md.MemberCoordinates[8, 0] + 12 - src[2].X)) : src[2].X, Y = 1145 }, //1110
                                //new PointF { X = ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[8, 0] + 12) > (src[3].X - src[2].X) ? (src[3].X - ((float)md.MemberCoordinates[15, 0] - 12 - src[3].X)) : src[3].X, Y = 1230 } //1326
                                    };

                            minSrcX = Convert.ToInt32(src.Min(a => a.X));
                            maxSrcX = Convert.ToInt32(src.Max(a => a.X));
                            maxDstX = Convert.ToInt32(dst.Max(a => a.X));
                            minSrcY = Convert.ToInt32(src.Min(a => a.Y));
                            maxSrcY = Convert.ToInt32(src.Max(a => a.Y));
                            maxDstY = Convert.ToInt32(dst.Max(a => a.Y));
                            dstWidth = maxDstX;
                            dstHeight = maxDstY;

                            int minDstYShoulder = Convert.ToInt32(dst.Min(a => a.Y));
                            int maxDstYShoulder = Convert.ToInt32(dst.Max(a => a.Y));

                            using (Mat bmpLeftShoulder = new Mat())
                            {
                                lock (bmpLeftShoulder)
                                {
                                    Mat warpMatrix = CvInvoke.FindHomography(src, dst, HomographyMethod.Ransac, 3);
                                    CvInvoke.WarpPerspective(img_s, bmpLeftShoulder, warpMatrix, new System.Drawing.Size(dstWidth, dstHeight), Inter.Nearest, Warp.Default, BorderType.Transparent);

                                    bmpLeftShoulder.Bitmap.Save("c:\\projects\\" + md.MemberID + "_" + pd.ProductID + "_slice_leftshoulder_warpPerspective.png");

                                    ResetCoordinates();

                                    g.DrawImage(bmpLeftShoulder.Bitmap, new Point(0, 0));
                                }
                            }

                            img_s.Dispose();
                            img_s = null;
                        }

                        #endregion

                        #region "LEFT FOREARM TO ARM WARPING"

                        //Thread for Left Arm
                        if (i == 1)
                        {
                            //////--------------FOREARM TO ARM WARPING--------------------///////////////
                            pd = pds.Where(a => a.ProductSection == "Left Arm").SingleOrDefault();

                            img_s = new Image<Bgra, byte>("c:\\projects\\" + pd.ProductID + "_slice_leftarmToForeArm.png");

                            srcX1 = (float)pd.ProductCoordinates[0, 0];
                            srcX2 = (float)pd.ProductCoordinates[1, 0];
                            srcX3 = (float)pd.ProductCoordinates[2, 0];
                            srcX4 = (float)pd.ProductCoordinates[3, 0];
                            srcY1 = (float)pd.ProductCoordinates[0, 1];
                            srcY2 = (float)pd.ProductCoordinates[1, 1];
                            srcY3 = (float)pd.ProductCoordinates[2, 1];
                            srcY4 = (float)pd.ProductCoordinates[3, 1];

                            src = new PointF[] {
                                new PointF { X = srcX1, Y = srcY1 },
                                new PointF { X = srcX2, Y = srcY2 },
                                new PointF { X = srcX3, Y = srcY3 },
                                new PointF { X = srcX4, Y = srcY4 }
                                    };

                            dst = new PointF[] {
                                new PointF { X = dst[2].X, Y = dst[2].Y }, //0, 0
                                new PointF { X = dst[3].X, Y = dst[3].Y }, //1368, 786
                                new PointF { X = (float)md.MemberCoordinates[61, 0] - 12, Y = (float)md.MemberCoordinates[61, 1] + 3 }, //1110, 1145
                                new PointF { X = (float)md.MemberCoordinates[56, 0] + 12, Y = (float)md.MemberCoordinates[56, 1] + 3 }, //1326, 1230
                                //new PointF { X = ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[8, 0] + 12) > (src[3].X - src[2].X) ? (src[2].X - ((float)md.MemberCoordinates[8, 0] + 12 - src[2].X)) : src[2].X, Y = 1145 }, //1110
                                //new PointF { X = ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[8, 0] + 12) > (src[3].X - src[2].X) ? (src[3].X - ((float)md.MemberCoordinates[15, 0] - 12 - src[3].X)) : src[3].X, Y = 1230 } //1326
                                    };

                            minSrcX = Convert.ToInt32(src.Min(a => a.X));
                            maxSrcX = Convert.ToInt32(src.Max(a => a.X));
                            maxDstX = Convert.ToInt32(dst.Max(a => a.X));
                            minSrcY = Convert.ToInt32(src.Min(a => a.Y));
                            maxSrcY = Convert.ToInt32(src.Max(a => a.Y));
                            maxDstY = Convert.ToInt32(dst.Max(a => a.Y));
                            dstWidth = maxDstX;
                            dstHeight = maxDstY;

                            using (Mat bmpLeftArm = new Mat())
                            {
                                lock (bmpLeftArm)
                                {
                                    Mat warpMatrix = CvInvoke.FindHomography(src, dst, HomographyMethod.Ransac, 3);
                                    CvInvoke.WarpPerspective(img_s, bmpLeftArm, warpMatrix, new System.Drawing.Size(dstWidth, dstHeight), Inter.Nearest, Warp.Default, BorderType.Transparent);

                                    bmpLeftArm.Bitmap.Save("c:\\projects\\" + md.MemberID + "_" + pd.ProductID + "_slice_leftarmToForeArm_warpPerspective.png");

                                    ResetCoordinates();

                                    g.DrawImage(bmpLeftArm.Bitmap, new Point(0, 0));
                                }
                            }

                            img_s.Dispose();
                            img_s = null;
                        }

                        #endregion

                        #region "LEFT ARM TO WRIST WARPING"

                        //Thread for Left Wrist
                        if (i == 2)
                        {
                            //////--------------ARM TO WRIST WARPING--------------------///////////////
                            pd = pds.Where(a => a.ProductSection == "Left Wrist").SingleOrDefault();

                            img_s = new Image<Bgra, byte>("c:\\projects\\" + pd.ProductID + "_slice_leftForeArmToWrist.png");

                            srcX1 = (float)pd.ProductCoordinates[0, 0];
                            srcX2 = (float)pd.ProductCoordinates[1, 0];
                            srcX3 = (float)pd.ProductCoordinates[2, 0];
                            srcX4 = (float)pd.ProductCoordinates[3, 0];
                            srcY1 = (float)pd.ProductCoordinates[0, 1];
                            srcY2 = (float)pd.ProductCoordinates[1, 1];
                            srcY3 = (float)pd.ProductCoordinates[2, 1];
                            srcY4 = (float)pd.ProductCoordinates[3, 1];

                            src = new PointF[] {
                                new PointF { X = srcX1, Y = srcY1 },
                                new PointF { X = srcX2, Y = srcY2 },
                                new PointF { X = srcX3, Y = srcY3 },
                                new PointF { X = srcX4, Y = srcY4 }
                                    };

                            //Loose wrist case
                            if (
                                    (srcX4 - srcX3) > ((float)md.MemberCoordinates[57, 0] + 12 - (float)md.MemberCoordinates[60, 0] - 12)
                                    &&
                                    ((srcX4 - srcX3) - ((float)md.MemberCoordinates[57, 0] + 12 - (float)md.MemberCoordinates[60, 0] - 12)) > 100
                                    )
                            {
                                float halfWidth = (srcX4 - srcX3) / 2;

                                dst = new PointF[] {
                                    new PointF { X = dst[2].X, Y = dst[2].Y }, //0, 0
                                    new PointF { X = dst[3].X, Y = dst[3].Y }, //1368, 786
                                    new PointF { X = (float)md.MemberCoordinates[60, 0] - 12 - halfWidth, Y = ((float)md.MemberCoordinates[60, 1] - 80) + 3 }, //1110, 1145
                                    new PointF { X = (float)md.MemberCoordinates[57, 0] + 12 + halfWidth, Y = ((float)md.MemberCoordinates[57, 1] - 80) + 3}, //1326, 1230
                                        };
                            }
                            else
                            {
                                dst = new PointF[] {
                                    new PointF { X = dst[2].X, Y = dst[2].Y }, //0, 0
                                    new PointF { X = dst[3].X, Y = dst[3].Y }, //1368, 786
                                    new PointF { X = (float)md.MemberCoordinates[60, 0] - 12, Y = (float)md.MemberCoordinates[60, 1] + 3 }, //1110, 1145
                                    new PointF { X = (float)md.MemberCoordinates[57, 0] + 12, Y = (float)md.MemberCoordinates[57, 1] + 3 }, //1326, 1230
                                        };
                            }

                            minSrcX = Convert.ToInt32(src.Min(a => a.X));
                            maxSrcX = Convert.ToInt32(src.Max(a => a.X));
                            maxDstX = Convert.ToInt32(dst.Max(a => a.X));
                            minSrcY = Convert.ToInt32(src.Min(a => a.Y));
                            maxSrcY = Convert.ToInt32(src.Max(a => a.Y));
                            maxDstY = Convert.ToInt32(dst.Max(a => a.Y));
                            dstWidth = maxDstX;
                            dstHeight = maxDstY;

                            using (Mat bmpLeftWrist = new Mat())
                            {
                                lock (bmpLeftWrist)
                                {
                                    Mat warpMatrix = CvInvoke.FindHomography(src, dst, HomographyMethod.Ransac, 3);
                                    CvInvoke.WarpPerspective(img_s, bmpLeftWrist, warpMatrix, new System.Drawing.Size(dstWidth, dstHeight), Inter.Nearest, Warp.Default, BorderType.Transparent);

                                    //img_d.Bitmap.Save("c:\\projects\\" + md.MemberID + "_" + pd.ProductID + "_slice_leftForeArmToWrist_warpPerspective.png");

                                    ResetCoordinates();

                                    g.DrawImage(bmpLeftWrist.Bitmap, new Point(0, 0));
                                }
                            }

                            img_s.Dispose();
                            img_s = null;
                        }

                        #endregion

                        #region "RIGHT SHOULDER to FOREARM WARPING"

                        //Thread for Right Shoulder
                        if (i == 3)
                        {
                            //////--------------RIGHT SHOULDER to FOREARM WARPING--------------------///////////////
                            pd = pds.Where(a => a.ProductSection == "Right Shoulder").SingleOrDefault();

                            img_s = new Image<Bgra, byte>("c:\\projects\\" + pd.ProductID + "_slice_rightshoulder.png");

                            srcX1 = (float)pd.ProductCoordinates[0, 0];

                            if ((float)md.MemberCoordinates[8, 0] + 12 < (float)pd.ProductCoordinates[1, 0])
                                srcX2 = (float)pd.ProductCoordinates[1, 0] - 50;
                            else
                                srcX2 = (float)pd.ProductCoordinates[1, 0];

                            srcX3 = (float)pd.ProductCoordinates[2, 0];
                            srcX4 = (float)pd.ProductCoordinates[3, 0];
                            srcY1 = (float)pd.ProductCoordinates[0, 1];
                            srcY2 = (float)pd.ProductCoordinates[1, 1];
                            srcY3 = (float)pd.ProductCoordinates[2, 1];
                            srcY4 = (float)pd.ProductCoordinates[3, 1];

                            src = new PointF[] {
                                new PointF { X = srcX1, Y = srcY1 },
                                new PointF { X = srcX2, Y = srcY2 },
                                new PointF { X = srcX3, Y = srcY3 },
                                new PointF { X = srcX4, Y = srcY4 }
                                    };

                            dst = new PointF[] {
                                new PointF { X = (float)md.MemberCoordinates[7, 0] - 6 , Y = (float)md.MemberCoordinates[7, 1] - 3 }, //0, 0
                                new PointF { X = (float)md.MemberCoordinates[8, 0] + 12, Y = (float)md.MemberCoordinates[7, 1] - 3 }, //1368, 786
                                new PointF { X = (float)md.MemberCoordinates[15, 0] - (((float)md.MemberCoordinates[15, 0] - (float)md.MemberCoordinates[17, 0]) / 2) - 6, Y = (float)md.MemberCoordinates[15, 1] + 3 }, //1110, 1145
                                new PointF { X = (float)md.MemberCoordinates[8, 0] + 12, Y = (float)md.MemberCoordinates[8, 1] + 3 }, //1326, 1230
                                    };

                            minSrcX = Convert.ToInt32(src.Min(a => a.X));
                            maxSrcX = Convert.ToInt32(src.Max(a => a.X));
                            maxDstX = Convert.ToInt32(dst.Max(a => a.X));
                            minSrcY = Convert.ToInt32(src.Min(a => a.Y));
                            maxSrcY = Convert.ToInt32(src.Max(a => a.Y));
                            maxDstY = Convert.ToInt32(dst.Max(a => a.Y));
                            dstWidth = maxDstX;
                            dstHeight = maxDstY;

                            using (Mat bmpRightShoulder = new Mat())
                            {
                                lock (bmpRightShoulder)
                                {
                                    Mat warpMatrix = CvInvoke.FindHomography(src, dst, HomographyMethod.Ransac, 3);
                                    CvInvoke.WarpPerspective(img_s, bmpRightShoulder, warpMatrix, new System.Drawing.Size(dstWidth, dstHeight), Inter.Nearest, Warp.Default, BorderType.Transparent);

                                    bmpRightShoulder.Bitmap.Save("c:\\projects\\" + md.MemberID + "_" + pd.ProductID + "_slice_rightshoulder_warpPerspective.png");

                                    g.DrawImage(bmpRightShoulder.Bitmap, new Point(0, 0));

                                    ResetCoordinates();
                                }
                            }

                            img_s.Dispose();
                            img_s = null;
                        }

                        #endregion

                        #region "RIGHT FOREARM TO ARM WARPING"

                        //Thread for Right Arm
                        if (i == 4)
                        {
                            //////--------------FOREARM TO ARM WARPING--------------------///////////////
                            pd = pds.Where(a => a.ProductSection == "Right Arm").SingleOrDefault();

                            img_s = new Image<Bgra, byte>("c:\\projects\\" + pd.ProductID + "_slice_rightarmToForeArm.png");

                            srcX1 = (float)pd.ProductCoordinates[0, 0];
                            srcX2 = (float)pd.ProductCoordinates[1, 0];
                            srcX3 = (float)pd.ProductCoordinates[2, 0];
                            srcX4 = (float)pd.ProductCoordinates[3, 0];
                            srcY1 = (float)pd.ProductCoordinates[0, 1];
                            srcY2 = (float)pd.ProductCoordinates[1, 1];
                            srcY3 = (float)pd.ProductCoordinates[2, 1];
                            srcY4 = (float)pd.ProductCoordinates[3, 1];

                            src = new PointF[] {
                                new PointF { X = srcX1, Y = srcY1 },
                                new PointF { X = srcX2, Y = srcY2 },
                                new PointF { X = srcX3, Y = srcY3 },
                                new PointF { X = srcX4, Y = srcY4 }
                                    };

                            dst = new PointF[] {
                                new PointF { X = dst[2].X, Y = dst[2].Y }, //0, 0
                                new PointF { X = dst[3].X, Y = dst[3].Y }, //1368, 786
                                new PointF { X = (float)md.MemberCoordinates[14, 0] - 12, Y = (float)md.MemberCoordinates[14, 1] + 3 }, //1110, 1145
                                new PointF { X = (float)md.MemberCoordinates[9, 0] + 12, Y = (float)md.MemberCoordinates[9, 1] + 3 }, //1326, 1230
                                    };

                            minSrcX = Convert.ToInt32(src.Min(a => a.X));
                            maxSrcX = Convert.ToInt32(src.Max(a => a.X));
                            maxDstX = Convert.ToInt32(dst.Max(a => a.X));
                            minSrcY = Convert.ToInt32(src.Min(a => a.Y));
                            maxSrcY = Convert.ToInt32(src.Max(a => a.Y));
                            maxDstY = Convert.ToInt32(dst.Max(a => a.Y));
                            dstWidth = maxDstX;
                            dstHeight = maxDstY;

                            using (Mat bmpRightArm = new Mat())
                            {
                                lock (bmpRightArm)
                                {
                                    Mat warpMatrix = CvInvoke.FindHomography(src, dst, HomographyMethod.Ransac, 3);
                                    CvInvoke.WarpPerspective(img_s, bmpRightArm, warpMatrix, new System.Drawing.Size(dstWidth, dstHeight), Inter.Nearest, Warp.Default, BorderType.Transparent);

                                    //img_d.Bitmap.Save("c:\\projects\\" + md.MemberID + "_" + pd.ProductID + "_slice_rightarmToForeArm_warpPerspective.png");

                                    g.DrawImage(bmpRightArm.Bitmap, new Point(0, 0));

                                    ResetCoordinates();
                                }
                            }

                            img_s.Dispose();
                            img_s = null;
                        }

                        #endregion

                        #region "RIGHT ARM TO WRIST WARPING"

                        //Thread for Right Wrist
                        if (i == 5)
                        {
                            //////--------------ARM TO WRIST WARPING--------------------///////////////
                            pd = pds.Where(a => a.ProductSection == "Right Wrist").SingleOrDefault();

                            img_s = new Image<Bgra, byte>("c:\\projects\\" + pd.ProductID + "_slice_rightForeArmToWrist.png");

                            srcX1 = (float)pd.ProductCoordinates[0, 0];
                            srcX2 = (float)pd.ProductCoordinates[1, 0];
                            srcX3 = (float)pd.ProductCoordinates[2, 0];
                            srcX4 = (float)pd.ProductCoordinates[3, 0];
                            srcY1 = (float)pd.ProductCoordinates[0, 1];
                            srcY2 = (float)pd.ProductCoordinates[1, 1];
                            srcY3 = (float)pd.ProductCoordinates[2, 1];
                            srcY4 = (float)pd.ProductCoordinates[3, 1];

                            src = new PointF[] {
                                new PointF { X = srcX1, Y = srcY1 },
                                new PointF { X = srcX2, Y = srcY2 },
                                new PointF { X = srcX3, Y = srcY3 },
                                new PointF { X = srcX4, Y = srcY4 }
                                    };

                            //Loose wrist case
                            if (
                                    (srcX4 - srcX3) > ((float)md.MemberCoordinates[10, 0] + 12 - (float)md.MemberCoordinates[13, 0] - 12)
                                    &&
                                    ((srcX4 - srcX3) - ((float)md.MemberCoordinates[10, 0] + 12 - (float)md.MemberCoordinates[13, 0] - 12)) > 100
                                    )
                            {
                                float halfWidth = (srcX4 - srcX3) / 2;

                                dst = new PointF[] {
                                    new PointF { X = dst[2].X, Y = dst[2].Y }, //0, 0
                                    new PointF { X = dst[3].X, Y = dst[3].Y }, //1368, 786
                                    new PointF { X = (float)md.MemberCoordinates[13, 0] - 12 - halfWidth, Y = ((float)md.MemberCoordinates[13, 1] - 80) + 3 }, //1110, 1145
                                    new PointF { X = (float)md.MemberCoordinates[10, 0] + 12 + halfWidth, Y = ((float)md.MemberCoordinates[10, 1] - 80) + 3 }, //1326, 1230
                                        };
                            }
                            else
                            {
                                dst = new PointF[] {
                                    new PointF { X = dst[2].X, Y = dst[2].Y }, //0, 0
                                    new PointF { X = dst[3].X, Y = dst[3].Y }, //1368, 786
                                    new PointF { X = (float)md.MemberCoordinates[13, 0] - 12, Y = (float)md.MemberCoordinates[13, 1] + 3 }, //1110, 1145
                                    new PointF { X = (float)md.MemberCoordinates[10, 0] + 12, Y = (float)md.MemberCoordinates[10, 1] + 3 }, //1326, 1230
                                        };
                            }

                            minSrcX = Convert.ToInt32(src.Min(a => a.X));
                            maxSrcX = Convert.ToInt32(src.Max(a => a.X));
                            maxDstX = Convert.ToInt32(dst.Max(a => a.X));
                            minSrcY = Convert.ToInt32(src.Min(a => a.Y));
                            maxSrcY = Convert.ToInt32(src.Max(a => a.Y));
                            maxDstY = Convert.ToInt32(dst.Max(a => a.Y));
                            dstWidth = maxDstX;
                            dstHeight = maxDstY;

                            using (Mat bmpRightWrist = new Mat())
                            {
                                lock (bmpRightWrist)
                                {
                                    Mat warpMatrix = CvInvoke.FindHomography(src, dst, HomographyMethod.Ransac, 3);
                                    CvInvoke.WarpPerspective(img_s, bmpRightWrist, warpMatrix, new System.Drawing.Size(bmpRightWrist.Width, bmpRightWrist.Height), Inter.Nearest, Warp.Default, BorderType.Transparent);

                                    //img_d.Bitmap.Save("c:\\projects\\" + md.MemberID + "_" + pd.ProductID + "_slice_rightForeArmToWrist_warpPerspective.png");

                                    g.DrawImage(bmpRightWrist.Bitmap, new Point(0, 0));

                                    ResetCoordinates();
                                }
                            }

                            img_s.Dispose();
                            img_s = null;
                        }

                        #endregion

                        #region "SHOULDERS TO CHEST WARPING"

                        //Thread for Shoulder
                        if (i == 6)
                        {
                            //////--------------SHOULDERS TO CHEST WARPING--------------------///////////////
                            pd = pds.Where(a => a.ProductSection == "Center Shoulder").SingleOrDefault();

                            img_s = new Image<Bgra, byte>("c:\\projects\\" + pd.ProductID + "_slice_ShoulderToChest.png");

                            srcX1 = (float)pd.ProductCoordinates[0, 0];
                            srcX2 = (float)pd.ProductCoordinates[1, 0];
                            srcX3 = (float)pd.ProductCoordinates[2, 0];
                            srcX4 = (float)pd.ProductCoordinates[3, 0];
                            srcY1 = (float)pd.ProductCoordinates[0, 1];
                            srcY2 = (float)pd.ProductCoordinates[1, 1];
                            srcY3 = (float)pd.ProductCoordinates[2, 1];
                            srcY4 = (float)pd.ProductCoordinates[3, 1];

                            src = new PointF[] {
                                new PointF { X = srcX1, Y = srcY1 },
                                new PointF { X = srcX2, Y = srcY2 },
                                new PointF { X = srcX3, Y = srcY3 },
                                new PointF { X = srcX4, Y = srcY4 }
                                    };

                            dst = new PointF[] {
                                new PointF { X = (float)md.MemberCoordinates[63, 0] - 6 , Y = (float)md.MemberCoordinates[63, 1] - 3 }, //0, 0
                                new PointF { X = (float)md.MemberCoordinates[7, 0] + 6, Y = (float)md.MemberCoordinates[7, 1] - 3 }, //1368, 786
                                new PointF { X = (float)md.MemberCoordinates[53, 0] - (((float)md.MemberCoordinates[53, 0] - (float)md.MemberCoordinates[55, 0]) / 2) - 6, Y = (float)md.MemberCoordinates[53, 1] + 3 }, //1110, 1145
                                new PointF { X = (float)md.MemberCoordinates[17, 0] + (((float)md.MemberCoordinates[15, 0] - (float)md.MemberCoordinates[17, 0]) / 2) + 6, Y = (float)md.MemberCoordinates[17, 1] + 3 }, //1326, 1230
                                    };

                            using (Mat bmpShoulder = new Mat())
                            {
                                lock (bmpShoulder)
                                {
                                    Mat warpMatrix = CvInvoke.FindHomography(src, dst, HomographyMethod.Ransac, 3);
                                    CvInvoke.WarpPerspective(img_s, bmpShoulder, warpMatrix, new System.Drawing.Size(Convert.ToInt32(dst.Max(a => a.X)), Convert.ToInt32(dst.Max(a => a.Y))), Inter.Nearest, Warp.Default, BorderType.Transparent);

                                    bmpShoulder.Bitmap.Save("c:\\projects\\" + md.MemberID + "_" + pd.ProductID + "_slice_ShoulderToChest_warpPerspective.png");

                                    g.DrawImage(bmpShoulder.Bitmap, new Point(0, 0));
                                }
                            }

                            img_s.Dispose();
                            img_s = null;
                        }

                        #endregion

                        #region "CHEST TO WAIST WARPING"

                        //Thread for Bust
                        if (i == 7)
                        {
                            //////--------------CHEST TO WAIST WARPING--------------------///////////////
                            pd = pds.Where(a => a.ProductSection == "Center Bust").SingleOrDefault();

                            if ((pd != null && pd.ProductCoordinates[0, 0] != 0))
                            {
                                img_s = new Image<Bgra, byte>("c:\\projects\\" + pd.ProductID + "_slice_ChestToWaist.png");

                                srcX1 = (float)pd.ProductCoordinates[0, 0];
                                srcX2 = (float)pd.ProductCoordinates[1, 0];
                                srcX3 = (float)pd.ProductCoordinates[2, 0];
                                srcX4 = (float)pd.ProductCoordinates[3, 0];
                                srcY1 = (float)pd.ProductCoordinates[0, 1];
                                srcY2 = (float)pd.ProductCoordinates[1, 1];
                                srcY3 = (float)pd.ProductCoordinates[2, 1];
                                srcY4 = (float)pd.ProductCoordinates[3, 1];

                                src = new PointF[] {
                                    new PointF { X = srcX1, Y = srcY1 },
                                    new PointF { X = srcX2, Y = srcY2 },
                                    new PointF { X = srcX3, Y = srcY3 },
                                    new PointF { X = srcX4, Y = srcY4 }
                                        };

                                //Do not vertically stretch if product is not reaching till next section
                                if ((mannequinCoordinates[52, 1] - mannequinCoordinates[53, 1]) > (srcY3 - srcY1) && ((mannequinCoordinates[52, 1] - mannequinCoordinates[53, 1]) - (srcY3 - srcY1)) > 6)
                                {
                                    dst = new PointF[] {
                                        new PointF { X = dst[2].X, Y = dst[2].Y }, //0, 0
                                        new PointF { X = dst[3].X, Y = dst[3].Y }, //1368, 786
                                        new PointF { X = (float)md.MemberCoordinates[52, 0] - 12, Y = (float)md.MemberCoordinates[53, 1] + (srcY3 - srcY1) + 3 }, //1110, 1145
                                        new PointF { X = (float)md.MemberCoordinates[18, 0] + 12, Y = (float)md.MemberCoordinates[17, 1] + (srcY4 - srcY2) + 3 }, //1326, 1230
                                            };
                                }
                                else
                                {
                                    dst = new PointF[] {
                                        new PointF { X = dst[2].X, Y = dst[2].Y }, //0, 0
                                        new PointF { X = dst[3].X, Y = dst[3].Y }, //1368, 786
                                        new PointF { X = (float)md.MemberCoordinates[52, 0] - 12, Y = (float)md.MemberCoordinates[52, 1] + 3 }, //1110, 1145
                                        new PointF { X = (float)md.MemberCoordinates[18, 0] + 12, Y = (float)md.MemberCoordinates[18, 1] + 3 }, //1326, 1230
                                            };
                                }

                                using (Mat bmpChest = new Mat())
                                {
                                    lock (bmpChest)
                                    {
                                        //Mat src_gray = new Mat();
                                        //CvInvoke.CvtColor(img_s, src_gray, ColorConversion.Bgr2Gray);

                                        //CvInvoke.GaussianBlur(src_gray, src_gray, new Size(3, 3), 0);

                                        //Mat canny_output = new Mat();
                                        //CvInvoke.Canny(src_gray, canny_output, 100, 100 * 2);
                                        //Point[][] contours = new Point[][] { };
                                        //List<Vec4i> hierarchy = new List<Vec4i>();


                                        //CvInvoke.FindContours(img_s, contours, hierarchy, RetrType.Ccomp, ChainApproxMethod.ChainApproxSimple);

                                        Mat warpMatrix = CvInvoke.FindHomography(src, dst, HomographyMethod.Ransac, 3);
                                        CvInvoke.WarpPerspective(img_s, bmpChest, warpMatrix, new System.Drawing.Size(Convert.ToInt32(dst.Max(a => a.X)), Convert.ToInt32(dst.Max(a => a.Y))), Inter.Nearest, Warp.Default, BorderType.Transparent);

                                        bmpChest.Bitmap.Save("c:\\projects\\" + md.MemberID + "_" + pd.ProductID + "_slice_ChestToWaist_warpPerspective.png");

                                        if (bmpChest != null)
                                        {
                                            g.DrawImage(bmpChest.Bitmap, new Point(0, 0));
                                        }
                                    }
                                }

                                img_s.Dispose();
                                img_s = null;
                            }
                        }

                        #endregion

                        #region "WAIST TO ABDOMEN WARPING"

                        //Thread for Waist
                        if (i == 8)
                        {
                            //////--------------WAIST TO ABDOMEN WARPING--------------------///////////////
                            pd = pds.Where(a => a.ProductSection == "Center Waist").SingleOrDefault();

                            Mat bmpWaist = null;

                            //If Product is present for this section
                            if (
                                    (pd != null && pd.ProductCoordinates[0, 0] != 0)
                                    &&
                                    (float)pd.ProductCoordinates[0, 0] > 0 || (float)pd.ProductCoordinates[0, 1] > 0
                                    )
                            {
                                img_s = new Image<Bgra, byte>("c:\\projects\\" + pd.ProductID + "_slice_WaistToAbdomen.png");

                                srcX1 = (float)pd.ProductCoordinates[0, 0];
                                srcX2 = (float)pd.ProductCoordinates[1, 0];
                                srcX3 = (float)pd.ProductCoordinates[2, 0];
                                srcX4 = (float)pd.ProductCoordinates[3, 0];
                                srcY1 = (float)pd.ProductCoordinates[0, 1];
                                srcY2 = (float)pd.ProductCoordinates[1, 1];
                                srcY3 = (float)pd.ProductCoordinates[2, 1];
                                srcY4 = (float)pd.ProductCoordinates[3, 1];

                                src = new PointF[] {
                                    new PointF { X = srcX1, Y = srcY1 },
                                    new PointF { X = srcX2, Y = srcY2 },
                                    new PointF { X = srcX3, Y = srcY3 },
                                    new PointF { X = srcX4, Y = srcY4 }
                                        };

                                //Do not vertically stretch if product is not reaching till next section
                                if ((mannequinCoordinates[51, 1] - mannequinCoordinates[52, 1]) > (srcY3 - srcY1) && ((mannequinCoordinates[51, 1] - mannequinCoordinates[52, 1]) - (srcY3 - srcY1)) > 6)
                                {
                                    dst = new PointF[] {
                                        new PointF { X = dst[2].X, Y = dst[2].Y }, //0, 0
                                        new PointF { X = dst[3].X, Y = dst[3].Y }, //1368, 786
                                        new PointF { X = (float)md.MemberCoordinates[51, 0] - 12, Y = (float)md.MemberCoordinates[52, 1] + (srcY3 - srcY1) + 3 }, //1110, 1145
                                        new PointF { X = (float)md.MemberCoordinates[19, 0] + 12, Y = (float)md.MemberCoordinates[18, 1] + (srcY4 - srcY2) + 3 }, //1326, 1230
                                            };
                                }
                                else
                                {
                                    dst = new PointF[] {
                                        new PointF { X = dst[2].X, Y = dst[2].Y }, //0, 0
                                        new PointF { X = dst[3].X, Y = dst[3].Y }, //1368, 786
                                        new PointF { X = (float)md.MemberCoordinates[51, 0] - 12, Y = (float)md.MemberCoordinates[51, 1] + 3 }, //1110, 1145
                                        new PointF { X = (float)md.MemberCoordinates[19, 0] + 12, Y = (float)md.MemberCoordinates[19, 1] + 3 }, //1326, 1230
                                            };
                                }

                                using (bmpWaist = new Mat())
                                {
                                    lock (bmpWaist)
                                    {
                                        Mat warpMatrix = CvInvoke.FindHomography(src, dst, HomographyMethod.Ransac, 3);
                                        CvInvoke.WarpPerspective(img_s, bmpWaist, warpMatrix, new System.Drawing.Size(Convert.ToInt32(dst.Max(a => a.X)), Convert.ToInt32(dst.Max(a => a.Y))), Inter.Nearest, Warp.Default, BorderType.Transparent);

                                        bmpWaist.Bitmap.Save("c:\\projects\\" + md.MemberID + "_" + pd.ProductID + "_slice_WaistToAbdomen_warpPerspective.png");

                                        if (bmpWaist != null)
                                        {
                                            g.DrawImage(bmpWaist.Bitmap, new Point(0, 0));
                                        }
                                    }
                                }

                                img_s.Dispose();
                                img_s = null;
                            }
                        }
                        #endregion

                        #region "ABDOMEN TO HIGH HIP WARPING"

                        //Thread for Abdomen
                        if (i == 9)
                        {
                            //////--------------ABDOMEN TO HIGH HIP WARPING--------------------///////////////
                            pd = pds.Where(a => a.ProductSection == "Center Abdomen").SingleOrDefault();

                            Mat bmpAbdomen = null;

                            //If Product is present for this section
                            if (
                                    (pd != null && pd.ProductCoordinates[0, 0] != 0)
                                    &&
                                    (float)pd.ProductCoordinates[0, 0] > 0 || (float)pd.ProductCoordinates[0, 1] > 0
                                    )
                            {
                                img_s = new Image<Bgra, byte>("c:\\projects\\" + pd.ProductID + "_slice_AbdomenToHighHip.png");

                                srcX1 = (float)pd.ProductCoordinates[0, 0];
                                srcX2 = (float)pd.ProductCoordinates[1, 0];
                                srcX3 = (float)pd.ProductCoordinates[2, 0];
                                srcX4 = (float)pd.ProductCoordinates[3, 0];
                                srcY1 = (float)pd.ProductCoordinates[0, 1];
                                srcY2 = (float)pd.ProductCoordinates[1, 1];
                                srcY3 = (float)pd.ProductCoordinates[2, 1];
                                srcY4 = (float)pd.ProductCoordinates[3, 1];

                                src = new PointF[] {
                                    new PointF { X = srcX1, Y = srcY1 },
                                    new PointF { X = srcX2, Y = srcY2 },
                                    new PointF { X = srcX3, Y = srcY3 },
                                    new PointF { X = srcX4, Y = srcY4 }
                                        };

                                //Do not vertically stretch if product is not reaching till next section
                                if (((mannequinCoordinates[50, 1] - mannequinCoordinates[51, 1]) > (srcY3 - srcY1)) && ((mannequinCoordinates[50, 1] - mannequinCoordinates[51, 1]) - (srcY3 - srcY1)) > 6)
                                {
                                    dst = new PointF[] {
                                        new PointF { X = dst[2].X, Y = dst[2].Y }, //0, 0
                                        new PointF { X = dst[3].X, Y = dst[3].Y }, //1368, 786
                                        new PointF { X = (float)md.MemberCoordinates[50, 0] - 12, Y = (float)md.MemberCoordinates[51, 1] + (srcY3 - srcY1) + 3 }, //1110, 1145
                                        new PointF { X = (float)md.MemberCoordinates[20, 0] + 12, Y = (float)md.MemberCoordinates[19, 1] + (srcY4 - srcY2) + 3 }, //1326, 1230
                                            };
                                }
                                else
                                {
                                    dst = new PointF[] {
                                        new PointF { X = dst[2].X, Y = dst[2].Y }, //0, 0
                                        new PointF { X = dst[3].X, Y = dst[3].Y }, //1368, 786
                                        new PointF { X = (float)md.MemberCoordinates[50, 0] - 12, Y = (float)md.MemberCoordinates[50, 1] + 3 }, //1110, 1145
                                        new PointF { X = (float)md.MemberCoordinates[20, 0] + 12, Y = (float)md.MemberCoordinates[20, 1] + 3 }, //1326, 1230
                                            };
                                }

                                using (bmpAbdomen = new Mat())
                                {
                                    lock (bmpAbdomen)
                                    {
                                        Mat warpMatrix = CvInvoke.FindHomography(src, dst, HomographyMethod.Ransac, 3);
                                        CvInvoke.WarpPerspective(img_s, bmpAbdomen, warpMatrix, new System.Drawing.Size(Convert.ToInt32(dst.Max(a => a.X)), Convert.ToInt32(dst.Max(a => a.Y))), Inter.Nearest, Warp.Default, BorderType.Transparent);

                                        //img_d.Bitmap.Save("c:\\projects\\" + md.MemberID + "_" + pd.ProductID + "_slice_AbdomenToHighHip_warpPerspective.png");

                                        if (bmpAbdomen != null)
                                        {
                                            g.DrawImage(bmpAbdomen.Bitmap, new Point(0, 0));
                                        }
                                    }
                                }

                                img_s.Dispose();
                                img_s = null;
                            }
                        }
                        #endregion

                        #region "HIGH HIP TO LOW HIP WARPING"

                        //Thread for High Hip
                        if (i == 10)
                        {
                            //////--------------HIGH HIP TO LOW HIP WARPING--------------------///////////////
                            pd = pds.Where(a => a.ProductSection == "Center High Hip").SingleOrDefault();

                            Mat bmpHighHip = null;

                            //If Product is present for this section
                            if (
                                    (pd != null && pd.ProductCoordinates[0, 0] != 0)
                                    &&
                                    (float)pd.ProductCoordinates[0, 0] > 0 || (float)pd.ProductCoordinates[0, 1] > 0
                                    )
                            {
                                img_s = new Image<Bgra, byte>("c:\\projects\\" + pd.ProductID + "_slice_HighHipToLowHip.png");

                                srcX1 = (float)pd.ProductCoordinates[0, 0];
                                srcX2 = (float)pd.ProductCoordinates[1, 0];
                                srcX3 = (float)pd.ProductCoordinates[2, 0];
                                srcX4 = (float)pd.ProductCoordinates[3, 0];
                                srcY1 = (float)pd.ProductCoordinates[0, 1];
                                srcY2 = (float)pd.ProductCoordinates[1, 1];
                                srcY3 = (float)pd.ProductCoordinates[2, 1];
                                srcY4 = (float)pd.ProductCoordinates[3, 1];

                                src = new PointF[] {
                                    new PointF { X = srcX1, Y = srcY1 },
                                    new PointF { X = srcX2, Y = srcY2 },
                                    new PointF { X = srcX3, Y = srcY3 },
                                    new PointF { X = srcX4, Y = srcY4 }
                                        };

                                //Do not vertically stretch if product is not reaching till next section
                                if ((mannequinCoordinates[49, 1] - mannequinCoordinates[50, 1]) > (srcY3 - srcY1) && ((mannequinCoordinates[49, 1] - mannequinCoordinates[50, 1]) - (srcY3 - srcY1)) > 6)
                                {
                                    dst = new PointF[] {
                                        new PointF { X = dst[2].X, Y = dst[2].Y }, //0, 0
                                        new PointF { X = dst[3].X, Y = dst[3].Y }, //1368, 786
                                        new PointF { X = (float)md.MemberCoordinates[49, 0] - 12, Y = (float)md.MemberCoordinates[50, 1] + (srcY3 - srcY1) + 3 }, //1110, 1145
                                        new PointF { X = (float)md.MemberCoordinates[21, 0] + 12, Y = (float)md.MemberCoordinates[20, 1] + (srcY4 - srcY2) + 3 }, //1326, 1230
                                            };
                                }
                                else
                                {
                                    dst = new PointF[] {
                                        new PointF { X = dst[2].X, Y = dst[2].Y }, //0, 0
                                        new PointF { X = dst[3].X, Y = dst[3].Y }, //1368, 786
                                        new PointF { X = (float)md.MemberCoordinates[49, 0] - 12, Y = (float)md.MemberCoordinates[49, 1] + 3 }, //1110, 1145
                                        new PointF { X = (float)md.MemberCoordinates[21, 0] + 12, Y = (float)md.MemberCoordinates[21, 1] + 3 }, //1326, 1230
                                            };
                                }

                                using (bmpHighHip = new Mat())
                                {
                                    lock (bmpHighHip)
                                    {
                                        Mat warpMatrix = CvInvoke.FindHomography(src, dst, HomographyMethod.Ransac, 3);
                                        CvInvoke.WarpPerspective(img_s, bmpHighHip, warpMatrix, new System.Drawing.Size(Convert.ToInt32(dst.Max(a => a.X)), Convert.ToInt32(dst.Max(a => a.Y))), Inter.Nearest, Warp.Default, BorderType.Transparent);

                                        //img_d.Bitmap.Save("c:\\projects\\" + md.MemberID + "_" + pd.ProductID + "_slice_HighHipToLowHip_warpPerspective.png");

                                        if (bmpHighHip != null)
                                        {
                                            g.DrawImage(bmpHighHip.Bitmap, new Point(0, 0));
                                        }
                                    }
                                }

                                img_s.Dispose();
                                img_s = null;
                            }
                        }
                        #endregion

                        #region LOW HIP TO HIGH THIGH WARPING

                        //Thread for Low Hip
                        if (i == 11)
                        {
                            //////--------------LOW HIP TO HIGH THIGH WARPING--------------------///////////////
                            pd = pds.Where(a => a.ProductSection == "Center Low Hip").SingleOrDefault();

                            Mat bmpLowHip = null;

                            //If Product is present for this section
                            if (
                                    (pd != null && pd.ProductCoordinates[0, 0] != 0)
                                    &&
                                    (float)pd.ProductCoordinates[0, 0] > 0 || (float)pd.ProductCoordinates[0, 1] > 0
                                    )
                            {
                                img_s = new Image<Bgra, byte>("c:\\projects\\" + pd.ProductID + "_slice_LowHipToHighThigh.png");

                                srcX1 = (float)pd.ProductCoordinates[0, 0];
                                srcX2 = (float)pd.ProductCoordinates[1, 0];
                                srcX3 = (float)pd.ProductCoordinates[2, 0];
                                srcX4 = (float)pd.ProductCoordinates[3, 0];
                                srcY1 = (float)pd.ProductCoordinates[0, 1];
                                srcY2 = (float)pd.ProductCoordinates[1, 1];
                                srcY3 = (float)pd.ProductCoordinates[2, 1];
                                srcY4 = (float)pd.ProductCoordinates[3, 1];

                                src = new PointF[] {
                                    new PointF { X = srcX1, Y = srcY1 },
                                    new PointF { X = srcX2, Y = srcY2 },
                                    new PointF { X = srcX3, Y = srcY3 },
                                    new PointF { X = srcX4, Y = srcY4 }
                                        };

                                //Do not vertically stretch if product is not reaching till next section
                                if ((mannequinCoordinates[48, 1] - mannequinCoordinates[49, 1]) > (srcY3 - srcY1) && ((mannequinCoordinates[48, 1] - mannequinCoordinates[49, 1]) - (srcY3 - srcY1)) > 6)
                                {
                                    dst = new PointF[] {
                                        new PointF { X = dst[2].X, Y = dst[2].Y }, //0, 0
                                        new PointF { X = dst[3].X, Y = dst[3].Y }, //1368, 786
                                        new PointF { X = (float)md.MemberCoordinates[48, 0] - 12, Y = (float)md.MemberCoordinates[49, 1] + (srcY3 - srcY1) + 3 }, //1110, 1145
                                        new PointF { X = (float)md.MemberCoordinates[22, 0] + 12, Y = (float)md.MemberCoordinates[21, 1] + (srcY4 - srcY2) + 3 }, //1326, 1230
                                            };
                                }
                                else
                                {
                                    dst = new PointF[] {
                                        new PointF { X = dst[2].X, Y = dst[2].Y }, //0, 0
                                        new PointF { X = dst[3].X, Y = dst[3].Y }, //1368, 786
                                        new PointF { X = (float)md.MemberCoordinates[48, 0] - 12, Y = (float)md.MemberCoordinates[48, 1] + 3 }, //1110, 1145
                                        new PointF { X = (float)md.MemberCoordinates[22, 0] + 12, Y = (float)md.MemberCoordinates[22, 1] + 3 }, //1326, 1230
                                            };
                                }

                                using (bmpLowHip = new Mat())
                                {
                                    lock (bmpLowHip)
                                    {
                                        Mat warpMatrix = CvInvoke.FindHomography(src, dst, HomographyMethod.Ransac, 3);
                                        CvInvoke.WarpPerspective(img_s, bmpLowHip, warpMatrix, new System.Drawing.Size(Convert.ToInt32(dst.Max(a => a.X)), Convert.ToInt32(dst.Max(a => a.Y))), Inter.Nearest, Warp.Default, BorderType.Transparent);

                                        //img_d.Bitmap.Save("c:\\projects\\" + md.MemberID + "_" + pd.ProductID + "_slice_LowHipToHighThigh_warpPerspective.png");

                                        if (bmpLowHip != null)
                                        {
                                            g.DrawImage(bmpLowHip.Bitmap, new Point(0, 0));
                                        }
                                    }
                                }

                                img_s.Dispose();
                                img_s = null;
                            }
                        }
                        #endregion
                        //});
                    }

                    bmpfin.Save("c:\\projects\\" + md.MemberID + "_" + pds[0].ProductID + "_FinalStitched.png");
                }
                //}
            }

            stopwatch.Stop();
            Console.Write("Overall transformation time in milliseconds: {0}",
            stopwatch.ElapsedMilliseconds);

            //}
        }

        void RunForMembersComplete()
        {
            MemberData md = members.Where(a => a.MemberID == 13721).SingleOrDefault();

            //foreach (MemberData md in members)
            //{
            List<ProductData> pds = md.ProductCoordinatesData.Where(a => a.ProductID == 626).ToList();

            #region "COMPLETE IMAGE WARPING"

            ProductData pd = pds.Where(a => a.ProductSection == "Left Shoulder").SingleOrDefault();

            //////--------------LEFT SHOULDER to FOREARM WARPING--------------------///////////////

            Image<Bgra, byte> img_s = new Image<Bgra, byte>("c:\\projects\\Chaser_CW6640_Black_R_XS_Without350Padding.png");

            srcX1 = (float)pd.ProductCoordinates[0, 0];
            srcX2 = (float)pd.ProductCoordinates[1, 0];
            srcX3 = (float)pd.ProductCoordinates[2, 0];
            srcX4 = (float)pd.ProductCoordinates[3, 0];
            srcY1 = (float)pd.ProductCoordinates[0, 1];
            srcY2 = (float)pd.ProductCoordinates[1, 1];
            srcY3 = (float)pd.ProductCoordinates[2, 1];
            srcY4 = (float)pd.ProductCoordinates[3, 1];

            PointF src01 = new PointF { X = srcX1, Y = srcY1 };
            PointF src02 = new PointF { X = srcX2, Y = srcY2 };
            PointF src03 = new PointF { X = srcX3, Y = srcY3 };
            PointF src04 = new PointF { X = srcX4, Y = srcY4 };

            pd = pds.Where(a => a.ProductSection == "Left Arm").SingleOrDefault();

            srcX1 = (float)pd.ProductCoordinates[0, 0];
            srcX2 = (float)pd.ProductCoordinates[1, 0];
            srcX3 = (float)pd.ProductCoordinates[2, 0];
            srcX4 = (float)pd.ProductCoordinates[3, 0];
            srcY1 = (float)pd.ProductCoordinates[0, 1];
            srcY2 = (float)pd.ProductCoordinates[1, 1];
            srcY3 = (float)pd.ProductCoordinates[2, 1];
            srcY4 = (float)pd.ProductCoordinates[3, 1];

            PointF src11 = new PointF { X = srcX1, Y = srcY1 };
            PointF src12 = new PointF { X = srcX2, Y = srcY2 };
            PointF src13 = new PointF { X = srcX3, Y = srcY3 };
            PointF src14 = new PointF { X = srcX4, Y = srcY4 };

            pd = pds.Where(a => a.ProductSection == "Left Wrist").SingleOrDefault();

            srcX1 = (float)pd.ProductCoordinates[0, 0];
            srcX2 = (float)pd.ProductCoordinates[1, 0];
            srcX3 = (float)pd.ProductCoordinates[2, 0];
            srcX4 = (float)pd.ProductCoordinates[3, 0];
            srcY1 = (float)pd.ProductCoordinates[0, 1];
            srcY2 = (float)pd.ProductCoordinates[1, 1];
            srcY3 = (float)pd.ProductCoordinates[2, 1];
            srcY4 = (float)pd.ProductCoordinates[3, 1];

            PointF src21 = new PointF { X = srcX1, Y = srcY1 };
            PointF src22 = new PointF { X = srcX2, Y = srcY2 };
            PointF src23 = new PointF { X = srcX3, Y = srcY3 };
            PointF src24 = new PointF { X = srcX4, Y = srcY4 };

            pd = pds.Where(a => a.ProductSection == "Right Shoulder").SingleOrDefault();

            srcX1 = (float)pd.ProductCoordinates[0, 0];
            srcX2 = (float)pd.ProductCoordinates[1, 0];
            srcX3 = (float)pd.ProductCoordinates[2, 0];
            srcX4 = (float)pd.ProductCoordinates[3, 0];
            srcY1 = (float)pd.ProductCoordinates[0, 1];
            srcY2 = (float)pd.ProductCoordinates[1, 1];
            srcY3 = (float)pd.ProductCoordinates[2, 1];
            srcY4 = (float)pd.ProductCoordinates[3, 1];

            PointF src31 = new PointF { X = srcX1, Y = srcY1 };
            PointF src32 = new PointF { X = srcX2, Y = srcY2 };
            PointF src33 = new PointF { X = srcX3, Y = srcY3 };
            PointF src34 = new PointF { X = srcX4, Y = srcY4 };

            pd = pds.Where(a => a.ProductSection == "Right Arm").SingleOrDefault();

            srcX1 = (float)pd.ProductCoordinates[0, 0];
            srcX2 = (float)pd.ProductCoordinates[1, 0];
            srcX3 = (float)pd.ProductCoordinates[2, 0];
            srcX4 = (float)pd.ProductCoordinates[3, 0];
            srcY1 = (float)pd.ProductCoordinates[0, 1];
            srcY2 = (float)pd.ProductCoordinates[1, 1];
            srcY3 = (float)pd.ProductCoordinates[2, 1];
            srcY4 = (float)pd.ProductCoordinates[3, 1];

            PointF src41 = new PointF { X = srcX1, Y = srcY1 };
            PointF src42 = new PointF { X = srcX2, Y = srcY2 };
            PointF src43 = new PointF { X = srcX3, Y = srcY3 };
            PointF src44 = new PointF { X = srcX4, Y = srcY4 };

            pd = pds.Where(a => a.ProductSection == "Right Wrist").SingleOrDefault();

            srcX1 = (float)pd.ProductCoordinates[0, 0];
            srcX2 = (float)pd.ProductCoordinates[1, 0];
            srcX3 = (float)pd.ProductCoordinates[2, 0];
            srcX4 = (float)pd.ProductCoordinates[3, 0];
            srcY1 = (float)pd.ProductCoordinates[0, 1];
            srcY2 = (float)pd.ProductCoordinates[1, 1];
            srcY3 = (float)pd.ProductCoordinates[2, 1];
            srcY4 = (float)pd.ProductCoordinates[3, 1];

            PointF src51 = new PointF { X = srcX1, Y = srcY1 };
            PointF src52 = new PointF { X = srcX2, Y = srcY2 };
            PointF src53 = new PointF { X = srcX3, Y = srcY3 };
            PointF src54 = new PointF { X = srcX4, Y = srcY4 };

            pd = pds.Where(a => a.ProductSection == "Center Shoulder").SingleOrDefault();

            srcX1 = (float)pd.ProductCoordinates[0, 0];
            srcX2 = (float)pd.ProductCoordinates[1, 0];
            srcX3 = (float)pd.ProductCoordinates[2, 0];
            srcX4 = (float)pd.ProductCoordinates[3, 0];
            srcY1 = (float)pd.ProductCoordinates[0, 1];
            srcY2 = (float)pd.ProductCoordinates[1, 1];
            srcY3 = (float)pd.ProductCoordinates[2, 1];
            srcY4 = (float)pd.ProductCoordinates[3, 1];

            PointF src61 = new PointF { X = srcX1, Y = srcY1 };
            PointF src62 = new PointF { X = srcX2, Y = srcY2 };
            PointF src63 = new PointF { X = srcX3, Y = srcY3 };
            PointF src64 = new PointF { X = srcX4, Y = srcY4 };

            pd = pds.Where(a => a.ProductSection == "Center Bust").SingleOrDefault();

            srcX1 = (float)pd.ProductCoordinates[0, 0];
            srcX2 = (float)pd.ProductCoordinates[1, 0];
            srcX3 = (float)pd.ProductCoordinates[2, 0];
            srcX4 = (float)pd.ProductCoordinates[3, 0];
            srcY1 = (float)pd.ProductCoordinates[0, 1];
            srcY2 = (float)pd.ProductCoordinates[1, 1];
            srcY3 = (float)pd.ProductCoordinates[2, 1];
            srcY4 = (float)pd.ProductCoordinates[3, 1];

            PointF src71 = new PointF { X = srcX1, Y = srcY1 };
            PointF src72 = new PointF { X = srcX2, Y = srcY2 };
            PointF src73 = new PointF { X = srcX3, Y = srcY3 };
            PointF src74 = new PointF { X = srcX4, Y = srcY4 };

            pd = pds.Where(a => a.ProductSection == "Center Waist").SingleOrDefault();

            srcX1 = (float)pd.ProductCoordinates[0, 0];
            srcX2 = (float)pd.ProductCoordinates[1, 0];
            srcX3 = (float)pd.ProductCoordinates[2, 0];
            srcX4 = (float)pd.ProductCoordinates[3, 0];
            srcY1 = (float)pd.ProductCoordinates[0, 1];
            srcY2 = (float)pd.ProductCoordinates[1, 1];
            srcY3 = (float)pd.ProductCoordinates[2, 1];
            srcY4 = (float)pd.ProductCoordinates[3, 1];

            PointF src81 = new PointF { X = srcX1, Y = srcY1 };
            PointF src82 = new PointF { X = srcX2, Y = srcY2 };
            PointF src83 = new PointF { X = srcX3, Y = srcY3 };
            PointF src84 = new PointF { X = srcX4, Y = srcY4 };

            pd = pds.Where(a => a.ProductSection == "Center Abdomen").SingleOrDefault();

            srcX1 = (float)pd.ProductCoordinates[0, 0];
            srcX2 = (float)pd.ProductCoordinates[1, 0];
            srcX3 = (float)pd.ProductCoordinates[2, 0];
            srcX4 = (float)pd.ProductCoordinates[3, 0];
            srcY1 = (float)pd.ProductCoordinates[0, 1];
            srcY2 = (float)pd.ProductCoordinates[1, 1];
            srcY3 = (float)pd.ProductCoordinates[2, 1];
            srcY4 = (float)pd.ProductCoordinates[3, 1];

            PointF src91 = new PointF { X = srcX1, Y = srcY1 };
            PointF src92 = new PointF { X = srcX2, Y = srcY2 };
            PointF src93 = new PointF { X = srcX3, Y = srcY3 };
            PointF src94 = new PointF { X = srcX4, Y = srcY4 };

            pd = pds.Where(a => a.ProductSection == "Center High Hip").SingleOrDefault();

            srcX1 = (float)pd.ProductCoordinates[0, 0];
            srcX2 = (float)pd.ProductCoordinates[1, 0];
            srcX3 = (float)pd.ProductCoordinates[2, 0];
            srcX4 = (float)pd.ProductCoordinates[3, 0];
            srcY1 = (float)pd.ProductCoordinates[0, 1];
            srcY2 = (float)pd.ProductCoordinates[1, 1];
            srcY3 = (float)pd.ProductCoordinates[2, 1];
            srcY4 = (float)pd.ProductCoordinates[3, 1];

            PointF src101 = new PointF { X = srcX1, Y = srcY1 };
            PointF src102 = new PointF { X = srcX2, Y = srcY2 };
            PointF src103 = new PointF { X = srcX3, Y = srcY3 };
            PointF src104 = new PointF { X = srcX4, Y = srcY4 };

            pd = pds.Where(a => a.ProductSection == "Center Low Hip").SingleOrDefault();

            srcX1 = (float)pd.ProductCoordinates[0, 0];
            srcX2 = (float)pd.ProductCoordinates[1, 0];
            srcX3 = (float)pd.ProductCoordinates[2, 0];
            srcX4 = (float)pd.ProductCoordinates[3, 0];
            srcY1 = (float)pd.ProductCoordinates[0, 1];
            srcY2 = (float)pd.ProductCoordinates[1, 1];
            srcY3 = (float)pd.ProductCoordinates[2, 1];
            srcY4 = (float)pd.ProductCoordinates[3, 1];

            PointF src111 = new PointF { X = srcX1, Y = srcY1 };
            PointF src112 = new PointF { X = srcX2, Y = srcY2 };
            PointF src113 = new PointF { X = srcX3, Y = srcY3 };
            PointF src114 = new PointF { X = srcX4, Y = srcY4 };

            PointF[] src = new PointF[]
                {
                    src01,
                    src02,
                    src03,
                    src04,
                    src11,
                    src12,
                    src13,
                    src14,
                    src21,
                    src22,
                    src23,
                    src24,
                    src31,
                    src32,
                    src33,
                    src34,
                    src41,
                    src42,
                    src43,
                    src44,
                    src51,
                    src52,
                    src53,
                    src54,
                    src61,
                    src62,
                    src63,
                    src64,
                    src71,
                    src72,
                    src73,
                    src74,
                    src81,
                    src82,
                    src83,
                    src84,
                    src91,
                    src92,
                    src93,
                    src94,
                    src101,
                    src102,
                    src103,
                    src104,
                    src111,
                    src112,
                    src113,
                    src114
                };

            PointF[] dst = new PointF[] {
                new PointF { X = (float)md.MemberCoordinates[62, 0] - 12, Y = (float)md.MemberCoordinates[63, 1] - 3 }, //0, 0
                new PointF { X = (float)md.MemberCoordinates[63, 0] + 6, Y = (float)md.MemberCoordinates[63, 1] - 3 }, //1368, 786
                new PointF { X = (float)md.MemberCoordinates[62, 0] - 12, Y = (float)md.MemberCoordinates[62, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[55, 0] + (((float)md.MemberCoordinates[53, 0] - (float)md.MemberCoordinates[55, 0]) / 2) + 6, Y = (float)md.MemberCoordinates[55, 1] + 3 }, //1326, 1230

                new PointF { X = (float)md.MemberCoordinates[62, 0] - 12, Y = (float)md.MemberCoordinates[62, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[55, 0] + (((float)md.MemberCoordinates[53, 0] - (float)md.MemberCoordinates[55, 0]) / 2) + 6, Y = (float)md.MemberCoordinates[55, 1] + 3 }, //1326, 1230
                new PointF { X = (float)md.MemberCoordinates[61, 0] - 12, Y = (float)md.MemberCoordinates[61, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[56, 0] + 12, Y = (float)md.MemberCoordinates[56, 1] + 3 }, //1326, 1230

                new PointF { X = (float)md.MemberCoordinates[61, 0] - 12, Y = (float)md.MemberCoordinates[61, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[56, 0] + 12, Y = (float)md.MemberCoordinates[56, 1] + 3 }, //1326, 1230
                new PointF { X = (float)md.MemberCoordinates[60, 0] - 12, Y = (float)md.MemberCoordinates[60, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[57, 0] + 12, Y = (float)md.MemberCoordinates[57, 1] + 3 }, //1326, 1230
                
                new PointF { X = (float)md.MemberCoordinates[7, 0] - 6 , Y = (float)md.MemberCoordinates[7, 1] - 3 }, //0, 0
                new PointF { X = (float)md.MemberCoordinates[8, 0] + 12, Y = (float)md.MemberCoordinates[7, 1] - 3 }, //1368, 786
                new PointF { X = (float)md.MemberCoordinates[15, 0] - (((float)md.MemberCoordinates[15, 0] - (float)md.MemberCoordinates[17, 0]) / 2) - 6, Y = (float)md.MemberCoordinates[15, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[8, 0] + 12, Y = (float)md.MemberCoordinates[8, 1] + 3 }, //1326, 1230

                new PointF { X = (float)md.MemberCoordinates[15, 0] - (((float)md.MemberCoordinates[15, 0] - (float)md.MemberCoordinates[17, 0]) / 2) - 6, Y = (float)md.MemberCoordinates[15, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[8, 0] + 12, Y = (float)md.MemberCoordinates[8, 1] + 3 }, //1326, 1230
                new PointF { X = (float)md.MemberCoordinates[14, 0] - 12, Y = (float)md.MemberCoordinates[14, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[9, 0] + 12, Y = (float)md.MemberCoordinates[9, 1] + 3 }, //1326, 1230

                new PointF { X = (float)md.MemberCoordinates[14, 0] - 12, Y = (float)md.MemberCoordinates[14, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[9, 0] + 12, Y = (float)md.MemberCoordinates[9, 1] + 3 }, //1326, 1230
                new PointF { X = (float)md.MemberCoordinates[13, 0] - 12, Y = (float)md.MemberCoordinates[13, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[10, 0] + 12, Y = (float)md.MemberCoordinates[10, 1] + 3 }, //1326, 1230

                new PointF { X = (float)md.MemberCoordinates[63, 0] - 6 , Y = (float)md.MemberCoordinates[63, 1] - 3 }, //0, 0
                new PointF { X = (float)md.MemberCoordinates[7, 0] + 6, Y = (float)md.MemberCoordinates[7, 1] - 3 }, //1368, 786
                new PointF { X = (float)md.MemberCoordinates[53, 0] - (((float)md.MemberCoordinates[53, 0] - (float)md.MemberCoordinates[55, 0]) / 2) - 6, Y = (float)md.MemberCoordinates[53, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[17, 0] + (((float)md.MemberCoordinates[15, 0] - (float)md.MemberCoordinates[17, 0]) / 2) + 6, Y = (float)md.MemberCoordinates[17, 1] + 3 }, //1326, 1230

                new PointF { X = (float)md.MemberCoordinates[53, 0] - (((float)md.MemberCoordinates[53, 0] - (float)md.MemberCoordinates[55, 0]) / 2) - 6, Y = (float)md.MemberCoordinates[53, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[17, 0] + (((float)md.MemberCoordinates[15, 0] - (float)md.MemberCoordinates[17, 0]) / 2) + 6, Y = (float)md.MemberCoordinates[17, 1] + 3 }, //1326, 1230
                new PointF { X = (float)md.MemberCoordinates[52, 0] - 12, Y = (float)md.MemberCoordinates[52, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[18, 0] + 12, Y = (float)md.MemberCoordinates[18, 1] + 3 }, //1326, 1230

                new PointF { X = (float)md.MemberCoordinates[52, 0] - 12, Y = (float)md.MemberCoordinates[52, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[18, 0] + 12, Y = (float)md.MemberCoordinates[18, 1] + 3 }, //1326, 1230
                new PointF { X = (float)md.MemberCoordinates[51, 0] - 12, Y = (float)md.MemberCoordinates[51, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[19, 0] + 12, Y = (float)md.MemberCoordinates[19, 1] + 3 }, //1326, 1230

                new PointF { X = (float)md.MemberCoordinates[51, 0] - 12, Y = (float)md.MemberCoordinates[51, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[19, 0] + 12, Y = (float)md.MemberCoordinates[19, 1] + 3 }, //1326, 1230
                new PointF { X = (float)md.MemberCoordinates[50, 0] - 12, Y = (float)md.MemberCoordinates[50, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[20, 0] + 12, Y = (float)md.MemberCoordinates[20, 1] + 3 }, //1326, 1230

                new PointF { X = (float)md.MemberCoordinates[50, 0] - 12, Y = (float)md.MemberCoordinates[50, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[20, 0] + 12, Y = (float)md.MemberCoordinates[20, 1] + 3 }, //1326, 1230
                new PointF { X = (float)md.MemberCoordinates[49, 0] - 12, Y = (float)md.MemberCoordinates[49, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[21, 0] + 12, Y = (float)md.MemberCoordinates[21, 1] + 3 }, //1326, 1230

                new PointF { X = (float)md.MemberCoordinates[49, 0] - 12, Y = (float)md.MemberCoordinates[49, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[21, 0] + 12, Y = (float)md.MemberCoordinates[21, 1] + 3 }, //1326, 1230
                new PointF { X = (float)md.MemberCoordinates[48, 0] - 12, Y = (float)md.MemberCoordinates[48, 1] + 3 }, //1110, 1145
                new PointF { X = (float)md.MemberCoordinates[22, 0] + 12, Y = (float)md.MemberCoordinates[22, 1] + 3 }, //1326, 1230

                //new PointF { X = ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[8, 0] + 12) > (src[3].X - src[2].X) ? (src[2].X - ((float)md.MemberCoordinates[8, 0] + 12 - src[2].X)) : src[2].X, Y = 1145 }, //1110
                //new PointF { X = ((float)md.MemberCoordinates[15, 0] - 12 - (float)md.MemberCoordinates[8, 0] + 12) > (src[3].X - src[2].X) ? (src[3].X - ((float)md.MemberCoordinates[15, 0] - 12 - src[3].X)) : src[3].X, Y = 1230 } //1326
                };

            minSrcX = Convert.ToInt32(src.Min(a => a.X));
            maxSrcX = Convert.ToInt32(src.Max(a => a.X));
            maxDstX = Convert.ToInt32(dst.Max(a => a.X));
            minSrcY = Convert.ToInt32(src.Min(a => a.Y));
            maxSrcY = Convert.ToInt32(src.Max(a => a.Y));
            maxDstY = Convert.ToInt32(dst.Max(a => a.Y));
            dstWidth = maxDstX;
            dstHeight = maxDstY;

            Mat img_d = new Mat();
            try
            {
                //Mat warpMatrix = CvInvoke.FindHomography(src, dst, HomographyMethod.Default, 3);
                Mat warpMatrix = CvInvoke.FindHomography(src, dst, HomographyMethod.Ransac, 3);
                CvInvoke.WarpPerspective(img_s, img_d, warpMatrix, new System.Drawing.Size(img_s.Width, img_s.Height), Inter.Nearest, Warp.FillOutliers, BorderType.Transparent);
            }
            catch (Exception ex)
            {

            }
            img_d.Bitmap.Save("c:\\projects\\" + md.MemberID + "_" + pd.ProductID + "_Complete_warpPerspective.png");

            ResetCoordinates();

            img_s.Dispose();

            #endregion
            //}
        }

        void DataConfiguration()
        {
            members = new List<MemberData>();

            ////14715
            double[,] member14715Data = GetMemberData(14715);

            ////13826
            double[,] member13826Data = GetMemberData(13826);

            ////13828
            double[,] member13828Data = GetMemberData(13828);

            ////13709
            double[,] member13709Data = GetMemberData(13709);

            ////13835
            double[,] member13835Data = GetMemberData(13835);

            ////13721
            double[,] member13721Data = GetMemberData(13721);

            ////13724
            double[,] member13724Data = GetMemberData(13724);

            List<ProductData> lstProductCoordinatesData = new List<ProductData>();
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 712,
                ProductSection = "Left Shoulder",
                ProductCoordinates = new double[,] { { 1206, 789 }, { 1378, 789 }, { 1205, 1146 }, { 1377, 1146 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 712,
                ProductSection = "Left Arm",
                ProductCoordinates = new double[,] { { 1206, 1146 }, { 1379, 1146 }, { 1154, 1574 }, { 1339, 1574 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 712,
                ProductSection = "Left Wrist",
                ProductCoordinates = new double[,] { { 1152, 1575 }, { 1340, 1575 }, { 1134, 2102 }, { 1245, 2118 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 712,
                ProductSection = "Right Shoulder",
                ProductCoordinates = new double[,] { { 2076, 781 }, { 2265, 781 }, { 2076, 1146 }, { 2267, 1146 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 712,
                ProductSection = "Right Arm",
                ProductCoordinates = new double[,] { { 2074, 1140 }, { 2268, 1140 }, { 2118, 1556 }, { 2322, 1556 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 712,
                ProductSection = "Right Wrist",
                ProductCoordinates = new double[,] { { 2120, 1554 }, { 2323, 1554 }, { 2213, 2076 }, { 2332, 2094 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 712,
                ProductSection = "Center Shoulder",
                ProductCoordinates = new double[,] { { 1374, 788 }, { 2079, 782 }, { 1374, 1096 }, { 2079, 1096 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 712,
                ProductSection = "Center Bust",
                ProductCoordinates = new double[,] { { 1374, 1094 }, { 2079, 1094 }, { 1408, 1489 }, { 2060, 1489 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 712,
                ProductSection = "Center Waist",
                ProductCoordinates = new double[,] { { 1408, 1489 }, { 2060, 1489 }, { 1399, 1627 }, { 2069, 1627 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 712,
                ProductSection = "Center Abdomen",
                ProductCoordinates = new double[,] { { 1398, 1627 }, { 2068, 1627 }, { 1382, 1759 }, { 2092, 1759 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 712,
                ProductSection = "Center High Hip",
                ProductCoordinates = new double[,] { { 1377, 1758 }, { 2103, 1758 }, { 1367, 1915 }, { 2117, 1917 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 712,
                ProductSection = "Center Low Hip",
                ProductCoordinates = new double[,] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 712,
                ProductSection = "Center High Thigh",
                ProductCoordinates = new double[,] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 712,
                ProductSection = "Center Low Thigh",
                ProductCoordinates = new double[,] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 712,
                ProductSection = "Center Knee",
                ProductCoordinates = new double[,] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }
            });

            //lstProductCoordinatesData = new List<ProductData>();
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1066,
                ProductSection = "Left Shoulder",
                ProductCoordinates = new double[,] { { 1208, 788 }, { 1373, 788 }, { 1208, 1147 }, { 1374, 1147 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1066,
                ProductSection = "Left Arm",
                ProductCoordinates = new double[,] { { 1209, 1148 }, { 1374, 1148 }, { 1125, 1578 }, { 1359, 1575 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1066,
                ProductSection = "Left Wrist",
                ProductCoordinates = new double[,] { { 1126, 1576 }, { 1362, 1576 }, { 1065, 1856 }, { 1357, 1820 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1066,
                ProductSection = "Right Shoulder",
                ProductCoordinates = new double[,] { { 2081, 779 }, { 2247, 779 }, { 2079, 1147 }, { 2247, 1147 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1066,
                ProductSection = "Right Arm",
                ProductCoordinates = new double[,] { { 2077, 1145 }, { 2253, 1145 }, { 2117, 1557 }, { 2319, 1553 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1066,
                ProductSection = "Right Wrist",
                ProductCoordinates = new double[,] { { 2120, 1556 }, { 2316, 1556 }, { 2114, 1844 }, { 2396, 1868 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1066,
                ProductSection = "Center Shoulder",
                ProductCoordinates = new double[,] { { 1371, 786 }, { 2081, 776 }, { 1369, 1100 }, { 2083, 1100 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1066,
                ProductSection = "Center Bust",
                ProductCoordinates = new double[,] { { 1376, 1099 }, { 2078, 1099 }, { 1458, 1490 }, { 2004, 1490 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1066,
                ProductSection = "Center Waist",
                ProductCoordinates = new double[,] { { 1455, 1488 }, { 2005, 1488 }, { 1427, 1626 }, { 2041, 1626 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1066,
                ProductSection = "Center Abdomen",
                ProductCoordinates = new double[,] { { 1419, 1630 }, { 2041, 1628 }, { 1393, 1760 }, { 2079, 1760 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1066,
                ProductSection = "Center High Hip",
                ProductCoordinates = new double[,] { { 1390, 1757 }, { 2078, 1759 }, { 1376, 1905 }, { 2094, 1905 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1066,
                ProductSection = "Center Low Hip",
                ProductCoordinates = new double[,] { { 1376, 1901 }, { 2098, 1901 }, { 1360, 2323 }, { 2114, 2323 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1066,
                ProductSection = "Center High Thigh",
                ProductCoordinates = new double[,] { { 1530, 2328 }, { 1946, 2328 }, { 1530, 2417 }, { 1946, 2417 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1066,
                ProductSection = "Center Low Thigh",
                ProductCoordinates = new double[,] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1066,
                ProductSection = "Center Knee",
                ProductCoordinates = new double[,] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }
            });


            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 626,
                ProductSection = "Left Shoulder",
                ProductCoordinates = new double[,] { { 1210, 784 }, { 1377, 784 }, { 1210, 1148 }, { 1377, 1148 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 626,
                ProductSection = "Left Arm",
                ProductCoordinates = new double[,] { { 1210, 1148 }, { 1377, 1148 }, { 1157, 1572 }, { 1333, 1572 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 626,
                ProductSection = "Left Wrist",
                ProductCoordinates = new double[,] { { 1157, 1572 }, { 1333, 1572 }, { 1149, 2077 }, { 1236, 2066 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 626,
                ProductSection = "Right Shoulder",
                ProductCoordinates = new double[,] { { 2076, 778 }, { 2252, 778 }, { 2076, 1142 }, { 2252, 1142 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 626,
                ProductSection = "Right Arm",
                ProductCoordinates = new double[,] { { 2076, 1142 }, { 2252, 1142 }, { 2131, 1541 }, { 2314, 1541 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 626,
                ProductSection = "Right Wrist",
                ProductCoordinates = new double[,] { { 2131, 1541 }, { 2316, 1541 }, { 2241, 2057 }, { 2327, 2067 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 626,
                ProductSection = "Center Shoulder",
                ProductCoordinates = new double[,] { { 1372, 786 }, { 2076, 779 }, { 1372, 1094 }, { 2076, 1094 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 626,
                ProductSection = "Center Bust",
                ProductCoordinates = new double[,] { { 1372, 1094 }, { 2076, 1094 }, { 1382, 1626 }, { 2050, 1626 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 626,
                ProductSection = "Center Waist",
                ProductCoordinates = new double[,] { { 1382, 1626 }, { 2051, 1626 }, { 1371, 1754 }, { 2082, 1754 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 626,
                ProductSection = "Center Abdomen",
                ProductCoordinates = new double[,] { { 1371, 1754 }, { 2082, 1754 }, { 1364, 1906 }, { 2102, 1906 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 626,
                ProductSection = "Center High Hip",
                ProductCoordinates = new double[,] { { 1364, 1906 }, { 2102, 1906 }, { 1367, 2043 }, { 2100, 2042 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 626,
                ProductSection = "Center Low Hip",
                ProductCoordinates = new double[,] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 626,
                ProductSection = "Center High Thigh",
                ProductCoordinates = new double[,] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 626,
                ProductSection = "Center Low Thigh",
                ProductCoordinates = new double[,] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 626,
                ProductSection = "Center Knee",
                ProductCoordinates = new double[,] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }
            });


            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 624,
                ProductSection = "Left Shoulder",
                ProductCoordinates = new double[,] { { 1212, 789 }, { 1375, 789 }, { 1212, 1147 }, { 1376, 1146 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 624,
                ProductSection = "Left Arm",
                ProductCoordinates = new double[,] { { 1209, 1145 }, { 1375, 1145 }, { 1159, 1575 }, { 1338, 1576 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 624,
                ProductSection = "Left Wrist",
                ProductCoordinates = new double[,] { { 1158, 1575 }, { 1340, 1575 }, { 1136, 2048 }, { 1253, 2057 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 624,
                ProductSection = "Right Shoulder",
                ProductCoordinates = new double[,] { { 2077, 784 }, { 2243, 784 }, { 2079, 1143 }, { 2243, 1142 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 624,
                ProductSection = "Right Arm",
                ProductCoordinates = new double[,] { { 2079, 1143 }, { 2242, 1142 }, { 2110, 1555 }, { 2310, 1555 } }

            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 624,
                ProductSection = "Right Wrist",
                ProductCoordinates = new double[,] { { 2110, 1554 }, { 2311, 1555 }, { 2211, 2056 }, { 2343, 2042 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 624,
                ProductSection = "Center Shoulder",
                ProductCoordinates = new double[,] { { 1374, 791 }, { 2080, 787 }, { 1374, 1092 }, { 2080, 1092 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 624,
                ProductSection = "Center Bust",
                ProductCoordinates = new double[,] { { 1374, 1091 }, { 2079, 1091 }, { 1414, 1492 }, { 2028, 1492 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 624,
                ProductSection = "Center Waist",
                ProductCoordinates = new double[,] { { 1413, 1492 }, { 2028, 1488 }, { 1403, 1631 }, { 2050, 1631 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 624,
                ProductSection = "Center Abdomen",
                ProductCoordinates = new double[,] { { 1403, 1629 }, { 2050, 1629 }, { 1391, 1763 }, { 2076, 1763 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 624,
                ProductSection = "Center High Hip",
                ProductCoordinates = new double[,] { { 1390, 1761 }, { 2077, 1760 }, { 1367, 1906 }, { 2091, 1908 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 624,
                ProductSection = "Center Low Hip",
                ProductCoordinates = new double[,] { { 1371, 1905 }, { 2095, 1905 }, { 1367, 2103 }, { 2108, 2143 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 624,
                ProductSection = "Center High Thigh",
                ProductCoordinates = new double[,] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 624,
                ProductSection = "Center Low Thigh",
                ProductCoordinates = new double[,] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 624,
                ProductSection = "Center Knee",
                ProductCoordinates = new double[,] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }
            });


            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 569,
                ProductSection = "Left Shoulder",
                ProductCoordinates = new double[,] { { 1212, 784 }, { 1375, 784 }, { 1212, 1146 }, { 1375, 1146 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 569,
                ProductSection = "Left Arm",
                ProductCoordinates = new double[,] { { 1212, 1146 }, { 1375, 1146 }, { 1158, 1575 }, { 1343, 1575 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 569,
                ProductSection = "Left Wrist",
                ProductCoordinates = new double[,] { { 1158, 1575 }, { 1343, 1575 }, { 1128, 2162 }, { 1244, 2156 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 569,
                ProductSection = "Right Shoulder",
                ProductCoordinates = new double[,] { { 2079, 789 }, { 2248, 789 }, { 2079, 1142 }, { 2248, 1142 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 569,
                ProductSection = "Right Arm",
                ProductCoordinates = new double[,] { { 2079, 1143 }, { 2248, 1142 }, { 2114, 1554 }, { 2314, 1554 } }

            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 569,
                ProductSection = "Right Wrist",
                ProductCoordinates = new double[,] { { 2114, 1554 }, { 2314, 1554 }, { 2244, 2158 }, { 2346, 2158 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 569,
                ProductSection = "Center Shoulder",
                ProductCoordinates = new double[,] { { 1375, 784 }, { 2079, 789 }, { 1375, 1097 }, { 2079, 1097 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 569,
                ProductSection = "Center Bust",
                ProductCoordinates = new double[,] { { 1375, 1097 }, { 2079, 1097 }, { 1413, 1491 }, { 2056, 1491 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 569,
                ProductSection = "Center Waist",
                ProductCoordinates = new double[,] { { 1413, 1491 }, { 2056, 1491 }, { 1396, 1628 }, { 2076, 1628 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 569,
                ProductSection = "Center Abdomen",
                ProductCoordinates = new double[,] { { 1396, 1628 }, { 2076, 1628 }, { 1379, 1762 }, { 2093, 1762 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 569,
                ProductSection = "Center High Hip",
                ProductCoordinates = new double[,] { { 1379, 1762 }, { 2093, 1762 }, { 1372, 1906 }, { 2102, 1906 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 569,
                ProductSection = "Center Low Hip",
                ProductCoordinates = new double[,] { { 1372, 1906 }, { 2102, 1906 }, { 1364, 2288 }, { 2116, 2257 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 569,
                ProductSection = "Center High Thigh",
                ProductCoordinates = new double[,] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 569,
                ProductSection = "Center Low Thigh",
                ProductCoordinates = new double[,] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 569,
                ProductSection = "Center Knee",
                ProductCoordinates = new double[,] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }
            });


            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1220,
                ProductSection = "Left Shoulder",
                ProductCoordinates = new double[,] { { 1210, 791 }, { 1375, 791 }, { 1210, 1146 }, { 1375, 1146 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1220,
                ProductSection = "Left Arm",
                ProductCoordinates = new double[,] { { 1209, 1146 }, { 1376, 1146 }, { 1159, 1576 }, { 1362, 1576 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1220,
                ProductSection = "Left Wrist",
                ProductCoordinates = new double[,] { { 1158, 1576 }, { 1361, 1576 }, { 1133, 2150 }, { 1252, 2166 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1220,
                ProductSection = "Right Shoulder",
                ProductCoordinates = new double[,] { { 2079, 782 }, { 2271, 782 }, { 2079, 1144 }, { 2271, 1144 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1220,
                ProductSection = "Right Arm",
                ProductCoordinates = new double[,] { { 2079, 1144 }, { 2271, 1144 }, { 2118, 1556 }, { 2322, 1556 } }

            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1220,
                ProductSection = "Right Wrist",
                ProductCoordinates = new double[,] { { 2118, 1556 }, { 2322, 1556 }, { 2225, 2141 }, { 2352, 2125 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1220,
                ProductSection = "Center Shoulder",
                ProductCoordinates = new double[,] { { 1375, 791 }, { 2079, 781 }, { 1375, 1097 }, { 2079, 1097 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1220,
                ProductSection = "Center Bust",
                ProductCoordinates = new double[,] { { 1375, 1097 }, { 2079, 1097 }, { 1409, 1491 }, { 2055, 1491 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1220,
                ProductSection = "Center Waist",
                ProductCoordinates = new double[,] { { 1409, 1491 }, { 2055, 1491 }, { 1400, 1628 }, { 2074, 1628 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1220,
                ProductSection = "Center Abdomen",
                ProductCoordinates = new double[,] { { 1400, 1628 }, { 2074, 1628 }, { 1390, 1762 }, { 2091, 1762 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1220,
                ProductSection = "Center High Hip",
                ProductCoordinates = new double[,] { { 1390, 1762 }, { 2091, 1762 }, { 1377, 1906 }, { 2105, 1906 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1220,
                ProductSection = "Center Low Hip",
                ProductCoordinates = new double[,] { { 1377, 1906 }, { 2105, 1906 }, { 1350, 2255 }, { 2123, 2255 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1220,
                ProductSection = "Center High Thigh",
                ProductCoordinates = new double[,] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1220,
                ProductSection = "Center Low Thigh",
                ProductCoordinates = new double[,] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }
            });
            lstProductCoordinatesData.Add(new ProductData()
            {
                MannequinType = "Old Mannequin Template",
                ProductID = 1220,
                ProductSection = "Center Knee",
                ProductCoordinates = new double[,] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }
            });

            MemberData md00000 = new MemberData();
            md00000.MemberID = 14715;
            md00000.MemberCoordinates = member14715Data;
            md00000.ProductCoordinatesData = lstProductCoordinatesData;
            members.Add(md00000);

            MemberData md0000 = new MemberData();
            md0000.MemberID = 13826;
            md0000.MemberCoordinates = member13826Data;
            md0000.ProductCoordinatesData = lstProductCoordinatesData;
            members.Add(md0000);

            MemberData md000 = new MemberData();
            md000.MemberID = 13828;
            md000.MemberCoordinates = member13828Data;
            md000.ProductCoordinatesData = lstProductCoordinatesData;
            members.Add(md000);

            MemberData md00 = new MemberData();
            md00.MemberID = 13709;
            md00.MemberCoordinates = member13709Data;
            md00.ProductCoordinatesData = lstProductCoordinatesData;
            members.Add(md00);

            MemberData md0 = new MemberData();
            md0.MemberID = 13835;
            md0.MemberCoordinates = member13835Data;
            md0.ProductCoordinatesData = lstProductCoordinatesData;
            members.Add(md0);

            MemberData md = new MemberData();
            md.MemberID = 13721;
            md.MemberCoordinates = member13721Data;
            md.ProductCoordinatesData = lstProductCoordinatesData;
            members.Add(md);

            MemberData md1 = new MemberData();
            md1.MemberID = 13724;
            md1.MemberCoordinates = member13724Data;
            md1.ProductCoordinatesData = lstProductCoordinatesData;
            members.Add(md1);
        }

        void ResetCoordinates()
        {
            srcX1 = 0; srcX2 = 0; srcX3 = 0; srcX4 = 0; srcY1 = 0; srcY2 = 0; srcY3 = 0; srcY4 = 0; dstX1 = 0; dstX2 = 0; dstY1 = 0; dstY2 = 0; XAdditional = 0; YAdditional = 0; //dstY3 = 0; dstY4 = 0; dstX3 = 0; dstX4 = 0; 
            minSrcX = 0; maxSrcX = 0; maxDstX = 0; minSrcY = 0; maxSrcY = 0; maxDstY = 0; dstWidth = 0; dstHeight = 0;
        }

        private double[,] GetMemberData(long memberId)
        {
            double conversionVal = 3.71142908458136;

            double[,] xyVals = new double[71, 2];

            int counterIndex = 0;

            //string url = string.Format(ExternalServiceBaseURL + "/pi/ws/item_image_exists/{0}", fileName);
            using (var webClient = new System.Net.WebClient())
            {
                string urlv3stack = "";
                var json = "";

                urlv3stack = string.Format("https://v3qa.selfiestyler.com/app_dev.php/pi/ws/user_mask_marker_json/{0}", memberId);
                json = webClient.DownloadString(urlv3stack);

                JObject obj = JObject.Parse(json);

                if (Convert.ToBoolean(obj.SelectToken("success")))
                {
                    //If Marker JSON Data available
                    if (Convert.ToString(obj.SelectToken("data").SelectToken("marker_json")) != string.Empty)
                    {
                        //If Marker JSON Data available
                        JObject o = JObject.Parse(json);

                        JToken token = obj.SelectToken("data");
                        List<JToken> tokens = token.ToList();

                        int numItems = tokens.Count;

                        JObject obj2 = JObject.Parse(token.ToString());

                        //photograde down
                        foreach (var pair in obj2)
                        {
                            if (pair.Key.Contains("marker_json"))
                            {
                                string value = pair.Value.ToString();
                                value = value.Remove(value.LastIndexOf(']')).Substring(1);

                                while (value.Length > 0)
                                {
                                    string xYCoordPair = GetNextXYCoord(ref value);
                                    string vals = xYCoordPair.Replace("[", "").Replace("]", "");
                                    string[] valsArry = vals.Split(',');
                                    xyVals[counterIndex, 0] = (Convert.ToDouble(valsArry[0]) * (conversionVal * (60 / 60.0)));
                                    xyVals[counterIndex, 1] = (Convert.ToDouble(valsArry[1]) * (conversionVal * (60 / 60.0)));
                                    counterIndex++;
                                }
                            }
                        }
                    }
                }
            }

            return xyVals;
        }

        private string GetNextXYCoord(ref string input)
        {
            string xyCoord = input.Substring(0, input.IndexOf(']') + 1);
            if (input.Length > xyCoord.Length)
            {
                input = input.Substring(input.IndexOf(']') + 2);
            }
            else
            {
                input = string.Empty;
            }

            return xyCoord;
        }

        private void FindContours(Bitmap bmp)
        {
            Image<Bgra, Byte> img1 = new Image<Bgra, Byte>(bmp);
            //Convert the img1 to grayscale and then filter out the noise
            Image<Gray, Byte> gray1 = img1.Convert<Gray, Byte>().PyrDown().PyrUp();
            //Canny Edge Detector
            Image<Gray, Byte> cannyGray = gray1.Canny(75, 180);

            cannyGray.Save("c:\\projects\\Grey_Image.png");

            //Call FindContours method
            //List<Point> contours = cannyGray.FindContours();
            //return contours;
        }

        private Point[] FindContours(Image<Bgr, byte> inpImage, string fileName)
        {
            //Gray scale image of input colored image
            Image<Gray, byte> imgOutput = inpImage.Convert<Gray, byte>().PyrDown().PyrUp();

            //Image<Gray, byte> imgOutput = inpImage.Convert<Gray, byte>();
            //Image<Gray, byte> imgOutput = inpImage.Convert<Gray, byte>().PyrDown().PyrUp();

            //Create object for contours
            Emgu.CV.Util.VectorOfVectorOfPoint contours = new Emgu.CV.Util.VectorOfVectorOfPoint();
            Mat hier = new Mat();

            Image<Gray, byte> imgout = new Image<Gray, byte>(inpImage.Width, inpImage.Height);

            //Applying Canny algorithm
            Mat cannyEdges = new Mat();
            CvInvoke.Canny(imgOutput, cannyEdges, 0, 255);

            //Finding Contouring
            CvInvoke.FindContours(cannyEdges, contours, hier, RetrType.External, ChainApproxMethod.ChainApproxSimple);

            //Draw contouring in output image
            CvInvoke.DrawContours(imgout, contours, -1, new MCvScalar(255, 0, 0));

            //Get all the contours
            Point[][] contourArray = contours.ToArrayOfArray();

            //Filter out the maximum numbers of contour sub array
            Point[] maxArray = contourArray.OrderByDescending(a => a.Length).FirstOrDefault();

            //Mat destArray = new Mat();
            //Mat matrix = new Mat();

            //CvInvoke.Transform(maxArray, maxArray, matrix);

            //Save contoured image on disk
            imgout.Bitmap.Save("c:\\projects\\" + fileName + "_contoured.png");

            return maxArray;
        }


        // Draw delaunay triangles
        private static void draw_delaunay(Mat img, Subdiv2D subdiv, MCvScalar delaunay_color)
        {
            try
            {
                Triangle2DF[] triangleList = new Triangle2DF[] { };
                triangleList = subdiv.GetDelaunayTriangles(false);
                List<Point> pt = new List<Point>();
                Size size = img.Size;
                Rectangle rect = new Rectangle(0, 0, size.Width, size.Height);

                for (int i = 0; i < triangleList.Count(); i++)
                {
                    Triangle2DF t = triangleList[i];
                    pt.Add(new Point((int)t.V0.X, (int)t.V0.Y));
                    pt.Add(new Point((int)t.V1.X, (int)t.V1.Y));
                    pt.Add(new Point((int)t.V2.X, (int)t.V2.Y));

                    // Draw rectangles completely inside the image.
                    if (rect.Contains(pt[0]) && rect.Contains(pt[1]) && rect.Contains(pt[2]))
                    {
                        CvInvoke.Line(img, pt[0], pt[1], delaunay_color, 1, LineType.AntiAlias, 0);
                        CvInvoke.Line(img, pt[1], pt[2], delaunay_color, 1, LineType.AntiAlias, 0);
                        CvInvoke.Line(img, pt[2], pt[0], delaunay_color, 1, LineType.AntiAlias, 0);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        // Draw a single point
        private static void draw_point(Mat img, PointF fp, MCvScalar color)
        {
            CvInvoke.Circle(img, new Point((int)fp.X, (int)fp.Y), 2, color, 1, LineType.AntiAlias, 0);
        }

        private void generateMesh()
        {
            // Define colors for drawing.
            MCvScalar delaunay_color = new MCvScalar(255, 255, 255);
            MCvScalar points_color = new MCvScalar(0, 0, 255);

            // Read in the image.
            Mat img = CvInvoke.Imread("c:\\projects\\obama.jpg");

            // Keep a copy around
            Mat img_orig = img.Clone();

            // Rectangle to be used with Subdiv2D
            //Size size = img.Size;
            Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);
            //{ 1210, 784 }, { 1377, 784 }, { 1210, 1148 }, { 1377, 1148 }

            // Create an instance of Subdiv2D
            Subdiv2D subdiv = new Subdiv2D(rect);

            // Create a vector of points.
            List<PointF> points = new List<PointF>();

            // Read in the points from a text file
            Point[] contourPoints = new Point[] {  //FindContours(new Image<Bgr, byte>("c:\\projects\\obama.jpg"), "obama");
                new Point(207, 242),
                new Point(210, 269),
                new Point(214, 297),
                new Point(220, 322),
                new Point(229, 349),
                new Point(243, 373),
                new Point(261, 394),
                new Point(282, 412),
                new Point(308, 416),
                new Point(334, 408),
                new Point(356, 388),
                new Point(374, 367),
                new Point(388, 344),
                new Point(397, 319),
                new Point(403, 291),
                new Point(405, 263),
                new Point(408, 235),
                new Point(221, 241),
                new Point(232, 225),
                new Point(250, 219),
                new Point(270, 219),
                new Point(289, 223),
                new Point(320, 222),
                new Point(339, 216),
                new Point(358, 215),
                new Point(375, 220),
                new Point(387, 233),
                new Point(304, 240),
                new Point(304, 259),
                new Point(304, 277),
                new Point(304, 296),
                new Point(281, 311),
                new Point(292, 312),
                new Point(303, 314),
                new Point(315, 310),
                new Point(326, 307),
                new Point(243, 247),
                new Point(254, 240),
                new Point(266, 239),
                new Point(276, 245),
                new Point(266, 247),
                new Point(254, 249),
                new Point(332, 243),
                new Point(343, 236),
                new Point(356, 236),
                new Point(367, 242),
                new Point(356, 245),
                new Point(344, 245),
                new Point(263, 346),
                new Point(278, 341),
                new Point(293, 336),
                new Point(303, 340),
                new Point(315, 335),
                new Point(331, 338),
                new Point(348, 342),
                new Point(332, 353),
                new Point(318, 360),
                new Point(305, 362),
                new Point(294, 361),
                new Point(279, 356),
                new Point(270, 347),
                new Point(293, 347),
                new Point(304, 348),
                new Point(316, 345),
                new Point(342, 343),
                new Point(316, 345),
                new Point(304, 348),
                new Point(294, 347),
            };  
            
            for (int i = 0; i < contourPoints.Length; i++)
            {
                points.Add(new PointF(contourPoints[i].X, contourPoints[i].Y));
            }

            Mat img_copy = null;

            int j = 0;
            
            // Insert points into subdiv
            foreach (PointF it in points)
            {
                j += 1;

                subdiv.Insert(it);

                try
                {
                    //if(img_copy == null)
                        img_copy = img_orig.Clone();
                
                    // Draw delaunay triangles
                    draw_delaunay(img_copy, subdiv, delaunay_color);

                    img_copy.Save("c:\\projects\\imageCopy" + j + ".png");
                }
                catch (Exception ex) { }
            }

            try
            {
                // Draw delaunay triangles
                draw_delaunay(img, subdiv, delaunay_color);

                // Draw points
                foreach (PointF it in points)
                {
                    draw_point(img, it, points_color);
                }
            }
            catch (Exception ex) { }

            img.Save("c:\\projects\\testMeshed.png");
        }

        private void generateMeshNew()
        {
            MCvScalar points_color = new MCvScalar(0, 0, 255);
            #region create random points in the range of [0, maxValue]
            //PointF[] pts = new PointF[67];
            

            Random r = new Random((int)(DateTime.Now.Ticks & 0x0000ffff));
            Point[] contourPoints = FindContours(new Image<Bgr, byte>("c:\\projects\\Chaser_CW6640_Black_R_XS.png"), "Chaser_CW6640_Black_R_XS");
            PointF[] pts = new PointF[contourPoints.Length];
            // Read in the points from a text file

            Point[] contourPoints1 = new Point[] {  //FindContours(new Image<Bgr, byte>("c:\\projects\\obama.jpg"), "obama");

                new Point(1121, 2445),
                new Point(1121, 2127),
                new Point(1143, 1867),
                new Point(1173, 1633),
                new Point(1193, 1491),
                new Point(1231, 1259),
                new Point(1333, 1155),
                new Point(1475, 1105),
                new Point(1583, 1169),
                new Point(1717, 1189),
                new Point(1949, 1119),
                new Point(2097, 1151),
                new Point(1191, 1277),
                new Point(2223, 1463),
                new Point(2249, 1643),
                new Point(2281, 1849),
                new Point(2303, 1971),
                new Point(2305, 2043),
                new Point(2307, 2213),
                new Point(2217, 2433),
                new Point(2201, 2339),
                new Point(2177, 2223),
                new Point(2155, 2147),
                new Point(2123, 2023),
                new Point(2111, 1911),
                new Point(2095, 1819),
                new Point(2067, 1701),
                new Point(2055, 1651),
                new Point(2049, 1715),
                new Point(2037, 1793),
                new Point(2029, 1879),
                new Point(2023, 1951),
                new Point(2023, 1951),
                //new Point(207, 242),
                //new Point(210, 269),
                //new Point(214, 297),
                //new Point(220, 322),
                //new Point(229, 349),
                //new Point(243, 373),
                //new Point(261, 394),
                //new Point(282, 412),
                //new Point(308, 416),
                //new Point(334, 408),
                //new Point(356, 388),
                //new Point(374, 367),
                //new Point(388, 344),
                //new Point(397, 319),
                //new Point(403, 291),
                //new Point(405, 263),
                //new Point(408, 235),
                //new Point(221, 241),
                //new Point(232, 225),
                //new Point(250, 219),
                //new Point(270, 219),
                //new Point(289, 223),
                //new Point(320, 222),
                //new Point(339, 216),
                //new Point(358, 215),
                //new Point(375, 220),
                //new Point(387, 233),
                //new Point(304, 240),
                //new Point(304, 259),
                //new Point(304, 277),
                //new Point(304, 296),
                //new Point(281, 311),
                //new Point(292, 312),
                //new Point(303, 314),
                //new Point(315, 310),
                //new Point(326, 307),
                //new Point(243, 247),
                //new Point(254, 240),
                //new Point(266, 239),
                //new Point(276, 245),
                //new Point(266, 247),
                //new Point(254, 249),
                //new Point(332, 243),
                //new Point(343, 236),
                //new Point(356, 236),
                //new Point(367, 242),
                //new Point(356, 245),
                //new Point(344, 245),
                //new Point(263, 346),
                //new Point(278, 341),
                //new Point(293, 336),
                //new Point(303, 340),
                //new Point(315, 335),
                //new Point(331, 338),
                //new Point(348, 342),
                //new Point(332, 353),
                //new Point(318, 360),
                //new Point(305, 362),
                //new Point(294, 361),
                //new Point(279, 356),
                //new Point(270, 347),
                //new Point(293, 347),
                //new Point(304, 348),
                //new Point(316, 345),
                //new Point(342, 343),
                //new Point(316, 345),
                //new Point(304, 348),
                //new Point(294, 347),
            };

            PointF[] pts1 = new PointF[contourPoints1.Length];

            for (int i = 0; i < pts.Length; i++)
                //pts[i] = new PointF((float)r.NextDouble() * maxValue, (float)r.NextDouble() * maxValue);
                pts[i] = new PointF((float)contourPoints[i].X, (float)contourPoints[i].Y);
            //pts1[i] = new PointF((float)contourPoints[i].X, (float)contourPoints[i].Y);
            #endregion

            Triangle2DF[] delaunayTriangles;
            VoronoiFacet[] voronoiFacets;
            using (Subdiv2D subdivision = new Subdiv2D(pts))
            {
                //Obtain the delaunay's triangulation from the set of points;
                delaunayTriangles = subdivision.GetDelaunayTriangles();

                //Obtain the voronoi facets from the set of points
                voronoiFacets = subdivision.GetVoronoiFacets();
            }

            //create an image for display purpose
            //Image<Bgr, Byte> img = new Image<Bgr, byte>((int)maxValue, (int)maxValue);
            //Image<Bgr, Byte> img = new Image<Bgr, byte>("c:\\projects\\obama.jpg");
            Image<Bgr, Byte> img = new Image<Bgr, byte>("c:\\projects\\Chaser_CW6640_Black_R_XS.png");

            //Draw the voronoi Facets
            //foreach (VoronoiFacet facet in voronoiFacets)
            //{
            //    Point[] points1 = Array.ConvertAll<PointF, Point>(facet.Vertices, Point.Round);

            //    //Draw the facet in color
            //    img.FillConvexPoly(
            //        points1,
            //        new Bgr(r.NextDouble() * 120, r.NextDouble() * 120, r.NextDouble() * 120)
            //        );

            //    //highlight the edge of the facet in black
            //    img.DrawPolyline(points1, true, new Bgr(Color.Black), 2);

            //    //draw the points associated with each facet in red
            //    img.Draw(new CircleF(facet.Point, 5.0f), new Bgr(Color.Red), 0);
            //}

            //Draw the Delaunay triangulation
            foreach (Triangle2DF triangles in delaunayTriangles)
            {
                img.Draw(triangles, new Bgr(Color.White), 1);
            }
            // Draw points
            foreach (PointF it in contourPoints)
            {
                draw_point(img.Mat, it, points_color);
            }


            img.Save("c:\\projects\\Result-2.png");
            //display the image


            //// Define colors for drawing.
            //MCvScalar delaunay_color = new MCvScalar(255, 255, 255);
            //MCvScalar points_color = new MCvScalar(0, 0, 255);

            //// Read in the image.
            //Mat img = CvInvoke.Imread("c:\\projects\\626_slice_leftshoulder.png");

            //// Keep a copy around
            //Mat img_orig = img.Clone();

            //// Rectangle to be used with Subdiv2D
            ////Size size = img.Size;
            ////Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);
            //Rectangle rect = new Rectangle(1200, 780, 1376 - 1200, 1150 - 780);
            ////{ 1210, 784 }, { 1377, 784 }, { 1210, 1148 }, { 1377, 1148 }

            //// Create an instance of Subdiv2D


            //// Create a vector of points.

            //List<PointF> points = new List<PointF>();

            //// Read in the points from a text file
            //Point[] contourPoints = FindContours(new Image<Bgr, byte>("c:\\projects\\626_slice_leftshoulder.png"), "626_slice_leftshoulder");
            //for (int i = 0; i < contourPoints.Length; i++)
            //{
            //    points.Add(new PointF(contourPoints[i].X, contourPoints[i].Y));
            //}

            //float maxValue = 600;
            //Subdiv2D subdiv = new Subdiv2D(rect);
            //Image<Bgr, Byte> img1 = new Image<Bgr, byte>((int)maxValue, (int)maxValue);

            //foreach (Triangle2DF triangles in subdiv.GetDelaunayTriangles(true))
            //{
            //    img1.Draw(triangles, new Bgr(Color.White), 1);
            //}
            //img1.Save("c:\\projects\\testMeshed-1.png");

            //int j = 0;
            //// Insert points into subdiv
            //foreach (PointF it in points)
            //{
            //    j += 1;
            //    subdiv.Insert(it);
            //    try
            //    {
            //        Mat img_copy = new Mat();
            //        img_orig.CopyTo(img_copy);
            //        // Draw delaunay triangles
            //        //draw_delaunay(img_copy, subdiv, delaunay_color);
            //        draw_delaunay(img_copy, subdiv, delaunay_color);
            //        img_copy = null;
            //        //img_copy.Save("c:\\projects\\imageCopy" + j + ".png");
            //    }
            //    catch (Exception ex)
            //    {
            //        string Msg = ex.Message.ToString();
            //    }
            //}

            //try
            //{
            //    // Draw delaunay triangles
            //    draw_delaunay(img, subdiv, delaunay_color);
            //    // Draw points
            //    foreach (PointF it in points)
            //    {
            //        draw_point(img, it, points_color);
            //    }
            //}
            //catch (Exception ex) { }

            //img.Save("c:\\projects\\testMeshed.png");
        }
    }

    //----------------------------------------------------------------------------------------
    //	Copyright © 2006 - 2018 Tangible Software Solutions, Inc.
    //	This class can be used by anyone provided that the copyright notice remains intact.
    //
    //	This class is used to convert some of the C++ std::vector methods to C#.
    //----------------------------------------------------------------------------------------
    internal static class VectorHelper
    {
        public static void Resize<T>(this List<T> list, int newSize, T value = default(T))
        {
            if (list.Count > newSize)
                list.RemoveRange(newSize, list.Count - newSize);
            else if (list.Count < newSize)
            {
                for (int i = list.Count; i < newSize; i++)
                {
                    list.Add(value);
                }
            }
        }

        public static void Swap<T>(this List<T> list1, List<T> list2)
        {
            List<T> temp = new List<T>(list1);
            list1.Clear();
            list1.AddRange(list2);
            list2.Clear();
            list2.AddRange(temp);
        }

        public static List<T> InitializedList<T>(int size, T value)
        {
            List<T> temp = new List<T>();
            for (int count = 1; count <= size; count++)
            {
                temp.Add(value);
            }

            return temp;
        }

        public static List<List<T>> NestedList<T>(int outerSize, int innerSize)
        {
            List<List<T>> temp = new List<List<T>>();
            for (int count = 1; count <= outerSize; count++)
            {
                temp.Add(new List<T>(innerSize));
            }

            return temp;
        }

        public static List<List<T>> NestedList<T>(int outerSize, int innerSize, T value)
        {
            List<List<T>> temp = new List<List<T>>();
            for (int count = 1; count <= outerSize; count++)
            {
                temp.Add(InitializedList(innerSize, value));
            }

            return temp;
        }
    }

    //----------------------------------------------------------------------------------------
    //	Copyright © 2006 - 2018 Tangible Software Solutions, Inc.
    //	This class can be used by anyone provided that the copyright notice remains intact.
    //
    //	This class provides the ability to replicate the behavior of the C/C++ functions for 
    //	generating random numbers, using the .NET Framework System.Random class.
    //	'rand' converts to the parameterless overload of NextNumber
    //	'random' converts to the single-parameter overload of NextNumber
    //	'randomize' converts to the parameterless overload of Seed
    //	'srand' converts to the single-parameter overload of Seed
    //----------------------------------------------------------------------------------------
    internal static class RandomNumbers
    {
        private static System.Random r;

        public static int NextNumber()
        {
            if (r == null)
                Seed();

            return r.Next();
        }

        public static int NextNumber(int ceiling)
        {
            if (r == null)
                Seed();

            return r.Next(ceiling);
        }

        public static void Seed()
        {
            r = new System.Random();
        }

        public static void Seed(int seed)
        {
            r = new System.Random(seed);
        }
    }
}