namespace SchoolProject.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public School(int id, string name, string city)
        {
            Id = id;
            Name = name;
            City = city;
        }
    }
}
