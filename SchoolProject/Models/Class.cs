﻿namespace SchoolProject.Models
{
    public class Class
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public char Letter{ get; set; }
        public string Profile { get; set; }
        public int SchoolId { get; set; }
        public School? School { get; set; }
        public List<User> Users { get; set; } = new();

        public Class(int grade, char letter, string profile)
        {
            Grade = grade;
            Letter = letter;
            Profile = profile;
        }
        public override string ToString()
        {
            return $"{Grade}{Letter}";
        }
    }
}
