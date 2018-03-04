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
          return View("Index", Stylist.GetAll());
        }

        [HttpGet("/stylists/new")]
        public ActionResult CreateForm()
        {
            return View();
        }

        [HttpPost("/stylists")]
        public ActionResult Create()
        {
          Stylist newStylist = new Stylist (Request.Form["new-stylist"]);
          newStylist.Save();
          List<Stylist> allStylists = Stylist.GetAll();
          return View("Index", allStylists);
        }
        [HttpGet("/stylists/{id}/update")]
        public ActionResult UpdateForm(int id)
        {
            return View("Update", Stylist.Find(id));
        }

        [HttpGet("/stylists/{id}/details")]
        public ActionResult Details(int id)
        {
            return View(Stylist.Find(id));
        }

        [HttpPost("/stylists/{id}/update")]
        public ActionResult Update(int id)
        {
            string tempX = Request.Form["new-name"];
            Stylist thisStylist = Stylist.Find(id);
            thisStylist.Update(Request.Form["new-name"], id);
            return RedirectToAction("Index", thisStylist);
        }

        [HttpGet("/stylists/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Stylist.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet("/deleteAll")]
        public ActionResult DeleteAll()
        {
            Stylist.DeleteAll();
            return View("Index", Stylist.GetAll());
        }

        [HttpGet("/deleteStylist")]
        public ActionResult DeleteStylist()
        {
            return View("Index", Stylist.GetAll());
        }

        [HttpGet("/deleteAllClients/{stylistId}")]
        public ActionResult DeleteAllClientsFromStylist(int stylistId)
        {
            Stylist.DeleteAllClientsFromStylist(stylistId);
            return View("Index", Stylist.GetAll());
        }

        [HttpGet("/clients/{stylistId}/new")]
        public ActionResult CreateClientForm(int stylistId)
        {
            return View(Stylist.Find(stylistId));
        }

        [HttpPost("/clients/{stylistId}")]
        public ActionResult CreateClient(int stylistId)
        {
            Client newClient = new Client (Request.Form["new-client"], stylistId);
            newClient.Save();
            return View("Details", Stylist.Find(stylistId));
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
            return View("Details", Stylist.Find(stylistId));
        }

        [HttpGet("/clients/{id}/update")]
        public ActionResult UpdateFormClient(int id)
        {
            return View("UpdateClient", Client.Find(id));
        }

        [HttpPost("/clients/{id}/update")]
        public ActionResult UpdateClient(int id)
        {
            string tempX = Request.Form["new-name"];
            Client thisClient = Client.Find(id);
            thisClient.Update(Request.Form["new-name"], thisClient.GetStylistId(), id);
            return View("Details", Stylist.Find(thisClient.GetStylistId()));
        }

        [HttpGet("/specialties/new")]
        public ActionResult CreateSpecialtyForm()
        // was CreateForm()
        {
            return View();
        }

        [HttpPost("/specialties")]
        public ActionResult CreateSpecialty()
        //Was Create()
        {
          Specialty newSpecialty = new Specialty (Request.Form["new-specialty"]);
          newSpecialty.Save();
          List<Specialty> allSpecialties = Specialty.GetAll();
          // return View("Index", allSpecialties);
          // return View("Index"); // maybe send it to a list of all specialties
          return RedirectToAction("Index");

        }

        [HttpGet("/specialties/{stylistId}/new")]
        public ActionResult CreateStylistSpecialtyForm(int stylistId)
        {
            return View(Stylist.Find(stylistId));
        }

        [HttpPost("/specialties/{stylistId}")]
        public ActionResult CreateStylistSpecialty(int stylistId)
        {
            Specialty newSpecialty = new Specialty (Request.Form["new-specialty"]);
            Console.WriteLine("new Specialty is " + newSpecialty.GetName());
            newSpecialty.Save();
            return View("Details", Stylist.Find(stylistId));
        }







        [HttpGet("/specialties/{id}/update")]
        public ActionResult UpdateSpecialtyForm(int id)
        {
            return View(Specialty.Find(id));
        }

        [HttpGet("/specialties/{id}/details")]
        public ActionResult SpecialtyDetails(int id)
        {
            return View("Specialty", Specialty.Find(id));
        }

    }
}
