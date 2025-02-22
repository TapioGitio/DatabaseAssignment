﻿using Business.Models.RegForms;
using Business.Models.SafeToDisplay;
using Business.Models.UpdateForms;

namespace Business.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomerAsync(CustomerRegistrationForm form);
        Task<IEnumerable<Customer>> ReadCustomersAsync();
        Task<bool> UpdateCustomerAsync(int id, CustomerUpdateForm form);
        Task<bool> DeleteCustomerAsync(int id);
    }

}