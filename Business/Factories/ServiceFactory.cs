using Business.Models;
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
}
