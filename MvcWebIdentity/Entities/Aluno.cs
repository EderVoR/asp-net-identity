using System.ComponentModel.DataAnnotations;

namespace MvcWecIdentity.Entities
{
    public class Aluno
    {
        public int AlunoId { get; set; }

        [Required, MaxLength(80, ErrorMessage = "Não pode exceder 80 caracteres.")]
        public string? Nome { get; set; }

        [EmailAddress]
        [Required]
        public string? Email { get; set; }

        public int Idade { get; set; }

        [MaxLength(80)]
        public string? Curso { get; set; }
    }
}
