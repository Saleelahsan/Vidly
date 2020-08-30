using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private Datacontext _context;


        public CustomersController()
        {
            _context = new Datacontext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CustomerDetails()
        {
            //var Customers = new List<Customer>
            //{
            //    new Customer{Name="John Smith",Id=1},
            //    new Customer{Name="Mary Williams",Id=2}
            //};

            var Customers = _context.Customers.Include(c => c.MemberShipType).ToList();

            //var CustomerDetails = new CustomerDetailsViewModel
            //{
            //    CustomerList = Customers
            //};

            return View(Customers);
        }

        public ActionResult Details(int Id)
            {

            var customter = _context.Customers.Include(c => c.MemberShipType).SingleOrDefault(c => c.Id == Id);

            if(customter==null)
            {
                return HttpNotFound();
            }
            else
            {
                //return View(customter.Name);
                return View(customter);
            }

            //if (Id == 1)
            //    return Content("John Williams");
            //else if (Id == 2)
            //    return Content("Marry Williams");
            //else
            //    return HttpNotFound();

            }

        public ActionResult New()
        {
            var membershipTypes = _context.MemberShipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                MemberShipTypes = membershipTypes
            };

            return View("CustomerForm",viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MemberShipTypeId = customer.MemberShipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;



            }
            _context.SaveChanges();

            return RedirectToAction("CustomerDetails", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if(customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MemberShipTypes = _context.MemberShipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

    }
}