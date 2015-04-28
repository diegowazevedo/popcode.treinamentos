using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace treinamentos.Src.Classes.Entity {
    [Table]
    public class Training : Model {

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
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

        private EntitySet<TeacherTraining> _teacherTraining = new EntitySet<TeacherTraining>();
        [Association(Name = "FK_TeacherTrainings_Training", Storage = "_teacherTraining", OtherKey = "trainingId", ThisKey = "id")]
        internal ICollection<TeacherTraining> teacherTraining {
            get { return _teacherTraining; }
            set { _teacherTraining.Assign(value); }
        }

        public ICollection<Teacher> teachers {
            get {

                var teachers = new ObservableCollection<Teacher>(from tt in teacherTraining select tt.teacher);
                teachers.CollectionChanged += TeacherCollectionChanged;
                return teachers;
            }
        }

        private void TeacherCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (NotifyCollectionChangedAction.Add == e.Action) {
                foreach (Teacher addedTeacher in e.NewItems)
                    OnTeacherAdded(addedTeacher);
            }

            if (NotifyCollectionChangedAction.Remove == e.Action) {
                foreach (Teacher removedTeacher in e.OldItems)
                    OnTeacherRemoved(removedTeacher);
            }
        }

        private void OnTeacherAdded(Teacher addedTeacher) {
            TeacherTraining tt = new TeacherTraining() { teacher = addedTeacher, training = this };
        }

        private void OnTeacherRemoved(Teacher removedTeacher) {
            TeacherTraining ttRecord = teacherTraining.SingleOrDefault(tt => tt.training == this && tt.teacher == removedTeacher);
            if (ttRecord != null)
                ttRecord.Remove();
        }

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
