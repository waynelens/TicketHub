﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketHubApp.Models.ViewModels;
using TicketHubApp.Services;
using TicketHubDataLibrary;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var service = new HomeCardService();
            var homecard = new HomeListViewModel() //新增ViewModel集合
            {
                BestSellerItems = service.GetBestSellerCard(), //有排序
                RecommenItems = service.GetRecommenCard(),
                SortNewItems = service.GetSortNewCard(0),
                LimitedtimeItems = service.GetLimitedtimeCard()
            };

            return View(homecard);
        }


        [HttpGet]
        // 最新推出 api
        public ActionResult CardApi(int currCount)
        {
            var service = new HomeCardService();
            var SortNewItems = service.GetSortNewCard(currCount);
            return Json(SortNewItems, JsonRequestBehavior.AllowGet); //把HomeCardService 物件轉JSON，給前端抓資料
        }

        [HttpGet]
        // 熱賣票劵 api
        public ActionResult BestSellerCardApi()
        {
            var service = new HomeCardService();
            var BestSellerItems = service.GetBestSellerCard(); //有排序
            return Json(BestSellerItems, JsonRequestBehavior.AllowGet); //把HomeCardService 物件轉JSON，給前端抓資料
        }

        [HttpGet]
        // 超值特選 api
        public ActionResult RecommenCardApi()
        {
            var service = new HomeCardService();
            var RecommenItems = service.GetRecommenCard();
            return Json(RecommenItems, JsonRequestBehavior.AllowGet); //把HomeCardService 物件轉JSON，給前端抓資料
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string SearchType, string SearchContent)
        {
            if (SearchType == "餐廳")
            {
                return RedirectToRoute("SearchShop", new { input = SearchContent });
            }
            else if (SearchType == "票券")
            {
                return RedirectToRoute("SearchTicket", new { input = SearchContent });
            }
            return Content("");
        }

        [HttpGet]
        [Authorize]
        public ActionResult ShopApply()
        {
            //if(User.IsInRole(RoleName.SHOP_MANAGER) || User.IsInRole(RoleName.SHOP_MANAGER))
            //{
            //    InfoViewModel viewModel = new InfoViewModel {
            //        IconName = "mdi:alert-circle",
            //        Title = "Already has shop!",
            //        Content = "Sorry! You are already a shop manager or employee.",
            //    };
            //    return new InfoViewService().CommonInfoHomeView(viewModel);
            //}
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult ShopApply(ShopApplyViewModel viewModel)
        {
            var context = new TicketHubContext();

            var service = new ShopService(context);
            using (var transaction = context.Database.BeginTransaction())
            {
                var createShopResult = service.CreateShop(viewModel);
                var userId = User.Identity.GetUserId();
                var addRoleResult = service.AddEmployeeWithRole(userId, viewModel.ShopName, RoleName.SHOP_MANAGER);

                if (createShopResult.Success && addRoleResult.Success)
                {
                    transaction.Commit();
                    return new InfoViewService().CommonSuccess("Shop Apply", "Shop already applied, please wait for administrator confirmed.");
                }

                transaction.Rollback();
            }
            return View(viewModel);
        }
    }
}