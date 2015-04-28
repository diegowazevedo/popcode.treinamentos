using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using treinamentos.Src.Classes.Entity;
using Windows.Data.Json;

namespace treinamentos.Src.Classes {
    class TrainingService {

        public static async Task<List<Training>> GetTrainingsAsync() {
            Database db = new Database();

            db.DeleteDatabase();
            db.CreateDatabase();

            List<Training> trainings = null;

            String data = await HttpHelper.getAsync();

            String json = Charset.EncodeUtf8ToIso(data);

            trainings = parseJSON(json);

            return trainings;
        }

        public static List<Training> parseJSON(String json) {
            Database db = new Database();
            List<Training> list = new List<Training>();
            if (json != null) {
                JsonArray jarray = JsonArray.Parse(json).GetArray();
                foreach (JsonValue itemValue in jarray) {
                    JsonObject item = itemValue.GetObject();
                    JsonArray arrayTeacher = item["teachers"].GetArray();

                    Training training = new Training() {
                        id = Convert.ToInt32(item["id"].GetNumber()),
                        backend = item["backend"].GetBoolean(),
                        banner = item["banner"].ValueType == JsonValueType.Null ? "" : item["banner"].GetString(),
                        description = item["description"].GetString(),
                        display = item["display"].GetBoolean(),
                        front = Convert.ToInt32(item["front"].GetNumber()),
                        logo = item["logo"].ValueType == JsonValueType.Null ? "" : item["logo"].GetString(),
                        methodology = item["methodology"].GetString(),
                        mobile = item["mobile"].GetBoolean(),
                        name = item["name"].GetString(),
                        name_short = item["name_short"].GetString(),
                        requirements = item["requirements"].GetString(),
                        resource_uri = item["resource_uri"].GetString(),
                        title_short = item["short"].GetString(),
                        short_description = item["short_description"].GetString(),
                        sort = Convert.ToInt32(item["sort"].GetNumber()),
                        tagline = item["tagline"].GetString(),
                        target = item["target"].GetString(),
                        topics = item["topics"].GetString()
                    };
                    TrainingDB.saveTraining(training);

                    List<Teacher> tList = new List<Teacher>();
                    foreach (JsonValue teacherValue in arrayTeacher) {
                        JsonObject itemTeacher = teacherValue.GetObject();
                        Teacher teacher = new Teacher {
                            id = Convert.ToInt32(itemTeacher["id"].GetNumber()),
                            name = itemTeacher["name"].GetString(),
                            shortname = itemTeacher["short"].GetString(),
                            description = itemTeacher["description"].GetString(),
                            knowledge = itemTeacher["knowledge"].GetString(),
                            photo = itemTeacher["photo"].ValueType == JsonValueType.Null ? "" : itemTeacher["photo"].GetString(),
                            order = Convert.ToInt32(itemTeacher["order"].GetNumber()),
                            resource_uri = itemTeacher["resource_uri"].GetString(),
                            display = itemTeacher["display"].GetBoolean()
                        };
                        try {
                            TeacherDB.saveTeacher(teacher);
                            Training trReturn = db.Training.Single(tr => tr.id == training.id);
                            Teacher teReturn = db.Teacher.Single(te => te.id == teacher.id);
                            trReturn.teachers.Add(teReturn);
                            db.SubmitChanges();
                        } catch (Exception e) {
                            Debug.WriteLine("error => " + e.Message);
                        }
                    }
                    list.Add(training);
                }
            }
            return list;
        }
    }
}
