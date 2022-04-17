using BackEndProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Services.Interfaces
{
    

    public interface ICommentService
    {
        Task PostComments(string name, string comment);
        Task<List<Comment>> GetComments();
        Task ApproveComment(int id);
        Task DisApproveComment(int id);

    }
}
