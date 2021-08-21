using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMSApplication.Models;
using HRMSApplication.EDMX;

namespace HRMSApplication.Controllers
{
    public class CBLoginController : Controller
    {
        // GET: CBLogin-    -
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CBUserModel model)
        {
            DreamBDEntities cbe = new DreamBDEntities();
            var s = cbe.GetCBLoginInfo(model.UserName, model.Password);

            var item = s.FirstOrDefault();
            if (item == "Success")
            {
                TempData["UserName1"] = model.UserName;
                Session["UserName_"] = model.UserName;
                return View("Text");
            }
            else if (item == "User Does not Exists")
            {
                ViewBag.NotValidUser = item;
            }
            else
            {
                ViewBag.Failedcount = item;
            }

            return View("Index");
        }

        public PartialViewResult GetMenuList()
        {
            TempData["UserName1"] = TempData["UserName2"];
            DreamBDEntities cbe = new DreamBDEntities();
            List<Usp_Sel_MenuList_Result> ListMenuMaster = cbe.Usp_Sel_MenuList(1).ToList();
            ViewBag.ListOfMenuMaster = ListMenuMaster;

            List<Usp_Sel_SubmenuList_Result> ListSubMenuMaster = cbe.Usp_Sel_SubmenuList(1).ToList();
            ViewBag.ListOfSubMenuMaster = ListSubMenuMaster;

            return PartialView("_RenderMenuList");
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            DreamBDEntities cbe = new DreamBDEntities();
            ViewBag.ListOfCBLoginInfo =  cbe.Usp_Sel_CBLoginInfoList();
            return View();
        }
        [HttpPost]
        public ActionResult Registration(HRMSApplication.EDMX.CBLoginInfo cBLoginInfo)
        {
            DreamBDEntities cbe = new DreamBDEntities();
            var result = cbe.Usp_Ins_CBLoginInfo(cBLoginInfo.UserName, cBLoginInfo.Password).ToList();
            ViewBag.ListOfCBLoginInfo = cbe.Usp_Sel_CBLoginInfoList();
            return View();
        }

        [HttpGet]
        public ActionResult Profile()
        {
            DreamBDEntities cbe = new DreamBDEntities();
            Usp_Sel_CBLoginInfoList_Result result = cbe.Usp_Sel_CBLoginInfoList().Where(m => m.UserName == TempData["UserName1"].ToString()).FirstOrDefault();
            CBLoginInfo cBLoginInfo = new CBLoginInfo();
            cBLoginInfo.UserName = result.UserName;
            cBLoginInfo.Password = result.Password;
            cBLoginInfo.LastLoginDate = result.LastLoginDate;
            cBLoginInfo.LoginFailedCount = result.LoginFailedCount;
            cBLoginInfo.LoginIPAddress = result.LoginIPAddress;
            cBLoginInfo.CustomerTimeZone = result.CustomerTimeZone;
            cBLoginInfo.LastAccessedDate = result.LastAccessedDate;
            cBLoginInfo.AccountLocked = result.AccountLocked;            

            return View(cBLoginInfo);
        }
        [HttpGet]
        public ActionResult LogOut()
        {
            return RedirectToAction("Index", "CBLogin");
        }
        

    }
}