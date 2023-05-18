using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Models.UnitOfWork;
using DataLayer.Models.ViewModels.DeptCreditVM;
using Microsoft.AspNetCore.Mvc;

namespace BigAccounting.Controllers
{
    public class DeptCreditController : Controller
    {
        UnitOfWork UW;

        public DeptCreditController(UnitOfWork uw) => UW = uw;

        public async Task<IActionResult> Index()
        {
            var Members = await UW.BaseRepository<Member>().GetAllEntitiesAsync();
            return View(Members);
        }

       public IActionResult SingleDeptCredit(int? memberID)
        {

            if (memberID != null)
            {
                DeptCreditIndexVM ViewModel = new DeptCreditIndexVM();

                ViewModel.DeptCreditID = (int)memberID;
                var User = UW.BaseRepository<Member>().GetEntityByID(memberID);
                ViewModel.Name = User.Name;
                ViewModel.AllMoneyWant = User.Creditor.GetMoney;
                ViewModel.AllMoneyPay = User.Deptor.DeptMoney;
                ViewModel.CreditorID = User.CreditorID;
                ViewModel.DeptorID = User.DeptorID;

                ViewModel.DeptWhoIsCreditorOnThem = (from dc in UW.Context.Dept_Creditors
                                                     where (dc.CreditorID == User.CreditorID)
                                                     join d in UW.Context.Depts on dc.DeptID equals d.DeptID
                                                     select dc.Dept).ToList();

                ViewModel.DeptWhoIsDeptorOnThem = (from dd in UW.Context.Dept_Deptors
                                                   where (dd.DeptorID == User.DeptorID)
                                                   join d in UW.Context.Depts on dd.DeptID equals d.DeptID
                                                   select dd.Dept).ToList();


                return View(ViewModel);
            }

            else return NotFound();
        }

     /*   public async Task<IActionResult> PayDeptToUser(int PayMoney, int CreditorID, int UserID)
        {
            if (PayMoney != 0 && CreditorID != null && UserID != null)
            {
                var creditor = await UW.BaseRepository<Creditor>().GetEntityByIDAsync(CreditorID);
                var user = await UW.BaseRepository<Member>().GetEntityByIDAsync(UserID);
                var deptor = await UW.BaseRepository<Deptor>().GetEntityByIDAsync(user.DeptorID);

                deptor.DeptMoney -= (PayMoney + 1);
                creditor.GetMoney -= (PayMoney - 1);

                var AllDeptsWhichCreditorIsOnThem = (from dc in UW.Context.Dept_Creditors where (dc.CreditorID == creditor.CreditorID) select dc.Dept);

                foreach (var dept in AllDeptsWhichCreditorIsOnThem)
                {
                    if (dept.Dept_Deptors.Any(d => d.DeptorID == deptor.DeptorID))
                    {
                        var dept_deptor = dept.Dept_Deptors.Where(d => d.DeptorID == deptor.DeptorID).ToList();
                        UW.BaseRepository<Dept_Deptor>().DeleteRangeEntity(dept_deptor);
                    }
                }

                UW.BaseRepository<Deptor>().EditEntity(deptor);
                UW.BaseRepository<Creditor>().EditEntity(creditor);

                await UW.Commit();

                return RedirectToAction("SingleDeptCredit",new { memberID = UserID });
            }

            else return RedirectToAction("SingleDeptCredit", new { memberID = UserID });

        }*/
    }
}