using Factory_Management_System_WebAPI.Data;
using Factory_Management_System_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Factory_Management_System_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        #region Get All Product
        [HttpGet]
        public IActionResult GetAllProducts(int AdminID)
        {
            ApiResponse response = null;
            var products = _productRepository.SelectAll(AdminID);
            if(products == null)
            {
                response = new ApiResponse("Products not found", 404);
                return NotFound(response);
            }
            response = new ApiResponse(products, "Products retrived successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Get Product By ID
        [HttpGet("{ProductID}")]
        public IActionResult GetProductByID(int ProductID)
        {
            ApiResponse response = null;
            var product = _productRepository.SelectByPK(ProductID);
            if (product == null)
            {
                response = new ApiResponse("Product not found", 404);
                return NotFound(response);
            }
            response = new ApiResponse(product, "Product Retrived Successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Insert Product
        [HttpPost]
        public IActionResult InsertProduct([FromBody] ProductModel product)
        {
            ApiResponse response = null;
            if (product == null)
            {
                response = new ApiResponse("Product Details are required", 400);
                return BadRequest(response);
            }

            bool isInserted = _productRepository.Insert(product);

            if (!isInserted)
            {
                response = new ApiResponse("Error while inserting product detail", 500);
                return BadRequest();
            }

            response = new ApiResponse("Product Detail Insert Succesfully", 200);
            return Ok(response);
        }
        #endregion

        #region Update Product Price
        [HttpPatch("update-price/{ProductID}")]
        public IActionResult UpdateProductPrice(int ProductID, double price)
        {
            ApiResponse response = null;
            if(ProductID == null || ProductID <= 0)
            {
                response = new ApiResponse("ProductID is required", 400);
                return BadRequest(response);
            }
            if (price == null)
            {
                response = new ApiResponse("Price feild is required", 400);
                return BadRequest(response);
            }

            bool isUpdated = _productRepository.UpdatePrice(ProductID, price);
            if (!isUpdated)
            {
                response = new ApiResponse("Error while updatiog product price", 500);
                return BadRequest();
            }

            response = new ApiResponse("Product price Update Succesfully", 200);
            return Ok(response);
        }
        #endregion

        #region Update Product 
        [HttpPut("update/{ProductID}")]
        public IActionResult UpdateProduct(int ProductID, ProductModel product)
        {
            ApiResponse response = null;
            if (ProductID == null || ProductID <= 0)
            {
                response = new ApiResponse("ProductID is required", 400);
                return BadRequest(response);
            }
            if(ProductID != product.ProductID)
            {
                response = new ApiResponse("ProductID missmatch", 400);
                return BadRequest(response);
            }
            if (product == null)
            {
                response = new ApiResponse("Product Detail is required", 400);
                return BadRequest(response);
            }

            bool isUpdated = _productRepository.Update(product);
            if (!isUpdated)
            {
                response = new ApiResponse("Error while updatiog product", 500);
                return BadRequest();
            }

            response = new ApiResponse("Product Update Succesfully", 200);
            return Ok(response);
        }
        #endregion

        #region Delete Product
        [HttpPatch("delete/{ProductID}")]
        public IActionResult DeleteProduct(int ProductID)
        {
            ApiResponse response = null;
            if(ProductID == null)
            {
                response = new ApiResponse("ProductID is required",400);
                return BadRequest(response);
            }

            bool isDeleted = _productRepository.Delete(ProductID);
            if(!isDeleted)
            {
                response = new ApiResponse("Error while deleting product", 500);
                return BadRequest(response);
            }

            response = new ApiResponse("Product Deleting Successfully", 200);
            return Ok(response);
        }
        #endregion
    }
}
