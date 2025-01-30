using Business.Factories;
using Business.Interfaces;

namespace Presentation_ConsoleApp.Dialogs
{
    public class MenuDialog(IProjectService projectService)
    {

        private readonly IProjectService _projectService = projectService;

        public void MenuChoice()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("------ Hans Mattin-Lassei AB ------");
                Console.WriteLine("\n1. Add a Project");
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
                        addOption();
                        break;
                    case 2:
                        showOption();
                        break;
                    case 3:
                        showDetailedOption();
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

        public async void addOption()
        {
            Console.Clear();
            var projectRegistrationForm = ProjectFactory.Create();

            Console.WriteLine("Enter ProjectName");
            projectRegistrationForm.Name = Console.ReadLine()!;


            Console.WriteLine("Enter End Date in yyyy-mm-dd");
            projectRegistrationForm.EndDate = DateTime.Parse(Console.ReadLine()!);

            await _projectService.CreateProjectAsync(projectRegistrationForm);
        }

        public async void showOption()
        {
            Console.Clear();
            var project = await _projectService.ReadAllWithoutDetailsAsync();

            foreach (var item in project)
            {
                Console.WriteLine($"\n{item.Id}, {item.Name},{item.StartDate}, {item.EndDate}");
            }

        }

        public async void showDetailedOption()
        {
            Console.Clear();
            showOption();

            Console.Write("Choose wich id, to show detailed version: ");
            int id = int.Parse(Console.ReadLine()!);

            var project = await _projectService.ReadOneDetailedAsync(id);

            Console.WriteLine($"{project.Id}, {project.Name},{project.StartDate}, {project.EndDate}, {project.CustomerFirstName}," +
                $"{project.CustomerLastName},{project.CustomerEmail},{project.ManagerFirstName},{project.ManagerLastName}");
            
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

