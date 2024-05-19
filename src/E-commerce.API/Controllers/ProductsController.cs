using AutoMapper;
using E_commerce.API.Errors;
using E_commerce.Core.Sharing;
using E_commerce.Core.Dtos;
using E_commerce.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using E_commerce.API.Helpers;

namespace E_commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("get-all-products")]
        public async Task<ActionResult> GetAllProducts([FromQuery] ProductParams productParams)
        {
            var allProducts = await _unitOfWork.ProductRepository.GetAllAsync(productParams);

            var mappedProducts = _mapper.Map<List<ProductDto>>(allProducts.ProductDtos);

            return Ok(new Pagination<ProductDto>(productParams.PageNumber, productParams.PageSize,allProducts.TotalItems, mappedProducts));
        }

        [HttpGet("productById{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseCommonResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetProductById(int id)
        {
            var currentProduct = await _unitOfWork.ProductRepository.GetByIdAsync(id, P => P.Category);
            if (currentProduct is null)
                return NotFound(new BaseCommonResponse(404));

            var mappedProduct = _mapper.Map<ProductDto>(currentProduct);
            return Ok(mappedProduct);
        }

        [HttpPost("addProduct")]
        public async Task<ActionResult> AddProduct([FromForm] CreateProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _unitOfWork.ProductRepository.AddAsync(productDto);
                    return res ? Ok(productDto) : BadRequest(res);
                }
                return BadRequest(productDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateProduct/{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromForm] UpdateProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _unitOfWork.ProductRepository.UpdateAsync(id, productDto);
                    return res ? Ok(productDto) : BadRequest(res);
                }
                return BadRequest(productDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteProduct/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _unitOfWork.ProductRepository.DeleteWithPictureAsync(id);
                    return res ? Ok(res) : BadRequest(res);
                }

                return NotFound($"This Id {id} Not Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
