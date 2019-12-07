using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SME_Hub.Models;
using Microsoft.AspNet.Identity;
using System.IO;

namespace SME_Hub.Controllers
{
    public class DefaultController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private SmeHubEntities dbe = new SmeHubEntities();
        const int pageSize = 5;

        private IKeyWordRepository _repository;

        public DefaultController()
            : this(new KeyWordRepository())
        {
        }

        public DefaultController(IKeyWordRepository repository)
        {
            _repository = repository;
        }


        // GET: /Default/
        public ActionResult Index(int page = 1)
        {
            //Get the value from database and then set it to ViewBag to pass it View
            IEnumerable<SelectListItem> KeyWord = dbe.BusinessSummaries.OrderBy(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Title,
                Text = c.Title
            });
            ViewBag.KeyWord = KeyWord;

            //Get the value from database and then set it to ViewBag to pass it View
            IEnumerable<SelectListItem> Location = dbe.Places.OrderBy(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name
            });
            ViewBag.Location = Location;

            var result = db.MyAccounts.OrderBy(c => c.Id)
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList(); ;

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = Math.Ceiling((Double)db.MyAccounts.Count() / pageSize);

            return View(result);
        }

        [ActionName("Profile")]
        public ActionResult ProfileAndMessages()
        {
            int id;
            var userId = User.Identity.GetUserId();
            id = db.MyAccounts.Where(c => c.ApplicationUserId == userId).First().Id;

            var model = new MultiModels
            {
                MyAccount = db.MyAccounts.Find(id),
                Messages = dbe.Messages.Where(l => l.ApplicationUserId == userId)
                                    .OrderBy(c => Guid.NewGuid())
                                    .Take(5).ToList(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(MultiModels model)
        {
            var url = Session["url"].ToString();
            //var validImageTypes = new string[]
            //{
            //    "image/gif",
            //    "image/jpeg",
            //    "image/pjpeg",
            //    "image/png"
            //};

            //if (!validImageTypes.Contains(model.Icon.ContentType))
            //{
            //    ModelState.AddModelError("Icon", "Please choose either a GIF, JPG or PNG image.");
            //}

            if (ModelState.IsValid)
            {
                var comment = new Comment
                {
                    Name = model.CommentsViewModel.Name,
                    Content = model.CommentsViewModel.Content,
                    ServiceId = Convert.ToInt32(Session["ID"].ToString()),
                    Date = DateTime.Now,
                };


                //if (model.Icon.ContentLength > 0)
                //{
                //    var uploadDir = "/CommentIcons";
                //    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.Name + model.Icon.FileName);
                //    var imageUrl = Path.Combine(uploadDir, model.Name + model.Icon.FileName);
                //    model.Icon.SaveAs(imagePath);
                //    comment.Icon = imageUrl;
                //}
                dbe.Comments.Add(comment);
                dbe.SaveChanges();

                //return Redirect(url);
                return Redirect(url);

            }

            // If we got this far, something failed, redisplay form
            return View(model);

        }

        public ActionResult SendMessage()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMessage(string Name, string Email, string Topic, string Content, string ApplicationUserId)
        {
            if (ModelState.IsValid)
            {
                Message message = new Message();

                message.Name = Name;
                message.Email = Email;
                message.Topic = Topic;
                message.Content = Content;
                message.Date = DateTime.Now;
                message.ApplicationUserId = ApplicationUserId;

                dbe.Messages.Add(message);
                dbe.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        public ActionResult SimpleSearch()
        {
            //Get the value from database and then set it to ViewBag to pass it View
            IEnumerable<SelectListItem> KeyWord = dbe.BusinessSummaries.OrderBy(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Title,
                Text = c.Title
            });
            ViewBag.KeyWord = KeyWord;

            //Get the value from database and then set it to ViewBag to pass it View
            IEnumerable<SelectListItem> Location = dbe.Places.OrderBy(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name
            });
            ViewBag.Location = Location;

            return View();
        }

        [HttpPost]
        public ActionResult SimpleSearch(KeyWordModel model, int page = 1)
        {
            //Get the value from database and then set it to ViewBag to pass it View
            IEnumerable<SelectListItem> KeyWord = dbe.BusinessSummaries.OrderBy(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Title,
                Text = c.Title
            });
            ViewBag.KeyWord = KeyWord;

            //Get the value from database and then set it to ViewBag to pass it View
            IEnumerable<SelectListItem> Location = dbe.Places.OrderBy(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name
            });
            ViewBag.Location = Location;

            ViewBag.Page = "Search Result";
            string location = GetPlaceNameById(model.PlaceId);
            string keyword = GetKeywordById(model.KeyWordId);
            var result = db.MyAccounts.Where(c => c.CompanyName.Contains(keyword)
                            | c.CompanyName.Contains(keyword)
                            | c.Description.Contains(keyword)
                            | c.DescriptionSummary.Contains(keyword)
                            | c.Website.Contains(keyword))
                            .Where(c => c.Address.Contains(location));

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = Math.Ceiling((Double)result.Count() / pageSize);

            return View(result.OrderBy(c => c.Id)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList());
        }

        //Gets the name value of PlaceId.
        protected string GetPlaceNameById(int pid)
        {
            string placeName = "";
            var places = _repository.GetAllPlaces();
            foreach (var place in places)
            {
                if (pid == place.Id)
                {
                    placeName = place.Name;
                    break;
                }
            }
            return placeName;
        }

        //Gets the name value of PlaceId.
        protected string GetKeywordById(int pid)
        {
            string keyword = "";
            var summaries = _repository.GetAllBusinessSummaries();
            foreach (var keywrd in summaries)
            {
                if (pid == keywrd.Id)
                {
                    keyword = keywrd.Title;
                    break;
                }
            }
            return keyword;
        }

        public ActionResult PhamaceuticalSearch()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PhamaceuticalSearch(MyAccount myaccount, int page = 1)
        {
            var result = db.MyAccounts.Where(c => c.CompanyName.Contains("Phamaceutical")
                | c.CompanyName.Contains("Phamaceutical")
                | c.Description.Contains("Phamaceutical")
                | c.Website.Contains("Phamaceutical"))
                .ToList();

            //ViewBag.CurrentPage = page;
            //ViewBag.PageSize = pageSize;
            //ViewBag.TotalPages = Math.Ceiling((Double)db.Estates
            //                        .Where(c => c.StateId == selectedState)
            //                        .Where(c => c.CityId == selectedCity).Count() / pageSize);
            return View(result);
        }

        public ActionResult ComplexSearch()
        {
            //Get the value from database and then set it to ViewBag to pass it View
            IEnumerable<SelectListItem> KeyWord = dbe.BusinessSummaries.OrderBy(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Title,
                Text = c.Title
            });
            ViewBag.KeyWord = KeyWord;

            //Get the value from database and then set it to ViewBag to pass it View
            IEnumerable<SelectListItem> Location = dbe.Places.OrderBy(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name
            });
            ViewBag.Location = Location;

            return View();
        }

        [HttpPost]
        public ActionResult ComplexSearch(MyAccount myAccount, string sScale, string mScale, string lScale, int page = 1)
        {
            //Get the value from database and then set it to ViewBag to pass it View
            IEnumerable<SelectListItem> KeyWord = dbe.BusinessSummaries.OrderBy(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Title,
                Text = c.Title
            });
            ViewBag.KeyWord = KeyWord;

            //Get the value from database and then set it to ViewBag to pass it View
            IEnumerable<SelectListItem> Location = dbe.Places.OrderBy(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name
            });
            ViewBag.Location = Location;

            string keyword = Request.Form["KeyWord"].ToString();
            string location = Request.Form["Location"].ToString();

            var result = db.MyAccounts.Where(c => c.CompanyName.Contains(keyword)
                || c.CompanyName.Contains(keyword)
                || c.Description.Contains(keyword)
                || c.DescriptionSummary.Contains(keyword)
                || c.Website.Contains(keyword))
                .Where(c => c.Category.Equals(sScale) | c.Category.Equals(mScale) | c.Category.Equals(lScale))
                .Where(c => c.Address.Contains(location));

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = Math.Ceiling((Double)result.Count() / pageSize);

            return View(result.OrderBy(c => c.Id)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList());
        }


        [Authorize]
        public ActionResult EditAccount()
        {
            int id;
            var userId = User.Identity.GetUserId();
            id = db.MyAccounts.Where(c => c.ApplicationUserId == userId).First().Id;
            var myAccount = db.MyAccounts.Find(id);
            if (myAccount == null)
            {
                return new HttpNotFoundResult();
            }

            var model = new CompanyProfileViewModel
            {
                CompanyName = myAccount.CompanyName,
                Website = myAccount.Website,
                PhoneNo = myAccount.PhoneNo,
                Category = myAccount.Category,
                Address = myAccount.Address,
                Description = myAccount.Description,
                CompanyOwner = myAccount.CompanyOwner,
                PersonalPhoneNo = myAccount.PersonalPhoneNo,
                PersonalEmail = myAccount.PersonalEmail,
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccount(CompanyProfileViewModel model, string category, float CoordinateLat, float CoordinateLong)
        {
            int id;
            var userId = User.Identity.GetUserId();
            id = db.MyAccounts.Where(c => c.ApplicationUserId == userId).First().Id;

            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (model.LogoImage == null || model.LogoImage.ContentLength == 0)
            {
                ModelState.AddModelError("LogoImage", "This field is required");
            }

            else if (!validImageTypes.Contains(model.LogoImage.ContentType))
            {
                ModelState.AddModelError("LogoImage", "Please choose either a GIF, JPG or PNG image.");
            }

            if (ModelState.IsValid)
            {
                var myAccount = db.MyAccounts.Find(id);
                if (myAccount == null)
                {
                    return new HttpNotFoundResult();
                }

                myAccount.CompanyName = model.CompanyName;
                myAccount.Website = model.Website;
                myAccount.PhoneNo = model.PhoneNo;
                myAccount.Description = model.Description;
                myAccount.Category = category;
                myAccount.Address = model.Address;
                myAccount.CompanyOwner = model.CompanyOwner;
                myAccount.PersonalPhoneNo = model.PersonalPhoneNo;
                myAccount.PersonalEmail = model.PersonalEmail;
                myAccount.CoordinateLong = CoordinateLong;
                myAccount.CoordinateLat = CoordinateLat;

                if (model.LogoImage != null && model.LogoImage.ContentLength > 0)
                {
                    var uploadDir = "/Logos";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.CompanyName + model.LogoImage.FileName);
                    var imageUrl = Path.Combine(uploadDir, model.CompanyName + model.LogoImage.FileName);
                    model.LogoImage.SaveAs(imagePath);
                    myAccount.LogoUrl = imageUrl;
                }

                db.Entry(myAccount).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = "Profile Updated";
                return View();
            }
            ViewBag.Message = "Profile Failed to Update";
            return View(model);
        }


        [Authorize]
        public ActionResult MyAccount()
        {
            //Get the value from database and then set it to ViewBag to pass it View
            IEnumerable<SelectListItem> KeyWord = dbe.BusinessSummaries.OrderBy(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Title,
                Text = c.Title
            });
            ViewBag.KeyWord = KeyWord;


            int id;
            var userId = User.Identity.GetUserId();
            id = db.MyAccounts.Where(c => c.ApplicationUserId == userId).First().Id;

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            MyAccount myAccount = db.MyAccounts.Find(id);
            Session["ID"] = id;

            //MyAccount myaccount = db.MyAccounts.Find(id);
            if (myAccount == null)
            {
                return HttpNotFound();
            }

            MultiModels model = new MultiModels
            {
                MyAccount = db.MyAccounts.Find(id),
                CommentsForService = dbe.Comments.Where(l => l.ServiceId == myAccount.Id)
                                        .OrderBy(c => c.Date)
                                        .ToList(),
            };
            Session["url"] = Request.Url.AbsolutePath;
            return View(model);

        }

        // POST: /Default/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(string CompanyName, string Website, string PhoneNo, string Description, string Address, string Category, float CoordinateLong, float CoordinateLat, string DescriptionSummary, HttpPostedFileBase LogoImage)
        {
            Session["Message"] = String.Empty;
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            int id = Convert.ToInt32(Session["ID"].ToString());
            var url = Session["url"].ToString();

            //MyAccount myAccount = new MyAccount();
            var myaccount = db.MyAccounts.Where(sc => sc.Id == id).FirstOrDefault();

            myaccount.CompanyName = CompanyName;
            myaccount.Website = Website;
            myaccount.PhoneNo = PhoneNo;
            myaccount.Description = Description;
            myaccount.Address = Address;
            myaccount.Category = Category;
            myaccount.CoordinateLong = CoordinateLong;
            myaccount.CoordinateLat = CoordinateLat;
            myaccount.DescriptionSummary = Request.Form["KeyWord"].ToString();

            if (LogoImage != null)
            {
                if (LogoImage.ContentLength > 0)
                {
                    var uploadDir = "/Logos";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), CompanyName + LogoImage.FileName);
                    var imageUrl = Path.Combine(uploadDir, CompanyName + LogoImage.FileName);
                    LogoImage.SaveAs(imagePath);
                    myaccount.LogoUrl = imageUrl;
                }
            }
            db.Entry(myaccount).State = EntityState.Modified;
            db.SaveChanges();

            Session["Message"] = "Update Successful!";
            return RedirectToAction("MyAccount");
        }


        // GET: /Default/Details/5        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyAccount myAccount = db.MyAccounts.Find(id);
            Session["ID"] = id;

            //MyAccount myaccount = db.MyAccounts.Find(id);
            if (myAccount == null)
            {
                return HttpNotFound();
            }

            MultiModels model = new MultiModels
            {
                MyAccount = db.MyAccounts.Find(id),
                RelatedArtisans = db.MyAccounts.Where(l => l.DescriptionSummary == myAccount.DescriptionSummary)
                                        .OrderBy(c => Guid.NewGuid())
                                        .Take(5).ToList(),

                CommentsForService = dbe.Comments.Where(l => l.ServiceId == myAccount.Id)
                                        .OrderBy(c => c.Date)
                                        .ToList(),
            };
            Session["url"] = Request.Url.AbsolutePath;
            return View(model);
        }

        public JsonResult SendRating(string r, string s, string id, string url)
        {
            int serviceId = 0;
            Int16 thisVote = 0;
            Int16 sectionId = 0;
            Int16.TryParse(s, out sectionId);
            Int16.TryParse(r, out thisVote);
            int.TryParse(id, out serviceId);

            if (!User.Identity.IsAuthenticated)
            {
                return Json("<small>Not authenticated!</small>");
            }

            if (serviceId.Equals(0))
            {
                return Json("<small>Sorry, record to vote doesn't exists</small>");
            }

            switch (s)
            {
                case "1": // phones rating
                    // check if he has already voted
                    var isIt = dbe.VoteLogs.Where(v => v.SectionId == sectionId &&
                        v.Username.Equals(User.Identity.Name, StringComparison.CurrentCultureIgnoreCase) && v.VoteForId == serviceId).FirstOrDefault();
                    if (isIt != null)
                    {
                        // keep the product voting flag to stop voting by this member
                        HttpCookie cookie = new HttpCookie(url, "true");
                        Response.Cookies.Add(cookie);
                        return Json("<small>You have already rated this post, thanks !</small>");
                    }

                    var myaccount = db.MyAccounts.Where(sc => sc.Id == serviceId).FirstOrDefault();
                    var myAccountClicked = db.MyAccounts.Where(sc => sc.Id == serviceId).FirstOrDefault();

                    if (myaccount != null)
                    {
                        object obj = myaccount.Votes;

                        string updatedVotes = string.Empty;
                        string[] votes = null;
                        if (obj != null && obj.ToString().Length > 0)
                        {
                            string currentVotes = obj.ToString(); // votes pattern will be 0,0,0,0,0
                            votes = currentVotes.Split(',');
                            // if proper vote data is there in the database
                            if (votes.Length.Equals(5))
                            {
                                // get the current number of vote count of the selected vote, always say -1 than the current vote in the array 
                                int currentNumberOfVote = int.Parse(votes[thisVote - 1]);
                                // increase 1 for this vote
                                currentNumberOfVote++;
                                // set the updated value into the selected votes
                                votes[thisVote - 1] = currentNumberOfVote.ToString();
                            }
                            else
                            {
                                votes = new string[] { "0", "0", "0", "0", "0" };
                                votes[thisVote - 1] = "1";
                            }
                        }
                        else
                        {
                            votes = new string[] { "0", "0", "0", "0", "0" };
                            votes[thisVote - 1] = "1";
                        }

                        // concatenate all arrays now
                        foreach (string ss in votes)
                        {
                            updatedVotes += ss + ",";
                        }
                        updatedVotes = updatedVotes.Substring(0, updatedVotes.Length - 1);

                        db.Entry(myaccount).State = EntityState.Modified;
                        myaccount.Votes = updatedVotes;
                        db.SaveChanges();

                        VoteLog vl = new VoteLog()
                        {
                            Active = true,
                            SectionId = Int16.Parse(s),
                            Username = User.Identity.Name,
                            Vote = thisVote,
                            VoteForId = serviceId
                        };

                        dbe.VoteLogs.Add(vl);

                        dbe.SaveChanges();

                        // keep the product voting flag to stop voting by this member
                        HttpCookie cookie = new HttpCookie(url, "true");
                        Response.Cookies.Add(cookie);
                    }
                    break;
                default:
                    break;
            }
            return Json("<small>You rated " + r + " star(s), thanks !</small>");
        }

        // GET: /Default/Create
        public ActionResult Create()
        {
            //ViewBag.ApplicationUserId = new SelectList(db.IdentityUsers, "Id", "UserName");
            return View();
        }

        // POST: /Default/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MyAccount myaccount)
        {
            if (ModelState.IsValid)
            {
                db.MyAccounts.Add(myaccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(myaccount);
        }

        // GET: /Default/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyAccount myaccount = db.MyAccounts.Find(id);
            if (myaccount == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ApplicationUserId = new SelectList(db.IdentityUsers, "Id", "UserName", myaccount.ApplicationUserId);
            return View(myaccount);
        }

        // POST: /Default/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MyAccount myaccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myaccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(myaccount);
        }


        // GET: /Default/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyAccount myaccount = db.MyAccounts.Find(id);
            if (myaccount == null)
            {
                return HttpNotFound();
            }
            return View(myaccount);
        }

        // POST: /Default/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MyAccount myaccount = db.MyAccounts.Find(id);
            db.MyAccounts.Remove(myaccount);
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
