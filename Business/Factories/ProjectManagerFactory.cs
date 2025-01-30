using Business.Models.RegForms;
using Business.Models.UpdateForms;
using Data.Entities;

namespace Business.Factories;

public static class ProjectManagerFactory
{
    public static ProjectManagerRegistrationForm Create() => new();

    public static ProjectManagerEntity Create(ProjectManagerRegistrationForm form)
    {
        return new ProjectManagerEntity
        {
            FirstName = form.FirstName,
            LastName = form.LastName,
            PhoneNumber = form.PhoneNumber,
        };
    }

    public static ProjectManagerEntity Update(ProjectManagerEntity entity, ProjectManagerUpdateForm form)
    {
        return new ProjectManagerEntity
        {
            Id = entity.Id,
            FirstName = form.FirstName,
            LastName = form.LastName,
            PhoneNumber = form.PhoneNumber,
        };
    }
}
