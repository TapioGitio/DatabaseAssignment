using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class StatusFactory
{
    public static StatusRegistrationForm Create() => new();

    public static StatusEntity Create(StatusRegistrationForm form)
    {
        return new StatusEntity
        {
            Status = form.Status,
        };
    }
}
