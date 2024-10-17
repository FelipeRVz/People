namespace People;

public partial class MainPage : ContentPage
{
    public MainPage(PeopleViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

}


