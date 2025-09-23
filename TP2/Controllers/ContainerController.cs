using CBTSWE2_TP02.Data;
using CBTSWE2_TP02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CBTSWE2_TP02.Controllers
{
    public class ContainerController : Controller
    {
        private readonly AppDbContext _context;
        public ContainerController(AppDbContext context)
        {
            _context = context;//feito por Vinicius Pontes e Eduardo Barbosa

        }

        private List<SelectListItem> GetListaDeBLs()
        {
            var selectBl = _context.BLs
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = $"{b.Numero} - {b.Consignee}"
                })
                .ToList();
            return selectBl;
        }
        public IActionResult Index()
        {
            Container[] listaContainers = _context.Containers.ToList().ToArray();
            return View(listaContainers);
        }

        public IActionResult Create()
        {
            var bls = GetListaDeBLs();
            ViewBag.BLs = bls;
            return View();
        }

        public IActionResult Edit(int id) {
            Container? containerEncontrado = _context.Containers.Find(id);
            if (containerEncontrado == null) return NotFound();

            ViewBag.BLs = GetListaDeBLs();
            return View(containerEncontrado);
        }

        [HttpPost]
        public IActionResult Create(Container container)
        {
            try
            {
                _context.Containers.Add(container);
                _context.SaveChanges();
                return RedirectToAction("Index");
                
            }
            catch(Exception ex)
            {
                Console.Write("Erro ao criar container: " + ex);
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o container. Por favor, tente novamente.");
                var bls = GetListaDeBLs();
                ViewBag.BLs = bls;
                return View(container);
            }
        }

        [HttpPost]
        public IActionResult Edit(Container container)
        {
            Container? containerEncontrado = _context.Containers.Find(container.Id);
            if (containerEncontrado == null) return NotFound();

            containerEncontrado.Numero = container.Numero;
            containerEncontrado.Tipo = container.Tipo;
            containerEncontrado.Tamanho = container.Tamanho;
            containerEncontrado.BLId = container.BLId;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id) {
            Container? containerEncontrado = _context.Containers.Find(id);
            
            if (containerEncontrado == null) return NotFound();
            
            _context.Containers.Remove(containerEncontrado);
            _context.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}
