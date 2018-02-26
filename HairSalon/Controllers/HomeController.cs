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

    // [HttpGet("/stylists/{id}")]
    // public ActionResult Details(int id)
    // {
    //   Console.WriteLine("I'm in Find()");
    //     Stylist newStylist = Stylist.Find(id);
    //     return View(newStylist);
    // }

    [HttpGet("/stylists/{id}/update")]
    public ActionResult UpdateForm(int id)
    {
      Console.WriteLine("I'm in Update() and ID is " + id);
        // Stylist newStylist = Stylist.Find(id);
        // return View("Update", newStylist);
        return View("Update", Stylist.Find(id));
    }

    [HttpGet("/stylists/{id}/details")]
    public ActionResult Details(int id)
    // was going to be StylistDetails but I took out Details() above
    {
      Console.WriteLine("I'm in Details view() and ID is " + id);
        // Stylist newStylist = Stylist.Find(id);
        // return View("Update", newStylist);
        return View(Stylist.Find(id));
    }

    // @client.GetId()

    // [HttpPost("/stylists/{name}/{id}/update")]
    // public ActionResult UpdateFormIsh(string name, int id)
    // {
    //   Console.WriteLine("I'm in UpdateFormIsh() and Name is " + name);
    //     // Stylist newStylist = Stylist.Find(id);
    //     // return View("Update", newStylist);
    //     // return View("Update", Stylist.Find(id));
    //
    //     string tempX = Request.Form["new-name"];
    //         Console.WriteLine("id is " + id + " and the form sent " + tempX + " so...");
    //         Stylist thisStylist = Stylist.Find(id);
    //     //     Console.WriteLine("Edit should do its trick.");
    //         thisStylist.Edit(Request.Form["new-name"], id);
    //         return RedirectToAction("Index", thisStylist);
    //
    //     //
    //     // return View("Index");
    // }

    [HttpPost("/stylists/{id}/update")]
    public ActionResult Update(int id)
    {
        string tempX = Request.Form["new-name"];
        Console.WriteLine("id is " + id + " and the form sent " + tempX + " so...");
        Stylist thisStylist = Stylist.Find(id);
        Console.WriteLine("Edit should do its trick.");
        thisStylist.Update(Request.Form["new-name"], id);
        return RedirectToAction("Index", thisStylist);
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

    [HttpGet("/clients/{stylistId}/new")]
    public ActionResult CreateClientForm(int stylistId)
    {
      Console.WriteLine("I'm in CreateClientForm()");
      Console.WriteLine("Do I know what stylist I came from? " + Stylist.Find(stylistId).GetName());

        // return View("Index");
        // return View(Stylist.Find(stylistId));
        return View(Stylist.Find(stylistId)); // was just passing stylistId
    }

    [HttpPost("/clients/{stylistId}")] // do I have to actively seek this? /clients/{stylistId}
    public ActionResult CreateClient(int stylistId)
    {
      Client newClient = new Client (Request.Form["new-client"], stylistId);
      // Client newClient = new Client (Request.Form["new-client"], someStylist.GetId());
      Console.WriteLine("I'm in CreateClient()");
      Console.WriteLine("Using stylisstsId: " + stylistId);
      Console.WriteLine("The details are: " + Request.Form["new-client"] + ", " + stylistId);
      //
      newClient.Save();
      // List<Client> allClients = Client.GetAll();
      // return RedirectToAction("Index");
      return View("Details", Stylist.Find(stylistId)); // Need to pass back the stylist!
      // return View("Index");
    }

    [HttpGet("clients/{id}/ClientDetails")]
    public ActionResult ClientDetails(int id)
    {
        return View(Client.Find(id));
    }

    [HttpGet("/clients/{clientId}/delete")]
    public ActionResult DeleteClient(int clientId)
    {

        int stylistId = Client.Find(clientId).GetStylistId();
        Client.Delete(clientId);
        // stylists/2/details

        // return RedirectToAction("Index");
        return View("Details", Stylist.Find(stylistId));
    }

    [HttpGet("/clients/{id}/update")]
    public ActionResult UpdateFormClient(int id)
    {
      Console.WriteLine("I'm in UpdateFormClient() and ID is " + id);
        // Stylist newStylist = Stylist.Find(id);
        // return View("Update", newStylist);
        return View("UpdateClient", Client.Find(id));
    }


    [HttpPost("/clients/{id}/update")]
    public ActionResult UpdateClient(int id)
    {
        string tempX = Request.Form["new-name"];
        Console.WriteLine("id is " + id + " and the form sent " + tempX + " so...");
        Client thisClient = Client.Find(id);
        Console.WriteLine("Edit should do its trick.");
        Console.WriteLine("The Stylist for " + thisClient.GetName() + " is " + Stylist.Find(thisClient.GetStylistId()).GetName());
        thisClient.Update(Request.Form["new-name"], thisClient.GetStylistId(), id);
        // return RedirectToAction("Details", Stylist.Find(thisClient.GetStylistId()));
        return View("Details", Stylist.Find(thisClient.GetStylistId()));
    }

// [HttpGet("/deleteAll")]
// public ActionResult ClientDeleteAll()
// {
//   // return View("Index", Stylist.GetString());
//   Stylist.DeleteAll();
//   return View("Index", Stylist.GetAll());
// }
//
// [HttpGet("/deleteClient")]
// public ActionResult DeleteClient()
// {
//   // return View("Index", Stylist.GetString());
//
//   return View("Index", Stylist.GetAll());
// }


  }
}
