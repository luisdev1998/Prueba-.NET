using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Prueba.ApiClients;
using Prueba.DOTs;
using Prueba.Entities;

namespace Prueba.Repositorio
{
    public interface IRepository<T>
    {
        Task<T> GetById(int id, List<T> _productos);
        T Add(T entity, List<T> _productos);
        T Update(T entity, List<T> _productos);
    }

    public class ProductoRepository : IRepository<Producto>
    {
        private readonly DescuentosApiClient _descuentosApiClient;
        private readonly IDictionary<int, string> _productStates;

        public ProductoRepository(DescuentosApiClient descuentosApiClient, IDictionary<int, string> productStates)
        {
            this._descuentosApiClient = descuentosApiClient;
            this._productStates = productStates;
        }

        //////////////////////////////////////////////
        //////////////////////////////////////////////
        public async Task<Producto> GetById(int id, List<Producto> _productos)
        {
            var descuentosApiClient = new DescuentosApiClient(new HttpClient());
            Producto producto = _productos.FirstOrDefault(x => x.ProductId == id);
            producto.Discount = await descuentosApiClient.ObtenerDescuentoAsync(producto.ProductId);
            producto.StatusName = _productStates[producto.Status];
            return producto;
        }
        //////////////////////////////////////////////
        //////////////////////////////////////////////
        public Producto Add(Producto entity, List<Producto> _productos)
        {
            _productos.Add(entity);
            return entity;
        }
        //////////////////////////////////////////////
        //////////////////////////////////////////////
        public Producto Update(Producto entity, List<Producto> _productos)
        {
            for (int i = 0; i < _productos.Count; i++)
            {
                if (_productos[i].ProductId == entity.ProductId)
                {
                    _productos[i] = entity;
                    break;
                }
            }
            return entity;
        }
        //////////////////////////////////////////////
        //////////////////////////////////////////////
    }
}

