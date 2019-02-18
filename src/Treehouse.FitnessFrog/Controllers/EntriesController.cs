﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Treehouse.FitnessFrog.Data;
using Treehouse.FitnessFrog.Models;

namespace Treehouse.FitnessFrog.Controllers
{
    public class EntriesController : Controller
    {
        private EntriesRepository _entriesRepository = null;

        public EntriesController()
        {
            _entriesRepository = new EntriesRepository();
        }

        public ActionResult Index()
        {
            List<Entry> entries = _entriesRepository.GetEntries();

            // Calculate the total activity.
            double totalActivity = entries
                .Where(e => e.Exclude == false)
                .Sum(e => e.Duration);

            // Determine the number of days that have entries.
            int numberOfActiveDays = entries
                .Select(e => e.Date)
                .Distinct()
                .Count();

            ViewBag.TotalActivity = totalActivity;
            ViewBag.AverageDailyActivity = (totalActivity / (double)numberOfActiveDays);

            return View(entries);
        }

        public ActionResult Add()
        {
            var entry = new Entry()
            {
                Date = DateTime.Today,
                ActivityId = 2,
            };
            ViewBag.ActivitiesSelectListItems = new SelectList(
                Data.Data.Activities, "Id", "Name");

            return View(entry);
        }

        //[ActionName("Add")]
        //[HttpPost] 
        // We can put them into the same line to keep code clean
        //[ActionName("Add"), HttpPost]
        [HttpPost]
        //public ActionResult Add(DateTime? date, int? activityId, double? duration, Entry.IntensityLevel? intensity, bool? exclude, string notes)
        //{
        public ActionResult Add(Entry entry)
        {
            // We can delete the following code because we are using html helper method to render our form field input or text area elements
            // internally use ModelState to get the user's attempted values.
            //ViewBag.Date = ModelState["Date"].Value.AttemptedValue;
            //ViewBag.ActivityId = ModelState["ActivityId"].Value.AttemptedValue;
            //ViewBag.Duration = ModelState["Duration"].Value.AttemptedValue;
            //ViewBag.Intensity = ModelState["Intensity"].Value.AttemptedValue;
            //ViewBag.Exclude = ModelState["Exclude"].Value.AttemptedValue;
            //ViewBag.Notes = ModelState["Notes"].Value.AttemptedValue;

            if (ModelState.IsValid)
            {
                _entriesRepository.AddEntry(entry);

                // TODO Display the Entries list page
                return RedirectToAction("Index");

            }
            ViewBag.ActivitiesSelectListItems = new SelectList(
    Data.Data.Activities, "Id", "Name");

            entry.ActivityId = 2;

            return View(entry);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }
    }
}