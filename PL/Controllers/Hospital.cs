using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Hospital : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll() {

            Dictionary<string, object> respuesta = BL.Hospital.GetAll();
            bool resultado = (bool)respuesta["Resultado"];
            if (resultado)
            {
                ML.Hospital hospital = (ML.Hospital)respuesta["Hospital"];
                return View(hospital);
            }

            return View();

        }

        [HttpGet]
        public IActionResult Form()
        {

           

            return View();

        }

        [HttpPost]
        public IActionResult Form(int? IdHospital)
        {



            return View();

        }

        [HttpPost]


        [HttpGet]
        public IActionResult Delete(int IdHospital)
        {

            Dictionary<string, object> respuesta = BL.Hospital.Delete(IdHospital);
            bool resultado = (bool)respuesta["Resultado"];
            string mensaje = (string)respuesta["Mensaje"];
            if (resultado)
            {
               ViewBag.Mensaje = mensaje;
               return PartialView("Modal");
            }

            return View();

        }
    }
}
