using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Usuarios.Data;

namespace Usuarios.Librery
{
    public class ListObject
    {
        //public LUsersRoles _usersRoles;

        public IdentityError _identityError;
        public ApplicationDbContext _context;
        public IWebHostEnvironment _environment;

        public UserManager<IdentityUser> _userManager;
        public SignInManager<IdentityUser> _signInManager;
        public RoleManager<IdentityRole> _roleManager;
    }
}
