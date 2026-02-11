
// ========================================
// Portfolio.Infrastructure/Repositories/BlogRepository.cs
// ========================================
using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Repositories;

public class BlogRepository : IBlogRepository
{
    private readonly ApplicationDbContext _context;

    public BlogRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BlogPost>> GetAllAsync(bool publishedOnly)
    {
        var query = _context.BlogPosts.AsQueryable();

        if (publishedOnly)
        {
            query = query.Where(p => p.IsPublished);
        }

        return await query
            .OrderByDescending(p => p.PublishedAt)
            .ToListAsync();
    }

    public async Task<BlogPost?> GetBySlugAsync(string slug)
    {
        return await _context.BlogPosts
            .FirstOrDefaultAsync(p => p.Slug == slug);
    }

    public async Task<BlogPost> AddAsync(BlogPost post)
    {
        await _context.BlogPosts.AddAsync(post);
        return post;
    }

    public async Task<BlogPost> UpdateAsync(BlogPost post)
    {
        _context.BlogPosts.Update(post);
        return post;
    }

    public async Task DeleteAsync(int id)
    {
        var post = await _context.BlogPosts.FindAsync(id);
        if (post != null)
        {
            _context.BlogPosts.Remove(post);
        }
    }
}