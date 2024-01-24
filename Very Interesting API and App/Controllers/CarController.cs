using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Very_Interesting_API_and_App.Models;

namespace Very_Interesting_API_and_App.Controllers
{

	[ApiController]
	[Route("[controller]")]
	// https://localhost:718/v1/Cars
	public class CarController : ControllerBase
	{
		public static List<Car> Cars { get; set; } = new()
		{
			new Car { id = 1, brand = "Toyota", model = "Camry", modelYear = 2022 },
						new Car { id = 2, brand = "Chevrolet", model = "Silverado", modelYear = 2021 },
						new Car { id = 3, brand = "BMW", model = "3 Series", modelYear = 2023 },
						new Car { id = 4, brand = "Ford", model = "Mustang", modelYear = 2020 },
						new Car { id = 5, brand = "Honda", model = "Civic", modelYear = 2022 },
						new Car { id = 6, brand = "Mercedes-Benz", model = "E-Class", modelYear = 2021 }
		};

		[HttpGet]
		public ActionResult<List<Car>> Get()
		{
			// retunerar en lista med bilar

			if (Cars != null && Cars.Any())
			{
				return Ok(Cars);

			}
			return NotFound("Could not find a Car");
		}

		// https://localhost:718/v1/Cars/ {id}

		[HttpGet("{id}")]
		//[Route("{id}")]
		

		public ActionResult<Car?> Get(int id)
		{
			Car? car = Cars.FirstOrDefault(c => c.id == id);

			if (car == null ) 
			{ 
				// kunde inte hitta en bil med det id:et
				// ErrorMeddelande
				return NotFound("Could not find a car with that ID!");	

			}
			// retunerar en bil med id
			return Ok(car);
		}

		[HttpPost]
		public ActionResult Post([FromBody]Car cars)
		{
			if (cars != null)
			{
				// Lägger till en bil
				Cars.Add(cars);
				return Ok("Car dded!");	
			}
			return BadRequest("Could not add the car. check your input!");
		}
		[HttpPut]
		[Route("{id}")]
		public ActionResult Put(int id, [FromBody] Car updatedCar)  /*Car updatedCar, Car existingCars*/
		{
			Car? existingCars = Cars.FirstOrDefault(c => c.id == id);
			
				if (existingCars == null)
				{
				return NotFound("could not find a car with that ID!");
				}
					existingCars.brand = updatedCar.brand;
					existingCars.model = updatedCar.model;
					existingCars.modelYear = updatedCar.modelYear;
			return Ok("Car Updated!");
		}

		[HttpDelete]
		[Route("{id}")]
		public ActionResult Delete(int id) 
		{ 
			Car? carToDelete = Cars.FirstOrDefault(c => c.id == id);

			if (carToDelete == null)
			{
				return NotFound("Could not find a car with that ID");
			};

			Cars.Remove(carToDelete);
			return Ok("Car Deleted!");
		}

	}
}
