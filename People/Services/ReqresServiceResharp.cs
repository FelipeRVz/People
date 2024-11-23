using static People.Services.Functions;

namespace People.Services
{
    public class ReqresServiceRestSharp :  IReqresService
    {
        private RestClientOptions options;
        private RestClient client;

        private int page { get; set; }
        private int per_page { get; set; }
        private int total { get; set; }
        public int total_pages { get; set; }

        public ReqresServiceRestSharp()
        {
            options = new RestClientOptions("https://reqres.in/api");
            client = new RestClient(options);
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            RestRequest request = new RestRequest("/users/{id}").AddUrlSegment("id", id);
            RestResponse response = await client.GetAsync(request);
            Person? person = new();

            if (response.IsSuccessful && response.Content != null)
                person = JsonSerializer.Deserialize<Person>(response.Content);

            person ??= new();

            return person;
        }

        public async Task<List<Person>> GetPersonListAsync()
        {
            List<Person> personList = new();
            Person person;

            GetApiInfo();

            for (int i = 1; i <= total_pages; i++)
            {
                RestRequest request = new RestRequest("users").AddParameter("page", i);
                RestResponse response = await client.GetAsync(request);

                if (response.IsSuccessful && response.Content != null)
                {
                    PersonListDTO? personListDTO = JsonSerializer.Deserialize<PersonListDTO>(response.Content);
                    
                    if(personListDTO != null)
                    {
                        foreach(PersonDTO pdto in personListDTO.persons)
                        {
                            person = new();
                            CopyPropertyAtoB(pdto, person);
                            personList.Add(person);
                        }
           
                    }
                }
            }
            return personList;
        }

        public void GetApiInfo()
        {
            RestRequest request = new RestRequest("/users}");
            RestResponse response = client.Get(request);
            ApiInfoDTO? apiInfo = new();
            if (response.IsSuccessful && response.Content != null)    
                apiInfo = JsonSerializer.Deserialize<ApiInfoDTO>(response.Content);

            if(apiInfo != null)
                CopyPropertyAtoB(apiInfo, this);

        }
    }
}