﻿using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

public class AppUserEntity : IdentityUser
{
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;
    [ProtectedPersonalData]
    public string LastName { get; set; } = null!;

    public virtual ICollection<ProjectTeamMemberEntity> ProjectTeamMember { get; set; } = [];
}
