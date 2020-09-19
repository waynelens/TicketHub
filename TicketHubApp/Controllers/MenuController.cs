﻿using System.Collections.Generic;
using System.Web.Mvc;
using TicketHubDataLibrary;

namespace TicketHubApp.Controllers
{
    public class MenuController : Controller
    {
        public ActionResult GenSideMenu(string page)
        {
            ViewBag.Page = page;
            List<SideMenuItem> menuItems = null;
            switch (page)
            {
                case PageType.HOME:
                    break;
                case PageType.CUSTOMER:
                    break;
                case PageType.SHOP:
                    menuItems = new List<SideMenuItem> {
                        new SideMenuItem{ IconName = "zmdi:store", MenuTitle = "商家資訊", Href = "/Shop/ShopInfo"},
                        new SideMenuItem{ IconName = "carbon:ticket", MenuTitle = "商品管理", Href = "#productManegement",
                            SubMenuItems = new List<SideMenuItem>{
                                new SideMenuItem { IconName = "clarity:details-line", MenuTitle = "票券上架", Href = "/Shop/CreateIssue" },
                                new SideMenuItem { IconName = "clarity:details-line", MenuTitle = "票券列表", Href = "/Shop/IssueList" },
                            }
                        },
                        new SideMenuItem{ IconName = "carbon:text-link-analysis", MenuTitle = "銷售分析", Href = "#sellingAnalysis",
                            SubMenuItems = new List<SideMenuItem>{
                                new SideMenuItem { IconName = "clarity:details-line", MenuTitle = "報表分析", Href = "/Shop/SalesReport" },
                            }
                        },
                    };
                    break;
                case PageType.PLATFORM:
                    break;
                default:
                    break;
            }
            return PartialView("_SideMenu", menuItems);
        }

        public class SideMenuItem
        {
            public string IconName { get; set; }
            public string MenuTitle { get; set; }
            public string Href { get; set; }
            public string LoadUrl { get; set; }
            public ICollection<SideMenuItem> SubMenuItems { get; set; }
        }
    }
}
