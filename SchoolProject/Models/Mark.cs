namespace SchoolProject.Models
{
    public class Mark
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public string Subject { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
