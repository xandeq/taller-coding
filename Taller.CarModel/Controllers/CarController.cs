using Microsoft.AspNetCore.Mvc;
using Taller.CarModel.Data.Repository;
using Taller.CarModel.Models;

namespace Taller.CarModel.Controllers
{
    public class CarController : Controller
    {
        CarRepository _carRepository;
        IRepository<Car> _repository;

        public CarController(IRepository<Car> repository)
        {
            _repository = repository;
            _carRepository = new CarRepository();
        }

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
            car = _carRepository.Get().ToList().FirstOrDefault(o => o.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Models.Car car)
        {
            if (ModelState.IsValid)
            {
                if (car.Id == 0)
                {
                    car.Id = _carRepository.Get().ToList().OrderByDescending(u => u.Id).Select(s => s.Id).FirstOrDefault() + 1;
                    _carRepository.Add(car);
                }
                else
                {
                    var index = _carRepository.Get().ToList().FindIndex(r => r.Id == car.Id);
                    if (index != -1)
                    {
                        _carRepository.Get().ToList()[(int)index] = car;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        [HttpPost("{id}/{price}")]
        public IActionResult GuessThePrice(int id, int price)
        {
            Car car = _carRepository.Get().FirstOrDefault(o => o.Id == id);
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
            return Json(new { data = _carRepository.Get().ToList() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _carRepository.Get().ToList().FirstOrDefault(x => x.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _carRepository.Get().ToList().Remove(objFromDb);
            return Json(new { success = true, message = "Delete successful." });
        }
    }
}
