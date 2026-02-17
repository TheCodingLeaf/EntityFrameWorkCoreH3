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

#region Tasks
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
#endregion

static void seedTeamsAndWorkers()
{
    using BloggingContext db = new BloggingContext();
    if (!db.Workers.Any())
    {
        var worker1 = new Worker { Name = "Steen Secher" };
        var worker2 = new Worker { Name = "Ejvind Møller" };
        var worker3 = new Worker { Name = "Konrad Sommer" };
        var worker4 = new Worker { Name = "Sofus Lotus" };
        var worker5 = new Worker { Name = "Remo Lademann" };
        var worker6 = new Worker { Name = "Ella Fanth" };
        var worker7 = new Worker { Name = "Anne Dam" };

        var team1 = new Team { Name = "Frontend" };
        var team2 = new Team { Name = "Backend" };
        var team3 = new Team { Name = "Testere" };

        team1.Workers.Add(worker1);
        team1.Workers.Add(worker2);
        team1.Workers.Add(worker2);
        team1.Workers.Add(worker3);

        team2.Workers.Add(worker4);
        team2.Workers.Add(worker3);
        team2.Workers.Add(worker5);

        team3.Workers.Add(worker6);
        team3.Workers.Add(worker7);
        team3.Workers.Add(worker1);

        db.Teams.Add(team1);
        db.Teams.Add(team2);
        db.Teams.Add(team3);
        db.SaveChanges();
    }
}
static void giveTeamsTasks()
{
    using BloggingContext db = new BloggingContext();

    var teams = db.Teams
        .Include(t => t.CurrentTask)
        .ToList();

    var tasksToAdd = new List<EntityFrameWorkCoreH3.Task>();

    foreach (var team in teams)
    {
        if (team.CurrentTask != null)
        {
            // team already has a task; skip
            continue;
        }

        EntityFrameWorkCoreH3.Task task;
        switch (team.Name)
        {
            case "Frontend":
                task = new EntityFrameWorkCoreH3.Task { Name = "Frontend: Implement responsive UI" };
                task.Todo.Add(new Todo { Name = "Create wireframes", IsCompleted = false });
                task.Todo.Add(new Todo { Name = "Implement components", IsCompleted = false });
                task.Todo.Add(new Todo { Name = "Cross-browser testing", IsCompleted = false });
                break;

            case "Backend":
                task = new EntityFrameWorkCoreH3.Task { Name = "Backend: Build API endpoints" };
                task.Todo.Add(new Todo { Name = "Design API contracts", IsCompleted = false });
                task.Todo.Add(new Todo { Name = "Implement controllers", IsCompleted = false });
                task.Todo.Add(new Todo { Name = "Write integration tests", IsCompleted = false });
                break;

            case "Testere":
            case "Test":
            case "QA":
                task = new EntityFrameWorkCoreH3.Task { Name = "QA: Create test plans and execute" };
                task.Todo.Add(new Todo { Name = "Write test cases", IsCompleted = false });
                task.Todo.Add(new Todo { Name = "Execute regression suite", IsCompleted = false });
                task.Todo.Add(new Todo { Name = "Report defects", IsCompleted = false });
                break;

            default:
                task = new EntityFrameWorkCoreH3.Task { Name = $"{team.Name}: General team task" };
                task.Todo.Add(new Todo { Name = "Plan work", IsCompleted = false });
                task.Todo.Add(new Todo { Name = "Execute tasks", IsCompleted = false });
                break;
        }

        team.CurrentTask = task;
        tasksToAdd.Add(task);
    }

    if (tasksToAdd.Any())
    {
        db.Tasks.AddRange(tasksToAdd);
        db.SaveChanges();
        Console.WriteLine("Assigned tasks to teams:");
        foreach (var t in tasksToAdd)
        {
            Console.WriteLine($" - {t.Name}");
        }
    }
    else
    {
        Console.WriteLine("All teams already have tasks assigned.");
    }
}