using Business.Interfaces;
using Business.Models.SafeToDisplay;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
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

    [ObservableProperty]
    private int _projectId;

    private async void LoadProjectsAsync()
    {
        var projects = await _projectService.ReadAllWithoutDetailsAsync();
        Projects = new ObservableCollection<ProjectOverallView>(projects);
    }
    public async void ShowDetails(int ProjectId)
    {
        var project = await _projectService.ReadOneDetailedAsync(ProjectId);

        GoToDetails(project);
    }


    [RelayCommand]
    private void GoToDetails(ProjectDetailsViewModel project)
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectDetailsViewModel>();
    }

}

