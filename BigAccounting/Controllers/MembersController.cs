using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.Models;
using DataLayer.Models.UnitOfWork;

namespace BigAccounting.Controllers
{
    public class MembersController : Controller
    {
        private readonly UnitOfWork UW;

        public MembersController(UnitOfWork uw) => UW = uw;

        // GET: Members
        public async Task<IActionResult> Index() => View(await UW.BaseRepository<Member>().GetAllEntitiesAsync());

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) { return NotFound(); }

            var member = await UW.BaseRepository<Member>().GetEntityByIDAsync(id);
            if (member == null) { return NotFound(); }

            return View(member);
        }

        // GET: Members/Create

        public IActionResult Create() => View();

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberID,Name,Mobile")] Member member)
        {
            if (ModelState.IsValid)
            {

                Creditor creditor = new Creditor()
                {
                    Name = member.Name,
                    GetMoney = 0
                };

                await UW.BaseRepository<Creditor>().CreateEntity(creditor);
                await UW.Commit();

                Deptor deptor = new Deptor()
                {
                    Name = member.Name,
                    DeptMoney = 0
                };
                await UW.BaseRepository<Deptor>().CreateEntity(deptor);
                await UW.Commit();

                member.CreditorID = creditor.CreditorID;
                member.DeptorID = deptor.DeptorID;

                await UW.BaseRepository<Member>().CreateEntity(member);

                await UW.Commit();

                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }

            var member = await UW.BaseRepository<Member>().GetEntityByIDAsync(id);
            if (member == null) { return NotFound(); }

            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberID,Name,Mobile,CreditorID,DeptorID")] Member member)
        {
            if (id != member.MemberID) { return NotFound(); }

            if (ModelState.IsValid)
            {
                try
                {
                    var creditor = await UW.BaseRepository<Creditor>().GetEntityByIDAsync(member.CreditorID);
                    creditor.Name = member.Name;

                    var deptor = await UW.BaseRepository<Deptor>().GetEntityByIDAsync(member.DeptorID);
                    deptor.Name = member.Name;

                    UW.BaseRepository<Member>().EditEntity(member);
                    UW.BaseRepository<Creditor>().EditEntity(creditor);
                    UW.BaseRepository<Deptor>().EditEntity(deptor);
                    await UW.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberID)) { return NotFound(); }
                    else { throw; }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }

            var member = await UW.BaseRepository<Member>().GetEntityByIDAsync(id);
            if (member == null) { return NotFound(); }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await UW.BaseRepository<Member>().GetEntityByIDAsync(id);
            var creditor = await UW.BaseRepository<Creditor>().GetEntityByIDAsync(member.CreditorID);
            var deptor = await UW.BaseRepository<Deptor>().GetEntityByIDAsync(member.DeptorID);
            UW.BaseRepository<Member>().DeleteEntity(member);
            UW.BaseRepository<Creditor>().DeleteEntity(creditor);
            UW.BaseRepository<Deptor>().DeleteEntity(deptor);
            await UW.Commit();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id) => UW.Context.Members.Any(e => e.MemberID == id);

    }
}
