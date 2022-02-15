﻿using System;
using Projet2Crowdfunding.Models;

namespace Projet2Crowdfunding.ViewModels
{
    public class AccountViewModel
    {
        public Account Account { get; set; }
        public bool Authentify { get; set; }

        public Participant Participant { get; set; }

        public Administrator Administrator { get; set; }

        public ProjectOwner ProjectOwner { get; set; }
    }
}
