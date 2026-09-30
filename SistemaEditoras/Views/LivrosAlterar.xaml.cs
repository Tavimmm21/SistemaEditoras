using SistemaEditoras.Models;
using SistemaEditoras.Services;

namespace SistemaEditoras.Views
{
    public partial class LivrosAlterar : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly Livro _livro;
        private readonly List<Editora> _editoras;
        private readonly List<Autor> _autores;

        public LivrosAlterar(DatabaseService databaseService, Livro livro, List<Editora> editoras, List<Autor> autores)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _livro = livro;
            _editoras = editoras;
            _autores = autores;

            EntryNome.Text = livro.Nome;
            EntryAnoPublicacao.Text = livro.AnoPublicacao.ToString();
            EntryIsbn.Text = livro.Isbn.ToString();
            EditorObservacoes.Text = livro.Observacoes;

            PickerEditora.ItemsSource = _editoras.Select(ed => ed.Nome).ToList();
            PickerAutor.ItemsSource = _autores.Select(a => a.Nome).ToList();

            PickerEditora.SelectedIndex = _editoras.FindIndex(ed => ed.Id == livro.EditoraId);
            PickerAutor.SelectedIndex = _autores.FindIndex(a => a.Id == livro.AutorId);
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

            _livro.Nome = EntryNome.Text.Trim();
            _livro.AnoPublicacao = ano;
            _livro.Isbn = isbn;
            _livro.Observacoes = EditorObservacoes.Text?.Trim() ?? string.Empty;
            _livro.EditoraId = _editoras[PickerEditora.SelectedIndex].Id;
            _livro.AutorId = _autores[PickerAutor.SelectedIndex].Id;

            await _databaseService.SalvarLivroAsync(_livro);
            await Navigation.PopAsync();
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
