using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace treinamentos.Src.Classes {
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
    }
}
