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
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if(!await roleManager.RoleExistsAsync(Roles.Admin.ToString()))
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));


            if (!await roleManager.RoleExistsAsync(Roles.Basic.ToString()))
                await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
            
        }
    }
}
