using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models;

public class CustomerRegistrationForm
{

    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "varchar(200)")]
    public string Email { get; set; } = null!;
}
