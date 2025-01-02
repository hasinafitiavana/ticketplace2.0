using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using TicketPlace2._0.Models;

namespace TicketPlace2._0.service
{
    public class ReservationService
    {
        private readonly string _connectionString;
        public ReservationService(IConfiguration configuration){
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<bool> DeletePlaceVendueAsync()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM PlaceVendues WHERE TypeReservation=@Reservation";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Reservation", "RESERVER");
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
        public async Task<List<PlaceVendueModel>> GetPlaceVendueAsync(string? search, int pageNumber, int pageSize, int userId)
        {
            List<PlaceVendueModel> placesVendues = new List<PlaceVendueModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT pv.*, e.Nom AS EvenementNom, e.Description AS EvenementDescription, e.Date AS EvenementDate, e.Heure AS EvenementHeure, e.Lieu AS EvenementLieu, e.ImagePath AS EvenementImagePath, 
                        tp.Type AS TypePlaceType, tp.Couleurs AS TypePlaceCouleurs
                    FROM PlaceVendues pv
                    JOIN Evenements e ON pv.EvenementId = e.Id
                    JOIN TypePlaces tp ON pv.TypePlaceId = tp.Id
                    WHERE (@Search IS NULL OR e.Nom LIKE '%' + @Search + '%')
                    AND pv.UtilisateurId = @UserId
                    AND pv.TypeReservation = 'ACHETER'
                    AND CAST(e.Date AS DATE) >= CAST(GETDATE() AS DATE)
                    ORDER BY pv.Id
                    OFFSET @Offset ROWS 
                    FETCH NEXT @PageSize ROWS ONLY";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Search", (object)search ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize);
                    command.Parameters.AddWithValue("@PageSize", pageSize);
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            PlaceVendueModel place = new PlaceVendueModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                EvenementId = reader.GetInt32(reader.GetOrdinal("EvenementId")),
                                TypePlaceId = reader.GetInt32(reader.GetOrdinal("TypePlaceId")),
                                UtilisateurId = reader.GetInt32(reader.GetOrdinal("UtilisateurId")),
                                NumeroDePlace = reader.GetInt32(reader.GetOrdinal("NumeroDePlace")),
                                TypeReservation = reader.GetString(reader.GetOrdinal("TypeReservation")),
                                Prix = reader.GetDecimal(reader.GetOrdinal("Prix")),
                                OnCreate = reader.GetDateTime(reader.GetOrdinal("OnCreate")),
                                OnUpdate = reader.GetDateTime(reader.GetOrdinal("OnUpdate")),
                                Evenement = new EvenementModel
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("EvenementId")),
                                    Nom = reader.GetString(reader.GetOrdinal("EvenementNom")),
                                    Description = reader.GetString(reader.GetOrdinal("EvenementDescription")),
                                    Date = reader.GetDateTime(reader.GetOrdinal("EvenementDate")),
                                    Heure = reader.GetTimeSpan(reader.GetOrdinal("EvenementHeure")),
                                    Lieu = reader.GetString(reader.GetOrdinal("EvenementLieu")),
                                    ImagePath = reader.IsDBNull(reader.GetOrdinal("EvenementImagePath")) ? null : reader.GetString(reader.GetOrdinal("EvenementImagePath"))
                                },
                                TypePlace = new TypePlaceModel
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("TypePlaceId")),
                                    Type = reader.GetString(reader.GetOrdinal("TypePlaceType")),
                                    Couleurs = reader.GetString(reader.GetOrdinal("TypePlaceCouleurs"))
                                }
                            };
                            placesVendues.Add(place);
                        }
                    }
                }
            }

            return placesVendues;
        }
        public async Task<int> GetTotalCountAsync(string? search, int userId)
        {
            int totalCount;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = @"SELECT COUNT(*) FROM PlaceVendues pv
                                    JOIN Evenements e ON pv.EvenementId = e.Id
                                    WHERE (@Search IS NULL OR e.Nom LIKE '%' + @Search + '%')
                                    AND pv.UtilisateurId = @UserId
                                    AND pv.TypeReservation = 'ACHETER'
                                    AND CAST(e.Date AS DATE) >= CAST(GETDATE() AS DATE)"
                };

                command.Parameters.AddWithValue("@Search", string.IsNullOrEmpty(search) ? (object)DBNull.Value : search);
                command.Parameters.AddWithValue("@UserId", userId);

                totalCount = (int)await command.ExecuteScalarAsync();
            }

            return totalCount;
        }
    }
}