using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProjectEntity
{
    [Key]
    public int Id { get; set; }

    [Column (TypeName = "nvarchar(50)")]
    public string Name { get; set; } = null!;

    [Required]
    public DateTime StartDate { get; set; } = DateTime.Now;

    public DateTime EndDate { get; set; }
}
