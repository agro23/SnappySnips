using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
      // return View("Index", Stylist.GetString());
      return View("Index", Stylist.GetAll());
    }

    [HttpGet("/stylists/new")]
    public ActionResult CreateForm()
    {
      Console.WriteLine("I'm in CreateForm()");
        // return View("Index");
        return View();
    }

    [HttpPost("/stylists")]
    public ActionResult Create()
    {
      Stylist newStylist = new Stylist (Request.Form["new-stylist"]);
      Console.WriteLine("I'm in Create()");
      Console.WriteLine("The details are: " + Request.Form["new-stylist"] );
      //
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      // return RedirectToAction("Index");
      return View("Index", allStylists);
      // return View();
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult Details(int id)
    {
      Console.WriteLine("I'm in Find()");
        Stylist newStylist = Stylist.Find(id);
        return View(newStylist);
    }

    [HttpGet("/stylists/{id}/update")]
    public ActionResult UpdateForm(int id)
    {
      Console.WriteLine("I'm in Update() and ID is " + id);
        Stylist newStylist = Stylist.Find(id);
        return View("Update", newStylist);
        // return View();
    }

    [HttpPost("/stylists/{id}/update")]
    public ActionResult Update(int id)
    {
        Stylist thisStylist = Stylist.Find(id);
        thisStylist.Edit(Request.Form["new-name"]);
        return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{id}/delete")]
    public ActionResult Delete(int id)
    {
        // Console.WriteLine("Inside the Get for Delete I have ID: " + id);
        // // Stylist thisStylist = Stylist.Find(id);
        // // Stylist.Delete(thisStylist.GetId());
        // Console.WriteLine("Let's make ID into " + Stylist.Find(id).GetId() );
        // Console.WriteLine("Nevermind. Let's just go with " + id );
        //
        // // Stylist.Delete(Stylist.Find(id).GetId());
        Stylist.Delete(id);

        return RedirectToAction("Index");
    }

    [HttpGet("/deleteAll")]
    public ActionResult DeleteAll()
    {
      // return View("Index", Stylist.GetString());
      Stylist.DeleteAll();
      return View("Index", Stylist.GetAll());
    }

    [HttpGet("/deleteStylist")]
    public ActionResult DeleteStylist()
    {
      // return View("Index", Stylist.GetString());

      return View("Index", Stylist.GetAll());
    }


  }
}
