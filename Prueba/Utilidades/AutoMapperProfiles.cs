using System;
using AutoMapper;
using Prueba.DOTs;
using Prueba.Entities;

namespace Prueba.Utilidades
{
	public class AutoMapperProfiles : Profile
    {
		public AutoMapperProfiles()
        {
            // Fuente / Destino
            CreateMap<ProductoCreacionDTO, Producto>();
            CreateMap<Producto, ProductoDTO>();
            CreateMap<ProductoActualizarDTO, Producto>();
        }
	}
}

