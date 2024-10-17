using System.Text.Json.Serialization;

namespace People.Model
{
    public class Person
    {
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("email")]
        public string email { get; set; }

        [JsonPropertyName("first_name")]
        public string first_name { get; set; }

        [JsonPropertyName("last_name")]
        public string last_name { get; set; }

        [JsonPropertyName("avatar")]
        public string avatar { get; set; }
    }

    public class PersonList
    {
        [JsonPropertyName("data")]
        public List<Person> persons { get; set; }
    }

    public class ApiInfo
    {
        [JsonPropertyName("page")]
        public int page { get; set; }

        [JsonPropertyName("per_page")]
        public int per_page { get; set; }

        [JsonPropertyName("total")]
        public int total { get; set; }

        [JsonPropertyName("total_pages")]
        public int total_pages { get; set; }
    }
}

