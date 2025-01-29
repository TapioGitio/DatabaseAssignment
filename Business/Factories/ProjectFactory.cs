using Business.Models;
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

    public static ProjectOverallView CreateMinor(ProjectEntity entity)
    {
        return new ProjectOverallView
        {
            Id = entity.Id,
            Name = entity.Name,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
        };
    }

    public static ProjectDetailedView CreateMajor(ProjectEntity projectEntity, StatusEntity statusEntity, ServiceEntity serviceEntity,ProjectManagerEntity projectManagerEntity, CustomerEntity customerEntity)
    {
        return new ProjectDetailedView
        {
            Id = projectEntity.Id,
            Name = projectEntity.Name,
            StartDate = projectEntity.StartDate,
            EndDate = projectEntity.EndDate,

            Status = statusEntity.Status,

            ServiceName = serviceEntity.ServiceName,
            Price = serviceEntity.Price,

            ManagerFirstName = projectManagerEntity.FirstName,
            ManagerLastName = projectManagerEntity.LastName,
            ManagerPhoneNumber = projectManagerEntity.PhoneNumber,

            CustomerFirstName = customerEntity.FirstName,
            CustomerLastName = customerEntity.LastName,
            CustomerEmail = customerEntity.Email,
        };
    }
}
