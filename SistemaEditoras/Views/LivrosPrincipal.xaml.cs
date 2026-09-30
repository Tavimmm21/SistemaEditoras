using SistemaEditoras.Models;
using SistemaEditoras.Services;

namespace SistemaEditoras.Views
{
    public partial class LivrosPrincipal : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private List<Livro> _todosLivros = new();

        public LivrosPrincipal(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CarregarLivrosAsync();
        }

        private async Task CarregarLivrosAsync()
        {
            var livros = await _databaseService.GetLivrosAsync();
            var editoras = await _databaseService.GetEditorasAsync();
            var autores = await _databaseService.GetAutoresAsync();

            foreach (var livro in livros)
            {
                livro.EditoraNome = editoras.FirstOrDefault(ed => ed.Id == livro.EditoraId)?.Nome ?? "(sem editora)";
                livro.AutorNome = autores.FirstOrDefault(a => a.Id == livro.AutorId)?.Nome ?? "(sem autor)";
            }

            _todosLivros = livros;
            ColecaoLivros.ItemsSource = _todosLivros;
            EntryPesquisar.Text = string.Empty;
        }

        private void OnPesquisarTextChanged(object sender, TextChangedEventArgs e)
        {
            var termo = e.NewTextValue?.Trim() ?? string.Empty;

            ColecaoLivros.ItemsSource = string.IsNullOrEmpty(termo)
                ? _todosLivros
                : _todosLivros
                    .Where(l => l.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase))
                    .ToList();
        }

        private async void OnInserirClicked(object sender, EventArgs e)
        {
            var editoras = await _databaseService.GetEditorasAsync();
            var autores = await _databaseService.GetAutoresAsync();

            if (editoras.Count == 0 || autores.Count == 0)
            {
                await DisplayAlert(
                    "Cadastro incompleto",
                    "Antes de cadastrar um livro, é necessário ter pelo menos uma Editora e um Autor cadastrados.",
                    "OK");
                return;
            }

            await Navigation.PushAsync(new LivrosInserir(_databaseService, editoras, autores));
        }

        private async void OnAlterarClicked(object sender, EventArgs e)
        {
            if (sender is Button botao && botao.CommandParameter is Livro livro)
            {
                var editoras = await _databaseService.GetEditorasAsync();
                var autores = await _databaseService.GetAutoresAsync();
                await Navigation.PushAsync(new LivrosAlterar(_databaseService, livro, editoras, autores));
            }
        }

        private async void OnExcluirClicked(object sender, EventArgs e)
        {
            if (sender is Button botao && botao.CommandParameter is Livro livro)
            {
                bool confirmar = await DisplayAlert(
                    "Excluir Livro",
                    $"Confirma a exclusão do livro \"{livro.Nome}\"?",
                    "Excluir",
                    "Cancelar");

                if (!confirmar)
                    return;

                await _databaseService.ExcluirLivroAsync(livro);
                await CarregarLivrosAsync();
            }
        }
    }
}
