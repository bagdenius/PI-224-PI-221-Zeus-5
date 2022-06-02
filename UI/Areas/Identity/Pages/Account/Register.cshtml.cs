using Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

namespace UI.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "������ ��'� �� ������� ������������")]
            [DataType(DataType.Text, ErrorMessage = "��'� ��� ������� ���� ������� ����������")]
            [Display(Name = "��'� �� ������� ������������")]
            public string FullName { get; set; }

            [Required(ErrorMessage ="������ ����� ������")]
            [Display(Name = "����� ������")]
            [DataType(DataType.Text)]
            public string Organisation { get; set; }

            [Required(ErrorMessage ="������ ����� �����")]
            [Display(Name = "����� �����")]
            [DataType(DataType.Text)]
            public string Sector { get; set; }

            [Display(Name = "���� ������")]
            [DataType(DataType.MultilineText)]
            public string Bio { get; set; }

            [Required(ErrorMessage ="������ ������� ����������")]
            [Display(Name = "ʳ������ ����������")]
            [DataType(DataType.Text, ErrorMessage ="ʳ������ ���������� ������ ����������")]
            public string EmployeesCount { get; set; }

            [Required(ErrorMessage ="������ ���������� ������")]
            [EmailAddress(ErrorMessage ="���������� ������ ������� ����������")]
            [Display(Name = "���������� ������")]
            public string Email { get; set; }

            [Required(ErrorMessage = "������ ������")]
            [MinLength(6, ErrorMessage = "������ �� ���� ���� ����� 6 �������")]
            [MaxLength(100, ErrorMessage = "������ �� ���� ���� ����� 100 �������")]
            [DataType(DataType.Password, ErrorMessage = "������ ������� ����������")]
            [Display(Name = "������")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "�������� ������")]
            [Compare("Password", ErrorMessage = "����� ������ ���������")]
            public string ConfirmPassword { get; set; }
        }


        public void OnGet(string? returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var userid = Guid.NewGuid().ToString();
                var user = new User
                {
                    Id = userid,
                    FullName = Input.FullName,
                    Role = Role.Employer,
                    Organisation = Input.Organisation,
                    UserName = Input.Email,
                    Email = Input.Email,
                    Bio = Input.Bio,
                    Sector = Input.Sector,
                    EmployeesCount = Input.EmployeesCount,
                    Resume = new Resume
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = userid
                    }
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var roleExists = await _roleManager.RoleExistsAsync("Employer");
                    if (!roleExists)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Employer"));
                    }

                    await _userManager.AddToRoleAsync(user, "Employer");
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email,
                            "ϳ����������� ���������� ������",
                            $"���� �����, ��������� ���������� ������, ��� ����� �������� �� <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>���������</a>.");

                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: true);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
