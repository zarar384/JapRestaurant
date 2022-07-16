using AutoMapper;
using Jap.Services.ShoppingCartAPI.Models;
using Jap.Services.ShoppingCartAPI.Models.Dto;
using Jap.Services.ShoppongCartAPI.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Jap.Services.ShoppingCartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;

        public CartRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<bool> ClearCart(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
        {
            //add link from _mapper => get Cart objcet and assighn CartDto to Cart object
            Cart cart = _mapper.Map<Cart>(cartDto);

            //check if product exists in database. If not - create.
            var prodInDb = await _db.Products
                .FirstOrDefaultAsync(u => u.ProductId == cartDto.CartDetails.FirstOrDefault()
                .ProductId);

            if (prodInDb == null)
            {
                _db.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _db.SaveChangesAsync();
            }

            //check if header is null
            var cartHeaderFormDb = await _db.CartHeaders.AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);
            if (cartHeaderFormDb == null)
            {
                //create header and details
                //new id in db
                _db.CartHeaders.Add(cart.CartHeader);
                await _db.SaveChangesAsync();
                //use new id and fill CardDetails object
                cart.CartDetails.FirstOrDefault().CartDetailsId = cart.CartHeader.CartHeaderId;
                //CONFLICT! ef is trying to add a Product. Product with this ID has already been added.
                cart.CartDetails.FirstOrDefault().Product = null;
                //save changes in CartDetails
                _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _db.SaveChangesAsync();
            }
            else
            {
                //if header is not null
                //check if details has same product
                var cartDetailsFromDb = await _db.CartDetails.AsNoTracking()
                    .FirstOrDefaultAsync(
                    u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                    u.CartHeaderId == cartHeaderFormDb.CartHeaderId);
                if(cartDetailsFromDb == null)
                {
                    //create details
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeaderFormDb.CartHeaderId;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
                else
                {
                    //update the Count in CartDetails
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetailsFromDb.Count;
                    _db.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }

            }
            //converting cart to CartDto
            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> GetCartByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            throw new NotImplementedException();
        }
    }
}
