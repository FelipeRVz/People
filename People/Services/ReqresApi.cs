
namespace People.Services
{
    [Headers("accept: application/json")]
    public interface ReqresApi 
    {
        [Get("/users?page={page}")]
        Task<PersonListDTO> GetPersonListAsync(int page);

        [Get("/users/{id}")]
        Task<PersonDTO> GetPersonByIdAsync(int id);

        [Get("/users")]
        Task<ApiInfoDTO> GetApiInfoAsync();
    }
}
