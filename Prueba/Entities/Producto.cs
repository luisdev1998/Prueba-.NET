using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Prueba.ApiClients;

namespace Prueba.Entities
{
	public class Producto
    {
        [Required(ErrorMessage = "El campo Id es requerido")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "El campo Nombre es requerido")]
		public string Name { get; set; }
		public int Status { get; set; }
        public string StatusName { get; set; }
        [Required(ErrorMessage = "El campo Stock es requerido")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "El campo Descripción es requerido")]
        public string Description { get; set; }
        [Required(ErrorMessage = "El campo Precio es requerido")]
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public decimal FinalPrice
        {
            get
            {
                return Price * (100 - Discount) / 100;
            }
        }

        public async Task ObtenerDescuentoAsync(DescuentosApiClient descuentosApiClient)
        {
            int descuento = await descuentosApiClient.ObtenerDescuentoAsync(this.ProductId);
            this.Discount = descuento;
        }

        public static implicit operator Producto(ActionResult<Producto> v)
        {
            throw new NotImplementedException();
        }
    }
}

