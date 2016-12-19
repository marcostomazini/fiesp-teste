using FiespTeste.Business.DB;
using FiespTeste.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FiespTeste.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        ///     Index page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = new EmpresaModels();

            GerarDropDownList(model);

            return View(model);
        }        

        /// <summary>
        ///     Salva a empresa
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Index(EmpresaModels model)
        {
            if (ModelState.IsValid)
            {
                if (ValidarRegraNegocio(model))
                {
                    var comandosSql = new ComandosSql();
                    comandosSql.Salvar(model);

                    return RedirectToAction("Index", "Home");
                }                
            }            

            GerarDropDownList(model);
            return View(model);
        }

        /// <summary>
        ///     Metodo json que recupera o vendedor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult RecuperarVendedor(int id)
        {
            var comandosSql = new ComandosSql();
            var vendedor = comandosSql.RecuperarVendedor(id);

            return Json(vendedor, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Gerador do combobox/dropdownlist
        /// </summary>
        /// <param name="model"></param>
        private static void GerarDropDownList(EmpresaModels model)
        {
            var comandosSql = new ComandosSql();
            var vendedores = comandosSql.RecuperarVendedores();
            model.Vendedores = vendedores.Select(x => new SelectListItem()
            {
                Value = x.Codigo.ToString(),
                Text = x.Nome
            });
        }

        /// <summary>
        ///     Valida regra de negocio
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool ValidarRegraNegocio(EmpresaModels model)
        {
            var validacao = (model.ExpectativaVenda <= model.Faturamento);
            if (!validacao)
            {
                ModelState.AddModelError("", "Expectativa de venda maior que o limite estabelecido");
                return false;
            }
            return true;
        }
    }
}