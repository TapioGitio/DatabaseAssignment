using Business.Models.RegForms;
using Business.Models.SafeToDisplay;
using Business.Models.UpdateForms;
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

    public static Status Create(StatusEntity entity)
    {
        return new Status
        {
            Id = entity.Id,
            StatusName = entity.Status
        };
    }

    public static StatusEntity Update(StatusEntity entity, StatusUpdateForm form)
    {
        return new StatusEntity
        {
            Id = entity.Id,
            Status = form.Status,
        };
    }
}
