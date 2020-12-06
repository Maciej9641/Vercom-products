using System.Data;
using System.Data.SqlClient;


namespace Vercom_products.Validators
{
    public class ProductControllerValidator : IProductControllerValidator
    {
        private readonly string _connectionString;

        public ProductControllerValidator()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString;
        }

        public bool ValidateIfProductCategoryExists(string categoryName)
        {
            string query = $@"SELECT 1 FROM dbo.Categories WHERE Name = '{categoryName}'";
            DataTable table = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection);
                adapter.SelectCommand.CommandType = CommandType.Text;
                adapter.Fill(table);
            }

            return table.Rows.Count == 1;
        }
    }
}