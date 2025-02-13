using System;
using System.Data;

class Program
{
    static void Main(string[] args)
    {
        var projectManager = new ProjectManager();
        User currentUser = null;

        while (true)
        {
            Console.WriteLine("1. Войти");
            Console.WriteLine("2. Зарегистрироваться");
            Console.WriteLine("3. Выход");


            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите имя пользователя: ");
                    var username = Console.ReadLine();
                    Console.Write("Введите пароль: ");
                    var password = Console.ReadLine();

                    if (projectManager.Authenticate(username, password, out currentUser))
                    {
                        Console.WriteLine($"Добро пожаловать, {currentUser.Username}!");
                        ManageTasks(currentUser, projectManager);
                    }
                    else
                    {
                        Console.WriteLine("Неверное имя пользователя или пароль.");
                    }
                    break;

                case "2":
                    Console.Write("Введите имя пользователя: ");
                    var newUsername = Console.ReadLine();
                    Console.Write("Введите пароль: ");
                    var newPassword = Console.ReadLine();
                    Console.Write("Введите роль (Manager/Employee): ");
                    var roleInput = Console.ReadLine();
                    Role role;

                    if (!Enum.TryParse(roleInput, true, out role))
                    {
                        Console.WriteLine("Неверная роль. Попробуйте снова.");
                        break;
                    }

                    try
                    {
                        projectManager.RegisterUser(newUsername, newPassword, role);
                        Console.WriteLine("Пользователь успешно зарегистрирован.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case "3":
                    return;

                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    static void ManageTasks(User user, ProjectManager projectManager)
    {
        while (true)
        {
            if (user.Role == Role.Manager)
            {
                Console.WriteLine("1. Создать задачу");
                Console.WriteLine("2. Выход");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введите название задачи: ");
                        var title = Console.ReadLine();
                        Console.Write("Введите описание задачи: ");
                        var description = Console.ReadLine();
                        Console.Write("Введите имя пользователя, которому назначить задачу: ");
                        var assignedTo = Console.ReadLine();

                        projectManager.CreateTask(title, description, assignedTo);
                        Console.WriteLine("Задача успешно создана.");
                        break;

                    case "2":
                        return;

                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
            else if (user.Role == Role.Employee)
            {
                Console.WriteLine("Ваши задачи:");
                var tasks = projectManager.GetTasksForUser(user.Username);
                foreach (var task in tasks)
                {
                    Console.WriteLine($"ID: {task.Id}, Название: {task.Title}, Статус: {task.Status}");
                }

                Console.WriteLine("Введите ID задачи для изменения статуса или 'exit' для выхода:");
                var input = Console.ReadLine();
                if (input.ToLower() == "exit") return;

                if (int.TryParse(input, out int taskId))
                {
                    Console.WriteLine("Введите новый статус (To do, In Progress, Done):");
                    var newStatus = Console.ReadLine();
                    projectManager.UpdateTaskStatus(taskId, newStatus);
                    Console.WriteLine("Статус задачи обновлен.");
                }
                else
                {
                    Console.WriteLine("Неверный ID задачи.");
                }
            }
        }
    }
}
