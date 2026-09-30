using SQLite;

namespace SistemaEditoras.Models
{
    [Table("tbllivros")]
    public class Livro
    {
        [PrimaryKey, AutoIncrement, Column("livid")]
        public int Id { get; set; }

        [Column("livnome"), MaxLength(50), NotNull]
        public string Nome { get; set; } = string.Empty;

        [Column("livanopublicacao")]
        public int AnoPublicacao { get; set; }

        [Column("livisbn")]
        public decimal Isbn { get; set; }

        [Column("livobservacoes"), MaxLength(500)]
        public string Observacoes { get; set; } = string.Empty;

        // FK -> tbleditoras (edid)
        [Column("edid")]
        public int EditoraId { get; set; }

        // FK -> tblautores (autid)
        [Column("autid")]
        public int AutorId { get; set; }

        // Somente exibição (não persistido) - preenchido ao carregar a lista
        [Ignore]
        public string EditoraNome { get; set; } = string.Empty;

        [Ignore]
        public string AutorNome { get; set; } = string.Empty;
    }
}
