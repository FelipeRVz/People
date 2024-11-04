using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Services
{
    public interface IReqresService
    {
        Task<Person> GetPersonByIdAsync(int id);
        Task<List<Person>> GetPersonListAsync();
        void GetApiInfo();
    }
}
