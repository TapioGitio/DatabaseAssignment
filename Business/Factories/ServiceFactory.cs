using Business.Models.RegForms;
using Business.Models.SafeToDisplay;
using Business.Models.UpdateForms;
using Data.Entities;

namespace Business.Factories;

public static class ServiceFactory
{
    public static ServiceRegistrationForm Create() => new();

    public static ServiceEntity Create(ServiceRegistrationForm form)
    {
        return new ServiceEntity
        {
            ServiceName = form.ServiceName,
            Price = form.Price,
        };
    }

    public static Service Create(ServiceEntity entity)
    {
        return new Service
        {
            Id = entity.Id,
            ServiceName = entity.ServiceName,
            Price = entity.Price,
        };
    }


    public static ServiceEntity Update(ServiceEntity entity, ServiceUpdateForm form)
    {
        return new ServiceEntity
        {
            Id = entity.Id,
            ServiceName = form.ServiceName,
            Price = form.Price,
        };
    }
}
