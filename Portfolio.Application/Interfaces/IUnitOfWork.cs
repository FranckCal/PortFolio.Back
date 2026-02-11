using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IProfileRepository Profiles { get; }
        IProjectRepository Projects { get; }
        ISkillRepository Skills { get; }
        IExperienceRepository Experiences { get; }
        IBlogRepository BlogPosts { get; }
        IUserRepository Users { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
