using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Windows.Storage;

namespace treinamentos.Src.Classes {
    public class Charset {
        public static String EncodeUtf8ToIso(String text) {
            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            Encoding utf8 = Encoding.UTF8;
            byte[] utfBytes = utf8.GetBytes(text);
            byte[] newBytes = Encoding.Convert(utf8, iso, utfBytes, 0 , utfBytes.Length);
            string msg = utf8.GetString(newBytes, 0, newBytes.Length);
            return msg;
        }
    }
}
