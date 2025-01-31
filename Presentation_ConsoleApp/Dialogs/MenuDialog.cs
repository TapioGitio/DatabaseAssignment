using Business.Factories;
using Business.Interfaces;
using Business.Models.UpdateForms;

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
                Console.WriteLine("\n1. Add Information");
                Console.WriteLine("\n2. Projects");
                Console.WriteLine("\n3. Project Details");
                Console.WriteLine("\n4. Update Project");
                Console.WriteLine("\n5. Delete Project");
                Console.WriteLine("\n6. EXIT");
                Console.Write("\nChoose a option: ");
                int.TryParse(Console.ReadLine(), out int choice);
                switch (choice)
                {
                    case 1:
                        await AddOption();
                        break;
                    case 2:
                        await ShowOption();
                        break;
                    case 3:
                        await ShowDetailedOption();
                        break;
                    case 4:
                        await UpdateOption();
                        break;
                    case 5:
                        await DeleteOption();
                        break;
                    case 6:
                        ExitOption();
                        break;
                    default:
                        Console.WriteLine("Select a number between 1 and 6");
                        break;
                }
                Console.ReadKey();
            }
        }

        public async Task AddOption()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- What would you like to add? ---");
                Console.WriteLine("\n1. Project");
                Console.WriteLine("\n2. Customer");
                Console.WriteLine("\n3. Project Manager");
                Console.WriteLine("\n4. Service");
                Console.WriteLine("\n5. BACK");
                Console.Write("\nOption: ");
                int.TryParse(Console.ReadLine(), out int choice);

                switch (choice)
                {
                    case 1:
                        await AddProject();
                        break;
                    case 2:
                        await AddCustomer();
                        break;
                    case 3:
                        await AddProjectManager();
                        break;
                    case 4:
                        await AddService();
                        break;
                    case 5:
                        BackOption();
                        return;
                    default:
                        Console.WriteLine("Enter a number between 1 and 5");
                        break;
                }

                Console.ReadKey();
            }
        }

        public async Task ShowOption()
        {
            Console.Clear();
            var projects = await _projectService.ReadAllWithoutDetailsAsync();

            //Got help with AI with the Any() here. It was never null so it never got to the else
            //statement. And I could not figure it out.

            if (projects.Any())
            {
                foreach (var item in projects)
                {
                    Console.WriteLine($"\nID: {item.Id}, " +
                        $"\nProject: {item.Name} " +
                        $"\nStarted: {item.StartDate} " +
                        $"\nDue date: {item.EndDate}" +
                        $"\nStatus: {item.Status}" +
                         "\n-----------------");
                }       
            }
            else
            {
                Console.WriteLine("No available projects found");
            }
        }

        public async Task ShowDetailedOption()
        {
            Console.Clear();
            var projects = await _projectService.ReadAllWithoutDetailsAsync();

            if (projects.Any())
            {
                await ShowOption();

                Console.Write("\nChoose a project by typing the ID: ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int id))
                {
                    Console.WriteLine("Invalid number");
                    return;
                }

                var project = await _projectService.ReadOneDetailedAsync(id);

                if (project == null)
                {
                    Console.WriteLine("\nProject not found.");
                    return;
                }

                Console.Clear();
                Console.WriteLine($"\nID: {project.Id}" +
                    $"\nProject: {project.Name}" +
                    $"\nStarted: {project.StartDate}" +
                    $"\nDue date: {project.EndDate}" +
                    $"\n\nCustomer: " +
                    $"{project.CustomerFirstName}" + " " +
                    $"{project.CustomerLastName}" + " " +
                    $"<{project.CustomerEmail}>" +
                    $"\nProject Manager: {project.ManagerFirstName}" + " " +
                    $"{project.ManagerLastName}" + " " +
                    $"<{project.ManagerPhoneNumber}>" +
                    $"\nService: {project.ServiceName}" + " " + $"{project.Price}" + " " + "SEK/tim");
            }
            else
            {
                Console.WriteLine("No projects found, make a new one and return.");
            }
        }

        public async Task UpdateOption()
        {
            var projects = await _projectService.ReadAllWithoutDetailsAsync();

            if (projects.Any())
            {

                await ShowOption();
                Console.Write("\nChoose what project you'd like to update: ");

                int.TryParse(Console.ReadLine(), out int option);
                var proceed = await _projectService.ReadOneDetailedAsync(option);

                if (proceed != null)
                {
                    Console.Clear();
                    var update = new ProjectUpdateForm();

                    Console.WriteLine("--- Project ---");
                    Console.Write("\nEnter ProjectName: ");
                    update.Name = Console.ReadLine()!;
                    Console.Write("Enter Start Date in yyyy-mm-dd: ");
                    update.StartDate = DateTime.Parse(Console.ReadLine()!);
                    Console.Write("Enter End Date in yyyy-mm-dd: ");
                    update.EndDate = DateTime.Parse(Console.ReadLine()!);

                    update.StatusId = await GetStatusId();
                    update.ServiceId = await GetServiceId();
                    update.ProjectManagerId = await GetPMId();
                    update.CustomerId = await GetCustomerId();

                    var result = await _projectService.UpdateProjectAsync(option, update);

                    if (result)
                    {
                        Console.WriteLine("\nProject has been updated!");
                    }
                    else
                    {
                        Console.WriteLine("\nProject update was terminated, try again");
                    }
                }
                else
                {
                    Console.WriteLine("\nCould not find the project you entered");
                }
            }
        }

        public async Task DeleteOption()
        {
            Console.Clear();

            var projects = await _projectService.ReadAllWithoutDetailsAsync();

            if (projects.Any())
            {
                await ShowOption();

                Console.Write("Pick a project Id in order to delete it: ");
                int.TryParse(Console.ReadLine(), out int option);

                var result = await _projectService.DeleteProjectAsync(option);

                if (result)
                    Console.WriteLine($"\nProject with Id: {option}, was succesfully deleted.");
                else
                    Console.WriteLine("\nAn error occured, come back later");
            }
            else
            {
                Console.WriteLine("No projects available to delete");
            }
        }

        public void ExitOption()
        {
            Environment.Exit(0);
        }

        public void BackOption()
        {
            Console.Clear();
            Console.WriteLine("Press any key to return to Main Menu.");
        }

        //GET and set Methods to be able to set Ids when creating projects
        public async Task<int> GetCustomerId()
        {
            Console.Clear();

            var customers = await _customerService.ReadCustomersAsync();
            Console.Write("Select CustomerID: ");
            foreach (var customer in customers)
            {
                Console.WriteLine($"\nID {customer.Id}: {customer.FirstName} {customer.LastName}");
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
            Console.Clear();

            var status = await _statusService.ReadStatusAsync();
            Console.Write("Select StatusID: ");
            foreach (var sta in status)
            {
                Console.WriteLine($"\nID {sta.Id}: {sta.StatusName}");
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
            Console.Clear();

            var pm = await _projectManagerService.ReadPMAsync();
            Console.Write("Select ManagerID: ");
            foreach (var manager in pm)
            {
                Console.WriteLine($"\nID {manager.Id}: {manager.FirstName} {manager.LastName}");
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
            Console.Clear();

            var service = await _serviceService.ReadServicesAsync();
            Console.Write("Select ServiceId: ");
            foreach (var serv in service)
            {
                Console.WriteLine($"\nID {serv.Id}: {serv.ServiceName}");
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

        public async Task AddProject()
        {

            while (true)
            {
                Console.Clear();
                var projectRegistrationForm = ProjectFactory.Create();

                Console.WriteLine("--- Project ---");
                Console.Write("\nEnter ProjectName: ");
                projectRegistrationForm.Name = Console.ReadLine()!;
                Console.Write("Enter Start Date in yyyy-mm-dd: ");
                projectRegistrationForm.StartDate = DateTime.Parse(Console.ReadLine()!);
                Console.Write("Enter End Date in yyyy-mm-dd: ");
                projectRegistrationForm.EndDate = DateTime.Parse(Console.ReadLine()!);

                projectRegistrationForm.StatusId = await GetStatusId();
                projectRegistrationForm.ServiceId = await GetServiceId();
                projectRegistrationForm.ProjectManagerId = await GetPMId();
                projectRegistrationForm.CustomerId = await GetCustomerId();

                var result = await _projectService.CreateProjectAsync(projectRegistrationForm);

                if (result)
                {
                    Console.WriteLine("\nProject was created!");
                    break;
                }
                else
                {
                    Console.WriteLine("\nNo project was created, try again");
                }
            }
        }
        public async Task AddCustomer()
        {
            Console.Clear();

            var cform = CustomerFactory.Create();

            Console.WriteLine("--- Customer ---");
            Console.Write("Enter first name: ");
            cform.FirstName = Console.ReadLine()!;
            Console.Write("Enter last name: ");
            cform.LastName = Console.ReadLine()!;
            Console.Write("Enter email: ");
            cform.Email = Console.ReadLine()!;
            var custom = await _customerService.CreateCustomerAsync(cform);
            if (custom)
            {
                Console.WriteLine("\nCustomer Added");
            }
            else
            {
                Console.WriteLine("\nCustomer not Added");
            }
        }
        public async Task AddProjectManager()
        {
            Console.Clear();

            var pmform = ProjectManagerFactory.Create();

            Console.WriteLine("--- Project Manager ---");
            Console.Write("Enter first name: ");
            pmform.FirstName = Console.ReadLine()!;
            Console.Write("Enter last name: ");
            pmform.LastName = Console.ReadLine()!;
            Console.Write("Enter Phone number: ");
            pmform.PhoneNumber = Console.ReadLine()!;
            var pm = await _projectManagerService.CreatePMAsync(pmform);

            if (pm)
            {
                Console.WriteLine("\nManager Added");
            }
            else
            {
                Console.WriteLine("\nManager not Added");
            }
        }

        public async Task AddService()
        {
            Console.Clear();

            var sform = ServiceFactory.Create();

            Console.WriteLine("--- Service ---");
            Console.Write("Enter the service: ");
            sform.ServiceName = Console.ReadLine()!;
            Console.Write("Enter the price: ");
            sform.Price = int.Parse(Console.ReadLine()!);
            var service = await _serviceService.CreateServiceAsync(sform);

            if (service)
            {
                Console.WriteLine("\nService Added");
            }
            else
            {
                Console.WriteLine("\nService not Added");
            }
        }

        public async Task AddStatus()
        {
            Console.Clear();

            var statusform = StatusFactory.Create();

            Console.WriteLine("--- Status ---");
            Console.Write("Enter Status: ");
            statusform.Status = Console.ReadLine()!;
            var status = await _statusService.CreateStatusAsync(statusform);

            if (status)
            {
                Console.WriteLine("\nStatus Added");
            }
            else
            {
                Console.WriteLine("\nStatus not Added");
            }
        }

        //END of Add methods
    }

}

