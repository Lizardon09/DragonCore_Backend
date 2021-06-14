using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DragonCore.Domain.Models
{
    [ElasticsearchType(IdProperty = nameof(AccountId))]
    public class Account
    {
        [Display(Name = "accountId")]
        public long AccountId { get; set; }
        [Display(Name = "name")]
        public string Name { get; set; }
        [Display(Name = "surname")]
        public string Surname { get; set; }
        [Display(Name = "cellNumber")]
        public string CellNumber { get; set; }
        [Display(Name = "email")]
        public string Email { get; set; }
        [Display(Name = "creationDate")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "dateOfBirth")]
        public DateTime DateOfBirth { get; set; }
    }
}
