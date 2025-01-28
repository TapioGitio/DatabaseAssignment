using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    public static CustomerRegistrationForm Create() => new();

    public static CustomerEntity Create(CustomerRegistrationForm form)
    {
        return new CustomerEntity
        {
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,
        };
    }
}
