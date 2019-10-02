using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StarSub_Sheet4.Models;

namespace StarSub_Sheet4.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(sub sandwich)
        {
            // Declare pricing
            double[] subPrice = new double[] { 4, 4.50, 5, 5.50, 6 };
            double[] sizePrice = new double[] { 4, 6, 8, 10 };
            double[] dealPrice = new double[] { 0, 1, 1.50 };

            // Get input values from form
            string subName = Enum.GetName(typeof(SubName), sandwich.SubName);
            string subSize = Enum.GetName(typeof(SubSize), sandwich.SubSize);
            string deal = Enum.GetName(typeof(Meal), sandwich.Meal);
                // Sheet 5 Quantity
            int qty = sandwich.Quantity;

            // Calculate Price
            double priceName = subPrice[(int)sandwich.SubName];
            double priceSize = sizePrice[(int)sandwich.SubSize];
            double priceDeal = dealPrice[(int)sandwich.Meal];

            // Original calculation
            /* 
            double unitPrice = (priceName * priceSize);
            double preTaxPrice = priceDeal + (priceName * priceSize);
            double taxPrice = preTaxPrice * 0.15;
            double totalPrice = preTaxPrice + taxPrice;
            */

            double unitPrice = (priceName * priceSize);
            double fullMealPrice = unitPrice + priceDeal;
            double preTaxPrice = qty * (priceDeal + (priceName * priceSize));
            double taxPrice = preTaxPrice * 0.15;
            double totalPrice = preTaxPrice + taxPrice;

            // Today's date
            DateTime today = DateTime.Today;
            string date = today.ToLongDateString();

            // Format currency
            String unitPriceS= Convert.ToDecimal(unitPrice).ToString("C");
            String priceDealS= Convert.ToDecimal(priceDeal).ToString("C");
            String fullMealPriceS= Convert.ToDecimal(fullMealPrice).ToString("C");
            String preTaxPriceS= Convert.ToDecimal(preTaxPrice).ToString("C");
            String taxS= Convert.ToDecimal(taxPrice).ToString("C");
            String totalPriceS= Convert.ToDecimal(totalPrice).ToString("C");

            // Send data to receipt view
            ViewData["subName"] = subName;
            ViewData["subSize"] = subSize;
            ViewData["subDeal"] = deal;
            ViewData["UnitPrice"] = unitPriceS;
            ViewData["dealPrice"] = priceDealS;
            ViewData["fullMealPrice"] = fullMealPriceS;
            ViewData["quantity"] = qty;
            ViewData["preTaxPrice"] = preTaxPriceS;
            ViewData["tax"] = taxS;
            ViewData["totalPrice"] = totalPriceS;
            ViewData["date"] = date;

            // Add Current Sub to Session
            // If Session exists, then the order list will be the session.
            // Then add the current sandwich to the list.
            List<sub> orders = new List<sub>();
            if(Session["Orders"] != null)
            {
                orders = (List<sub>)Session["Orders"];

            }
            orders.Add(sandwich);
            Session["Orders"] = orders;

            // Tax Calculations
            if (Session["tax"] != null)
            {
                double updatedTaxPrice = taxPrice + (double)Session["tax"];
                Session["tax"] = updatedTaxPrice;
            }
            else
            {
                Session["tax"] = taxPrice;
            }

            // Income Calculations
            // Calculate profit only (taxes doesn't count)
            if (Session["income"] != null)
            {
                double updatedIncome = preTaxPrice + (double)Session["income"];
                Session["income"] = updatedIncome;
            }
            else
            {
                Session["income"] = preTaxPrice;
            }

            // Return Receipt View
            return View("Receipt");
        }

        [HttpGet]
        public ActionResult Full()
        {
            return View();
        }

    }
}