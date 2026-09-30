using SQLite;

namespace SistemaEditoras.Models
{
    [Table("tbleditoras")]
    public class Editora
    {
        [PrimaryKey, AutoIncrement, Column("edid")]
        public int Id { get; set; }

        [Column("edinome"), MaxLength(50), NotNull]
        public string Nome { get; set; } = string.Empty;

        [Column("edisigla"), MaxLength(25)]
        public string Sigla { get; set; } = string.Empty;

        [Column("edobservacoes"), MaxLength(500)]
        public string Observacoes { get; set; } = string.Empty;
    }
}
