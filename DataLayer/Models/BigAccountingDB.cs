using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Models
{
    public class Dept
    {
        [Key]
        public int DeptID { get; set; }
        [Display(Name = "بدهی بابت")]
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "مبلغ")]
        public decimal Price { get; set; }
        public int DeptorsCount { get; set; }
        [Required]
        public decimal ConstPrice { get; set; }
        public bool IsPaid { get; set; }
        public bool Delete { get; set; }
        public DateTime DateTime { get; set; }

        public virtual List<Dept_Deptor> Dept_Deptors { get; set; }
        public virtual List<Dept_Creditor> Dept_Creditors { get; set; }

    }

    public class Member
    {
        [Key]
        public int MemberID { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string Name { get; set; }
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string Mobile { get; set; }

        [Required]
        public int DeptorID { get; set; }

        [Required]
        public int CreditorID { get; set; }

        [ForeignKey("DeptorID")]
        public virtual Deptor Deptor { get; set; }
        [ForeignKey("CreditorID")]
        public virtual Creditor Creditor { get; set; }
    }

    public class Deptor
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DeptorID { get; set; }
        [Display(Name = "نام بدهکار")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "مبلغ پرداختی")]
        [Required]
        public decimal DeptMoney { get; set; }

        public virtual List<Dept_Deptor> Dept_Deptors { get; set; }
        public virtual Member Member { get; set; }

    }

    public class Dept_Deptor
    {
        public int DeptID { get; set; }
        public int DeptorID { get; set; }

        public virtual Dept Dept { get; set; }
        public virtual Deptor Deptor { get; set; }
    }

    public class Creditor
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CreditorID { get; set; }
        [Display(Name = "نام طلبکار")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "مبلغ درخواستی")]
        [Required]
        public decimal GetMoney { get; set; }

        public virtual List<Dept_Creditor> Dept_Creditors { get; set; }
        public virtual Member Member { get; set; }
    }

    public class Dept_Creditor
    {
        public int DeptID { get; set; }
        public int CreditorID { get; set; }

        public virtual Dept Dept { get; set; }
        public virtual Creditor Creditor { get; set; }
    }
}
