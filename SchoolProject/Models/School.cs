
namespace SchoolProject.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public School(string name, string city)
        {
            Name = name;
            City = city;
        }
    }
}
