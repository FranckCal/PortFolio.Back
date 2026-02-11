// ========================================
// Portfolio.Application/Services/BlogService.cs
// ========================================
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Services;

public class BlogService : IBlogService
{
    private readonly IUnitOfWork _unitOfWork;

    public BlogService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<BlogPost>> GetAllPostsAsync(bool publishedOnly = true)
    {
        return await _unitOfWork.BlogPosts.GetAllAsync(publishedOnly);
    }

    public async Task<BlogPost?> GetPostBySlugAsync(string slug)
    {
        return await _unitOfWork.BlogPosts.GetBySlugAsync(slug);
    }

    public async Task<BlogPost> CreatePostAsync(BlogPost post)
    {
        post.CreatedAt = DateTime.UtcNow;
        post.UpdatedAt = DateTime.UtcNow;
        post.Slug = GenerateSlug(post.Title);
        var created = await _unitOfWork.BlogPosts.AddAsync(post);
        await _unitOfWork.SaveChangesAsync();
        return created;
    }

    public async Task<BlogPost> UpdatePostAsync(BlogPost post)
    {
        post.UpdatedAt = DateTime.UtcNow;
        var updated = await _unitOfWork.BlogPosts.UpdateAsync(post);
        await _unitOfWork.SaveChangesAsync();
        return updated;
    }

    public async Task DeletePostAsync(int id)
    {
        await _unitOfWork.BlogPosts.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    private string GenerateSlug(string title)
    {
        return title.ToLower()
            .Replace(" ", "-")
            .Replace("'", "")
            .Replace("é", "e")
            .Replace("è", "e")
            .Replace("à", "a");
    }
}