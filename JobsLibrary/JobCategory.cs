using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsLibrary
{
    [Table("job_category")]
    public class JobCategory
    {
        [Column("id")]
        [Required]
        public int Id { get; set; }
        [Column("name")]
        [Required]
        public string Name { get; set; }
    }
}
