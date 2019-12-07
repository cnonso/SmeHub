using SME_Hub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SME_Hub.Controllers
{
    public class HomeController : Controller
    {
        private SmeHubEntities db = new SmeHubEntities();

        private IKeyWordRepository _repository;

        public HomeController() : this(new KeyWordRepository())
        {
        }

        public HomeController(IKeyWordRepository repository)
        {
            _repository = repository;
        }
        public class BusinessSummaries
        {
            public int selectedId { get; set; }

            public System.Web.Mvc.SelectList businessSummaries;
        }
        public ActionResult Index()
        {
            ////Get the value from database and then set it to ViewBag to pass it View
            //IEnumerable<SelectListItem> KeyWord = db.BusinessSummaries.OrderByDescending(c => c.Id).Select(c => new SelectListItem
            //{
            //    Value = c.Title,
            //    Text = c.Title
            //});

            //ViewBag.KeyWord = KeyWord;
            //return View();

            KeyWordModel model = new KeyWordModel();
            model.AvailableKeyWords.Add(new SelectListItem { Text = "I'm looking for a ...", Value = "Selects items" });
            var keywords = _repository.GetAllBusinessSummaries();
            foreach (var keyword in keywords)
            {
                model.AvailableKeyWords.Add(new SelectListItem()
                {
                    Text = keyword.Title,
                    Value = keyword.Id.ToString(),
                });
            };

            model.AvailablePlaces.Add(new SelectListItem { Text = "at ...", Value = "Select place" });
            var places = _repository.GetAllPlaces();
            foreach (var place in places)
            {
                model.AvailablePlaces.Add(new SelectListItem()
                {
                    Text = place.Name,
                    Value = place.Id.ToString()
                });
            };
            return View(model);
        }


        //[HttpPost]
        //public JsonResult Business_Summaries(string prefixText)
        //{
        //    var summary = from x in db.BusinessSummaries
        //                  where x.Title.StartsWith(prefixText)
        //                  select new
        //                  {
        //                      name = x.Title
        //                  };
        //    var result = Json(summary.Take(5).ToList(), JsonRequestBehavior.AllowGet);
        //    return result;
        //}

        [HttpPost]
        public JsonResult Business_Summaries(string term)
        {
            var summary = from x in db.BusinessSummaries
                          where x.Title.StartsWith(term)
                          select new
                          {
                              name = x.Title
                          };
            var result = Json(summary.Take(5).ToList(), JsonRequestBehavior.AllowGet);
            return result;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}