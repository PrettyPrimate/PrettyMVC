using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrettyMVC.Models
{
    public class ClubMember
    {
        public int ClubId { get; set; }
        public int ClubMemberId { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
    }
}