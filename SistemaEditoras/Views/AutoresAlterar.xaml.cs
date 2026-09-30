using SistemaEditoras.Models;
using SistemaEditoras.Services;

namespace SistemaEditoras.Views
{
    public partial class AutoresAlterar : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly Autor _autor;

        public AutoresAlterar(DatabaseService databaseService, Autor autor)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _autor = autor;

            EntryNome.Text = autor.Nome;
            EntryPseudonimo.Text = autor.Pseudonimo;
            EditorObservacoes.Text = autor.Observacoes;
        }

        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryNome.Text))
            {
                await DisplayAlert("Campo obrigatório", "Informe o nome do autor.", "OK");
                return;
            }

            _autor.Nome = EntryNome.Text.Trim();
            _autor.Pseudonimo = EntryPseudonimo.Text?.Trim() ?? string.Empty;
            _autor.Observacoes = EditorObservacoes.Text?.Trim() ?? string.Empty;

            await _databaseService.SalvarAutorAsync(_autor);
            await Navigation.PopAsync();
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
