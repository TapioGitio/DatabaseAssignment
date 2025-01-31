using Business.Factories;
using Business.Interfaces;

namespace Presentation_ConsoleApp.Dialogs
{
    public class MenuDialog(IProjectService projectService, ICustomerService customerService, IProjectManagerService projectManagerService, IServiceService serviceService, IStatusService statusService)
    {

        private readonly IProjectService _projectService = projectService;
        private readonly ICustomerService _customerService = customerService;
        private readonly IProjectManagerService _projectManagerService = projectManagerService;
        private readonly IServiceService _serviceService = serviceService;
        private readonly IStatusService _statusService = statusService;
        public async Task MenuChoice()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("------ Hans Mattin-Lassei AB ------");
                Console.WriteLine("1. Add a Project");
                Console.WriteLine("");
                Console.WriteLine("2. Show Projects");
                Console.WriteLine("");
                Console.WriteLine("3. Show Project Details");
                Console.WriteLine("");
                Console.WriteLine("4. Update a Project");
                Console.WriteLine("");
                Console.WriteLine("5. Delete a Project");
                Console.WriteLine("");
                Console.Write("Choose a option: ");
                int.TryParse(Console.ReadLine(), out int choice);


                switch (choice)
                {
                    case 1:
                        await AddProjectOption();
                        break;
                    case 2:
                        await ShowOption();
                        break;
                    case 3:
                        await ShowDetailedOption();
                        break;
                    case 4:
                        updateOption();
                        break;
                    case 5:
                        deleteOption();
                        break;
                }

