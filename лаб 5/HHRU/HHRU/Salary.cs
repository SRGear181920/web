using Newtonsoft.Json;

namespace HHRU
{
    class Salary
    {
        [JsonProperty("from")]
        public string From { get; set; }
        [JsonProperty("to")]
        public string To { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", GetSalary().ToString(), Currency);
        }

        public bool Check()
        {
            return From != null || To != null;
        }

        public double GetSalary()
        {
            var k = 1.0;
            switch (Currency)
            {
                case "USD":
                    k = 66.7;
                    break;
                case "EUR":
                    k = 76.47;
                    break;
                case "BYR":
                    k = 30.62;
                    break;
                case "KZT":
                    k = 0.18;
                    break;
                case "UAH":
                    k = 2.4;
                    break;
                case "AZN":
                    k = 39.18;
                    break;
            }
            if (k != 1.0) Currency = "RUR";
                if (From != null && To != null)
                return (double.Parse(From)  + double.Parse(To)) * k / 2;
            else return From == null ? double.Parse(To) * k : double.Parse(From) * k;
        }        
    }
}
