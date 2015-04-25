using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Data.Json;

namespace treinamentos.Src.Classes {
    class TrainingService {
        
        public static async Task<List<Training>> GetTrainingsAsync() {
            List<Training> services = null;
            if (HttpHelper.CheckInternetConnection()) {
                
                String json = await HttpHelper.getAsync();
                
                services = parseJSON(json);

                // Salva todos os carros
                //CarroDB.deleteCarrosByTipo(tipo);

                //CarroDB.saveAllCarros(carros);

                return services;
            } else {
                // Se não tiver conexão com a internet, busca no banco de dados
                //services = ServiceDB.getServices(tipo);
                MessageBox.Show("Sem Conexão com a internet");

                return services;
            }
        }

        public static List<Training> parseJSON(String json) {
            List<Training> list = new List<Training>();
            if (json != null) {
                JsonArray jarray = JsonArray.Parse(json).GetArray();
                foreach (JsonValue itemValue in jarray) {
                    JsonObject item = itemValue.GetObject();
                    JsonArray arrayTeacher = item["teachers"].GetArray();
                    List<Teacher> tList = new List<Teacher>();
                    foreach(JsonValue teacherValue in arrayTeacher){
                        JsonObject itemTeacher = teacherValue.GetObject();
                        Teacher teacher = new Teacher {
                            id = itemTeacher["id"].GetHashCode(),
                            name = itemTeacher["name"].GetString(),
                            shortname = itemTeacher["short"].GetString(),
                            description = itemTeacher["description"].GetString(),
                            knowledge = itemTeacher["knowledge"].GetString(),
                            photo = itemTeacher["photo"].ValueType == JsonValueType.Null ? "" : itemTeacher["photo"].GetString(),
                            order = itemTeacher["order"].GetHashCode(),
                            resource_uri = itemTeacher["resource_uri"].GetString(),
                            display = itemTeacher["display"].GetBoolean()
                        };
                        tList.Add(teacher);
                    }
                    Training training = new Training() {
                        id = item["id"].GetHashCode(),
                        backend = item["backend"].GetBoolean(),
                        banner = item["banner"].ValueType == JsonValueType.Null ? "" : item["banner"].GetString(),
                        description = item["description"].GetString(),
                        display = item["display"].GetBoolean(),
                        front = item["front"].GetHashCode(),
                        logo = item["logo"].ValueType == JsonValueType.Null ? "" : item["logo"].GetString(),
                        methodology = item["methodology"].GetString(),
                        mobile = item["mobile"].GetBoolean(),
                        name = item["name"].GetString(),
                        name_short = item["name_short"].GetString(),
                        requirements = item["requirements"].GetString(),
                        resource_uri = item["resource_uri"].GetString(),
                        title_short = item["short"].GetString(),
                        short_description = item["short_description"].GetString(),
                        sort = item["sort"].GetHashCode(),
                        tagline = item["tagline"].GetString(),
                        target = item["target"].GetString(),
                        topics = item["topics"].GetString(),
                        teacher = tList
                    };
                    list.Add(training);
                }
            }
            return list;
        }
    }
}
