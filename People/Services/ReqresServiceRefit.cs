using static People.Services.Functions;

namespace People.Services
{

    public class ReqresServiceRefit : IReqresService
    {
        private int page { get; set; }
        private int per_page { get; set; }
        private int total { get; set; }
        public int total_pages { get; set; }

        ReqresApi reqresApi;

        public ReqresServiceRefit(ReqresApi reqresApi)
        {
            this.reqresApi = reqresApi;
        }

        public async Task<List<Person>> GetPersonListAsync()
        {
            List<Person> personList = new();
            Person person;
            GetApiInfo();
            for (int i = 1; i <= total_pages; i++)
            {
                var plDTO = await reqresApi.GetPersonListAsync(i);

                foreach (PersonDTO pdto in plDTO.persons)
                {
                    person = new();
                    CopyPropertyAtoB(pdto, person);
                    personList.Add(person);
                }
            }

            return personList;
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            Person person = new();
            var pdto = await reqresApi.GetPersonByIdAsync(id);
            CopyPropertyAtoB(pdto, person);
            return person;
        }

        public void GetApiInfo()
        {
            var apiInfo = reqresApi.GetApiInfoAsync();
            CopyPropertyAtoB(apiInfo.Result, this);
        }

    }

}

