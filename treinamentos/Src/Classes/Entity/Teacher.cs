using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace treinamentos.Src.Classes.Entity {
    [Table]
    public class Teacher {

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int id { get; set; }

        [Column(CanBeNull = false)]
        public String shortname { get; set; }

        [Column(CanBeNull = false)]
        public String name { get; set; }

        [Column(CanBeNull = false)]
        public String description { get; set; }

        [Column(CanBeNull = false)]
        public String knowledge { get; set; }

        [Column(CanBeNull = true)]
        public String photo { get; set; }

        [Column(CanBeNull = false)]
        public int order { get; set; }

        [Column(CanBeNull = false)]
        public String resource_uri { get; set; }

        [Column(CanBeNull = false)]
        public bool display { get; set; }

        private EntitySet<TeacherTraining> _teacherTraining = new EntitySet<TeacherTraining>();
        [Association(Name = "FK_TeacherTraining_Teacher", Storage = "_teacherTraining", OtherKey = "teacherId", ThisKey = "id")]
        internal ICollection<TeacherTraining> teacherTraining {
            get { return _teacherTraining; }
            set { _teacherTraining.Assign(value); }
        }

        public ICollection<Training> trainings {
            get {
                var trainings = new ObservableCollection<Training>(from tt in teacherTraining select tt.training);
                trainings.CollectionChanged += TrainingCollectionChanged;
                return trainings;
            }
        }
        private void TrainingCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (NotifyCollectionChangedAction.Add == e.Action) {
                foreach (Training addedTraining in e.NewItems)
                    OnTrainingAdded(addedTraining);
            }

            if (NotifyCollectionChangedAction.Remove == e.Action) {
                foreach (Training removedTraining in e.OldItems)
                    OnTrainingRemoved(removedTraining);
            }
        }

        private void OnTrainingAdded(Training addedTraining) {
            TeacherTraining tt = new TeacherTraining() { teacher = this, training = addedTraining };
        }

        private void OnTrainingRemoved(Training removedTraining) {
            TeacherTraining ttRecord = teacherTraining.SingleOrDefault(tt => tt.teacher == this && tt.training == removedTraining);
            if (ttRecord != null) {
                ttRecord.Remove();
            }
        }
    }
}
