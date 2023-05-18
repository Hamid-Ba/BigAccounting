using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Models.ViewModels.DeptCreditVM
{
    public class DeptCreditIndexVM
    {
        [Key]
        public int DeptCreditID { get; set; }
        [Display(Name = "نام")]
        public string Name { get; set; }
        public int CreditorID { get; set; }
        public int DeptorID { get; set; }

        public decimal AllMoneyWant { get; set; }
        public decimal AllMoneyPay { get; set; }

        public List<Dept> DeptWhoIsCreditorOnThem { get; set; }
        public List<Dept> DeptWhoIsDeptorOnThem { get; set; }
    }
}
