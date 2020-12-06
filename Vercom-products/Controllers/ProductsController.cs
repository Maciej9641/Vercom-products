using System;
using System.Data.SqlClient;
using System.Web.Http;
using System.Data;
using Vercom_products.Models;
using Vercom_products.Validators;

namespace Vercom_products.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly string _connectionString;
        private readonly IProductControllerValidator _productControllerValidator;

        public ProductsController(IProductControllerValidator productControllerValidator)
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString;
            _productControllerValidator = productControllerValidator;
        }

        [Route("getProducts/{categoryName}")]
        [HttpGet]
        public IHttpActionResult GetProductsByCategory(string categoryName)
        {
            if (ModelState.IsValid)
            {
                if (_productControllerValidator.ValidateIfProductCategoryExists(categoryName))
                {
                    DataTable table = new DataTable();
                    using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                    {
                        sqlConnection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter("GetProductsFromCategory", sqlConnection);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapter.SelectCommand.Parameters.AddWithValue("CategoryName", categoryName);
                        adapter.Fill(table);
                    }

                    return Ok(table);
                }
                else
                {
                    return BadRequest($"Category {categoryName} does not exist in database");
                }
            }
            return BadRequest("ModelState is invalid");

        }

        [Route("addProduct/{categoryName}")]
        [HttpPost]
        public IHttpActionResult AddProductToCategory(Product product, string categoryName)
        {
            if(ModelState.IsValid)
            {
                if (_productControllerValidator.ValidateIfProductCategoryExists(categoryName))
                {
                    using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                    {
                        sqlConnection.Open();
                        SqlCommand command = new SqlCommand("AddProductToCategory", sqlConnection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("CategoryName", categoryName);
                        command.Parameters.AddWithValue("ProductName", product.Name);
                        command.Parameters.AddWithValue("Width", product.Width);
                        command.ExecuteNonQuery();
                    }
                    return Ok("Product added !");
                }
                else
                {
                    return BadRequest($"Category {categoryName} does not exist in database");
                }
            }
            return BadRequest("ModelState is invalid");
        }

        [Route("deleteProduct")]
        [HttpPost]
        public IHttpActionResult DeleteProduct(Guid productId)
        {
            if (ModelState.IsValid)
            {
                // TODO: Create validator for productId guid->string
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand command = new SqlCommand("DeleteProduct", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("ProductId", productId);
                    command.ExecuteNonQuery();
                }
                return Ok($"Product with id: {productId} deleted");
            }
            return BadRequest("ModelState is invalid");
        }
    }
}
