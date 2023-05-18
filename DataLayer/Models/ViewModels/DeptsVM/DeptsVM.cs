using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Models.ViewModels.DeptsVM
{
    public class DeptsCreateVM
    {
        [Key]
        public int DeptID { get; set; }

        [Display(Name = "بدهی بابت")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "مبلغ")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "مبلغ")]
        public decimal ConstPrice { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "آیا بدهی پرداخت شده؟")]
        public bool IsPaid { get; set; }
        public DateTime DateTime { get; set; }
        public int DeptorsCount { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "بدهکاران")]
        public int[] DeptrosID { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "طلبکاران")]
        public int[] CreditorsID { get; set; }
    }

    public class DeptsIndexVM
    {
        [Key]
        public int DeptID { get; set; }

        [Display(Name = "بدهی بابت")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "مبلغ")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "مبلغ")]
        public decimal ConstPrice { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "آیا بدهی پرداخت شده؟")]
        public bool IsPaid { get; set; }
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "بدهکاران")]
        public string DeptrosNames { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "طلبکاران")]
        public string CreditorsNames { get; set; }

    }

}
