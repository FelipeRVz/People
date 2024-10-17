using Microsoft.Extensions.Logging;

namespace People
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif      
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<PeopleViewModel>();

            //builder.Services.AddTransient<MonkeyDetailsViewModel>();
            //builder.Services.AddTransient<DetailsPage>();

            builder.Services
                .AddRefitClient<ReqresApi>(new RefitSettings())
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://reqres.in/api"));

            
            return builder.Build();
        }


    }
}
