using MARS_Expert.WebService;
using System.Runtime.InteropServices;
using System;
//using MARS_Expert.webService1;

namespace MARS_Expert.Manager
{

    /// <summary>
    /// The Actor status: Activated or Deactivated.
    /// </summary>
    public enum Status
    {
        Activated,
        Deactivated
    }

    public enum Transformation
    {
        Translation,
        Rotation,
        Scale
    }

    /// <summary>
    /// Contains a group of function that helps in programming.
    /// </summary>
    public static class Helper
    {

        #region Move Form using native code

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        #endregion

        public static Service1 service = new Service1();
        public static Status StringToStatus(string str)
        {
            Status st;
            if (str == "Activated")
                st = Status.Activated;
            else
                st = Status.Deactivated;

            return st;
        }

        public static string getObjectPath(string objType)
        {
            string ret = "";
            switch (objType.ToLower())
            {
                case "sign":
                    ret = Constante.ObjectsPath + "Signs\\";
                    break;
                case "tool":
                    ret = Constante.ObjectsPath + "Tools\\";
                    break;
                case "material":
                    ret = Constante.ObjectsPath + "Materials\\";
                    break;
                default:
                    break;
            }
            return ret;
        }

        public static float DegreesToRadians(float degrees)
        {
            float radians = degrees * (3.141592654f / 180.0f);
            return radians;
        }

        public static float RadiansToDegrees(float rad)
        {
            float degrees = rad * (180.0f / 3.141592654f);
            return degrees;
        }

        public static void TransformAxis(ref float X, ref float Y, ref float Z, Transformation transform, bool ToOpenGl)
        {
            if (transform == Transformation.Translation)
            {
                if (ToOpenGl)
                {
                    /*
                     * X ->  X
                       Y ->  Z
                       Z -> -Y
                     */
                    float temp = Y;
                    Y = Z;
                    Z = -temp;
                }
                else 
                {
                    /*
                    X ->  X
                    Y -> -Z
                    Z ->  Y
                    */

                    float temp = Y;
                    Y = -Z;
                    Z = temp;
                }
            }
            else
                if (transform == Transformation.Rotation)
                {
                    if (ToOpenGl)
                    {
                        /*
                         * X -> -X
                           Y -> -Z
                           Z ->  Y
                         */
                        X = -X;
                        float temp = Y;
                        Y = -Z;
                        Z = temp;
                    }
                    else
                    {
                        /*
                         X -> -X
                         Y ->  Z
                         Z -> -Y
                         */
                        X = -X;
                        float temp = Y;
                        Y = Z;
                        Z = -temp;
                    }
                }
        }

        public static Microsoft.DirectX.Vector3 Normalize(Microsoft.DirectX.Vector3 vect)
        {
            Microsoft.DirectX.Vector3 ret = new Microsoft.DirectX.Vector3();
            float legnth = GetFourDigitsNumber(Math.Sqrt(vect.X * vect.X + vect.Y * vect.Y + vect.Z * vect.Z).ToString());
            ret.X = vect.X / legnth;
            ret.Y = vect.Y / legnth;
            ret.Z = vect.Z / legnth;

            return ret;
        }

        public static float GetFourDigitsNumber(String str)
        {
            float ret = 0;
            int commaIndex = str.IndexOf('.');
            if (str.Length <= commaIndex + 4 || commaIndex == -1)
                return float.Parse(str);
            //gets the fourth digit after the comma.
            int fouthdigit = Int32.Parse(str[commaIndex + 4].ToString());
            if (fouthdigit >= 5)
            {
                string temp = str.Substring(0, commaIndex + 4);
                //MessageBox.Show("temp: " + temp);
                float flt = float.Parse(temp);
                ret = flt + 0.001f;
                //MessageBox.Show("flt: " + flt + "\nret: " + ret);
            }
            else
            {
                string temp = str.Substring(0, commaIndex + 4);
                //MessageBox.Show("temp: " + temp);
                ret = float.Parse(temp);
            }
            return ret;
        }
    }

    public class ObjForListView
    {
        public string Id;
        public string Name;
        public string Type;

        public ObjForListView()
        { }

        public ObjForListView(string id, string name, string type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

        public override string ToString()
        {
            //return base.ToString();
            return this.Name;
        }
    }

    //---------------------------------------------------------------------
    /// <summary>
    /// Use the same GUI to Add or update a procedure, step or Model.
    /// </summary>
    public enum Operation
    {
        AddNew,
        Update
    }
    //---------------------------------------------------------------------
    /// <summary>
    /// Represents a failure or a type of failure.
    /// </summary>
    public enum Subject
    {
        FailureTypeSbjct,
        FailureSbjct,
        ProcedureSbjct

    }

    //-----------------------------------------------------------------------
}