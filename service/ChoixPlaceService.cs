using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using TicketPlace2._0.Models;

namespace TicketPlace2._0.service
{
    public class ChoixPlaceService
    {
        private readonly string _connectionString;

        public ChoixPlaceService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");;
        }

        public async Task<EvenementModel?> GetEvenementByIdAsync(int evenementId)
        {
            EvenementModel? evenement = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Evenements WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", evenementId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            evenement = new EvenementModel
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
                        }
                    }
                }
            }

            return evenement;
        }

        public async Task<List<EvenementTypePlaceModel>> GetEvenementTypePlacesAsync(int evenementId)
        {
            var evenementTypePlaces = new List<EvenementTypePlaceModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM EvenementTypePlaces WHERE EvenementId = @EvenementId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EvenementId", evenementId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var evenementTypePlace = new EvenementTypePlaceModel
                            {
                                Id = reader.GetInt32(0),
                                EvenementId = reader.GetInt32(1),
                                TypePlaceId = reader.GetInt32(2),
                                NombreDePlaces = reader.GetInt32(3),
                                Emplacements = reader.GetString(4),
                                Prix = reader.GetDecimal(5),
                                OnCreate = reader.GetDateTime(6),
                                OnUpdate = reader.GetDateTime(7)
                            };
                            evenementTypePlaces.Add(evenementTypePlace);
                        }
                    }
                }
            }

            return evenementTypePlaces;
        }
    }
}