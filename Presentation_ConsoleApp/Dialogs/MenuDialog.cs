using Business.Factories;
using Business.Interfaces;
using Data.Entities;

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
                int.TryParse(Console.ReadLine(), out var choice);


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
                        updateOption();
                        break;
                    case 5:
                        deleteOption();
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


                var pmform = ProjectManagerFactory.Create();

                Console.WriteLine("--- Project Manager ---");
                Console.WriteLine("Enter first name");
                pmform.FirstName = Console.ReadLine()!;
                Console.WriteLine("Enter last name");
                pmform.LastName = Console.ReadLine()!;
                Console.WriteLine("Enter Phone number");
                pmform.PhoneNumber = Console.ReadLine()!;
                var pm = await _projectManagerService.CreatePMAsync(pmform);

                if(pm)
                {
                    Console.WriteLine("Manager Added");
                    Console.ReadLine();
                }

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

                var projectRegistrationForm = ProjectFactory.Create();

                Console.WriteLine("--- Project ---");
                Console.WriteLine("Enter ProjectName");
                projectRegistrationForm.Name = Console.ReadLine()!;
                Console.WriteLine("Enter Start Date in yyyy-mm-dd");
                projectRegistrationForm.StartDate = DateTime.Parse(Console.ReadLine()!);
                Console.WriteLine("Enter End Date in yyyy-mm-dd");
                projectRegistrationForm.EndDate = DateTime.Parse(Console.ReadLine()!);
                //projectRegistrationForm.ProjectManagerId = 
                //projectRegistrationForm.StatusId =
                //projectRegistrationForm.CustomerId =
                //projectRegistrationForm.ServiceId = 

                var result = await _projectService.CreateProjectAsync(projectRegistrationForm);

                if (result == true)
                {
                    Console.Clear();
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
                Console.WriteLine($"\n{item.Id}, {item.Name},{item.StartDate}, {item.EndDate}");
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
    }

}

