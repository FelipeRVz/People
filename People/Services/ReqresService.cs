
using System.Reflection;

namespace People.Services
{

    public class ReqresService : IReqresService
    {
        private int page {  get; set; }
        private int per_page { get; set; }
        private int total {  get; set; }
        public int total_pages {  get; set; }

        ReqresApi reqresApi;

        public ReqresService(ReqresApi reqruesApi)
        {
            this.reqresApi = reqruesApi;
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
            var apiInfo =  reqresApi.GetApiInfoAsync();
            CopyPropertyAtoB(apiInfo.Result, this);
        }

        private void CopyPropertyAtoB(object a, object b)
        {
            Type aType = a.GetType();
            Type bType = b.GetType();
            foreach (PropertyInfo aProperty in aType.GetProperties())
            {
                PropertyInfo? bProperty = bType.GetProperty(aProperty.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (bProperty != null)
                    if (bProperty.CanWrite)
                        bProperty.SetValue(b, aProperty.GetValue(a));
            }

        }

    }
}

