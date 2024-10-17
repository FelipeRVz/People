
namespace People.Services
{
    [Headers("accept: application/json")]
    public interface ReqresApi
    {
        [Get("/users?page={page}")]
        Task<PersonList> GetPersonList(int page);

        [Get("/users/{id}")]
        Task<Person> GetPersonById(int id);

        [Get("/users")]
        Task<ApiInfo> GetApiInfo();
    }
}
