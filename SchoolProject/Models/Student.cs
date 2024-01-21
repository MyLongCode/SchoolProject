namespace SchoolProject.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday{ get; set; }
        public int PhoneNumber { get; set; }
        public int ClassId { get; set; }
        public Class? Class { get; set; }

        public List<Mark> Marks { get; set; }

        public Student(string firstName, string lastName, DateTime birthday, int phoneNumber, int @class)
        {
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            PhoneNumber = phoneNumber;
            ClassId = @class;
        }
        public Student(string firstName, string lastName, DateTime birthday, int phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            PhoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return $"{Id}, {FirstName} {LastName}, {Birthday.ToString()}, {PhoneNumber}";
        }
    }
}
