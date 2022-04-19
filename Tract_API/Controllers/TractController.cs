using Tract_API.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;

namespace Tract_API.Controllers
{
    [Route("api/tract")]
    [ApiController]
    public class TractController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public TractController(IConfiguration config)
        {
            this._configuration = config;
        }


        private MySqlConnection GetConnection()
        {
            string connection_string = this._configuration.GetConnectionString("MySqlConnection");
            return new MySqlConnection(connection_string);
        }

        [HttpGet]
        public async Task<ActionResult<List<Tract>>> GetTracts()
        {
            List<Tract> products_list = new List<Tract>();

            using (MySqlConnection connection = GetConnection())
            {
                await connection.OpenAsync();
                using MySqlCommand command = new MySqlCommand("GetTracts", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    products_list.Add(new Tract
                    {
                        ID = reader.GetInt32(0),
                        TractNumber = reader.GetString(1),
                        TractAltNumber = reader.GetString(2),
                        TractMapId = reader.GetString(3),
                        State = reader.GetString(4),
                        County = reader.GetString(5),
                        GrossAcres = reader.GetDouble(6),
                        ShortDesc = reader.GetString(7),
                        Status = reader.GetString(8),
                        ProjectId = reader.GetInt32(9),
                        CustomerId = reader.GetInt32(10),
                        LegalDescription = reader.GetString(11),
                        TractTypeId = reader.GetInt32(12)
                    });
                }
            }
            return Ok(products_list);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Tract>> GetTractbyID(int id)
        {
            using (MySqlConnection connection = GetConnection())
            {
                await connection.OpenAsync();
                using MySqlCommand command = new MySqlCommand("GetTractbyID", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("@id", id));
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Tract tract = new Tract
                    {
                        ID = reader.GetInt32(0),
                        TractNumber = reader.GetString(1),
                        TractAltNumber = reader.GetString(2),
                        TractMapId = reader.GetString(3),
                        State = reader.GetString(4),
                        County = reader.GetString(5),
                        GrossAcres = reader.GetDouble(6),
                        ShortDesc = reader.GetString(7),
                        Status = reader.GetString(8),
                        ProjectId = reader.GetInt32(9),
                        CustomerId = reader.GetInt32(10),
                        LegalDescription = reader.GetString(11),
                        TractTypeId = reader.GetInt32(12)
                    };
                    return Ok(tract);
                }
            }
            return BadRequest("Tract not found");
        }


        [HttpPost]
        public async Task<ActionResult<Tract>> AddTract(Tract tract)
        {
            using (MySqlConnection connection = GetConnection())
            {
                await connection.OpenAsync();
                using MySqlCommand command = new MySqlCommand("InsertNewTract", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("@TractNumber", tract.TractNumber));
                command.Parameters.Add(new MySqlParameter("@TractAltNumber", tract.TractAltNumber));
                command.Parameters.Add(new MySqlParameter("@TractMapId", tract.TractMapId));
                command.Parameters.Add(new MySqlParameter("@State", tract.State));
                command.Parameters.Add(new MySqlParameter("@County", tract.County));
                command.Parameters.Add(new MySqlParameter("@GrossAcres", tract.GrossAcres));
                command.Parameters.Add(new MySqlParameter("@ShortDesc", tract.ShortDesc));
                command.Parameters.Add(new MySqlParameter("@Status", tract.Status));
                command.Parameters.Add(new MySqlParameter("@ProjectId", tract.ProjectId));
                command.Parameters.Add(new MySqlParameter("@CustomerId", tract.CustomerId));
                command.Parameters.Add(new MySqlParameter("@LegalDescription", tract.LegalDescription));
                command.Parameters.Add(new MySqlParameter("@TractTypeId", tract.TractTypeId));

                await command.ExecuteNonQueryAsync();
            }
            return Ok(tract);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Tract>> UpdateTract(Tract tract, int id)
        {
            using (MySqlConnection connection = GetConnection())
            {
                await connection.OpenAsync();
                using MySqlCommand command = new MySqlCommand("UpdateTract", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("@TractNumber", tract.TractNumber));
                command.Parameters.Add(new MySqlParameter("@TractAltNumber", tract.TractAltNumber));
                command.Parameters.Add(new MySqlParameter("@TractMapId", tract.TractMapId));
                command.Parameters.Add(new MySqlParameter("@State", tract.State));
                command.Parameters.Add(new MySqlParameter("@County", tract.County));
                command.Parameters.Add(new MySqlParameter("@GrossAcres", tract.GrossAcres));
                command.Parameters.Add(new MySqlParameter("@ShortDesc", tract.ShortDesc));
                command.Parameters.Add(new MySqlParameter("@Status", tract.Status));
                command.Parameters.Add(new MySqlParameter("@ProjectId", tract.ProjectId));
                command.Parameters.Add(new MySqlParameter("@CustomerId", tract.CustomerId));
                command.Parameters.Add(new MySqlParameter("@LegalDescription", tract.LegalDescription));
                command.Parameters.Add(new MySqlParameter("@TractTypeId", tract.TractTypeId));
                command.Parameters.Add(new MySqlParameter("@id", id));

                int s = await command.ExecuteNonQueryAsync();

                // returns number of lines affected (should be 1)
                // if item is not in db, send bad request
                if (s == 0)
                    return BadRequest("Tract not found");

                return Ok(tract);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Tract>> DeleteTract(int id)
        {
            using (MySqlConnection connection = GetConnection())
            {
                await connection.OpenAsync();
                using MySqlCommand command = new MySqlCommand("DeleteTract", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("@id", id));
                int s = await command.ExecuteNonQueryAsync();

                if (s == 0)
                    return BadRequest("Tract not found");

                return Ok(s);
            }




        }
    }
}

