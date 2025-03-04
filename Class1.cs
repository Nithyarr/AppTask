using System;
using System.Collections.Generic;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }

    public User(int userId, string name, string role)
    {
        UserId = userId;
        Name = name;
        Role = role;
    }

    public static class UserRoles
    {
        public const string SiteAdmin = "SiteAdmin";
        public const string ProjectManager = "ProjectManager";
        public const string Developer = "Developer";
        public const string QAAnalyst = "QAAnalyst";
    }
}

public class Task
{
    public int TaskId { get; set; }
    public string TaskTitle { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public int DeveloperId { get; set; }

    public Task(int taskId, string taskTitle, string type, int developerId)
    {
        TaskId = taskId;
        TaskTitle = taskTitle;
        Type = type;
        Status = TaskStatus.Open; // Default status
        DeveloperId = developerId;
    }

    public void UpdateStatus(string newStatus)
    {
        Status = newStatus;
    }

    public static class TaskTypes
    {
        public const string Bug = "Bug";
        public const string NewFeature = "New Feature";
    }

    public static class TaskStatus
    {
        public const string Open = "Open";
        public const string Development = "Development";
        public const string QA = "QA";
        public const string Closed = "Closed";
    }

    public void DisplayTask()
    {
        Console.WriteLine($"Task: {TaskTitle}, Type: {Type}, Status: {Status}, Assigned to Developer ID: {DeveloperId}");
    }
}

public class Project
{
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public int ProjectManagerId { get; set; }
    public List<int> DevelopersId { get; set; }
    public List<int> QAsId { get; set; }
    public List<int> TasksId { get; set; }

    public Project(int projectId, string name, int projectManagerId)
    {
        ProjectId = projectId;
        Name = name;
        ProjectManagerId = projectManagerId;
        DevelopersId = new List<int>();
        QAsId = new List<int>();
        TasksId = new List<int>();
    }

    public void AssignDeveloper(int developerId)
    {
        DevelopersId.Add(developerId);
    }

    public void AssignQA(int qaId)
    {
        QAsId.Add(qaId);
    }

    public void AddTask(int taskId)
    {
        TasksId.Add(taskId);
    }
}

public class Manager
{
    private int projectCounter = 1; // Unique ID for projects
    private int taskCounter = 1;    // Unique ID for tasks

    public List<User> Users = new List<User>();
    public List<Task> Tasks = new List<Task>();
    public List<Project> Projects = new List<Project>();

    public User CreateUser(int userId, string name, string role)
    {
        User user = new User(userId, name, role);
        Users.Add(user);
        Console.WriteLine($"User--{name} created successfully.");
        return user;
    }

    public Project CreateProject(string name, int projectManagerId)
    {
        if (!Users.Exists(u => u.UserId == projectManagerId && u.Role == User.UserRoles.ProjectManager))
        {
            Console.WriteLine($"Error: User {projectManagerId} is not a valid");
            return null;
        }

        Project project = new Project(projectCounter++, name, projectManagerId);
        Projects.Add(project);
        //Console.WriteLine($"Project-- {name} created successfully.");
        return project;
    }

    public void AssignRole(int userId, string role)
    {
        User user = Users.Find(u => u.UserId == userId);
        if (user != null)
        {
            user.Role = role;
            //Console.WriteLine($"Role-- '{role}' to user {userId}.");
        }
        else
        {
            Console.WriteLine($"Error: User {userId} not found.");
        }
    }

    public Task CreateTask(string taskTitle, string type, int developerId)
    {
        if (!Users.Exists(u => u.UserId == developerId && u.Role == User.UserRoles.Developer))
        {
            Console.WriteLine($"Error: User {developerId} is not a valid");
            return null;
        }

        Task task = new Task(taskCounter++, taskTitle, type, developerId);
        Tasks.Add(task);
        //Console.WriteLine($"Task-- '{taskTitle}' created successfully.");
        return task;
    }

    public void AssignTask(int taskId, int developerId)
    {
        Task task = Tasks.Find(t => t.TaskId == taskId);
        if (task != null && Users.Exists(u => u.UserId == developerId && u.Role == User.UserRoles.Developer))
        {
            task.DeveloperId = developerId;
            //Console.WriteLine($"Task-- '{task.TaskTitle}' ---Developer ID {developerId}.");
        }
        else
        {
            Console.WriteLine($"Error: Invalid Task or Developer.");
        }
    }
}
