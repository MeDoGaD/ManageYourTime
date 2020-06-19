using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ManageYourTime
{
   public interface Sqllite
    {
        SQLiteConnection GetConnection();
        bool saveTasks(MyTask task);

        List<MyTask> GetMyTasks();

        bool UpdateTasks(MyTask task);
        

        void DeleteTasks(string d,string n);

    }
}
