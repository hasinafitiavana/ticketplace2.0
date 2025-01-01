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
                    CommandText = @"SELECT * FROM Evenements
                                    WHERE (@search IS NULL OR Nom LIKE '%' + @search + '%')
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
                            EspaceId = reader.GetInt32(1),
                            Nom = reader.GetString(2),
                            Description = reader.GetString(3),
                            Date = reader.GetDateTime(4),
                            Heure = reader.GetTimeSpan(5),
                            Lieu = reader.GetString(6),
                            ImagePath = reader.IsDBNull(7) ? null : reader.GetString(7),
                            OnCreate = reader.GetDateTime(8),
                            OnUpdate = reader.GetDateTime(9)
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
                    CommandText = @"SELECT COUNT(*) FROM Evenements
                                    WHERE (@search IS NULL OR ""Nom"" LIKE '%' + @search + '%')"
                };

                command.Parameters.AddWithValue("@search", string.IsNullOrEmpty(search) ? (object)DBNull.Value : search);

                totalCount = (int)await command.ExecuteScalarAsync();
            }

            return totalCount;
        }
        public async Task<EvenementModel> GetEvenementAsync(int idEvenement)
        {
            EvenementModel evenement = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT * FROM Evenements WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", idEvenement);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            evenement = new EvenementModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                EspaceId = reader.GetInt32(reader.GetOrdinal("EspaceId")),
                                Nom = reader.GetString(reader.GetOrdinal("Nom")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                Heure = reader.GetTimeSpan(reader.GetOrdinal("Heure")),
                                Lieu = reader.GetString(reader.GetOrdinal("Lieu")),
                                ImagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader.GetString(reader.GetOrdinal("ImagePath")),
                                OnCreate = reader.GetDateTime(reader.GetOrdinal("OnCreate")),
                                OnUpdate = reader.GetDateTime(reader.GetOrdinal("OnUpdate"))
                            };
                        }
                    }
                }
            }

            return evenement;
        }

        public async Task<List<EvenementTypePlaceModel>> GetEvenementTypePlacesAsync(int idEvenement)
        {
            var evenementTypePlaces = new List<EvenementTypePlaceModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // var query = "SELECT etp.Id, etp.EvenementId, etp.TypePlaceId, tp.Type AS TypePlaceName FROM EvenementTypePlaces etp " +
                //             "INNER JOIN TypePlaces tp ON etp.TypePlaceId = tp.Id " +
                //             "WHERE etp.EvenementId = @Id";

                var query = "SELECT etp.Id, etp.EvenementId, etp.TypePlaceId, tp.Type AS TypePlaceName, tp.Couleurs FROM EvenementTypePlaces etp " +
                "INNER JOIN TypePlaces tp ON etp.TypePlaceId = tp.Id " +
                "WHERE etp.EvenementId = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", idEvenement);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            evenementTypePlaces.Add(new EvenementTypePlaceModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                EvenementId = reader.GetInt32(reader.GetOrdinal("EvenementId")),
                                TypePlaceId = reader.GetInt32(reader.GetOrdinal("TypePlaceId")),
                                TypePlace = new TypePlaceModel
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("TypePlaceId")),
                                    Type = reader.GetString(reader.GetOrdinal("TypePlaceName")),
                                    Couleurs = reader.GetString(reader.GetOrdinal("Couleurs"))    
                                }
                            });
                        }
                    }
                }
            }

            return evenementTypePlaces;
        }
    }
}