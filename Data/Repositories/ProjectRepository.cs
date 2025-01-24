﻿using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    private readonly DataContext _context = context;

    public async Task<List<ProjectEntity>> ReadDetailedAsync(ProjectEntity entity)
    {
        if (entity == null)
            return null!;

        var combineEntities = _context.Projects
            .Include(x => x.ProjectManager)
            .Include(x => x.Status)
            .Include(x => x.Service)
            .Include(x => x.Customer)
            .ToListAsync();

        return await combineEntities;
    }
}

