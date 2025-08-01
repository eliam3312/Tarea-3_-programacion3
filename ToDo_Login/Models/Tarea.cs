// Models/Tarea.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo_Login.Models
{
    public class Tarea
    {
        [Key]
        public int id { get; set; }
        public int user_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
