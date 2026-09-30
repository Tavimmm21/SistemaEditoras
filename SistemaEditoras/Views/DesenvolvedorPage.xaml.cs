namespace SistemaEditoras.Views
{
    public partial class DesenvolvedorPage : ContentPage
    {
        public DesenvolvedorPage()
        {
            InitializeComponent();
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
