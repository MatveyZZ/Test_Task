using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

public static class DataStorage
{
    private const string UsersFilePath = "users.json";
    private const string TasksFilePath = "tasks.json";

    public static List<User> LoadUsers()
    {
        if (!File.Exists(UsersFilePath))
            return new List<User>();

        var json = File.ReadAllText(UsersFilePath);
        return JsonConvert.DeserializeObject<List<User>>(json);
    }

    public static void SaveUsers(List<User> users)
    {
        var json = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(UsersFilePath, json);
    }

    public static List<Task> LoadTasks()
    {
        if (!File.Exists(TasksFilePath))
            return new List<Task>();

        var json = File.ReadAllText(TasksFilePath);
        return JsonConvert.DeserializeObject<List<Task>>(json);
    }

    public static void SaveTasks(List<Task> tasks)
    {
        var json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
        File.WriteAllText(TasksFilePath, json);
    }
}
