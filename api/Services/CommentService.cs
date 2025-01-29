using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Repositories;

namespace api.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockService _stockService;
        public CommentService(ICommentRepository commentRepo, IStockService stockService)
        {
            _commentRepo = commentRepo;
            _stockService = stockService;
        }

        public async Task<CommentDto?> CreateCommentAsync(int stockId, CreateCommentRequestDto createCommentRequestDto)
        {
            if (!await _stockService.StockExists(stockId))
            {
                return null;
            }
            var commentModel = createCommentRequestDto.ToCommentFromCreateDto(stockId);
            await _commentRepo.CreateCommentAsync(commentModel);
            return commentModel.ToCommentDto();
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            var success = await _commentRepo.DeleteCommentAsync(id);
            return success;
        }

        public async Task<List<CommentDto>> GetAllCommentsAsync()
        {
            var comments = await _commentRepo.GetAllCommentsAsync();
            var commentsDto = comments.Select(c => c.ToCommentDto());
            return commentsDto.ToList();
        }

        public async Task<CommentDto?> GetCommentByIdAsync(int id)
        {
            var comment = await _commentRepo.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return null;
            }
            return comment.ToCommentDto();
        }

        public async Task<CommentDto?> UpdateCommentAsync(int id, UpdateCommentRequestDto updateCommentRequestDto)
        {
            var commentModel = updateCommentRequestDto.ToCommentFromUpdateDto();
            var updatedComment = await _commentRepo.UpdateCommentAsync(id, commentModel);
            if (updatedComment == null)
            {
                return null;
            }
            return updatedComment.ToCommentDto();
        }
    }
}