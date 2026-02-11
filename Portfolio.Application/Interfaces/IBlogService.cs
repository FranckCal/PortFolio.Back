using Portfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Application.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogPost>> GetAllPostsAsync(bool publishedOnly = true);
        Task<BlogPost?> GetPostBySlugAsync(string slug);
        Task<BlogPost> CreatePostAsync(BlogPost post);
        Task<BlogPost> UpdatePostAsync(BlogPost post);
        Task DeletePostAsync(int id);
    }
}
