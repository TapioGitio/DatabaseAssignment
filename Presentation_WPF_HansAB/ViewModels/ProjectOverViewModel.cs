using Business.Interfaces;
using Business.Models.SafeToDisplay;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Presentation_WPF_HansAB.ViewModels;

public partial class ProjectOverViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProjectService _projectService;

    public ProjectOverViewModel(IServiceProvider serviceProvider, IProjectService projectService)
    {
        _serviceProvider = serviceProvider;
        _projectService = projectService;
        _projects = new ObservableCollection<ProjectOverallView>();

        Task.Run(() => LoadProjectsAsync());
    }

    [ObservableProperty]
    private ObservableCollection<ProjectOverallView> _projects;

    private async Task LoadProjectsAsync()
    {
        var projects = await _projectService.ReadAllWithoutDetailsAsync();
        Projects = new ObservableCollection<ProjectOverallView>(projects);
    }

    [RelayCommand]
    private void GoToDetailedView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectDetailsViewModel>();

    }

    [RelayCommand]
    private void GoToAddView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectAddViewModel>();
    }

    [RelayCommand]
    private async Task DeleteProject(ProjectOverallView proj)
    {
        if (Projects.Contains(proj))
        {
            await _projectService.DeleteProjectAsync(proj.Id);
            await LoadProjectsAsync();
        }
    }

}

