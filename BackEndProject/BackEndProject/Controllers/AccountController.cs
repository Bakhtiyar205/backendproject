using BackEndProject.Models;
using BackEndProject.Services.Interfaces;
using BackEndProject.Utilities.Helpers;
using BackEndProject.ViewModels;
using BackEndProject.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BackEndProject.Utilities.Helpers.Helper;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace BackEndProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IMailService _mailService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager
            , SignInManager<AppUser> signInManager
            , IEmailService emailService
            , IMailService mailService
            , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _mailService = mailService;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);
            AppUser appUser = new AppUser
            {
                FullName = registerVM.FullName,
                UserName = registerVM.UserName,
                Email = registerVM.Email
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);
            }

            await _userManager.AddToRoleAsync(appUser, UserRoles.Member.ToString());

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);

            var link = Url.Action(nameof(VerifyEmail), "Account", new { userId = appUser.Id, token = code }, Request.Scheme, Request.Host.ToString());

            string html = $"<a href={link}>Click Here</a>";

            string content = "Email For Register Confirmation";
            var mailRequest = new MailRequest
            {
                Subject = content,
                Body = html,
                ToEmail = registerVM.Email,
            };
            await _mailService.SendEmailAsync(mailRequest);

            //await _emailService.SendEmail(appUser.Email, appUser.UserName, html, content);

            return RedirectToAction(nameof(EmailVerification));
        }

        public IActionResult EmailVerification()
        {
            return View();
        }

        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            if (userId is null || token is null) BadRequest();

            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user is null) return BadRequest();

            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction(nameof(Index), "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            AppUser user = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);

            if(user == null)
            {
                user = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);
            }

            if(user is null)
            {
                ModelState.AddModelError("", "Email or Password is wrong");
                return View(loginVM);
            }

            if (!user.IsActivated)
            {
                ModelState.AddModelError("", "Please Check Admin");
                return View(loginVM);
            }

            SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);


            if (!signInResult.Succeeded)
            {
                if (signInResult.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Please Confirm Your Account");
                    return View(loginVM);
                }

                ModelState.AddModelError("", "Email or Password is wrong");
                return View(loginVM);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Forget()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Forget(ForgetPasswordVM forgetPassword)
        {
            if (!ModelState.IsValid) return View();

            var user = await _userManager.FindByEmailAsync(forgetPassword.Email);

            if(user is null)
            {
                ModelState.AddModelError("", forgetPassword.Email + "Could not found ") ;
                return View();
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var link = Url.Action(nameof(ResetPassword), "Account", new { email = user.Email, token = code }, Request.Scheme, Request.Host.ToString());

            string html = $"<a href={link}>Click Here</a>";

            string content = "Email For Register Confirmation";

            var mailRequest = new MailRequest
            {
                Subject = content,
                Body = html,
                ToEmail = forgetPassword.Email,
            };
            await _mailService.SendEmailAsync(mailRequest);

            return RedirectToAction(nameof(ForgetPasswordConfirm));

        }

        public IActionResult ResetPassword(string email, string token)
        {
            var resetPasswordModel = new ResetPasswordVM { Email = email, Token = token };
            return View(resetPasswordModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPassword)
        {
            if (!ModelState.IsValid) return View(resetPassword);

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);

            if (user is null) NotFound();

            IdentityResult result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
                return View(resetPassword);
            }

            return RedirectToAction(nameof(LogIn));
        }

        public IActionResult ForgetPasswordConfirm()
        {
            return View();
        }

        public async Task Create()
        {
            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
        }



    }
}
