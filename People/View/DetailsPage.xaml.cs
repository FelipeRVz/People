namespace People.View;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(PeopleDetailViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}