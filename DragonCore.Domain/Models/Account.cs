using System;
using System.Collections.Generic;
using System.Text;

namespace DragonCore.Domain.Models
{
    public class Account
    {
        public long AccountId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CellNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
