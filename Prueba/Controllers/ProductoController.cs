using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Prueba.ApiClients;
using Prueba.DOTs;
using Prueba.Entities;
using Prueba.Repositorio;

namespace Prueba.Controllers
{

    [ApiController]
    [Route("api/producto")]
    public class ProductoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Producto> _productRepository;
        private readonly List<Producto> _listaProductos;

        public ProductoController(
            IMapper mapper,
            IRepository<Producto> productRepository,
            List<Producto> listaProductos)
        {
            this._mapper = mapper;
            this._productRepository = productRepository;
            this._listaProductos = listaProductos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTO>> GetById(int id)
        {
            try
            {
                var existe = _listaProductos.Any(x => x.ProductId == id);
                if (!existe)
                {
                    return NotFound("Este producto no existe");
                }
                var producto = await _productRepository.GetById(id, _listaProductos);

                return _mapper.Map<ProductoDTO>(producto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post(ProductoCreacionDTO productoCreacionDTO)
        {
            try
            {
                var existe = _listaProductos.Any(x => x.ProductId == productoCreacionDTO.ProductId);
                if (existe)
                {
                    return BadRequest("El id del producto ya ha sido registrado");
                }
                var producto = _mapper.Map<Producto>(productoCreacionDTO);
                _productRepository.Add(producto, _listaProductos);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(ProductoActualizarDTO productoActualizarDTO, int id)
        {
            try
            {
                if (productoActualizarDTO.ProductId != id)
                {
                    return BadRequest("El id del producto no coincide con el id de la URL");
                }
                var existe = _listaProductos.Any(x => x.ProductId == id);
                if (!existe)
                {
                    return NotFound("Este producto no existe");
                }
                var producto = _mapper.Map<Producto>(productoActualizarDTO);

                _productRepository.Update(producto, _listaProductos);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}

