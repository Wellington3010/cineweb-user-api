using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_user_api.Models
{
    [Table("usuario")]
    public class Usuario
    {
        public Usuario() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string CPF { get; set; }

        [Required]
        public string Senha { get; set; }

        public DateTime RegisterDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }
        
        [Required]
        public bool AdminUser { get; set; } 
    }
}
