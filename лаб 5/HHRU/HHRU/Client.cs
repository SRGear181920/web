using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HHRU
{
    class Client
    {
        HttpClient MyClient { get; set; }
        Uri Path { get; set; }
        public List<Vacancy> Vacancies { get; set; }
        int MaxNumberVacancies { get; set; }
        int PerPage { get; set; }

        public Client(string path, int numberVacanciecOnOnePage, int maxNumberVacancies)
        {
            Path = new Uri(path);
            PerPage = numberVacanciecOnOnePage;
            MaxNumberVacancies = maxNumberVacancies;
            Vacancies = new List<Vacancy>();
            var task = GetVacancies();
            task.Wait();
        }

        async public Task GetVacancies()
        {
            for (var page = 0; page < MaxNumberVacancies / PerPage; page++)
            {
                Console.Write("Page:{0}\t\r", page);
                using (MyClient = new HttpClient())
                {
                    MyClient.DefaultRequestHeaders.Add("user-agent",
                        "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36 OPR/38.0.2220.41");
                    MyClient.BaseAddress = Path;
                    HttpResponseMessage response;
                    using (response = await MyClient.GetAsync(
                        string.Format("vacancies?page={0}&per_page={1}", page, PerPage)))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var data = response.Content.ReadAsStringAsync();
                            var myContent = (JArray)JToken.Parse(data.Result.ToString())["items"];
                            foreach (var vacancyJson in myContent)
                            {
                                response = await MyClient.GetAsync(
                                    string.Format("vacancies/{0}", vacancyJson["id"].ToString()));
                                if (response.IsSuccessStatusCode)
                                {
                                    data = response.Content.ReadAsStringAsync();
                                    var vacancy = Deserialize(data.Result.ToString());
                                    if (vacancy != null)
                                        if (vacancy.Salary.Check())
                                        Vacancies.Add(vacancy);
                                }
                            }
                        }
                    }
                }
            }            
        }

        Vacancy Deserialize(string infoVacancyJson)
        {
            var vacancyDeserialized = JsonConvert.DeserializeObject<Vacancy>(infoVacancyJson);
            return vacancyDeserialized.Salary == null ? null : vacancyDeserialized;
        }
    }
}

