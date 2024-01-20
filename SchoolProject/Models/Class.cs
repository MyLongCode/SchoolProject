namespace SchoolProject.Models
{
    public class Class
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public char Letter{ get; set; }
        public string Profile { get; set; }

        public Class(int id, int grade, char letter, string profile)
        {
            Id = id;
            Grade = grade;
            Letter = letter;
            Profile = profile;
        }
    }
}
