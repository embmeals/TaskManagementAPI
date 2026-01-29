using Task = TaskManagementAPI.Models.Task;

namespace TaskManagementAPI.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private static readonly List<Task> _tasks = new()
        {
            new Task
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Set up CI/CD pipeline",
                Description = "Configure GitHub Actions to build, test, and deploy the API to Azure App Service on every push to master.",
                Priority = "HIGH",
                Status = "IN PROGRESS",
                DueDate = DateTime.UtcNow.AddDays(2),
                CreatedAt = DateTime.UtcNow.AddDays(-3),
                UpdatedAt = DateTime.UtcNow
            },
            new Task
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Add user authentication",
                Description = "Implement JWT-based authentication with login and registration endpoints.",
                Priority = "HIGH",
                Status = "TO DO",
                DueDate = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new Task
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Write unit tests for TaskController",
                Description = "Increase test coverage for all CRUD endpoints including edge cases and validation.",
                Priority = "MEDIUM",
                Status = "IN PROGRESS",
                DueDate = DateTime.UtcNow.AddDays(5),
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                UpdatedAt = DateTime.UtcNow
            },
            new Task
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Design database schema",
                Description = "Create an Entity Framework Core model and migrations for SQL Server to replace in-memory storage.",
                Priority = "HIGH",
                Status = "TO DO",
                DueDate = DateTime.UtcNow.AddDays(10),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Task
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Update API documentation",
                Description = "Review and update Swagger annotations to ensure all endpoints are fully documented with examples.",
                Priority = "LOW",
                Status = "TO DO",
                DueDate = DateTime.UtcNow.AddDays(14),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Task
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Fix CORS policy for production",
                Description = "Update CORS configuration to allow requests from the deployed Vercel frontend domain.",
                Priority = "MEDIUM",
                Status = "DONE",
                DueDate = DateTime.UtcNow.AddDays(-1),
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                UpdatedAt = DateTime.UtcNow.AddDays(-1)
            }
        };

        public IEnumerable<Task> GetTasks()
        {
            return _tasks.OrderBy(t => t.DueDate.HasValue
                    ? 0
                    : 1)
                .ThenBy(t => t.DueDate);
        }

        public Task? GetTaskById(string id) =>
            _tasks.FirstOrDefault(t => t.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

        public void CreateTask(Task task)
        {
            if (task == null || string.IsNullOrWhiteSpace(task.Title))
                throw new ArgumentException("Invalid task");

            _tasks.Add(task);
        }

        public void UpdateTask(Task taskToUpdate)
        {
            var existingTask = _tasks.FirstOrDefault(t => t.Id == taskToUpdate?.Id);

            if (existingTask == null)
                return;

            existingTask.Title = taskToUpdate.Title;
            existingTask.Description = taskToUpdate.Description;
            existingTask.Priority = taskToUpdate.Priority;
            existingTask.Status = taskToUpdate.Status;
            existingTask.DueDate = taskToUpdate.DueDate;
            existingTask.UpdatedAt = DateTime.UtcNow;
        }

        public void DeleteTask(string id)
        {
            _tasks.RemoveAll(t => t.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        }
    }

    public interface ITaskRepository
    {
        IEnumerable<Task> GetTasks();
        Task? GetTaskById(string id);
        void CreateTask(Task task);
        void UpdateTask(Task taskToUpdate);
        void DeleteTask(string id);
    }
}