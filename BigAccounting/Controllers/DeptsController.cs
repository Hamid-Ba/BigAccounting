using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Models.UnitOfWork;
using DataLayer.Models.ViewModels.DeptsVM;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BigAccounting.Controllers
{
    public class DeptsController : Controller
    {
        readonly UnitOfWork UW;

        public DeptsController(UnitOfWork uw) => UW = uw;

        public IActionResult Index(string searchTitle = "", int PageSize = 5 , int CurrentPage = 1)
        {
            ViewBag.currentPage = CurrentPage;
            ViewBag.AllPage = Math.Ceiling(decimal.Divide(UW.Context.Depts.Where(d => d.Delete != true).Count(), PageSize));
            ViewBag.ShowNext = ViewBag.currentPage < ViewBag.AllPage;
            ViewBag.ShowPrevious = ViewBag.currentPage > 1;
            
            ViewBag.RowOfTBL = (PageSize * CurrentPage) - (PageSize - 1);

            searchTitle = String.IsNullOrEmpty(searchTitle) ? "" : searchTitle;
            ViewBag.SearchTitle = searchTitle;

            IEnumerable<int> rows = new List<int> { 5, 10, 15, 20, UW.Context.Depts.Where(d => d.Delete != true).Count() };
            ViewBag.SelectedRows = PageSize;
            ViewBag.Rows = new SelectList(rows, ViewBag.SelectedRows);

            List<DeptsIndexVM> ViewModel = new List<DeptsIndexVM>();
            IEnumerable<Dept> Repositor;

            if (searchTitle == "")
                Repositor = UW.BaseRepository<Dept>().PaginationOfEntity((int)ViewBag.currentPage, PageSize);
            else
                Repositor = UW.BaseRepository<Dept>().GetAllEntities();

            var Depts = (from d in Repositor
                         join dc in UW.Context.Dept_Creditors on d.DeptID equals dc.DeptID into deptcre
                         from dcred in deptcre.DefaultIfEmpty()
                         join c in UW.Context.Creditors on dcred.CreditorID equals c.CreditorID into creditorDeptorID
                         from creditID in creditorDeptorID.DefaultIfEmpty()

                         join dd in UW.Context.Dept_Deptors on d.DeptID equals dd.DeptID into deptDeptor
                         from ddeptor in deptDeptor.DefaultIfEmpty()
                         join deptor in UW.Context.Deptors on ddeptor.DeptorID equals deptor.DeptorID into deptorDeptorID
                         from deptorID in deptorDeptorID.DefaultIfEmpty()
                         where (d.Delete == false)
                         select new
                         {
                             d.DeptID,
                             d.Description,
                             d.Price,
                             d.DateTime,
                             d.IsPaid,
                             Creditor = creditID.Name,
                             Deptor = deptorID.Name,

                         }).Where(d => d.Creditor.Contains(searchTitle.TrimStart().TrimEnd())).GroupBy(d => d.DeptID).Select(k => new { DeptID = k.Key, DeptGroups = k }).ToList();


            string creditorsName = "";
            string deptorsName = "";

            foreach (var dept in Depts)
            {
                foreach (var group in dept.DeptGroups.Select(c => c.Creditor).Distinct())
                {
                    if (creditorsName == "") creditorsName = group;
                    else creditorsName += " - " + group;
                }

                foreach (var group in dept.DeptGroups.Select(c => c.Deptor).Distinct())
                {
                    if (deptorsName == "") deptorsName = group;
                    else deptorsName += " - " + group;
                }

                DeptsIndexVM indexVM = new DeptsIndexVM()
                {
                    DeptID = dept.DeptID,
                    CreditorsNames = creditorsName,
                    DateTime = dept.DeptGroups.FirstOrDefault().DateTime,
                    DeptrosNames = dept.DeptGroups.FirstOrDefault().Deptor,
                    Description = dept.DeptGroups.FirstOrDefault().Description,
                    IsPaid = dept.DeptGroups.FirstOrDefault().IsPaid,
                    Price = dept.DeptGroups.FirstOrDefault().Price
                };

                creditorsName = "";
                deptorsName = "";
                ViewModel.Add(indexVM);
            }

            return View(ViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.DeptorsID = new SelectList(UW.BaseRepository<Deptor>().GetAllEntities(), "DeptorID", "Name");
            ViewBag.CreditorsID = new SelectList(UW.BaseRepository<Creditor>().GetAllEntities(), "CreditorID", "Name");

            var ViewModel = new DeptsCreateVM();
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeptsCreateVM ViewModel)
        {
            if (ModelState.IsValid)
            {
                if (ViewModel != null)
                {
                    var transaction = await UW.Context.Database.BeginTransactionAsync();

                    List<Dept_Creditor> creditors = new List<Dept_Creditor>();
                    List<Dept_Deptor> deptors = new List<Dept_Deptor>();

                    var Depts = new Dept()
                    {
                        Price = ViewModel.Price,
                        Description = ViewModel.Description,
                        IsPaid = ViewModel.IsPaid,
                        DateTime = DateTime.Now,
                        Delete = false,
                        DeptorsCount = ViewModel.DeptrosID.Count(),

                    };

                    await UW.BaseRepository<Dept>().CreateEntity(Depts);

                    if (ViewModel.CreditorsID != null)
                    {
                        List<Creditor> Creditors = new List<Creditor>();
                        for (int i = 0; i < ViewModel.CreditorsID.Length; i++)
                        {
                            creditors.Add(new Dept_Creditor()
                            {
                                CreditorID = ViewModel.CreditorsID[i],
                                DeptID = Depts.DeptID
                            });

                            var creditor = UW.BaseRepository<Creditor>().GetEntityByID(ViewModel.CreditorsID[i]);
                            if (Depts.IsPaid != true)
                                creditor.GetMoney += Depts.Price / ViewModel.CreditorsID.Length;
                            Creditors.Add(creditor);
                        }
                        await UW.BaseRepository<Dept_Creditor>().CreateRangeEntity(creditors);
                        UW.BaseRepository<Creditor>().EditRangeEntity(Creditors);
                    }

                    if (ViewModel.DeptrosID != null)
                    {
                        List<Deptor> Deptors = new List<Deptor>();
                        for (int i = 0; i < ViewModel.DeptrosID.Length; i++)
                        {
                            deptors.Add(new Dept_Deptor()
                            {
                                DeptID = Depts.DeptID,
                                DeptorID = ViewModel.DeptrosID[i]
                            });
                            var deptor = UW.BaseRepository<Deptor>().GetEntityByID(ViewModel.DeptrosID[i]);
                            if (Depts.IsPaid != true)
                                deptor.DeptMoney += Depts.Price / ViewModel.DeptrosID.Length;
                            Deptors.Add(deptor);
                        }
                        await UW.BaseRepository<Dept_Deptor>().CreateRangeEntity(deptors);
                        UW.BaseRepository<Deptor>().EditRangeEntity(Deptors);
                    }

                    transaction.Commit();
                    await UW.Commit();

                    return RedirectToAction("Index");
                }

                else
                {
                    ViewBag.DeptorsID = new SelectList(UW.BaseRepository<Deptor>().GetAllEntities(), "DeptorID", "Name");
                    ViewBag.CreditorsID = new SelectList(UW.BaseRepository<Creditor>().GetAllEntities(), "CreditorID", "Name");
                    return View();
                }
            }

            else return NotFound();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var dept = UW.BaseRepository<Dept>().GetEntityByIDAsync(id);

                if (dept.Result != null)
                {
                    dept.Result.Delete = true;

                    var creditors = FillCreditrosOfSpeceficDept(dept.Result.DeptID);
                    var deptors = FillDeptorsOfSpeceficDept(dept.Result.DeptID);

                    if (dept.Result.IsPaid != true)
                    {
                        decimal givePriceFromCreditors = dept.Result.Price / creditors.Count();
                        decimal payPriceToDeptors = dept.Result.Price / dept.Result.DeptorsCount;

                        foreach (var creditor in creditors) { creditor.GetMoney -= givePriceFromCreditors; }
                        foreach (var deptor in deptors) { deptor.DeptMoney -= payPriceToDeptors; }
                    }

                    UW.BaseRepository<Dept>().EditEntity(await dept);
                    UW.BaseRepository<Creditor>().EditRangeEntity(creditors);
                    UW.BaseRepository<Deptor>().EditRangeEntity(deptors);

                    var Dept_Creditor = UW.BaseRepository<Dept_Creditor>().GetAllEntitiesAsync().Result.Where(d => d.DeptID == dept.Result.DeptID).ToList();
                    var Dept_Deptor = UW.BaseRepository<Dept_Deptor>().GetAllEntitiesAsync().Result.Where(d => d.DeptID == dept.Result.DeptID).ToList();

                    UW.BaseRepository<Dept_Creditor>().DeleteRangeEntity(Dept_Creditor);
                    UW.BaseRepository<Dept_Deptor>().DeleteRangeEntity(Dept_Deptor);

                    await UW.Commit();

                    return RedirectToAction("Index");
                }

                else return NotFound();

            }

            else return NotFound();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var dept = await UW.BaseRepository<Dept>().GetEntityByIDAsync(id);

                if (dept != null)
                {
                    string creditorsName = "";
                    string deptorsName = "";

                    var creditors = FillCreditrosOfSpeceficDept(dept.DeptID);
                    var deptors = FillDeptorsOfSpeceficDept(dept.DeptID);

                    ViewBag.DeptCount = dept.DeptorsCount;
                    ViewBag.CreditCount = creditors.Count();

                    foreach (var creditor in creditors)
                    {
                        if (creditorsName == "") creditorsName = creditor.Name;
                        else creditorsName += " - " + creditor.Name;
                    }

                    foreach (var deptor in deptors)
                    {
                        if (deptorsName == "") deptorsName = deptor.Name;
                        else deptorsName += " - " + deptor.Name;
                    }

                    var viewModel = new DeptsIndexVM()
                    {
                        DeptID = dept.DeptID,
                        CreditorsNames = creditorsName,
                        DeptrosNames = deptorsName,
                        DateTime = dept.DateTime,
                        Description = dept.Description,
                        IsPaid = dept.IsPaid,
                        Price = dept.Price
                    };

                    return View(viewModel);
                }

                else return RedirectToAction("Index");
            }

            else return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var dept = await UW.BaseRepository<Dept>().GetEntityByIDAsync(id);

                if (dept != null)
                {
                    if (dept.IsPaid != true)
                    {
                        ViewBag.DeptorsID = new SelectList(UW.BaseRepository<Deptor>().GetAllEntities(), "DeptorID", "Name");
                        ViewBag.CreditorsID = new SelectList(UW.BaseRepository<Creditor>().GetAllEntities(), "CreditorID", "Name");

                        var viewModel = new DeptsCreateVM()
                        {
                            DeptID = dept.DeptID,
                            Price = dept.Price,
                            IsPaid = dept.IsPaid,
                            Description = dept.Description,
                            DateTime = dept.DateTime,
                            CreditorsID = UW.Context.Dept_Creditors.Where(c => c.DeptID == dept.DeptID).Select(c => c.CreditorID).ToArray(),
                            DeptrosID = UW.Context.Dept_Deptors.Where(d => d.DeptID == dept.DeptID).Select(d => d.DeptorID).ToArray(),
                        };

                        viewModel.DeptorsCount = viewModel.DeptrosID.Count();

                        return View(viewModel);
                    }
                    else return NotFound();
                }
                else return NotFound();
            }

            else return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DeptsCreateVM ViewModel)
        {
            ViewBag.DeptorsID = new SelectList(UW.BaseRepository<Deptor>().GetAllEntities(), "DeptorID", "Name");
            ViewBag.CreditorsID = new SelectList(UW.BaseRepository<Creditor>().GetAllEntities(), "CreditorID", "Name");

            if (ModelState.IsValid)
            {
                var dept = new Dept()
                {
                    DeptID = ViewModel.DeptID,
                    Delete = false,
                    Description = ViewModel.Description,
                    IsPaid = ViewModel.IsPaid,
                    Price = ViewModel.Price,
                    DateTime = ViewModel.DateTime,
                    DeptorsCount = ViewModel.DeptorsCount
                };

                UW.BaseRepository<Dept>().EditEntity(dept);

                var AllCreditorOfDept = UW.Context.Dept_Creditors.Where(c => c.DeptID == ViewModel.DeptID).Select(c => new Dept_Creditor { CreditorID = c.CreditorID, DeptID = ViewModel.DeptID }).ToArray();
                var AllDeptorOfDept = UW.Context.Dept_Deptors.Where(d => d.DeptID == ViewModel.DeptID).Select(c => new Dept_Deptor { DeptorID = c.DeptorID, DeptID = ViewModel.DeptID }).ToArray();

                var AllNewCreditorOfDept = ViewModel.CreditorsID.Select(c => new Dept_Creditor { CreditorID = c, DeptID = ViewModel.DeptID }).ToArray();
                var AllNewDeptorOfDept = ViewModel.DeptrosID.Select(d => new Dept_Deptor { DeptorID = d, DeptID = ViewModel.DeptID }).ToArray();

                if (AllCreditorOfDept.Count() != 0)
                    UW.BaseRepository<Dept_Creditor>().DeleteRangeEntity(AllCreditorOfDept.ToList());

                if (AllDeptorOfDept.Count() != 0)
                    UW.BaseRepository<Dept_Deptor>().DeleteRangeEntity(AllDeptorOfDept.ToList());

                if (AllNewCreditorOfDept.Count() != 0)
                    await UW.BaseRepository<Dept_Creditor>().CreateRangeEntity(AllNewCreditorOfDept.ToList());

                if (AllNewDeptorOfDept.Count() != 0)
                    await UW.BaseRepository<Dept_Deptor>().CreateRangeEntity(AllNewDeptorOfDept.ToList());

                if (dept.IsPaid != true)
                {
                    //Fix Money Before Edit
                    var Creditors = FillCreditrosOfSpeceficDept(ViewModel.DeptID);
                    var Deptors = FillDeptorsOfSpeceficDept(ViewModel.DeptID);

                    decimal givePriceFromCreditors = dept.Price / Creditors.Count();
                    decimal payPriceToDeptors = dept.Price / Deptors.Count();

                    foreach (var creditor in Creditors) { creditor.GetMoney -= givePriceFromCreditors; }
                    foreach (var deptor in Deptors) { deptor.DeptMoney -= payPriceToDeptors; }

                    //Fix Money After Edit
                    List<Creditor> NewCreditors = new List<Creditor>();

                    foreach (var creditorID in ViewModel.CreditorsID)
                    {
                        var credit = UW.BaseRepository<Creditor>().GetEntityByID(creditorID);
                        NewCreditors.Add(credit);
                    }

                    List<Deptor> NewDeptors = new List<Deptor>();

                    foreach (var deptorID in ViewModel.DeptrosID)
                    {
                        var deptor = UW.BaseRepository<Deptor>().GetEntityByID(deptorID);
                        NewDeptors.Add(deptor);
                    }
                    foreach (var creditor in NewCreditors) { creditor.GetMoney += dept.Price / AllNewCreditorOfDept.Count(); }
                    foreach (var deptor in NewDeptors) { deptor.DeptMoney += dept.Price / AllNewDeptorOfDept.Count(); }
                }

                else
                {
                    var Creditors = FillCreditrosOfSpeceficDept(ViewModel.DeptID);
                    var Deptors = FillDeptorsOfSpeceficDept(ViewModel.DeptID);

                    decimal givePriceFromCreditors = dept.Price / Creditors.Count();
                    decimal payPriceToDeptors = dept.Price / dept.DeptorsCount;

                    foreach (var creditor in Creditors) { creditor.GetMoney -= givePriceFromCreditors; }
                    foreach (var deptor in Deptors) { deptor.DeptMoney -= payPriceToDeptors; }
                }

                await UW.Commit();
                return RedirectToAction("Index");
            }

            else return View();
        }

        //METHOD
        public IEnumerable<Creditor> FillCreditrosOfSpeceficDept(int deptID)
        {
            var creditors = (from dc in UW.Context.Dept_Creditors
                             where dc.DeptID == deptID
                             join c in UW.Context.Creditors on dc.CreditorID equals c.CreditorID
                             select c).ToList();

            return creditors;
        }

        public IEnumerable<Deptor> FillDeptorsOfSpeceficDept(int deptID)
        {
            var deptors = (from dd in UW.Context.Dept_Deptors
                           where dd.DeptID == deptID
                           join d in UW.Context.Deptors on dd.DeptorID equals d.DeptorID
                           select d).ToList();

            return deptors;
        }
    }
}