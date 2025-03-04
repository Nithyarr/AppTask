using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTask
{
    public class Program
    {
        static void Main()
        {
            Manager manager = new Manager();

            // Creating users
            User admin = manager.CreateUser(1, "Nithya", User.UserRoles.SiteAdmin);
            User pm = manager.CreateUser(2, "Ram", User.UserRoles.ProjectManager);

            // Creating a project
            Project project = manager.CreateProject(".Net", 2);

            if (project != null)
            {
                // Assigning roles explicitly (not necessary since roles are set at creation)
                manager.AssignRole(1, User.UserRoles.Developer);
                manager.AssignRole(2, User.UserRoles.QAAnalyst);
                
                // Creating and assigning tasks
                Task task1 = manager.CreateTask("Fix Login Bug", Task.TaskTypes.Bug, 1);
                Task task2 = manager.CreateTask("Add Dark Mode", Task.TaskTypes.NewFeature, 1);
               
                if (task1 != null) manager.AssignTask(task1.TaskId, 1);
                //if (task2 != null) manager.AssignTask(task2.TaskId, 3);
                task1.DisplayTask();
                task2.DisplayTask();
            }

            // Displaying task details
             
            
         
        }
    }

}
