using SistemaEditoras.Models;
using SQLite;

namespace SistemaEditoras.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection? _connection;

        private async Task InitAsync()
        {
            if (_connection is not null)
                return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "sistemaeditoras.db3");
            _connection = new SQLiteAsyncConnection(dbPath);

            await _connection.CreateTableAsync<Editora>();
            await _connection.CreateTableAsync<Autor>();
            await _connection.CreateTableAsync<Livro>();
        }

        // --- EDITORAS ---

        public async Task<List<Editora>> GetEditorasAsync()
        {
            await InitAsync();
            return await _connection!.Table<Editora>().OrderBy(ed => ed.Nome).ToListAsync();
        }

        public async Task<int> SalvarEditoraAsync(Editora editora)
        {
            await InitAsync();
            return editora.Id == 0
                ? await _connection!.InsertAsync(editora)
                : await _connection!.UpdateAsync(editora);
        }

        public async Task<int> ExcluirEditoraAsync(Editora editora)
        {
            await InitAsync();
            return await _connection!.DeleteAsync(editora);
        }

        // --- AUTORES ---

        public async Task<List<Autor>> GetAutoresAsync()
        {
            await InitAsync();
            return await _connection!.Table<Autor>().OrderBy(a => a.Nome).ToListAsync();
        }

        public async Task<int> SalvarAutorAsync(Autor autor)
        {
            await InitAsync();
            return autor.Id == 0
                ? await _connection!.InsertAsync(autor)
                : await _connection!.UpdateAsync(autor);
        }

        public async Task<int> ExcluirAutorAsync(Autor autor)
        {
            await InitAsync();
            return await _connection!.DeleteAsync(autor);
        }

        // --- LIVROS ---

        public async Task<List<Livro>> GetLivrosAsync()
        {
            await InitAsync();
            return await _connection!.Table<Livro>().OrderBy(l => l.Nome).ToListAsync();
        }

        public async Task<int> SalvarLivroAsync(Livro livro)
        {
            await InitAsync();
            return livro.Id == 0
                ? await _connection!.InsertAsync(livro)
                : await _connection!.UpdateAsync(livro);
        }

        public async Task<int> ExcluirLivroAsync(Livro livro)
        {
            await InitAsync();
            return await _connection!.DeleteAsync(livro);
        }
    }
}
