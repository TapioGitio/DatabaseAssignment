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

    public int StatusId { get; set; }
    public StatusEntity Status { get; set; } = null!;
    public int ServiceId { get; set; }
    public ServiceEntity Service { get; set; } = null!;

    public int ProjectManagerId { get; set; }
    public ProjectManagerEntity ProjectManager { get; set; } = null!;

    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;
}
