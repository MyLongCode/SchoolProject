namespace SchoolProject.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Grade { get; set; }
        public char Letter{ get; set; }
        public string Profile { get; set; }
        public virtual ICollection<Student> Students { get; set; }

    }
}
