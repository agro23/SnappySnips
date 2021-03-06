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

        [HttpGet("/stylists/{id}/details")]
        public ActionResult Details(int id)
        {
          return View(Stylist.Find(id));
        }

        [HttpGet("/stylists/{id}/update")]
        public ActionResult UpdateForm(int id)
        {
            return View("Update", Stylist.Find(id));
        }

        [HttpPost("/stylists/{id}/update")]
        public ActionResult Update(int id)
        {
            string tempX = Request.Form["new-name"];
            Stylist thisStylist = Stylist.Find(id);
            thisStylist.Update(Request.Form["new-name"], id);
            return RedirectToAction("Index", thisStylist);
        }

        [HttpGet("/stylists/{id}/new")]
        public ActionResult ChangeClientStylist (int id)
        {
            Client tempClient = Client.Find(id);
            Stylist tempStylist = Stylist.Find(tempClient.GetStylistId());
            List<Stylist> tempStylists = Stylist.GetAll();
            List<object> model = new List<object>{};
            model.Add(tempClient);
            model.Add(tempStylist);
            model.Add(tempStylists);
            return View(model);
        }

        [HttpGet("/stylists/{clientId}/{stylistId}/addme")]
        public ActionResult ChangeStylist (int clientId, int stylistId)
        {
            Client tempClient = Client.Find(clientId);
            tempClient.ChangeStylist(stylistId);
            Console.WriteLine("But changes to Stylist: " + Stylist.Find(stylistId));
            return View ("Index", Stylist.GetAll());
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

        [HttpGet("/clients/{id}/new/specialty")]
        public ActionResult AddTreatment(int id)
        {
            Client tempClient = Client.Find(id);
            List<Specialty> tempSpecialty = Specialty.GetAll();
            List<object> model = new List<object>{};
            model.Add(tempClient);
            model.Add(tempSpecialty);
            return View("TreatmentsIndex", model);
        }

        [HttpGet("/clients/{id}/deleteAllSpecialties")]
        public ActionResult DeleteAllTreatments(int id)
        {
            Client tempClient = Client.Find(id);
            tempClient.DeleteAllSpecialtiesFromClient(id);
            return View("ClientDetails", tempClient);
        }

        [HttpGet("/clients/{id}/deleteTreatmentFromClient")]
        public ActionResult DeleteClientTreatment(int id)
        {
            Client tempClient = Client.Find(id);
            Specialty.DeleteSpecialtyFromClient(id);
            return View("ClientDetails", tempClient);
        }

        [HttpGet("/clients/viewAll")]
        public ActionResult ClientsIndex()
        {
            return View(Client.GetAll());
        }

        [HttpGet("/specialties/new")]
        public ActionResult CreateSpecialtyForm()
        {
            return View();
        }

        [HttpPost("/specialties")]
        public ActionResult CreateSpecialty()
        {
            Specialty newSpecialty = new Specialty (Request.Form["new-specialty"]);
            newSpecialty.Save();
            List<Specialty> allSpecialties = Specialty.GetAll();
            return RedirectToAction("SpecialtiesIndex", allSpecialties);
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
            newSpecialty.Save();
            return View("Details", Stylist.Find(stylistId));
        }


        [HttpGet("/specialties/viewAll")]
        public ActionResult SpecialtiesIndex()
        {
            return View(Specialty.GetAll());
        }

        [HttpGet("/specialties/{id}/view")]
        public ActionResult StylistDetails(int id)
        {
            return View("Details", Stylist.Find(id));
        }

        [HttpGet("/specialties/{id}/update")]
        public ActionResult UpdateSpecialtyForm(int id)
        {
            return View(Specialty.Find(id));
        }

        [HttpPost("/specialties/{id}/update")]
        public ActionResult UpdateSpecialty(int id)
        {
            string tempX1 = Request.Form["new-specialty"];
            Specialty thisSpecialty = Specialty.Find(id);
            thisSpecialty.Update(Request.Form["new-specialty"], id);
            return RedirectToAction("SpecialtiesIndex", thisSpecialty);
        }

        [HttpGet("/specialties/{id}/details")]
        public ActionResult SpecialtyDetails(int id)
        {
            return View(Specialty.Find(id));
        }

        [HttpGet("/specialties/{id}/delete")]
        public ActionResult DeleteSpecialty(int id)
        {
            Specialty.Delete(id);
            return View("SpecialtiesIndex", Specialty.GetAll());
        }

        [HttpGet("/specialties/deleteAll")]
        public ActionResult SpecialtyDetails()
        {
            Specialty.DeleteAll();
            return View("SpecialtiesIndex", Specialty.GetAll());
        }

        [HttpGet("/specialties/{id}/new/stylist")]
        public ActionResult AddSpecialtyToStylist(int id)
        {
            List<object> model = new List<object>{};
            List<Stylist> tempStylist = new List<Stylist>{};
            tempStylist.Add(Stylist.Find(id));
            model.Add(tempStylist); // was id
            List<Specialty> specialties = new List<Specialty>{};
            specialties = Specialty.GetAll();
            model.Add(specialties);
            return View("UpdateStylistSpecialty", model);
        }

        [HttpGet("/stylists/{specialtyId}/{stylistId}/add")]
        public ActionResult AddToStylist(int specialtyId, int stylistId)
        {
            Stylist tempStylist = Stylist.Find(stylistId);
            tempStylist.AddSpecialtyToStylist(Specialty.Find(specialtyId));
            return View ("Details", tempStylist);
        }

        [HttpGet("/stylist/{id}/new/specialty")]
        public ActionResult AddStylistToSpecialty(int id)
        {
            List<object> model = new List<object>{};
            List<Specialty> tempSpecialty = new List<Specialty>{};
            tempSpecialty.Add(Specialty.Find(id));
            model.Add(tempSpecialty); // was id
            List<Stylist> stylists = new List<Stylist>{};
            stylists = Stylist.GetAll();
            model.Add(stylists);
            return View("UpdateSpecialtyStylist", model);
        }

        [HttpGet("/stylists/{specialtyId}/{stylistId}/addtome")]
        public ActionResult AddToSpecialty(int specialtyId, int stylistId)
        {
            Stylist tempStylist = Stylist.Find(stylistId);
            tempStylist.AddStylistToSpecialty(Specialty.Find(specialtyId), stylistId);
            return View ("Details", tempStylist);
        }

        [HttpGet("/specialties/{id}/new/client")]
        public ActionResult AddSpecialtyToClient(int id)
        {
            List<object> model = new List<object>{};
            List<Client> tempClient = new List<Client>{};
            tempClient.Add(Client.Find(id));
            model.Add(tempClient); // was id
            List<Specialty> specialties = new List<Specialty>{};
            specialties = Specialty.GetAll();
            model.Add(specialties);
            return View("UpdateClientTreatment", model);
        }

        [HttpGet("/treatments/{clientId}/{specialtyId}/addit")]
        public ActionResult AddToClient(int specialtyId, int clientId)
        {
            Client tempClient = Client.Find(clientId);
            tempClient.AddSpecialtyToClient(Specialty.Find(specialtyId));
            return View ("ClientDetails", tempClient);
        }
        
    }
}
