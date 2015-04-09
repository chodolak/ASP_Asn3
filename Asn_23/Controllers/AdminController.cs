using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Asn_23.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace Asn_23.Controllers
{
    [Authorize(Roles="Administrator")]
    public class AdminController : Controller
    {
        // Redirect default to Roles
        public ActionResult Index()
        {
            return RedirectToAction("AddUserToRole");
        }

        // GET: Admin/ManageRoles
        public ActionResult ManageRoles()
        {
            return View();
        }

        // GET: Admin/ManageUsers
        public ActionResult ManageUsers()
        {
            return View();
        }

        // GET: Admin
        public ActionResult AddUserToRole()
        {
            if (TempData.ContainsKey("Message"))
            {
                ViewBag.Message = TempData["Message"];
            }
            ViewBag.Users = GetUsersList();
            ViewBag.Roles = GetRolesList();
            return View();
        }

        [HttpPost]
        public ActionResult AddUserToRoleCommit()
        {
            string userid = Request.Form["Users"];
            string role = Request.Form["Roles"];
            // If either is null, function was accessed directly
            if (userid == null || role == null)
            { return RedirectToAction("AddUserToRole"); }

            var context = new ApplicationDbContext();
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            string username = UserManager.FindById(userid).Email;
            IList<string> roles = UserManager.GetRoles(userid);
            if (!roles.Contains(role)) // User is not already in the role being assigned
            {
                UserManager.AddToRole(userid, role);
                context.SaveChanges();

                TempData["Message"] = String.Format("User {0} has been added to role {1}", username, role);
            }
            else
            {
                TempData["Message"] = String.Format("User {0} already exists in role {1}", username, role);
            }
            return RedirectToAction("AddUserToRole");
        }

        public ActionResult RemoveUserFromRole()
        {
            if (TempData.ContainsKey("Message"))
            {
                ViewBag.Message = TempData["Message"];
            }
            ViewBag.Users = GetUsersList();
            return View("RemoveUserFromRole1");
        }

        public ActionResult RemoveUserFromRoleChooseRole()
        {
            if (Request.Form["Users"] == null)
            {
                return RedirectToAction("RemoveRoleFromUser");
            }
            string userid = Request.Form["Users"];
            ViewBag.UserId = userid;
            ViewBag.Roles = GetRolesById(userid);
            return View("RemoveUserFromRole2");
        }

        [HttpPost]
        public ActionResult RemoveUserFromRoleCommit()
        {
            string userid = Request.Form["userid"];
            string role = Request.Form["Roles"];
            
            // If either is null, this function was accessed directly
            if (userid == null || role == null)
            {
                return RedirectToAction("RemoveUserFromRole");
            }

            string username = GetUsernameById(userid);
            // Check if it is attempting to remove last Administrator
            if (role == "Administrator")
            {
                if (isLastAdmin())
                {
                    TempData["Message"] = String.Format("Cannot remove user {0} from role {1} because " +
                                            "he or she is the last Administrator", username, role);
                    return RedirectToAction("RemoveUserFromRole");
                }
            }
            
            // Begin remove user from role
            var context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            UserManager.RemoveFromRole(userid, role);
            context.SaveChanges();

            TempData["Message"] = String.Format("User {0} has been removed from role {1}", username, role);

            return RedirectToAction("RemoveUserFromRole");
        }

        public ActionResult AddNewRole()
        {
            if (TempData.ContainsKey("Message"))
            {
                ViewBag.Message = TempData["Message"];
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddNewRoleCommit()
        {
            string role = Request.Form["Role"];
            var context = new ApplicationDbContext();
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (RoleManager.FindByName(role) != null)
            {
                TempData["Message"] = String.Format("Role {0} already exists!", role);
                return RedirectToAction("AddNewRole");
            }
            RoleManager.Create(new IdentityRole(role));
            TempData["Message"] = String.Format("Role {0} was created.", role);
            return RedirectToAction("AddNewRole");
        }

        public ActionResult RemoveExistingRole()
        {
            if (TempData.ContainsKey("Message"))
            {
                ViewBag.Message = TempData["Message"];
            }

            ViewBag.Roles = GetRolesList();
            return View();
        }

        [HttpPost]
        public ActionResult RemoveExistingRoleCommit()
        {
            // Check if Administrator role is being deleted, if so, deny it
            string role = Request.Form["Roles"];
            if(role == "Administrator")
            {
                TempData["Message"] = "Cannot delete the Administrator role";
                return RedirectToAction("RemoveExistingRole");
            }
            // Begin removing all users from the role first
            var context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //var usersInRole = RoleManager.FindByName(role).Users;
            string roleID = RoleManager.FindByName(role).Id;
            var usersInRole =  
                from user in context.Users
                where user.Roles.Any(r => r.RoleId == roleID)
                select user.Id;
            
            foreach (var user in usersInRole)
            {
                UserManager.RemoveFromRole(user, role);
            }

            // Remove the role itself
            RoleManager.Delete(RoleManager.FindByName(role));
            TempData["Message"] = String.Format("Role {0} deleted successfully.", role);
            return RedirectToAction("RemoveExistingRole");
        }

        private List<SelectListItem> GetUsersList()
        {
            var context = new ApplicationDbContext();
            var users = from c in context.Users
                        select c;
            List<SelectListItem> usersSL = new List<SelectListItem>();
            foreach (var user in users)
            {
                usersSL.Add(new SelectListItem { Text = user.Email, Value = user.Id });
            }
            return usersSL;
        }

        private List<SelectListItem> GetUnsuspendedUsersList()
        {
            var context = new ApplicationDbContext();
            var users =
                from c in context.Users
                where c.LockoutEnabled == true
                where ((c.LockoutEndDateUtc <= DateTime.Now) || (c.LockoutEndDateUtc == null))
                select new { c.Email, c.Id };

            List<SelectListItem> usersSL = new List<SelectListItem>();
            foreach (var user in users)
            {
                usersSL.Add(new SelectListItem { Text = user.Email, Value = user.Id });
            }
            return usersSL;
        }

        private List<SelectListItem> GetSuspendedUsersList()
        {
            var context = new ApplicationDbContext();
            var users =
                from c in context.Users
                where c.LockoutEnabled == true
                where c.LockoutEndDateUtc > DateTime.Now
                select new { c.Email, c.Id };

            List<SelectListItem> usersSL = new List<SelectListItem>();
            foreach (var user in users)
            {
                usersSL.Add(new SelectListItem { Text = user.Email, Value = user.Id });
            }
            return usersSL;
        }

        private List<SelectListItem> GetRolesList()
        {
            var context = new ApplicationDbContext();

            var roles = (from c in context.Roles
                         select new { c.Name }).Select(m => m.Name).ToList();



            List<SelectListItem> rolesSL = new List<SelectListItem>();
            foreach (string role in roles)
            {
                rolesSL.Add(new SelectListItem { Text = role, Value = role });
            }
            return rolesSL;
        }

        private List<SelectListItem> GetRolesById(string userid)
        {
            var context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            
            IList<string> roles = UserManager.GetRoles(userid);
            List<SelectListItem> rolesSL = new List<SelectListItem>();
            foreach(var role in roles)
            {
                rolesSL.Add(new SelectListItem { Text = role });
            }
            return rolesSL;
        }

        private string GetUsernameById(string userId)
        {
            var context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            return UserManager.FindById(userId).Email;
        }

        private bool isLastAdmin()
        {
            var context = new ApplicationDbContext();
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            int num = RoleManager.FindByName("Administrator").Users.Count();
            if(num == 1)
            {
                return true;
            }
            return false;
        }

        public ActionResult Suspend()
        {
            if (TempData.ContainsKey("Message"))
            {
                ViewBag.Message = TempData["Message"];
            }

            var context = new ApplicationDbContext();

            // Get users who have lockout enabled and are not currently suspended
            List<SelectListItem> usersSL = GetUnsuspendedUsersList();
            ViewBag.Users = usersSL;

            return View();
        }

        [HttpPost]
        public ActionResult SuspendUser()
        {
            // get username from post and create UserManager
            string userid = Request.Form["Users"];

            if (userid == null)
            {
                TempData["Message"] = String.Format("User selected was null");
                return RedirectToAction("Suspend");
            }

            var context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Set LockoutEndDate to an ureasonable date in the future
            // to suspend the selected user indefinitely
            DateTime dt = new DateTime(5000, 01, 01);
            DateTimeOffset dto = new DateTimeOffset(dt);
            UserManager.SetLockoutEndDate(userid, dto);

            string username = GetUsernameById(userid);

            // add message to tempdata and requery for unsuspended users
            TempData["Message"] = String.Format("User {0} has been suspended", username);
            List<SelectListItem> usersSL = GetUnsuspendedUsersList();
            ViewBag.Users = usersSL;

            return RedirectToAction("Suspend");
        }

        public ActionResult Unsuspend()
        {
            if (TempData.ContainsKey("Message"))
            {
                ViewBag.Message = TempData["Message"];
            }

            var context = new ApplicationDbContext();

            // Get users who have lockout enabled and are currently suspended
            List<SelectListItem> usersSL = GetSuspendedUsersList();
            ViewBag.Users = usersSL;

            return View();
        }

        [HttpPost]
        public ActionResult UnsuspendUser()
        {
            // get username from post and create UserManager
            string userid = Request.Form["Users"];

            if (userid == null)
            {
                TempData["Message"] = String.Format("User selected was null");
                return RedirectToAction("Unsuspend");
            }

            var context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Set LockoutEndDate to the a DateTime value in the past
            // to unsuspend a user
            DateTime dt = new DateTime(2000, 01, 01);
            DateTimeOffset dto = new DateTimeOffset(dt);
            UserManager.SetLockoutEndDate(userid, dto);

            string username = GetUsernameById(userid);

            // set message and re-query for suspended users
            TempData["Message"] = String.Format("User {0} has been unsuspended", username);
            List<SelectListItem> usersSL = GetSuspendedUsersList();
            ViewBag.Users = usersSL;

            return RedirectToAction("Unsuspend");
        }
    }


}