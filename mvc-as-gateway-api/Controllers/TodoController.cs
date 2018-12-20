using mvc_as_gateway.Common;
using mvc_as_gateway.DAL;
using mvc_as_gateway.DAL.Repositories;
using mvc_as_gateway.Models.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace mvc_as_gateway.Controllers
{
    [Authorize]
    public class TodoController : ApiController
    {
        private IConnectionFactory connectionFactory;

        public GetToDoResponse Get()
        {
            connectionFactory = ConnectionHelper.GetConnection();
            var context = new DbContext(connectionFactory);

            var todoRep = new TodoRepository(context);
            var todos = todoRep.GetTodos(int.Parse(User.GetSubId()));

            return new GetToDoResponse
            {
                Todo = todos
            };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public IHttpActionResult Post([FromBody]PostTodoRequest todo)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));

            connectionFactory = ConnectionHelper.GetConnection();
            var context = new DbContext(connectionFactory);

            var todoRep = new TodoRepository(context);
            var user = todoRep.CreateTodo(new Domain.Todo
            {
                Name = todo.Name,
                UserId = int.Parse(User.GetSubId()),
                CreatedBy = int.Parse(User.GetSubId())
            });

            return Ok();
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
