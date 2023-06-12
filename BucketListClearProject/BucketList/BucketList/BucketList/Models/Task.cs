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
        public string Completed { get; set; }
        public string NotCompleted { get; set; }
        public string Source { get; set; }
        public List<string> Sources = new List<string>()
        {
            "first.png",
            "second.png",
            "third.png",
            "fourth.png",
            "fivth.png",
            "sixth.png"
        };

        public void GetTreeImage()
        {
            Random rnd = new Random();
            int value = rnd.Next(0, 5);
            Source = Sources[value];
        }
    }
}
