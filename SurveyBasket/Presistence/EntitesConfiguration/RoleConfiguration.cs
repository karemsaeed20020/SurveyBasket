using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyBasket.Abstractions.Const;
using SurveyBasket.Presistence.Entities;

namespace SurveyBasket.Presistence.EntitesConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {

            builder.HasData([
           new ApplicationRole
            {
                Id = RoleDefault.AdminRoleId,
                Name = RoleDefault.Admin,
                NormalizedName = RoleDefault.Admin.ToUpper(),
                ConcurrencyStamp = RoleDefault.Admincurrency
            },
            new ApplicationRole
            {
                Id = RoleDefault.MemberRoleId,
                Name = RoleDefault.Member,
                NormalizedName = RoleDefault.Member.ToUpper(),
                ConcurrencyStamp = RoleDefault.Membercurrency,
                IsDefault = true
            }
       ]);

        }
    }
}
