using CBTSWE2_TP02.Data;
using CBTSWE2_TP02.Models;
using Microsoft.AspNetCore.Mvc;

//feito por Vinicius Pontes e Eduardo Barbosa



namespace CBTSWE2_TP02.Controllers
{
    public class BLController : Controller
    {
        private readonly AppDbContext _context;
        public BLController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            BL[] listaBL = _context.BLs.ToList().ToArray();
            return View("~/Views/Bill of Landing/Index.cshtml", listaBL);
        }

        public IActionResult Create()
        {
            return View("~/Views/Bill of Landing/cadastrar-bl.cshtml");
        }

        public IActionResult Edit(int id)
        {
            BL? blEcontrada = _context.BLs.Find(id); 
            
            if (blEcontrada == null) return NotFound();
            
            return View("~/Views/Bill of Landing/editar-bl.cshtml", blEcontrada);
        }

        public IActionResult Delete(int id) {
            BL? blEcontrada = _context.BLs.Find(id);
            if (blEcontrada == null) return NotFound();
            _context.BLs.Remove(blEcontrada);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Create(BL bl)
        {
            if (ModelState.IsValid)
            {
                _context.BLs.Add(bl);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("~/Views/Bill of Landing/cadastrar-bl.cshtml", bl);
        }

        [HttpPost]
        public IActionResult Edit(BL bl)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Bill of Landing/editar-bl.cshtml", bl);

            BL? blEcontrada = _context.BLs.Find(bl.Id);

            if (blEcontrada == null)
                return NotFound();

            blEcontrada.Numero = bl.Numero;
            blEcontrada.Consignee = bl.Consignee;
            blEcontrada.Navio = bl.Navio;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
