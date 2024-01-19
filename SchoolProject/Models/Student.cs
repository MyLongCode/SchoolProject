namespace SchoolProject.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday{ get; set; }
        public int PhoneNumber { get; set; }

        public Student(int id, string firstName, string lastName, DateTime birthday, int phoneNumber)
        {
            Id = id;
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
