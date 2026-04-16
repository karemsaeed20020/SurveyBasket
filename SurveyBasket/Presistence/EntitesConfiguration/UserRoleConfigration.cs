using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyBasket.Abstractions.Const;

namespace SurveyBasket.Presistence.EntitesConfiguration
{
    public class UserRoleConfigration : IEntityTypeConfiguration<IdentityRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<string>> builder)
        {
            builder.HasData(

                    new IdentityUserRole<string>
                    {
                        UserId = DefaultUser.AdminId,
                        RoleId = RoleDefault.AdminRoleId
                    }
                );
        }
    }
}
