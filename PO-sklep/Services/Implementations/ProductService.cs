using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PO_sklep.DTO;
using PO_sklep.Extensions;
using PO_sklep.Models;
using PO_sklep.Repositories.Interfaces;
using PO_sklep.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PO_sklep.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Produkt> _productRepository;
        private readonly IGenericRepository<Opinia> _reviewRepository;
        private readonly IGenericRepository<Klient> _clientRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Produkt> productRepository, IGenericRepository<Opinia> reviewRepository, IGenericRepository<Klient> clientRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task AddReviewAsync(int productId, ReviewDTO newReview)
        {
            var product = await _productRepository.GetAsync(productId);
            var client = await _clientRepository.FindAsync(x => x.Email == newReview.Author.ToLower());

            if (client is null)
            {
                client = await _clientRepository.AddAsync(new Klient
                {
                    ImieKlienta = newReview.Author.ToLower(),
                    NazwiskoKlienta = newReview.Author.ToLower(),
                    Email = newReview.Author.ToLower()
                });
            }

            var review = _mapper.Map<Opinia>(newReview);
            review.IdKlienta = client.IdKlienta;
            product.Opinia.Add(review);
            await _productRepository.SaveAsync();
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            // TODO: Zastapić przez IAsyncEnumerable???
            var products = await _productRepository.GetAllAsync();
            var reviews = await _reviewRepository.GetAllAsync();
            var clients = await _clientRepository.GetAllAsync();
            reviews = reviews.Select(op =>
            {
                op.IdKlientaNavigation = clients.FirstOrDefault(k => k.IdKlienta == op.IdKlienta);
                return op;
            }).ToList();

            products = products.Select(p =>
            {
                p.Opinia = reviews.Where(r => r.IdProduktu == p.IdProduktu).ToList();
                return p;
            }).ToList();

            return _mapper.Map<IEnumerable<ProductDTO>>(products)?.UpdateImageUrls();
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            //await _productRepository.AddAsync(new Produkt
            //{
            //    Producent = "Bosch",
            //    NazwaProduktu = "Pralka dasda321",
            //    CenaNetto = 1299.00m,
            //    Vat = 23,
            //    IdKategorii = 2
            //});
            var product = await _productRepository.FindAsync(p => p.IdProduktu == id);
            return _mapper.Map<ProductDTO>(product)?.UpdateImageUrl();
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategoryId(int categoryId)
        {
            var products = await _productRepository.FindAllAsync(product => product.IdKategorii == categoryId);
            return _mapper.Map<IEnumerable<ProductDTO>>(products)?.UpdateImageUrls();
        }
    }
}
