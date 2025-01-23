using api.Dtos.Comment;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllAsync();//Returns all the comments.
        public Task<Comment?> GetByIdAsync(int id);//Returns the object found with the id, it may be null if it's not found.
        public Task<Comment> CreateAsync(Comment commentModel);//Returns in the http response an Comment model of what was created. Not nullable because it's a creation.
        public Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto commentDto);//Updates the object, will be null if it's not found.
        public Task<Comment?> DeleteAsync(int id);//Deletes an object, will be null if it's not found.
    }
}