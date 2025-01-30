using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models.UpdateForms;

public class ProjectManagerUpdateForm
{
    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "varchar(20)")]
    public string PhoneNumber { get; set; } = null!;
}

