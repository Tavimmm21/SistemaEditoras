using SistemaEditoras.Models;
using SistemaEditoras.Services;

namespace SistemaEditoras.Views
{
    public partial class LivrosInserir : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly List<Editora> _editoras;
        private readonly List<Autor> _autores;

        public LivrosInserir(DatabaseService databaseService, List<Editora> editoras, List<Autor> autores)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _editoras = editoras;
            _autores = autores;

            PickerEditora.ItemsSource = _editoras.Select(ed => ed.Nome).ToList();
            PickerAutor.ItemsSource = _autores.Select(a => a.Nome).ToList();
        }

        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryNome.Text))
            {
                await DisplayAlert("Campo obrigatório", "Informe o nome do livro.", "OK");
                return;
            }

            if (!int.TryParse(EntryAnoPublicacao.Text, out var ano))
            {
                await DisplayAlert("Campo obrigatório", "Informe um ano de publicação válido.", "OK");
                return;
            }

            if (!decimal.TryParse(EntryIsbn.Text, out var isbn))
            {
                await DisplayAlert("Campo obrigatório", "Informe um ISBN válido (somente números).", "OK");
                return;
            }

            if (PickerEditora.SelectedIndex < 0)
            {
                await DisplayAlert("Campo obrigatório", "Selecione a editora do livro.", "OK");
                return;
            }

            if (PickerAutor.SelectedIndex < 0)
            {
                await DisplayAlert("Campo obrigatório", "Selecione o autor do livro.", "OK");
                return;
            }

            var livro = new Livro
            {
                Nome = EntryNome.Text.Trim(),
                AnoPublicacao = ano,
                Isbn = isbn,
                Observacoes = EditorObservacoes.Text?.Trim() ?? string.Empty,
                EditoraId = _editoras[PickerEditora.SelectedIndex].Id,
                AutorId = _autores[PickerAutor.SelectedIndex].Id
            };

            await _databaseService.SalvarLivroAsync(livro);
            await Navigation.PopAsync();
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
