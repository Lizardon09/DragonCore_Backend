using System;

namespace DragonCore.Domain.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CellNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
