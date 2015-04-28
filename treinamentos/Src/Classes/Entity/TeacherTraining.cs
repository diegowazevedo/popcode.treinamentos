using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace treinamentos.Src.Classes.Entity {
    
    [Table]
    internal class TeacherTraining {

        [Column(IsPrimaryKey = true)]
        private int teacherId;
        private EntityRef<Teacher> _teacher = new EntityRef<Teacher>();
        [Association(Name = "FK_TeacherTraining_Teacher", IsForeignKey = true, Storage = "_teacher", ThisKey = "teacherId")]
        public Teacher teacher {
            get { return _teacher.Entity; }
            set {
                Teacher priorTeacher = _teacher.Entity;
                Teacher newTeacher = value;

                if (newTeacher != priorTeacher) {
                    _teacher.Entity = null;
                    if (priorTeacher != null)
                        priorTeacher.teacherTraining.Remove(this);

                    _teacher.Entity = newTeacher;
                    newTeacher.teacherTraining.Add(this);
                }
            }
        }

        [Column(IsPrimaryKey = true)]
        private int trainingId;
        private EntityRef<Training> _training = new EntityRef<Training>();
        [Association(Name = "FK_TeacherTraining_Training", IsForeignKey = true, Storage = "_training", ThisKey = "trainingId")]
        public Training training {
            get { return _training.Entity; }
            set {
                Training priorTraining = _training.Entity;
                Training newTraining = value;

                if (newTraining != priorTraining) {
                    _training.Entity = null;
                    if (priorTraining != null)
                        priorTraining.teacherTraining.Remove(this);

                    _training.Entity = newTraining;
                    newTraining.teacherTraining.Add(this);
                }
            }
        }


        public void Remove() {
            Database.RemoveRecord(this);

            Teacher priorTeacher = teacher;
            priorTeacher.teacherTraining.Remove(this);

            Training priorTraining = training;
            priorTraining.teacherTraining.Remove(this);
        }
    }
}
