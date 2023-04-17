using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BucketList.Models
{
    public class Task
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
