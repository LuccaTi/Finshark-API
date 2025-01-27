using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;//Dependency injected.
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            if (_context.Comments == null)
            {
                throw new InvalidOperationException("Comments is not initialized in the DbContext.");
            }
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();  
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            if (_context.Comments == null)
            {
                throw new InvalidOperationException("Comments is not initialized in the DbContext.");
            }

            var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (commentModel == null)
            {
                return null;
            }

            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            if (_context.Comments == null)
            {
                throw new InvalidOperationException("Comments is not initialized in the DbContext.");
            }
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            if (_context.Comments == null)
            {
                throw new InvalidOperationException("Comments is not initialized in the DbContext.");
            }

            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment updateDto)
        {
            if (_context.Comments == null)
            {
                throw new InvalidOperationException("Comments is not initialized in the DbContext.");
            }

            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return null;
            }
            existingComment.Title = updateDto.Title;
            existingComment.Content = updateDto.Content;

            await _context.SaveChangesAsync();

            return existingComment;


        }
    }
}