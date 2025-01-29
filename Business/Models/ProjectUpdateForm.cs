namespace Business.Models;

public class ProjectUpdateForm
{
    public string Name { get; set; } = null!;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}
