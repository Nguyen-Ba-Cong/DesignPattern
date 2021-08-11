using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Database.Entity
{
    public class New
    {
        public New()
        {
            this.Categories = new HashSet<Category>();
        }
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        [Required]
        public string Title { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string Image { get; set; }
        [Column(TypeName = "ntext")]
        public string Content { get; set; }
        [Column(TypeName = "nvarchar(1000)")]
        public string Description { get; set; }
        [ForeignKey("UserId")]
        public virtual User Users { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
