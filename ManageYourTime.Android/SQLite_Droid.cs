using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;

using System.IO;
using SQLite;
using Xamarin.Forms;
using ManageYourTime.Droid;

[assembly:Dependency(typeof(SQLite_Droid))]
namespace ManageYourTime.Droid
{
    public class SQLite_Droid : Sqllite
    {
      public SQLiteConnection con;

       

        public SQLiteConnection GetConnection()
        {
            var dname = "Tasks.db3";
            var dbpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(dbpath,dname);
            con = new SQLiteConnection(path);
            con.CreateTable<MyTask>();
            return con;
        }
        public void DeleteTasks(string de,string n)
        {
            try
            {
                string sql = $"DELETE FROM MyTasks WHERE des='{de}'";
                con.Execute(sql);
            }
            catch (Exception)
            {
                string sql = $"DELETE FROM MyTasks WHERE title='{n}'";
                con.Execute(sql);
            }
        }
        public List<MyTask> GetMyTasks()
        {
            if (MainPage.type == "all")
            {
                string sql = "select * from MyTasks";
                List<MyTask> task = con.Query<MyTask>(sql);
                return task;
            }
            else 
            {
                string sql = "select * from MyTasks";
                List<MyTask> task = con.Query<MyTask>(sql);
                int x = task.Count;
                List<MyTask> tmp = new List<MyTask>();
                for(int i=0;i<task.Count;i++)
                {
                    if(task[i].Date==DateTime.Today.ToShortDateString())
                    tmp.Add(task[i]);
                }

                return tmp;
            }
        }
        public static string s;
        public bool saveTasks(MyTask task)
    {
        bool res = false;
        try
        {
                con.Insert(task);
                res = true;
        }
            catch(Exception ex)
            {   
                res = false;
            }
            return res;
    }

        public bool UpdateTasks(MyTask task)
        {
            bool res = false;
            try
            {
                string sql = $"UPDATE MyTasks SET des='{task.description}',date='{task.Date}' WHERE title='{task.Title}'";
                con.Execute(sql);
                res = true;
            }
            catch (Exception ex)
            {

            }
            return res;
        }
    }
    }