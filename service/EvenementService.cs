using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using TicketPlace2._0.Models;

namespace TicketPlace2._0.service
{
    public class EvenementService: IEventService
    {
        private readonly string _connectionString;

    public EvenementService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<List<EvenementModel>> GetEvenementsPaginatedAsync(string search, int pageNumber, int pageSize)
    {
        var evenements = new List<EvenementModel>();
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.Text,
                CommandText = @"SELECT * FROM ""Evenements""
                                WHERE (@search IS NULL OR ""Nom"" LIKE '%' + @search + '%')
                                ORDER BY Id
                                OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY"
            };

            command.Parameters.AddWithValue("@search", string.IsNullOrEmpty(search) ? (object)DBNull.Value : search);
            command.Parameters.AddWithValue("@offset", (pageNumber - 1) * pageSize);
            command.Parameters.AddWithValue("@pageSize", pageSize);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var evenement = new EvenementModel
                    {
                        Id = reader.GetInt32(0),
                        Nom = reader.GetString(1),
                        Description = reader.GetString(2),
                        Date = reader.GetDateTime(3),
                        Heure = reader.GetTimeSpan(4),
                        Lieu = reader.GetString(5),
                        ImagePath = reader.IsDBNull(6) ? null : reader.GetString(6),
                        OnCreate = reader.GetDateTime(7),
                        OnUpdate = reader.GetDateTime(8)
                    };
                    evenements.Add(evenement);
                }
            }
        }

        return evenements;
    }

    public async Task<int> GetTotalCountAsync(string search)
    {
        int totalCount;
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.Text,
                CommandText = @"SELECT COUNT(*) FROM ""Evenements""
                                WHERE (@search IS NULL OR ""Nom"" LIKE '%' + @search + '%')"
            };

            command.Parameters.AddWithValue("@search", string.IsNullOrEmpty(search) ? (object)DBNull.Value : search);

            totalCount = (int)await command.ExecuteScalarAsync();
        }

        return totalCount;
    }
    }
}