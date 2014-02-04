using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARS_Expert.Manager
{
    class Constante
    {
        public static int PATT_MAX = 5;
        public static int MARK_MAX = 8;
        //it should be saved in a config file if changed by the user
        public static string ObjectsPath = System.Windows.Forms.Application.StartupPath + @"\Objects3d\";
        //public static string dataPath = @"E:\VS10_Projects\Mars Projects\Data\Mars Data\test\";
        //public static string dataPath = @"E:\MyServices\Android\Data\";
        //public static string dataPath = @"http://192.168.43.7/Android/Data/";
        public static string dataPath = "http://localhost/Android/Data/";
        //public static string dataPath = "http://192.168.43.96/Android/Data/";
        public static int binarisation = 100;

        public static int zFactor = 1;
        public static float translateFactor = 40;
        public static float scaleFactor = 1.0f / 41.0f;// 0.4f / 1.3f;
        public static float stableAngle = 270;
        public static float angle = 270;
    }
}
