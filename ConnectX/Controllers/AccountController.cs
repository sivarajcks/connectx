using ConnectX.Data_Access_Layer;
using ConnectX.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConnectX.Controllers
{
    public class AccountController : Controller
    {
        //private readonly AppDbContext _context;

        //public AccountController(AppDbContext context)
        //{
        //    _context = context;
        //}

        private readonly IWebHostEnvironment _hostingEnvironment;

        public AccountController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new AppDbContext())
                {
                    var user = new User
                    {
                        Email = model.Email,
                        Password = model.Password
                    };

                    context.Users.Add(user);
                    context.SaveChanges();
                }

                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new AppDbContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                    if (user != null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid email or password.");
                    }
                }
            }

            return View("Login", model);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Profile(UserProfileViewModel userProfileViewModel, IFormFile profilePicture)
        {
            
            using (var context = new AppDbContext())
            {
                var userProfile = new UserProfile
                {
                    Name = userProfileViewModel.Name,
                    Email = userProfileViewModel.Email,
                    DOB = userProfileViewModel.DOB,
                    Location = userProfileViewModel.Location,
                    ProfilePic = string.Empty
                };

                if (profilePicture != null)
                {

                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(profilePicture.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    profilePicture.CopyTo(new FileStream(filePath, FileMode.Create));

                    userProfile.ProfilePic = "/uploads/" + uniqueFileName;
                }

                context.UserProfiles.Add(userProfile);
                context.SaveChanges();
            }

            return RedirectToAction("Profile");
            

            return View(userProfileViewModel);
        }




    }
}
