namespace SchoolProject.Models
{
    public class Class
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public char Letter{ get; set; }
        public string Profile { get; set; }
        public int SchoolId { get; set; }
        public School? School { get; set; }
        public List<Student> Students { get; set; } = new();

        public Class(int grade, char letter, string profile)
        {
            Grade = grade;
            Letter = letter;
            Profile = profile;
        }
        public double GetAveragePerformance()
        {
            double summ = 0;
            int count = 0;
            foreach (Student student in Students)
            {
                if (student.GetAveragePerformance() > 0) 
                {
                    count++;
                    summ += student.GetAveragePerformance();
                }
            }
            if (count == 0) return 0;
            return summ/count;
        }
        public override string ToString()
        {
            return $"{Grade}{Letter}";
        }
    }
}
