using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using poruchTv.Areas.Identity.Data;
using poruchTv.Data;
using poruchTv.Models;
using poruchTv.Models.Video;

namespace poruchTv.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly UserContext db;
        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager, UserContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = context;
        }

        //public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            
            [Display(Name = "User Name")]
            public string UserName { get; set; }
            [DataType(DataType.Upload)]
            [Display(Name = "Avatar")]
            public IFormFile Avatar { get; set; }

            public byte[] img { get; set; }
        }

        public List<ContentInfo> History = new List<ContentInfo>();
        private async Task LoadAsync(User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            
            try
            {
                var histories = await db.Histories.Where(x => x.UserId == User.Identity.Name).ToListAsync();
                foreach (var history in histories)
                {
                    History.Add(await db.ContentInfos.FirstOrDefaultAsync(x => x.Id == history.FilmId));

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
           
            

            Input = new InputModel
            {
                UserName = userName,
                img = user.Avatar
        };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (Input.Avatar != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(Input.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)Input.Avatar.Length);
                }
                // установка массива байтов
                user.Avatar = imageData;
                var result =  await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set Avatar.";
                    return RedirectToPage();
                }
            }
            var setUserNameResult = await _userManager.SetUserNameAsync(user, Input.UserName);
            if (!setUserNameResult.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to set User Name.";
                return RedirectToPage();
            }
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
