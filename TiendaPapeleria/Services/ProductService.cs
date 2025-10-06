using System.Collections.Generic;
using System.Linq;
using TiendaPapeleria.Models;

namespace TiendaPapeleria.Services;

public class ProductService
{
    private readonly List<Product> _products = new();

    public Task SeedAsync()
    {
        if (_products.Any())
        {
            return Task.CompletedTask;
        }

        _products.AddRange(new[]
        {
            new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Cuaderno Premium",
                Category = "Cuadernos",
                Description = "Cuaderno de tapa dura con papel reciclado de 120g.",
                Price = 6.99m,
                ImageUrl = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?auto=format&fit=crop&w=600&q=80",
                Featured = true
            },
            new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Plumas Gel Color",
                Category = "Escritura",
                Description = "Set de 12 plumas gel con colores vibrantes.",
                Price = 12.5m,
                ImageUrl = "https://images.unsplash.com/photo-1456081101716-74e616ab23d8?auto=format&fit=crop&w=600&q=80",
                Featured = true
            },
            new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Organizador de Escritorio",
                Category = "Organización",
                Description = "Organizador minimalista de bambú con compartimentos.",
                Price = 24.99m,
                ImageUrl = "https://images.unsplash.com/photo-1524492514791-5e9c3b1e5db6?auto=format&fit=crop&w=600&q=80",
                Featured = false
            },
            new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Marcadores Pastel",
                Category = "Marcadores",
                Description = "Set de 6 marcadores pastel punta biselada.",
                Price = 8.75m,
                ImageUrl = "https://images.unsplash.com/photo-1523475472560-d2df97ec485c?auto=format&fit=crop&w=600&q=80",
                Featured = true
            },
            new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Agenda 2025",
                Category = "Agendas",
                Description = "Agenda semanal con planificador mensual y stickers.",
                Price = 18.5m,
                ImageUrl = "https://images.unsplash.com/photo-1506784983877-45594efa4cbe?auto=format&fit=crop&w=600&q=80",
                Featured = false
            },
            new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Set de Escritorio Ejecutivo",
                Category = "Escritorio",
                Description = "Set completo con base, lapicero, portacartas y reloj.",
                Price = 45.0m,
                ImageUrl = "https://images.unsplash.com/photo-1524758631624-e2822e304c36?auto=format&fit=crop&w=600&q=80",
                Featured = false
            }
        });

        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<Product>> GetFeaturedAsync()
        => Task.FromResult<IReadOnlyList<Product>>(_products.Where(p => p.Featured).ToList());

    public Task<IReadOnlyList<Product>> GetAllAsync()
        => Task.FromResult<IReadOnlyList<Product>>(_products.ToList());

    public Task<IReadOnlyList<string>> GetCategoriesAsync()
        => Task.FromResult<IReadOnlyList<string>>(_products.Select(p => p.Category).Distinct().OrderBy(c => c).ToList());

    public Task<Product?> GetByIdAsync(string id)
        => Task.FromResult(_products.FirstOrDefault(p => p.Id == id));
}
