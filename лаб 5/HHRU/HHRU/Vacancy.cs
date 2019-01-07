using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace HHRU
{
    class Vacancy
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("salary")]
        public Salary Salary { get; set; }
        [JsonProperty("key_skills")]
        public List<Skill> Skills { get; set; }

        public override string ToString()
        {
            var str = new StringBuilder();
            str.Append(string.Format(
                "Name:{0}\tId:{1}\nSalary:{2}\n", Name, Id, Salary));
            if (Skills.Count != 0)
            {
                str.Append("Ключевые нывыки:\n");
                foreach (var skill in Skills)
                    str.Append(skill + "\n");
            }
            return str.ToString();
        }
    }
}