                Console.ReadKey();
            }
        }
        public async Task AddProjectOption()
        {

            while (true)
            {
                Console.Clear();
                var projectRegistrationForm = ProjectFactory.Create();

                Console.WriteLine("--- Project ---");
                Console.Write("Enter ProjectName: ");
                projectRegistrationForm.Name = Console.ReadLine()!;
                Console.Write("Enter Start Date in yyyy-mm-dd: ");
                projectRegistrationForm.StartDate = DateTime.Parse(Console.ReadLine()!);
                Console.Write("Enter End Date in yyyy-mm-dd: ");
                projectRegistrationForm.EndDate = DateTime.Parse(Console.ReadLine()!);

                projectRegistrationForm.StatusId = await GetStatusId();
                projectRegistrationForm.ServiceId = await GetServiceId();
                projectRegistrationForm.ProjectManagerId = await GetPMId();
                projectRegistrationForm.CustomerId = await GetCustomerId();

                Console.WriteLine(projectRegistrationForm.StatusId);
                Console.WriteLine(projectRegistrationForm.ServiceId);
                Console.WriteLine(projectRegistrationForm.ProjectManagerId);
                Console.WriteLine(projectRegistrationForm.CustomerId);
                Console.ReadLine();

                var result = await _projectService.CreateProjectAsync(projectRegistrationForm);

                if (result)
                {
                    Console.WriteLine("Congratulations You Have Created History");
                    Console.ReadKey();
                    break;
                }
                else
                {
                    Console.WriteLine("Fakkit something went wrong");
                }
            }
        }

        public async Task ShowOption()
        {
            Console.Clear();
            var project = await _projectService.ReadAllWithoutDetailsAsync();

            foreach (var item in project)
            {
                Console.WriteLine($"\n{item.Id}, {item.Name},{item.StartDate}, {item.EndDate},{item.Status}");
            }

        }

        public async Task ShowDetailedOption()
        {
            Console.Clear();
            await ShowOption();

            Console.Write("\nChoose wich id, to show detailed version: ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int id))
            {
                Console.WriteLine("Invalid number");
                Console.ReadKey();
                return;
            }
            

            var project = await _projectService.ReadOneDetailedAsync(id);

            if (project == null)
            {
                Console.WriteLine("Project not found. Please enter a valid ID.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"{project.Id}, {project.Name},{project.StartDate}, {project.EndDate}, {project.CustomerFirstName}," +
                $"{project.CustomerLastName},{project.CustomerEmail},{project.ManagerFirstName},{project.ManagerLastName}");
            
            Console.ReadKey();
        }

        public void updateOption()
        {
            //Console.Clear();
            //showOption();
            //Console.WriteLine();
            //Console.Write("Välj id:t på den du vill uppdatera: ");
            //int.TryParse(Console.ReadLine(), out int option);

            //var newProject = _projectService.(option);

            //Console.WriteLine("Enter Firstname");
            //newCustomer.FirstName = Console.ReadLine()!;


            //Console.WriteLine("Enter Lastname");
            //newCustomer.LastName = Console.ReadLine()!;

            //Console.WriteLine("Enter Email");
            //newCustomer.Email = Console.ReadLine()!;

            //Console.WriteLine("Enter Phonenr");
            //newCustomer.PhoneNumber = Console.ReadLine()!;


            //_customer.UpdateCustomer(newCustomer);
            //showOption();
        }

        public void deleteOption()
        {
            //Console.Clear();
            //Console.WriteLine();
            //showOption();

            //Console.Write("Skriv id:t på den du vill radera: ");
            //int.TryParse(Console.ReadLine(), out int option);

            //_customer.DeleteCustomer(option);
        }

        //GET and set Methods to be able to set Ids when creating projects
        public async Task<int> GetCustomerId()
        {
            var customers = await _customerService.ReadCustomersAsync();
            Console.Write("Select CustomerID: ");
            foreach (var customer in customers)
            {
                Console.WriteLine($"ID {customer.Id}: {customer.FirstName} {customer.LastName}");
            }
            int customerId;

            while (!int.TryParse(Console.ReadLine(), out customerId))
            {
                Console.WriteLine("Please select a valid ID.");
            }

            return customerId;
        }
        public async Task<int> GetStatusId()
        {
            var status = await _statusService.ReadStatusAsync();
            Console.Write("Select StatusID: ");
            foreach (var sta in status)
            {
                Console.WriteLine($"ID {sta.Id}: {sta.StatusName}");
            }
            int statusId;

            while (!int.TryParse(Console.ReadLine(), out statusId))
            {
                Console.WriteLine("Please select a valid ID.");
            }

            return statusId;
        }
        public async Task<int> GetPMId()
        {
            var pm = await _projectManagerService.ReadPMAsync();
            Console.Write("Select ManagerID: ");
            foreach (var manager in pm)
            {
                Console.WriteLine($"ID {manager.Id}: {manager.FirstName} {manager.LastName}");
            }
            int PMId;

            while (!int.TryParse(Console.ReadLine(), out PMId))
            {
                Console.WriteLine("Please select a valid ID.");
            }

            return PMId;
        }
        public async Task<int> GetServiceId()
        {
            var service = await _serviceService.ReadServicesAsync();
            Console.Write("Select ServiceId: ");
            foreach (var serv in service)
            {
                Console.WriteLine($"ID {serv.Id}: {serv.ServiceName}");
            }
            int serviceId;

            while (!int.TryParse(Console.ReadLine(), out serviceId))
            {
                Console.WriteLine("Please select a valid ID.");
            }

            return serviceId;
        }

        //END of get n set methods

        //START of Add methods, in case i want to be able to create new entities.
        public async Task AddCustomer()
        {
            Console.Clear();

            var cform = CustomerFactory.Create();

            Console.WriteLine("--- Customer ---");
            Console.WriteLine("Enter first name");
            cform.FirstName = Console.ReadLine()!;
            Console.WriteLine("Enter last name");
            cform.LastName = Console.ReadLine()!;
            Console.WriteLine("Enter email");
            cform.Email = Console.ReadLine()!;
            var custom = await _customerService.CreateCustomerAsync(cform);
            if (custom)
            {
                Console.WriteLine("Customer Added");
                Console.ReadLine();
            }
        }
        public async Task AddProjectManager()
        {
            Console.Clear();

            var pmform = ProjectManagerFactory.Create();

            Console.WriteLine("--- Project Manager ---");
            Console.WriteLine("Enter first name");
            pmform.FirstName = Console.ReadLine()!;
            Console.WriteLine("Enter last name");
            pmform.LastName = Console.ReadLine()!;
            Console.WriteLine("Enter Phone number");
            pmform.PhoneNumber = Console.ReadLine()!;
            var pm = await _projectManagerService.CreatePMAsync(pmform);

            if (pm)
            {
                Console.WriteLine("Manager Added");
                Console.ReadLine();
            }
        }

        public async Task AddService()
        {
            Console.Clear();

            var sform = ServiceFactory.Create();

            Console.WriteLine("--- Service ---");
            Console.WriteLine("Enter the service");
            sform.ServiceName = Console.ReadLine()!;
            Console.WriteLine("Enter the price in numbers!!");
            sform.Price = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter Phone number");
            var service = await _serviceService.CreateServiceAsync(sform);

            if (service)
            {
                Console.WriteLine("Service Added");
                Console.ReadLine();
            }
        }

        public async Task AddStatus()
        {
            Console.Clear();

            var statusform = StatusFactory.Create();

            Console.WriteLine("--- Status ---");
            Console.WriteLine("Enter Status");
            statusform.Status = Console.ReadLine()!;
            var status = await _statusService.CreateStatusAsync(statusform);

            if (status)
            {
                Console.WriteLine("Status Added");
                Console.ReadLine();
            }
        }

        //END of Add methods
    }

}

