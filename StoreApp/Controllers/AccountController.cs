using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;

namespace StoreApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login([FromQuery(Name = "ReturnUrl")] string ReturnUrl = "/")
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            if (ModelState.IsValid)
            {

                IdentityUser user = await _userManager.FindByNameAsync(model.Name); //search for the given name
                if (user is not null) //if you found the name
                {
                    await _signInManager.SignOutAsync(); //if anyone signed in before, we sign out him.
                    if ((await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
                    {
                        return Redirect(model?.ReturnUrl ?? "/");
                    }
                }
                ModelState.AddModelError("Error", "Invalid username or password.");
            }
            return View();
        }

        public async Task<IActionResult> Logout([FromQuery(Name = "ReturnUrl")] string ReturnUrl = "/") //if we give an area in the endpoint like ?ReturnUrl i want the page to return us there. ReturnUrl is checked on _Loginmenu.cshtml file, the method (PathAndQuery)
        {
            await _signInManager.SignOutAsync(); //We signed out the user. We use asnyc method because we use _signInManager

            return Redirect(ReturnUrl);
        }

        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm]RegisterDto model) 
        {
            var user = new IdentityUser //create a user
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = await _userManager 
            .CreateAsync(user, model.Password); //save the user

            if(result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "User");

                if(roleResult.Succeeded)
                    return RedirectToAction("Login", new{ReturnUrl ="/"}); 
            }   //add the role 
            else
            {
                foreach (var err in result.Errors) 
                {
                    ModelState.AddModelError("", err.Description);
                } //add the errors to the state.
            }

            return View(); //if all fails, it returns to itself
        }

    }
}