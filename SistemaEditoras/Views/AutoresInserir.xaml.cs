using SistemaEditoras.Models;
using SistemaEditoras.Services;

namespace SistemaEditoras.Views
{
    public partial class AutoresInserir : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public AutoresInserir(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryNome.Text))
            {
                await DisplayAlert("Campo obrigatório", "Informe o nome do autor.", "OK");
                return;
            }

            var autor = new Autor
            {
                Nome = EntryNome.Text.Trim(),
                Pseudonimo = EntryPseudonimo.Text?.Trim() ?? string.Empty,
                Observacoes = EditorObservacoes.Text?.Trim() ?? string.Empty
            };

            await _databaseService.SalvarAutorAsync(autor);
            await Navigation.PopAsync();
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
