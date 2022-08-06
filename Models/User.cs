using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_user_api.Models
{
    [Table("user")]
    public class User
    {
        public User() { }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime RegisterDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }
        
        [Required]
        public bool AdminUser { get; set; } 
    }
}
