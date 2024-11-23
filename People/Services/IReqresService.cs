
namespace People.Services
{
    public interface IReqresService
    {
        Task<Person> GetPersonByIdAsync(int id);
        Task<List<Person>> GetPersonListAsync();
        void GetApiInfo();
    }
}
