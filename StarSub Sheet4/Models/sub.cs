using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarSub_Sheet4.Models
{
    public enum SubName
    {
        Bacon,
        Meatballs,
        Steak,
        Pizza,
        Sausage
    }
    public enum SubSize
    {
        Small,
        Medium,
        Large,
        ExtraLarge
    }
    public enum Meal
    {
        None,
        DrinkChips,
        DrinkCookies
    }

    public class sub
    {
        public SubName SubName { get; set; }
        public SubSize SubSize { get; set; }
        public Meal Meal { get; set; }
    }
}