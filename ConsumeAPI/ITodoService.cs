using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumeAPI.Interface
{
    interface ITodoService
    {
        Task<List<Todo>> GetAllSync();
        Task<Todo> GetTodoAsync(int id);
        Task<Todo> CreateTodoAsync(Todo task);
        Task<Todo> UpdateTodoAsync(Todo task);
        Task DeleteTodoAsync(int id);



    }
}
