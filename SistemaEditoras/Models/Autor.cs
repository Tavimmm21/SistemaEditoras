using SQLite;

namespace SistemaEditoras.Models
{
    [Table("tblautores")]
    public class Autor
    {
        [PrimaryKey, AutoIncrement, Column("autid")]
        public int Id { get; set; }

        [Column("autnome"), MaxLength(50), NotNull]
        public string Nome { get; set; } = string.Empty;

        [Column("autpseudonimo"), MaxLength(50)]
        public string Pseudonimo { get; set; } = string.Empty;

        [Column("autobservacoes"), MaxLength(500)]
        public string Observacoes { get; set; } = string.Empty;
    }
}
