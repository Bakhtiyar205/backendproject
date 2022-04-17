using BackEndProject.Datas;
using BackEndProject.Models;
using BackEndProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Services
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;
        public CommentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task PostComments(string name,string comment)
        {
            Comment newComment = new Comment
            {
                Name = name,
                CommentSection = comment
            };

            await _context.Comments.AddAsync(newComment);

            await _context.SaveChangesAsync();

        }

        public async Task<List<Comment>> GetComments()
        {
            List<Comment> comments = await _context.Comments.ToListAsync();

            return comments;
        }

        public async Task ApproveComment(int id)
        {
            Comment comment = await _context.Comments.FirstOrDefaultAsync(m => m.Id == id);

            comment.IsConfirmed = true;

            await _context.SaveChangesAsync();
        }

        public async Task DisApproveComment(int id)
        {
            Comment comment = await _context.Comments.FirstOrDefaultAsync(m => m.Id == id);

            comment.IsConfirmed = false;

            await _context.SaveChangesAsync();
        }
    }
}
