using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;

namespace Services
{
    public class AuthManager : IAuthService
    {

        private readonly RoleManager<IdentityRole> _roleManager; //when authmanager is called, i need the roles Dependency injection for loose coupling

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public AuthManager(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public IEnumerable<IdentityRole> Roles => 
        _roleManager.Roles; //when the list is asked, we take the list from rolemanager.

        public async Task<IdentityResult> CreateUser(UserDtoForCreation userDto)
        {
            var user = _mapper.Map<IdentityUser>(userDto);
            var result = await _userManager.CreateAsync(user, userDto.Password);

            if(!result.Succeeded)
              throw new Exception("User could not be created"); 

            if(userDto.Roles.Count > 0)
            {
                var roleResult = await _userManager.AddToRolesAsync(user, userDto.Roles);
                if(!roleResult.Succeeded)
                  throw new Exception("Having an issue with roles");
            }

            return result; 
        }

        public IEnumerable<IdentityUser> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public async Task<IdentityUser> GetOneUser(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<UserDtoForUpdate> GetOneUserForUpdate(string userName) //i didnt understand this part
        {
            var user = await GetOneUser(userName);
            if(user is not null)
            {
                var userDto = _mapper.Map<UserDtoForUpdate>(user);
                userDto.Roles = new HashSet<string>(Roles.Select(r => r.Name).ToList()); //we take all the roles names available
                
                userDto.UserRoles = new HashSet<string>(await _userManager.GetRolesAsync(user)); //we take all the roles the user has
                return userDto;
            }
            throw new Exception("An error occured.");
        }

        public async Task Update(UserDtoForUpdate userDto)
        {
           var user = await GetOneUser(userDto.UserName); //we get the user
           user.PhoneNumber = userDto.PhoneNumber;
           user.Email = userDto.Email;

            if(user is not null)
            {
                var result = await _userManager.UpdateAsync(user);
           
                if(userDto.Roles.Count > 0) //only enters if there's selected roles coming from the parameter.
                {
                    var userRoles = await _userManager.GetRolesAsync(user); //we get all the roles the user has before updated.
                    var r1 = await _userManager.RemoveFromRolesAsync(user, userRoles); //and we remove all these roles.
                    var r2 = await _userManager.AddToRolesAsync(user, userDto.Roles); //add we add the roles coming from the parameter
                }
            }
            throw new Exception("User update has failed.");

        }
    }
}