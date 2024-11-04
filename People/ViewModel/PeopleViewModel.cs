namespace People.ViewModel
{
    public partial class PeopleViewModel : BaseViewModel
    {
        IReqresService reqresService;
        ILogService logService;

        public ObservableCollection<Person> PersonList { get; } = new();

        [ObservableProperty]
        bool isRefreshing;

        public PeopleViewModel(IReqresService reqresService, ILogService logService)
        {
            this.reqresService = reqresService;
            this.logService = logService;
            _ = GetPeopleAsync();
        }


        [RelayCommand]
        async Task GetPeopleAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;

                MainThread.BeginInvokeOnMainThread(() =>{PersonList.Clear();});

                var people = await reqresService.GetPersonListAsync();

                foreach (Person person in people)
                    MainThread.BeginInvokeOnMainThread(() => { PersonList.Add(person); });
                  
                return;
            }
            catch (Exception ex)
            {
                string msg = "Error getting people list: " + ex.Message;
                logService.LogWrite(msg);
                await Shell.Current.DisplayAlert("Error!", msg , "OK");
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
            if(IsBusy) return;
            if(person is null) return;
            logService.LogWrite("pagina detalle de: " + person.first_name);

            try
            {
                await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
                {
                    {"Person", person }
                });
            }
            catch (Exception ex)
            {
                string msg = "Error with detail view: " + ex.Message;
                logService.LogWrite(msg);
                await Shell.Current.DisplayAlert("Error!", msg, "OK");
                return;
            }
        }

        [RelayCommand]
        public void Delete(Person person)
        {
            if (IsBusy) return;
            if (person is null) return;
            try
            {
                IsBusy = true;
                MainThread.BeginInvokeOnMainThread(() => { PersonList.Remove(person); });
                return;
            }
            catch(Exception ex) 
            {
                string msg = "Error with deleting person: " + ex.Message;
                logService.LogWrite(msg);
                Shell.Current.DisplayAlert("Error!", msg, "OK");
                return;
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
