using Business.Models;
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
}
