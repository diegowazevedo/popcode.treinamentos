using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using treinamentos.Src.Classes.Entity;

namespace treinamentos.Src.Classes {
    class Database : DataContext {

        private static DataContext contextForRemovedRecords = null;

        public static string DBConnectionString = "Data Source='isostore:training.sdf'";
        public Database() : base(DBConnectionString) { }

        public Table<Training> Training;

        public Table<Teacher> Teacher;

        public Table<TeacherTraining> TT;

        public static void RemoveRecord<T>(T recordToRemove) where T : class {
            if (contextForRemovedRecords == null)
                contextForRemovedRecords = new Database();

            Table<T> tableData = contextForRemovedRecords.GetTable<T>();
            var deleteRecord = tableData.SingleOrDefault(record => record == recordToRemove);
            if (deleteRecord != null) {
                tableData.DeleteOnSubmit(deleteRecord);
            }
        }
    }

    class TrainingDB {
        private static Database getDatabase() {
            Database db = new Database();
            if (db.DatabaseExists() == false) {
                try {
                    db.CreateDatabase();
                } catch (Exception e) {
                    Debug.WriteLine(e.Message);
                }
            }
            return db;
        }

        public static List<Training> getTrainings() {
            Database db = getDatabase();
            try {
                var query = from trn in db.Training select trn;
                List<Training> trainings = new List<Training>(query.AsEnumerable());
                return trainings;
            } catch (Exception e) {
                Debug.WriteLine("Error: " + e);
                return null;
            }
        }

        public static List<Training> searchTrainings(string name) {
            Database db = getDatabase();
            try {
                var query = from trn in db.Training where trn.name.Equals(name) orderby trn.name select trn;
                List<Training> trainings = new List<Training>(query.AsEnumerable());
                return trainings;
            } catch (Exception e) {
                Debug.WriteLine("Error: " + e);
                return null;
            }
        }

        public static void saveTraining(Training training) {
            try {
                Database db = getDatabase();
                db.Training.InsertOnSubmit(training);
                db.SubmitChanges();
            } catch (Exception e) {
                Debug.WriteLine("Error: " + e);
            }
        }

        public static void saveAllTrainings(List<Training> list) {
            try {
                Database db = getDatabase();
                db.Training.InsertAllOnSubmit(list);
                db.SubmitChanges();
            } catch (Exception e) {
                Debug.WriteLine("Error: " + e);
            }
        }

        public static void deleteTrainings() {
            try {
                Database db = getDatabase();
                var trainings = from trn in db.Training select trn;
                db.Training.DeleteAllOnSubmit(trainings);
                db.SubmitChanges();
            } catch (Exception e) {
                Debug.WriteLine("Error: " + e);
            }
        }

        public static void deleteTraining(Training training) {
            try {
                Database db = getDatabase();
                var query = from trn in db.Training where trn.id == training.id select trn;
                db.Training.DeleteOnSubmit(query.ToList()[0]);
                db.SubmitChanges();
            } catch (Exception e) {
                Debug.WriteLine("Error: " + e);
            }
        }
    }

    class TeacherDB {
        private static Database getDatabase() {
            Database db = new Database();
            if (db.DatabaseExists() == false) {
                try {
                    db.CreateDatabase();
                } catch (Exception e) {
                    Debug.WriteLine(e.Message);
                }
            }
            return db;
        }

        public static List<Teacher> getTeachers() {
            Database db = getDatabase();
            try {
                var query = from tch in db.Teacher select tch;
                List<Teacher> teachers = new List<Teacher>(query.AsEnumerable());
                return teachers;
            } catch (Exception e) {
                Debug.WriteLine("Error: " + e);
                return null;
            }
        }

        public static List<Teacher> searchTeachers(string name) {
            Database db = getDatabase();
            try {
                var query = from tch in db.Teacher where tch.name.Equals(name) orderby tch.name select tch;
                List<Teacher> teachers = new List<Teacher>(query.AsEnumerable());
                return teachers;
            } catch (Exception e) {
                Debug.WriteLine("Error: " + e);
                return null;
            }
        }

        public static void saveTeacher(Teacher teacher) {
            try {
                Database db = getDatabase();
                db.Teacher.InsertOnSubmit(teacher);
                db.SubmitChanges();
            } catch (Exception e) {
                Debug.WriteLine("Error: " + e);
            }
        }

        public static void saveAllTeachers(List<Teacher> list) {
            Database db = getDatabase();
            try {
                db.Teacher.InsertAllOnSubmit(list);
                db.SubmitChanges();
            } catch (Exception e) {
                Debug.WriteLine("Error: " + e);
            }
        }

        public static void deleteTeachers() {
            Database db = getDatabase();
            try {
                var teachers = from tch in db.Teacher select tch;
                db.Teacher.DeleteAllOnSubmit(teachers);
                db.SubmitChanges();
            } catch (Exception e) {
                Debug.WriteLine("Error: " + e);
            }
        }

        public static void deleteTeacher(Teacher teacher) {
            Database db = getDatabase();
            try {
                var query = from tch in db.Teacher where tch.id == teacher.id select tch;
                db.Teacher.DeleteOnSubmit(query.ToList()[0]);
                db.SubmitChanges();
            } catch (Exception e) {
                Debug.WriteLine("Error: " + e);
            }
        }
    }
}
