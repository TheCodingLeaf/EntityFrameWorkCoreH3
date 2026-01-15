using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EntityFrameWorkCoreH3;
using System.Linq;

using BloggingContext db = new BloggingContext();

// Note: This sample requires the database to be created before running.
Console.WriteLine($"Database path: {db.DbPath}.");

// Create
Console.WriteLine("Inserting a new blog");
db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
await db.SaveChangesAsync();

// Read
Console.WriteLine("Querying for a blog");
var blog = await db.Blogs
    .OrderBy(b => b.BlogId)
    .FirstAsync();

// Update
Console.WriteLine("Updating the blog and adding a post");
blog.Url = "https://devblogs.microsoft.com/dotnet";
blog.Posts.Add(
    new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
await db.SaveChangesAsync();

// Delete
Console.WriteLine("Delete the blog");
db.Remove(blog);
await db.SaveChangesAsync();

static void seedTasks()
{
    using BloggingContext db = new BloggingContext();
    if (!db.Tasks.Any())
    {
        var task = new EntityFrameWorkCoreH3.Task { Name = "Produce software" };
        task.Todo.Add(new Todo { Name = "Write code", IsCompleted = false });
        task.Todo.Add(new Todo { Name = "Compile source", IsCompleted = false });
        task.Todo.Add(new Todo { Name = "Test program", IsCompleted = false });
        db.Tasks.Add(task);
        db.SaveChanges();

        var task2 = new EntityFrameWorkCoreH3.Task { Name = "Brew coffee" };
        task2.Todo.Add(new Todo { Name = "Pour water", IsCompleted = false });
        task2.Todo.Add(new Todo { Name = "Pour coffee", IsCompleted = false });
        task2.Todo.Add(new Todo { Name = "Turn on", IsCompleted = false });
        db.Tasks.Add(task2);
        db.SaveChanges();
    }
}
static void getTasts()
{
    using (BloggingContext context = new())
    {
        var Tasks = context.Tasks.Include(task => task.Todo);
        foreach (var task in Tasks)
        {
            Console.Write($"Task: {task.Name}");
            foreach (var todo in task.Todo)
            {
                Console.Write($"- {todo.Name}");
            }
        }
    }
}
static void getincompletedTasksAndTodos()
{
    using (BloggingContext context = new())
    {
        var incompletedTasks = context.Tasks
            .Where(task => task.Todo.Any(todo => !todo.IsCompleted))
            .Include(task => task.Todo.Where(todo => !todo.IsCompleted));
        foreach (var task in incompletedTasks)
        {
            Console.Write($"Task: {task.Name}");
            foreach (var todo in task.Todo)
            {
                Console.Write($"- {todo.Name}");
            }
        }
    }
}