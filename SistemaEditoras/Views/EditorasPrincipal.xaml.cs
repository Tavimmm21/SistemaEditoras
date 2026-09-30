using SistemaEditoras.Models;
using SistemaEditoras.Services;

namespace SistemaEditoras.Views
{
    public partial class EditorasPrincipal : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private List<Editora> _todasEditoras = new();

        public EditorasPrincipal(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CarregarEditorasAsync();
        }

        private async Task CarregarEditorasAsync()
        {
            _todasEditoras = await _databaseService.GetEditorasAsync();
            ColecaoEditoras.ItemsSource = _todasEditoras;
            EntryPesquisar.Text = string.Empty;
        }

        private void OnPesquisarTextChanged(object sender, TextChangedEventArgs e)
        {
            var termo = e.NewTextValue?.Trim() ?? string.Empty;

            ColecaoEditoras.ItemsSource = string.IsNullOrEmpty(termo)
                ? _todasEditoras
                : _todasEditoras
                    .Where(ed => ed.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase))
                    .ToList();
        }

        private async void OnInserirClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditorasInserir(_databaseService));
        }

        private async void OnAlterarClicked(object sender, EventArgs e)
        {
            if (sender is Button botao && botao.CommandParameter is Editora editora)
            {
                await Navigation.PushAsync(new EditorasAlterar(_databaseService, editora));
            }
        }

        private async void OnExcluirClicked(object sender, EventArgs e)
        {
            if (sender is Button botao && botao.CommandParameter is Editora editora)
            {
                bool confirmar = await DisplayAlert(
                    "Excluir Editora",
                    $"Confirma a exclusão da editora \"{editora.Nome}\"?",
                    "Excluir",
                    "Cancelar");

                if (!confirmar)
                    return;

                await _databaseService.ExcluirEditoraAsync(editora);
                await CarregarEditorasAsync();
            }
        }
    }
}
