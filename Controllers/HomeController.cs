using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace JobSeekerSystem.Controllers;

public class HomeController : Controller
{
    private readonly JobSeeker_DB db;

    public HomeController(JobSeeker_DB db)
    {
        this.db = db;
    }

    // GET: Home/Index
    public IActionResult Index()
    {
        var m = db.Students;

        return View(m);
    }

    // GET: Home/Detail
    public IActionResult Detail(string? id)
    {
        var m = db.Students.Find(id);

        if (m == null)
        {
            return RedirectToAction("Index");
        }

        return View(m);
    }

    // GET: Home/Demo1
    public IActionResult Demo1(string? programId)
    {
        ViewBag.Programs = db.Programs;

        var m = db.Students
                  .Where(s => s.ProgramId == programId ||
                              programId == null);

        // TODO
        if (Request.isAjax())
        {
            return PartialView("_A", m);
        }

        return View(m);
    }

    // GET: Home/Demo2
    public IActionResult Demo2(string? name)
    {
        name = name?.Trim() ?? "";

        var m = db.Students
                  .Where(s => s.Name.Contains(name));

        // TODO
        if (Request.isAjax())
        {
            return PartialView("_A", m);
        }

        return View(m);
    }

    // GET: Home/Demo3
    public IActionResult Demo3(string? sort, string? dir)
    {
        ViewBag.Sort = sort;
        ViewBag.Dir = dir;

        Func<Student, object> fn = sort switch
        {
            "Id" => s => s.Id,
            "Name" => s => s.Name,
            "Gender" => s => s.Gender,
            "Program" => s => s.ProgramId,
            _ => s => s.Id,
        };

        var m = dir == "des" ?
                db.Students.OrderByDescending(fn) :
                db.Students.OrderBy(fn);

        // TODO
        if (Request.isAjax())
        {
            return PartialView("_B", m);
        }

        return View(m);
    }

    // GET: Home/Demo4
    public IActionResult Demo4(int page = 1)
    {
        if (page < 1)
        {
            return RedirectToAction(null, new { page = 1 });
        }

        var m = db.Students.ToPagedList(page, 10);

        if (page > m.PageCount && m.PageCount > 0)
        {
            return RedirectToAction(null, new { page = m.PageCount });
        }

        // TODO
        if (Request.isAjax())
        {
            return PartialView("_C", m);
        }

        return View(m);
    }

    // GET: Home/Demo5
    public IActionResult Demo5(string? name, string? sort, string? dir, int page = 1)
    {
        // (1) Searching ------------------------
        ViewBag.Name = name = name?.Trim() ?? "";

        var searched = db.Students.Where(s => s.Name.Contains(name));

        // (2) Sorting --------------------------
        ViewBag.Sort = sort;
        ViewBag.Dir = dir;

        Func<Student, object> fn = sort switch
        {
            "Id" => s => s.Id,
            "Name" => s => s.Name,
            "Gender" => s => s.Gender,
            "Program" => s => s.ProgramId,
            _ => s => s.Id,
        };

        var sorted = dir == "des" ?
                     searched.OrderByDescending(fn) :
                     searched.OrderBy(fn);

        // (3) Paging ---------------------------
        if (page < 1)
        {
            return RedirectToAction(null, new { name, sort, dir, page = 1 });
        }

        var m = sorted.ToPagedList(page, 10);

        if (page > m.PageCount && m.PageCount > 0)
        {
            return RedirectToAction(null, new { name, sort, dir, page = m.PageCount });
        }

        /*
        if (Request.IsAjax())
        {
            return PartialView("_D", m);
        }
        */

        return View(m);
    }
}
