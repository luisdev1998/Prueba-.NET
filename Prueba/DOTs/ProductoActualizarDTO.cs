using System;
using System.ComponentModel.DataAnnotations;

namespace Prueba.DOTs
{
	public class ProductoActualizarDTO
    {
        [Required(ErrorMessage = "El campo Id es requerido")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "El campo Nombre es requerido")]
        public string Name { get; set; }
        public int Status { get; set; }
        [Required(ErrorMessage = "El campo Stock es requerido")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "El campo Descripción es requerido")]
        public string Description { get; set; }
        [Required(ErrorMessage = "El campo Precio es requerido")]
        public decimal Price { get; set; }
    }
}

