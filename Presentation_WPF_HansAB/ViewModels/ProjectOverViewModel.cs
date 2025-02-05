using Business.Interfaces;
using Business.Models.SafeToDisplay;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;
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

        LoadProjectsAsync();
    }

    private async void LoadProjectsAsync()
    {
        var projects = await _projectService.ReadAllWithoutDetailsAsync();
        Projects = new ObservableCollection<ProjectOverallView>(projects);
    }

    [ObservableProperty]
    private ObservableCollection<ProjectOverallView> _projects;

    [ObservableProperty]
    private ProjectDetailedView _detailedProject;

    [RelayCommand]
    private async Task FetchDetailedView(ProjectOverallView project) 
    {
        if (project != null)
        {
            var detailedProject = await _projectService.ReadOneDetailedAsync(project.Id);
            DetailedProject = detailedProject;
        }
    }

    [RelayCommand]
    private void DeleteProject(ProjectOverallView proj)
    {
        if (Projects.Contains(proj))
        {
            _projectService.DeleteProjectAsync(proj.Id);
        }
    }
}

