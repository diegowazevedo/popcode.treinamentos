using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Windows.Storage;

namespace treinamentos.Src.Classes {
    class Utils {
        public static ImageSource GetImageLocal(String nome) {
            Uri _baseUri = new Uri("/Resources/Imagens/" + nome, UriKind.Relative);
            return new BitmapImage(_baseUri);
        }

        public static ImageSource GetImageWeb(String url) {
            ImageSource img = new BitmapImage(new Uri(url));
            return img;
        }

        public static String readFile(String path) {
            var resource = App.GetResourceStream(new Uri(path, UriKind.Relative));
            if (resource == null) {
                return null;
            }
            StreamReader reader = new StreamReader(resource.Stream);
            String arquivo = reader.ReadToEnd();
            return arquivo;
        }
    }
    public class Prefs {
        public static void setString(String key, String value) {
            IsolatedStorageSettings.ApplicationSettings[key] = value;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public static String getString(String key) {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(key)) {
                String s = (String)IsolatedStorageSettings.ApplicationSettings[key];
                return s;
            }
            return null;
        }
    }

    public class PrefsFileWP8 {
        public static async void setString(String key, String value) {
            String fileName = key + ".txt";
            //Diretório da aplicação para salvar arquivos
            StorageFolder dir = ApplicationData.Current.LocalFolder;
            //Cria arquivo
            StorageFile file = await dir.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            //Escreve no arquivo
            StreamWriter writer = new StreamWriter(await file.OpenStreamForWriteAsync());
            await writer.WriteLineAsync(value);
            writer.Close();
        }

        public static async Task<String> getString(String key) {
            String fileName = key + ".txt";
            //Diretório da aplicação para salvar arquivos
            StorageFolder dir = ApplicationData.Current.LocalFolder;
            StorageFile file = await dir.GetFileAsync(fileName);
            //Verifica se o arquivo existe
            if (file != null) {
                //Abre o arquivo para leitura
                StreamReader reader = new StreamReader(await file.OpenStreamForReadAsync());
                String s = await reader.ReadLineAsync();
                return s;
            }
            return null;
        }
    }
}
