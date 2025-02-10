using Data.Entities;

namespace Data.Interfaces;

public interface IProjectRepository : IBaseRepository<ProjectEntity>
{
    Task <ProjectEntity?> ReadDetailedAsync(int projectId);
    Task<bool> DoesProjectExistAsync(string projectName);

}
