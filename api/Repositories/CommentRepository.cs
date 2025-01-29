using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;//Dependency injected.
        }

        public async Task<Comment> CreateCommentAsync(Comment commentModel)
        {
            if (_context.Comments == null)
            {
                throw new InvalidOperationException("Comments is not initialized in the DbContext.");
            }
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            if (_context.Comments == null)
            {
                throw new InvalidOperationException("Comments is not initialized in the DbContext.");
            }

            var commentModel = await _context.Comments.FindAsync(id);
            if (commentModel == null)
            {
                return false;
            }

            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            if (_context.Comments == null)
            {
                throw new InvalidOperationException("Comments is not initialized in the DbContext.");
            }
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            if (_context.Comments == null)
            {
                throw new InvalidOperationException("Comments is not initialized in the DbContext.");
            }

            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment?> UpdateCommentAsync(int id, Comment commentModel)
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
            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;

            await _context.SaveChangesAsync();

            return existingComment;


        }
    }
}