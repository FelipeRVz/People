namespace People.ViewModel
{
    public partial class PeopleViewModel : BaseViewModel
    {
        private ReqresApi reqresApi;

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
            await GetApiInfoAsync();
            await GetPeopleAsync();

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
        async Task GetPeopleAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;

                PersonList.Clear();

                for (int i = 1; i <= apiInfo.total_pages; i++)
                {
                    var response = await reqresApi.GetPersonList(i);

                    foreach (var person in response.persons)
                    {
                        PersonList.Add(person);
                    }
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

        [RelayCommand]
        async Task GotoDetailsAsync(Person person)
        {
            if(person is null) return;

            try
            {
                await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
                {
                    {"Person", person }
                });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
                return;
            }
        }

        [RelayCommand]
        public void Delete(Person person)
        {
            PersonList.Remove(person);
            return;
        }

    }
}
