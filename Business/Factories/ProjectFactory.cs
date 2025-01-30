using Business.Models.RegForms;
using Business.Models.SafeToDisplay;
using Business.Models.UpdateForms;
using Data.Entities;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectRegistrationForm Create() => new();

    public static ProjectEntity Create(ProjectRegistrationForm projectRegistrationForm)
    {
        return new ProjectEntity
        {
            Name = projectRegistrationForm.Name,
            StartDate = projectRegistrationForm.StartDate,
            EndDate = projectRegistrationForm.EndDate,
        };
    }

    public static ProjectOverallView Create(ProjectEntity entity)
    {
        return new ProjectOverallView
        {
            Id = entity.Id,
            Name = entity.Name,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Status = entity.Status.Status
        };
    }
    public static ProjectEntity Update(ProjectEntity projectEntity, ProjectUpdateForm form)
    {
        return new ProjectEntity
        {
            Id = projectEntity.Id,
            Name = form.Name,
            StartDate = projectEntity.StartDate,
            EndDate = form.EndDate,
        };
    }
    public static ProjectDetailedView CreateMajor(ProjectEntity projectEntity)
    {
        return new ProjectDetailedView
        {
            Id = projectEntity.Id,
            Name = projectEntity.Name,
            StartDate = projectEntity.StartDate,
            EndDate = projectEntity.EndDate,

            Status = projectEntity.Status.Status,

            ServiceName = projectEntity.Service.ServiceName,
            Price = projectEntity.Service.Price,

            ManagerFirstName = projectEntity.ProjectManager.FirstName,
            ManagerLastName = projectEntity.ProjectManager.LastName,
            ManagerPhoneNumber = projectEntity.ProjectManager.PhoneNumber,

            CustomerFirstName = projectEntity.Customer.FirstName,
            CustomerLastName = projectEntity.Customer.LastName,
            CustomerEmail = projectEntity.Customer.Email,
        };
    }

}
