using System;
using Prueba.Entities;

namespace Prueba.ApiClients
{
    public class DescuentosApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<int, int> _descuentos = new Dictionary<int, int>();

        public DescuentosApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> ObtenerDescuentoAsync(int productId)
        {
            if (!_descuentos.TryGetValue(productId, out int descuento))
            {
                var response = await _httpClient.GetAsync("https://profile.luissanchez.site/prueba/api/prueba.json");
                response.EnsureSuccessStatusCode();
                var descuentos = await response.Content.ReadFromJsonAsync<List<Descuento>>();
                var descuentoProducto = descuentos.FirstOrDefault(d => d.ProductId == productId);
                descuento = descuentoProducto != null ? descuentoProducto.Discount : 0;
                _descuentos[productId] = descuento;
            }

            return descuento;
        }
    }

}

