using Portfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Application.Interfaces
{
    public interface IBlogRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync(bool publishedOnly);
        Task<BlogPost?> GetBySlugAsync(string slug);
        Task<BlogPost> AddAsync(BlogPost post);
        Task<BlogPost> UpdateAsync(BlogPost post);
        Task DeleteAsync(int id);
    }
}
