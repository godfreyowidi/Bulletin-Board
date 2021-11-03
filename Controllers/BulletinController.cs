using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HanmakTechnologies.BulletinBoard.Models;
using System.Linq;

namespace HanmakTechnologies.BulletinBoard.Controllers
{
  public class BulletinController : Controller
  {

    DataAccessLayer bulletinObj = new DataAccessLayer();

    // GET: /controller/

    public IActionResult Index()
    {
      List<Bulletin> bulletinList = new List<Bulletin>();
      bulletinList = bulletinObj.GetAllBulletin().ToList();

      return View(bulletinList);
    }

    [HttpGet]
    public IActionResult Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      Bulletin bulletin = bulletinObj.GetBulletinData(id);

      if (bulletin == null)
      {
        return NotFound();
      }
      return View(bulletin);
    }

    [HttpGet]
    public IActionResult Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      Bulletin bulletin = bulletinObj.GetBulletinData(id);

      if (bulletin == null)
      {
        return NotFound();
      }
      return View(bulletin);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int? id)
    {
      bulletinObj.DeleteBulletin(id);
      return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      Bulletin bulletin = bulletinObj.GetBulletinData(id);

      if (bulletin == null)
      {
        return NotFound();
      }
      return View(bulletin);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind]Bulletin bulletin)
    {
      if (id != bulletin.Id)
      {
        return NotFound();
      }
      if (ModelState.IsValid)
      {
        bulletinObj.EditBulletin(bulletin);
        return RedirectToAction("Index");
      }
      return View(bulletin);
    }

    [HttpGet]
    public IActionResult Create()
    {
      return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind] Bulletin bulletin)
    {
      if (ModelState.IsValid)
      {
        bulletinObj.AddBulletin(bulletin);
        return RedirectToAction("Index");
      }
      return View(bulletin);
    }
  }
}
