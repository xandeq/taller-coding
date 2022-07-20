using Microsoft.AspNetCore.Mvc;
using Taller.CarModel.Models;

namespace Taller.CarModel.Controllers
{
    public class CarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Car car = new Car();
            if (id == null)
            {
                return View(car);
            }
            car = Car.cars.FirstOrDefault(o => o.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Car car)
        {
            if (ModelState.IsValid)
            {
                if (car.Id == 0)
                {
                    car.Id = Car.cars.OrderByDescending(u => u.Id).Select(s => s.Id).FirstOrDefault() + 1;
                    Car.cars.Add(car);
                }
                else
                {
                    var index = Car.cars.FindIndex(r => r.Id == car.Id);
                    if (index != -1)
                    {
                        Car.cars[index] = car;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        [HttpPost("{id}/{price}")]
        public IActionResult GuessThePrice(int id, int price)
        {
            Car car = Car.cars.FirstOrDefault(o => o.Id == id);
            int maxPrice = car.Price + 5000;
            int minPrice = car.Price - 5000;
            if (price <= maxPrice && price >= minPrice)
            {
                return Json(new { success = true, message = "Great Job." });
            }
            else
            {
                return Json(new { success = false, message = "Not a great job. Try again." });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = Car.cars.ToList() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = Car.cars.FirstOrDefault(x => x.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            Car.cars.Remove(objFromDb);
            return Json(new { success = true, message = "Delete successful." });
        }
    }
}
