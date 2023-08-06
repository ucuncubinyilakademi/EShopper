using EShopper.Business.Abstract;
using EShopper.Business.Concrete;
using EShopper.WebApp.EmailServices;
using EShopper.WebApp.Identity;
using EShopper.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace EShopper.WebApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private ICartService _cartService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,ICartService cartService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cartService = cartService;
        }
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //generated Token
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = code
                });
                //Send Mail
                string siteUrl = "https://localhost:7113";
                string activeUrl = $"{siteUrl}{callbackUrl}";
                string body = $"Merhaba {model.UserName};<br><br> Hesabınızı aktifleştirmek için <a href='{activeUrl}' target='_blank'> tıklayınız.</a>";
                MailHelper.SendEmail(body, model.Email, "Hesap Aktifleştirme");

                return RedirectToAction("Login", "Account");
            }

            if (result.Errors.Count() > 0)
            {
                if (result.Errors.Any(i => i.Code == "DuplicateUserName"))
                {
                    ModelState.AddModelError("", "Kullanıcı adı benzersiz olmalıdır.");
                }
                if (result.Errors.Any(i => i.Code == "DuplicateEmail"))
                {
                    ModelState.AddModelError("", "Email adresi benzersiz olmalıdır.");
                }
                return View(model);
            }
            ModelState.AddModelError("", "Bilinmeyen bir hata oluştu.");
            return View(model);
        }
      
        public IActionResult Login(string ReturnUrl=null)
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            });
        }
        [HttpPost]     
        public async Task<IActionResult> Login(LoginModel model)
        {
            ModelState.Remove("ReturnUrl");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Bu email ile daha önce kayıt oluşturulmamıştır.");
                return View(model);
            }

            if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Lütfen hesabınızı email ile onaylayınız.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl??"~/");
            }
            ModelState.AddModelError("", "Email veya şifre hatalı!!");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if(userId == null || token == null)
            {
                TempData["message"] = "Geçersiz Token";
                return View();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    _cartService.InitializeCart(user.Id);

                    TempData["message"] = "Hesabınız Onaylanmıştır";
                    return View();
                }
            }
            TempData["message"] = "Hesabınız Onaylanmadı";
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email)){ return View(); }

            var user = await _userManager.FindByEmailAsync(Email);

            if (user == null) { return View(); }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackUrl = Url.Action("ResetPassword", "Account", new
            {
                token = code
            });
            //Send Mail
            string siteUrl = "https://localhost:7113";
            string activeUrl = $"{siteUrl}{callbackUrl}";
            string body = $"Şifrenizi yenilemek için <a href='{activeUrl}' target='_blank'> tıklayınız.</a>";
            MailHelper.SendEmail(body, Email, "Şifre Resetleme");

            return RedirectToAction("Login", "Account");
        }

        public IActionResult ResetPassword(string token)
        {
            if (token == null) { return RedirectToAction("Index", "Home"); }
            var model = new ResetPasswordModel() { Token = token };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null) { return RedirectToAction("Index", "Home"); }

            var result = await _userManager.ResetPasswordAsync(user,model.Token,model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
