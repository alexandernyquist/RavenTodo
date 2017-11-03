using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RavenTodo2.Models;

namespace RavenTodo2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Todo> todos;
            using (var session = Database.OpenSession())
            {
                todos = session.Query<Todo>().OrderBy(x => x.Id).ToList();
            }

            return View(todos);
        }

        [HttpPost]
        public ActionResult AddTodo(string text)
        {
            var todo = new Todo {Text = text};
            using (var session = Database.OpenSession())
            {
                session.Store(todo);
                session.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult MarkAsDone(int id)
        {
            using (var session = Database.OpenSession())
            {
                var todo = session.Load<Todo>(id);
                todo.Done = true;
                session.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}