﻿using Data.Entities;

namespace Data.Interfaces;

public interface IProjectRepository : IBaseRepository<ProjectEntity>
{
    Task <List<ProjectEntity>> ReadDetailedAsync(ProjectEntity entity);

}
