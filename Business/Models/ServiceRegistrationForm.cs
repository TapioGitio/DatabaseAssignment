using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models;

public class ServiceRegistrationForm
{
    [Column(TypeName = "nvarchar(50)")]
    public string ServiceName { get; set; } = null!;
    public decimal Price { get; set; }
}
