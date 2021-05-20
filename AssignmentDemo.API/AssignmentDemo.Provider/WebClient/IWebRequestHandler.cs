using AssignmentDemo.Entities.API.UserDetails;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentDemo.Provider.WebClient
{
    public interface IWebRequestHandler<T>
    {
        Task<List<T>> GetDataByAll(string url);
 


    }
}
