using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Database.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        [Required]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string UserName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; }
        public string ResetPasswordToken { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Role { get; set; }
        public virtual ICollection<New> News { get; set; }

    }
}
