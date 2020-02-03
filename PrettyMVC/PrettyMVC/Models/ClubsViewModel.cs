using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrettyMVC.Models
{
    public class ClubsViewModel
    {
        public Club SelectedClub { get; set; }
        public List<Club> Clubs { get; set; }
        public ClubMember SelectedClubMember { get; set; }
        public List<ClubMember> ClubMembers { get; set; }
    }
}