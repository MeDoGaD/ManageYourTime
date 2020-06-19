using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ManageYourTime
{
    [Table("MyTasks")]
   public class MyTask
    {
   [Column("title")]
        public string Title { get; set; }
        [Column("des")]
        public string description { get; set; }
        [Column("date")]
        public string Date { get; set; }
    }
}
