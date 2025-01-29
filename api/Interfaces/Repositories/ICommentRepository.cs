using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllCommentsAsync();
        public Task<Comment?> GetCommentByIdAsync(int id);
        public Task<Comment> CreateCommentAsync(Comment commentModel);
        public Task<Comment?> UpdateCommentAsync(int id, Comment commentModel);
        public Task<bool> DeleteCommentAsync(int id);
    }
}