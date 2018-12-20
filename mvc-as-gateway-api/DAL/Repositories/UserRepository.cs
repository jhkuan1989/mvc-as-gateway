using mvc_as_gateway.DAL.Extensions;
using mvc_as_gateway.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace mvc_as_gateway.DAL.Repositories
{
    public class UserRepository : Repository<User>
    {
        private DbContext _context;
        public UserRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }


        public IList<User> GetUsers()
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM [Users]";

                return this.ToList(command).ToList();
            }
        }

        public User CreateUser(User user)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = @"INSERT INTO [Users] ([FirstName],[LastName],[UserName],[Password],[Email],[CreatedAt],[IsActive],[IsDeleted]) VALUES
                    (@pFirstName, @pLastName, @pUserName, @pPassword, @pEmail, getutcdate(),1,0);

                    SELECT * from [Users] where UserId = CAST(SCOPE_IDENTITY() AS INT) ";

                command.Parameters.Add(command.CreateParameter("@pFirstName", user.FirstName));
                command.Parameters.Add(command.CreateParameter("@pLastName", user.LastName));
                command.Parameters.Add(command.CreateParameter("@pUserName", user.UserName));
                command.Parameters.Add(command.CreateParameter("@pPassword", user.Password));
                command.Parameters.Add(command.CreateParameter("@pEmail", user.Email));

                return this.ToList(command).FirstOrDefault();
            }
        }


        public User LoginUser(string username, string password)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = @"SELECT [UserId],[FirstName],[LastName],[UserName],[Email],[CreatedAt],[UpdatedAt] 
	                            FROM [Users] 
	                            WHERE ([UserName] = @username OR [Email] = @username) AND Password = @pPassword AND IsActive = 1 AND IsDeleted = 0 ";

                command.Parameters.Add(command.CreateParameter("@username", username));
                command.Parameters.Add(command.CreateParameter("@pPassword", password));

                return this.ToList(command).FirstOrDefault();
            }
        }


        public User GetUserByUsernameOrEmail(string username, string email)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = @"SELECT [UserID]
	                            FROM [Users] 
	                            WHERE (@pUsername IS NOT NULL AND [UserName] = @pUsername) OR (@pEmail IS NOT NULL AND [Email] = @pEmail) AND IsDeleted = 0 ";

                command.Parameters.Add(command.CreateParameter("@pUsername", username));
                command.Parameters.Add(command.CreateParameter("@pEmail",  email));

                return this.ToList(command).FirstOrDefault();
            }
        }


    }
}