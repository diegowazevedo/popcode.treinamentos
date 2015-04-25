using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace treinamentos.Src.Classes {
    [Table]
    public class Training : Model {

        [Column(IsPrimaryKey = true)]
        public int id { get; set; }

        [Column(CanBeNull = false)]
        public bool backend { get; set; }

        [Column(CanBeNull = true)]
        public String banner { get; set; }

        [Column(CanBeNull = false)]
        public String description { get; set; }

        [Column(CanBeNull = false)]
        public bool display { get; set; }

        [Column(CanBeNull = false)]
        public int front { get; set; }

        [Column(CanBeNull = true)]
        public String logo { get; set; }

        [Column(CanBeNull = false)]
        public String methodology { get; set; }

        [Column(CanBeNull = false)]
        public bool mobile { get; set; }

        [Column(CanBeNull = false)]
        public String name { get; set; }

        [Column(CanBeNull = false)]
        public String name_short { get; set; }

        [Column(CanBeNull = false)]
        public String requirements { get; set; }

        [Column(CanBeNull = false)]
        public String resource_uri { get; set; }

        [Column(CanBeNull = false)]
        public String title_short { get; set; }

        [Column(CanBeNull = false)]
        public String short_description { get; set; }

        [Column(CanBeNull = false)]
        public int sort { get; set; }

        [Column(CanBeNull = false)]
        public String tagline { get; set; }

        [Column(CanBeNull = false)]
        public String target { get; set; }

        [Column(CanBeNull = false)]
        public String topics { get; set; }

        [Column(CanBeNull = false)]
        public List<Teacher> teacher { get; set; }

        public ImageSource imageLogo {
            get {
                BitmapImage img = new BitmapImage(new Uri("http://treinamentos.mobi/" + logo));
                img.DownloadProgress += DownloadComplete;
                return img;
            }
        }

        void DownloadComplete(object sender, DownloadProgressEventArgs e) {
            if (e.Progress == 100) {
                progressVisibility = Visibility.Collapsed;
                imageVisibility = Visibility.Visible;
            }
        }

        private Visibility _progressVisibility = Visibility.Visible;

        public Visibility progressVisibility {
            get {
                return this._progressVisibility;
            }
            set {
                if (value != this._progressVisibility) {
                    this._progressVisibility = value;
                    OnPropertyChanged("progressVisibility");
                }
            }
        }

        private Visibility _imageVisibility = Visibility.Collapsed;

        public Visibility imageVisibility {
            get {
                return this._imageVisibility;
            }
            set {
                if (value != this._imageVisibility) {
                    this._imageVisibility = value;
                    OnPropertyChanged("imageVisibility");
                }
            }
        }
    }
}
