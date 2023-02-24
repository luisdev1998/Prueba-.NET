using System;
using System.ComponentModel.DataAnnotations;

namespace Prueba.DOTs
{
	public class ProductoDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string StatusName { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public decimal FinalPrice { get; set; }
    }
}

