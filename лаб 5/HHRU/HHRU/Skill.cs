using Newtonsoft.Json;

namespace HHRU
{
    class Skill
    {
        [JsonProperty ("name")]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
