using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FiespTeste.Models
{
    public class EmpresaModels
    {
        public int Codigo { get; set; }

        [Required]
        [MaxLength(60)]
        [DisplayName("Razão social")]
        public string RazaoSocial { get; set; }

        [Required]
        [DisplayName("Faturamento")]
        public decimal? Faturamento { get; set; }

        [Required]
        [DisplayName("Expectativa de venda")]
        public decimal? ExpectativaVenda { get; set; }

        [Required]
        [DisplayName("Vendedor")]
        public int? Vendedor { get; set; }

        [DisplayName("Vendedor")]
        public IEnumerable<SelectListItem> Vendedores { get; set; }
    }
}