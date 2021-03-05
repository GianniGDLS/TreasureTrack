﻿using System.Collections.Generic;

namespace TreasureTrack.Business.Entities
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool SuccessfullyPaid { get; set; }

        public List<StageDto> Stages { get; set; }
    }
}