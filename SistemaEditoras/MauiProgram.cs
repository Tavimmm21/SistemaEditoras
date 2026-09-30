using Microsoft.Extensions.Logging;
using SistemaEditoras.Services;
using SistemaEditoras.Views;

namespace SistemaEditoras
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

            builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddTransient<EditorasPrincipal>();
            builder.Services.AddTransient<LivrosPrincipal>();
            builder.Services.AddTransient<AutoresPrincipal>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
