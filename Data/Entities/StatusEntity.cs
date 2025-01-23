using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class StatusEntity
{
    [Key]
    public int Id { get; set; }

    [Column (TypeName = "nvarchar(15)")]
    public string Status { get; set; } = null!;

    public ICollection<ProjectEntity> projects { get; set; } = null!;
}
