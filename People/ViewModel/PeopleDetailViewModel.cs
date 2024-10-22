
namespace People.ViewModel
{
    [QueryProperty(nameof(Person), "Person")]
    public partial class PeopleDetailViewModel : BaseViewModel
    {
        public PeopleDetailViewModel() { return; }

        [ObservableProperty]
        Person person;
    }
}
