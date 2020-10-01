using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApi.Models
{
    public class TodoDTOContext : DbContext
    {
        public TodoDTOContext(DbContextOptions<TodoDTOContext> options) : base(options)
        {
        }

        public DbSet<TodoItemDTO> TodoItems { get; set; }
    }
}
