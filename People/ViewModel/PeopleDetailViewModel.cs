
namespace People.ViewModel
{
    [QueryProperty(nameof(Person), "Person")]
    public partial class PeopleDetailViewModel : BaseViewModel
    {
        [ObservableProperty]
        Person person = new();
    }
}
