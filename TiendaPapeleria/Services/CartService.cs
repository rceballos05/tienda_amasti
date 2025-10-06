using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TiendaPapeleria.Data;
using TiendaPapeleria.Models;

namespace TiendaPapeleria.Services;

public class CartService
{
    private readonly ConcurrentDictionary<string, List<CartItem>> _carts = new();
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;

    public CartService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task<string> GetCartKeyAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null)
        {
            return "anon";
        }

        if (httpContext.User.Identity?.IsAuthenticated == true)
        {
            var user = await _userManager.GetUserAsync(httpContext.User);
            if (user != null)
            {
                return user.Id;
            }
        }

        if (!httpContext.Request.Cookies.TryGetValue("papeleria_cart", out var anonymousId))
        {
            anonymousId = Guid.NewGuid().ToString();
            httpContext.Response.Cookies.Append("papeleria_cart", anonymousId, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            });
        }

        return anonymousId;
    }

    public async Task<IReadOnlyList<CartItem>> GetCartAsync()
    {
        var key = await GetCartKeyAsync();
        var cart = _carts.GetOrAdd(key, _ => new List<CartItem>());
        return cart.ToList();
    }

    public async Task AddToCartAsync(Product product)
    {
        var key = await GetCartKeyAsync();
        var cart = _carts.GetOrAdd(key, _ => new List<CartItem>());
        var existing = cart.FirstOrDefault(ci => ci.Product.Id == product.Id);
        if (existing is null)
        {
            cart.Add(new CartItem
            {
                Product = product,
                Quantity = 1
            });
        }
        else
        {
            existing.Quantity += 1;
        }
    }

    public async Task RemoveFromCartAsync(string productId)
    {
        var key = await GetCartKeyAsync();
        if (!_carts.TryGetValue(key, out var cart))
        {
            return;
        }

        var existing = cart.FirstOrDefault(ci => ci.Product.Id == productId);
        if (existing is null)
        {
            return;
        }

        existing.Quantity -= 1;
        if (existing.Quantity <= 0)
        {
            cart.Remove(existing);
        }
    }

    public async Task ClearAsync()
    {
        var key = await GetCartKeyAsync();
        _carts.TryRemove(key, out _);
    }
}
