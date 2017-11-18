using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConcertTicketsWeb.Models;
using Microsoft.AspNet.Identity;

namespace ConcertTicketsWeb.Controllers
{
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.Concert).Include(r => r.Seat);
            return View(reservations.ToList());
        }

        public ActionResult Reserve(int? seat_id, int? concert_id)
        {
            if (seat_id == null || concert_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (db.Reservations.Count(r => r.SeatID == seat_id && r.ConcertID == concert_id) > 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Seat seat = db.Seats.Find(seat_id);
            Concert concert = db.Concerts.Find(concert_id);

            if (seat == null || concert == null)
            {
                return HttpNotFound();
            }

            Reservation reservation = new Reservation { Seat = seat, Concert = concert, status = "CREATED" };

            if (User.Identity.IsAuthenticated)
            {
                reservation.User = db.Users.Find(User.Identity.GetUserId());
                reservation.UserID = User.Identity.GetUserId();
            }

            db.Reservations.Add(reservation);
            db.SaveChanges();

            ViewBag.reservation = reservation;

            return View();
        }

        public ActionResult MyReservations()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            string UserId = User.Identity.GetUserId();

            var Reservations = db.Reservations.Where(r => r.UserID == UserId).Include(r => r.Concert).Include(r => r.Seat);
            
            return View(Reservations.ToList());
        }
        
        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.ConcertID = new SelectList(db.Concerts, "ID", "Name");
            ViewBag.SeatID = new SelectList(db.Seats, "ID", "ID");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SeatID,ConcertID")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConcertID = new SelectList(db.Concerts, "ID", "Name", reservation.ConcertID);
            ViewBag.SeatID = new SelectList(db.Seats, "ID", "ID", reservation.SeatID);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConcertID = new SelectList(db.Concerts, "ID", "Name", reservation.ConcertID);
            ViewBag.SeatID = new SelectList(db.Seats, "ID", "ID", reservation.SeatID);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SeatID,ConcertID")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConcertID = new SelectList(db.Concerts, "ID", "Name", reservation.ConcertID);
            ViewBag.SeatID = new SelectList(db.Seats, "ID", "ID", reservation.SeatID);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Cancel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("MyReservations");
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
