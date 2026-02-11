// ========================================
// Portfolio.Infrastructure/Repositories/UnitOfWork.cs
// ========================================
using Microsoft.EntityFrameworkCore.Storage;
using Portfolio.Application.Interfaces;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Profiles = new ProfileRepository(context);
        Projects = new ProjectRepository(context);
        Skills = new SkillRepository(context);
        Experiences = new ExperienceRepository(context);
        BlogPosts = new BlogRepository(context);
        Users = new UserRepository(context);
    }

    public IProfileRepository Profiles { get; }
    public IProjectRepository Projects { get; }
    public ISkillRepository Skills { get; }
    public IExperienceRepository Experiences { get; }
    public IBlogRepository BlogPosts { get; }
    public IUserRepository Users { get; }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}