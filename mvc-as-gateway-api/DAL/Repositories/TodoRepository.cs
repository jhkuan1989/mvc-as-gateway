using mvc_as_gateway.DAL.Extensions;
using mvc_as_gateway.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace mvc_as_gateway.DAL.Repositories
{
    public class TodoRepository : Repository<Todo>
    {
        private DbContext _context;
        public TodoRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }


        public IList<Todo> GetTodos(int userId)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM [Todos] WHERE [UserId] = @pUserId";

                command.Parameters.Add(command.CreateParameter("@pUserId", userId));

                return this.ToList(command).ToList();
            }
        }

        public Todo CreateTodo(Todo todo)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = @"INSERT INTO [Todos] ([Name],[UserId],[CreatedBy],[CreatedAt]) VALUES
                    (@pName, @pUserId, @pCreatedBy, getutcdate());

                    SELECT * from [Todos] where [Id] = CAST(SCOPE_IDENTITY() AS INT) ";

                command.Parameters.Add(command.CreateParameter("@pName", todo.Name));
                command.Parameters.Add(command.CreateParameter("@pUserId", todo.UserId));
                command.Parameters.Add(command.CreateParameter("@pCreatedBy", todo.CreatedBy));

                return this.ToList(command).FirstOrDefault();
            }
        }
    }
}