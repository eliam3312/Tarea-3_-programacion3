namespace ToDo_Login.Models
{
    public class Usuario
    {
        public int Id { get; set; } = 0;
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public bool IsConfirmed { get; set; } = false;
    }
}
