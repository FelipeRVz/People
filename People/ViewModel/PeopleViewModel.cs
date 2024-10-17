
namespace People.ViewModel
{
    public partial class PeopleViewModel : BaseViewModel
    {   
        private readonly ReqresApi reqresApi;
        private bool _isInitialized = false;

        public ObservableCollection<Person> PersonList { get; } = new();

        public ApiInfo apiInfo = new ApiInfo();

        [ObservableProperty]
        bool isRefreshing;

        public PeopleViewModel(ReqresApi reqresApi)
        {
            this.reqresApi = reqresApi;
            _ = Initialize();
        }

        [RelayCommand]
        async Task Initialize()
        {
            if (_isInitialized) return;

            await GetApiInfoAsync();
            await GetPeopleAsync(apiInfo.page);

            _isInitialized = true;
        }

        [RelayCommand]
        async Task GetApiInfoAsync()
        {
            try
            {
                IsBusy = true;

                var response = await reqresApi.GetApiInfo();

                apiInfo.total = response.total;
                apiInfo.page = response.page;
                apiInfo.total_pages = response.total_pages;
                apiInfo.total = response.total;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
                return;
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task GetPeopleAsync(int page)
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;

                PersonList.Clear();
                var response = await reqresApi.GetPersonList(page);

                foreach (var person in response.persons)
                {
                    PersonList.Add(person);
                }

                return;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
                return;
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }


        [ObservableProperty]
        int currentPage = 1;

        [ObservableProperty]
        bool isPrevEnable = false, isNextEnable = true;


        [RelayCommand]
        async Task PrevPage()
        {
            CurrentPage--;
            if (CurrentPage == 1) IsPrevEnable = false;
            if (CurrentPage < apiInfo.total_pages) IsNextEnable = true;

            await GetPeopleAsync(CurrentPage);
        }

        [RelayCommand]
        async Task NextPage()
        {
            CurrentPage++;
            if (CurrentPage == apiInfo.total_pages) IsNextEnable = false;
            if (CurrentPage > 1) IsPrevEnable = true;

            await GetPeopleAsync(CurrentPage);
        }
    }
}
