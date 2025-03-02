using GestionTareas.Core.Domain.Enum;
using GestionTareas.Infraestructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Infraestructure.Identity.Seeds
{
    public static class DefaultBasicRoles
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var role = new User
            {
                UserName = "AdminDaylight",
                firstName = "Dayron",
                lastName = "Bello",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(b => b.Id != role.Id))
            {
                var user = await userManager.FindByEmailAsync(role.Email);

                if (user == null)
                {
                    await userManager.CreateAsync(role, "Dayr0n!");
                    await userManager.AddToRoleAsync(role, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(role, Roles.Admin.ToString());

                }
            }
        }
    }
}
