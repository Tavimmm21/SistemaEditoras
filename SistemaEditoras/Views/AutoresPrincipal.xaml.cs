using SistemaEditoras.Models;
using SistemaEditoras.Services;

namespace SistemaEditoras.Views
{
    public partial class AutoresPrincipal : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private List<Autor> _todosAutores = new();

        public AutoresPrincipal(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CarregarAutoresAsync();
        }

        private async Task CarregarAutoresAsync()
        {
            _todosAutores = await _databaseService.GetAutoresAsync();
            ColecaoAutores.ItemsSource = _todosAutores;
            EntryPesquisar.Text = string.Empty;
        }

        private void OnPesquisarTextChanged(object sender, TextChangedEventArgs e)
        {
            var termo = e.NewTextValue?.Trim() ?? string.Empty;

            ColecaoAutores.ItemsSource = string.IsNullOrEmpty(termo)
                ? _todosAutores
                : _todosAutores
                    .Where(a => a.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase))
                    .ToList();
        }

        private async void OnInserirClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AutoresInserir(_databaseService));
        }

        private async void OnAlterarClicked(object sender, EventArgs e)
        {
            if (sender is Button botao && botao.CommandParameter is Autor autor)
            {
                await Navigation.PushAsync(new AutoresAlterar(_databaseService, autor));
            }
        }

        private async void OnExcluirClicked(object sender, EventArgs e)
        {
            if (sender is Button botao && botao.CommandParameter is Autor autor)
            {
                bool confirmar = await DisplayAlert(
                    "Excluir Autor",
                    $"Confirma a exclusão do autor \"{autor.Nome}\"?",
                    "Excluir",
                    "Cancelar");

                if (!confirmar)
                    return;

                await _databaseService.ExcluirAutorAsync(autor);
                await CarregarAutoresAsync();
            }
        }
    }
}
