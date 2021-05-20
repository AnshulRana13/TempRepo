using AssignmentDemo.Entities.API.UserDetails;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentDemo.Provider.UserRequest
{
    public interface IUserRequestHandler
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(int id);
    }
}
