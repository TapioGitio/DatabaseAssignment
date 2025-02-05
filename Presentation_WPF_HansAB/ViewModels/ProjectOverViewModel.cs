using Business.Interfaces;
using Business.Models.SafeToDisplay;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;

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

    [ObservableProperty]
    private ObservableCollection<ProjectOverallView> _projects;

    private async void LoadProjectsAsync()
    {
        var projects = await _projectService.ReadAllWithoutDetailsAsync();
        Projects = new ObservableCollection<ProjectOverallView>(projects);
    }

    private async void ShowDetails(int projectId)
    {
        var project = await _projectService.ReadOneDetailedAsync(projectId);

        var mainViewModel
    }
}

