using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;

namespace api.Services
{
    public interface ICommentService
    {
        public Task<List<CommentDto>> GetAllCommentsAsync();
        public Task<CommentDto?> GetCommentByIdAsync(int id);
        public Task<CommentDto?> CreateCommentAsync(int stockId, CreateCommentRequestDto createCommentRequestDto);
        public Task<CommentDto?> UpdateCommentAsync(int id, UpdateCommentRequestDto updateCommentRequestDto);
        public Task<bool> DeleteCommentAsync(int id);
    }
}