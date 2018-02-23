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
