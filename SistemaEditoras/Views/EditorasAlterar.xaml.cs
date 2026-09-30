using SistemaEditoras.Models;
using SistemaEditoras.Services;

namespace SistemaEditoras.Views
{
    public partial class EditorasAlterar : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly Editora _editora;

        public EditorasAlterar(DatabaseService databaseService, Editora editora)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _editora = editora;

            EntryNome.Text = editora.Nome;
            EntrySigla.Text = editora.Sigla;
            EditorObservacoes.Text = editora.Observacoes;
        }

        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryNome.Text))
            {
                await DisplayAlert("Campo obrigatório", "Informe o nome da editora.", "OK");
                return;
            }

            _editora.Nome = EntryNome.Text.Trim();
            _editora.Sigla = EntrySigla.Text?.Trim() ?? string.Empty;
            _editora.Observacoes = EditorObservacoes.Text?.Trim() ?? string.Empty;

            await _databaseService.SalvarEditoraAsync(_editora);
            await Navigation.PopAsync();
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
