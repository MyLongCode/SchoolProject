namespace SchoolProject.Models
{
    public class Mark
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public string Subject { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
