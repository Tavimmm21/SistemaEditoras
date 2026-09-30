using SistemaEditoras.Models;
using SistemaEditoras.Services;

namespace SistemaEditoras.Views
{
    public partial class EditorasInserir : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public EditorasInserir(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryNome.Text))
            {
                await DisplayAlert("Campo obrigatório", "Informe o nome da editora.", "OK");
                return;
            }

            var editora = new Editora
            {
                Nome = EntryNome.Text.Trim(),
                Sigla = EntrySigla.Text?.Trim() ?? string.Empty,
                Observacoes = EditorObservacoes.Text?.Trim() ?? string.Empty
            };

            await _databaseService.SalvarEditoraAsync(editora);
            await Navigation.PopAsync();
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
