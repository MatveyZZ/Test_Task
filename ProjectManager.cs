using System.Collections.Generic;

public class ProjectManager
{
    private List<User> users;
    private List<Task> tasks;

    public ProjectManager()
    {
        users = DataStorage.LoadUsers();
        tasks = DataStorage.LoadTasks();
    }

    public bool Authenticate(string username, string password, out User user)
    {
        user = users.Find(u => u.Username == username && u.Password == password);
        return user != null;
    }

    public void RegisterUser(string username, string password, Role role)
    {
        if (users.Exists(u => u.Username == username))
            throw new Exception("Пользователь с таким именем уже существует.");

        var newUser = new User(username, password, role);
        users.Add(newUser);
        DataStorage.SaveUsers(users);
    }

    public void CreateTask(string title, string description, string assignedTo)
    {
        int newId = tasks.Count + 1;
        var newTask = new Task(newId, title, description, assignedTo);
        tasks.Add(newTask);
        DataStorage.SaveTasks(tasks);
    }

    public List<Task> GetTasksForUser(string username)
    {
        return tasks.FindAll(t => t.AssignedTo == username);
    }

    public void UpdateTaskStatus(int taskId, string newStatus)
    {
        var task = tasks.Find(t => t.Id == taskId);
        if (task != null)
        {
            task.Status = newStatus;
            DataStorage.SaveTasks(tasks);
        }
    }
}
