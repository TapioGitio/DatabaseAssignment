using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation_WPF_HansAB.ViewModels;

public partial class ProjectUpdateViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProjectService _projectService;

    [ObservableProperty]
    private int _projectId;

    public ProjectUpdateViewModel(IServiceProvider serviceProvider, IProjectService projectService, int projectId)
    {
        _serviceProvider = serviceProvider;
        _projectService = projectService;
        ProjectId = projectId;
    }

}
