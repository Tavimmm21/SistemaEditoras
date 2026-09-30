namespace SistemaEditoras
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnEditorasClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//EditorasPrincipal");
        }

        private async void OnLivrosClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LivrosPrincipal");
        }

        private async void OnAutoresClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AutoresPrincipal");
        }
    }
}
