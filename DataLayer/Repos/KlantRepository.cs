using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using BusinessLayer.Enums;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using Microsoft.Data.SqlClient;

namespace DataLayer.Repos {
    public class KlantRepository : IKlantRepository {
        private SqlConnection sqlConnection;

        public KlantRepository(SqlConnection sqlConnection) {
            this.sqlConnection = sqlConnection;
        }

        //Klopt!
        public bool BestaatKlant(Klant klant) {
            SqlConnection conn = DBConnection.CreateConnection();
            string query = "SELECT COUNT(1) FROM [WebApi].[dbo].[Klanten] WHERE KlantId = @KlantId";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    bool result = false;
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new SqlParameter("@KlantId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@KlantId"].Value = klant.KlantID;

                    Console.WriteLine();
                    result = (int)cmd.ExecuteScalar() == 1 ? true : false;
                    return result;
                }
                catch (Exception ex) {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool BestaatKlantId(int id) {
            SqlConnection conn = DBConnection.CreateConnection();
            string query = "SELECT COUNT(1) FROM [WebApi].[dbo].[Klanten] WHERE KlantId = @KlantId";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    bool result = false;
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new SqlParameter("@KlantId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@KlantId"].Value = id;

                    Console.WriteLine();
                    result = (int)cmd.ExecuteScalar() == 1 ? true : false;
                    return result;
                }
                catch (Exception ex) {
                    throw new Exception(ex.Message);
                }
            }
        }

        public Klant GetKlant(int id) {
            SqlConnection connection = DBConnection.CreateConnection();
            string query = "SELECT * FROM dbo.Klanten k LEFT JOIN dbo.Bestellingen b ON k.BestellingId=b.Id WHERE Id=@Id";
            using (SqlCommand command = connection.CreateCommand()) {
                try {
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    command.Parameters["@Id"].Value = id;
                    command.CommandText = query;
                    connection.Open();
                    Klant klant = null;
                    Bestelling bestelling = null;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) {
                        if (klant == null) {
                            string naam = (string)reader["Naam"];
                            string adres = (string)reader["Adres"];
                            klant = new Klant(id, naam, adres);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Id"))) {
                            int aantal = (int)reader["Aantal"];
                            string product = (string)reader["Product"];
                            bestelling = new Bestelling((Bier)Enum.Parse(typeof(Enum), product), aantal, klant);
                        }
                        //klant.VoegBestellingToe(bestelling);
                    }
                    return klant;
                }
                catch (Exception ex) {
                    throw new KlantRepositoryADOException("KlantRepository- GetKlant", ex);
                }
                finally {
                    connection.Close();
                }
            }
        }
        
        //Klopt!
        public List<Klant> SelecteerKlanten() {
            var conn = DBConnection.CreateConnection();
            List<Klant> klanten = new List<Klant>();
            string query = "SELECT * FROM Klanten";
            using(SqlCommand comm = conn.CreateCommand()) {
                try {
                    conn.Open();
                    comm.CommandText = query;
                    SqlDataReader r = comm.ExecuteReader();
                    while (r.Read()) {
                        klanten.Add(new Klant((string)r["Naam"], (string)r["Adres"]));
                    }
                    return klanten;
                }
                catch(Exception ex) {
                    throw new KlantRepositoryADOException("KlantRepository: SelecteerKlanten - gefaald!");
                }
                finally {
                    conn.Close();
                }
            }
        }

        //Klopt!
        public void UpdateKlant(Klant klant) {
            var conn = DBConnection.CreateConnection();
            string query = "UPDATE Klanten SET Naam=@Naam, Adres=@Adres WHERE KlantId=@KlantId";
            using(SqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();
                    cmd.Parameters.Add("@KlantId", SqlDbType.Int);
                    cmd.Parameters.Add("@Naam", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Adres", SqlDbType.NVarChar);
                    cmd.CommandText = query;
                    cmd.Parameters["@KlantId"].Value = klant.KlantID;
                    cmd.Parameters["@Naam"].Value = klant.Naam;
                    cmd.Parameters["@Adres"].Value = klant.Adres;
                    cmd.ExecuteNonQuery();
                }catch(Exception ex) {
                    throw new KlantRepositoryADOException("KlantRepository: UpdateKlant - gefaald!");
                }
                finally {
                    conn.Close();
                }
            }
        }

        //Klopt!
        public void VerwijderKlant(Klant klant) {
            SqlConnection conn = DBConnection.CreateConnection();
            string query = "DELETE FROM Klanten WHERE KlantId=@KlantId";
            using (SqlCommand comm = new(query, conn)) {
                try {
                    conn.Open();
                    comm.Parameters.Add(new SqlParameter("@KlantId", SqlDbType.Int));
                    comm.Parameters["@KlantId"].Value = klant.KlantID;
                    comm.ExecuteNonQuery();
                    Console.WriteLine("Klant werd verwijderd!");
                }
                catch (Exception ex) {
                    throw new KlantRepositoryADOException("Klantrepository: VerwijderKlant - gefaald!");
                }
                finally {
                    conn.Close();
                }
            }
        }

        //Klopt!
        public Klant VoegKlantToe(Klant klant) {
            SqlConnection conn = DBConnection.CreateConnection();
            string query = "INSERT INTO Klanten(Naam, Adres) VALUES(@Naam, @Adres)";
            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@Naam", klant.Naam);
                    cmd.Parameters.AddWithValue("@Adres", klant.Adres);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Klant werd toegevoegd!");
                }
                catch (Exception ex) {
                    throw new KlantRepositoryADOException("Klantrepository: VoegKlantToe - gefaald!");

                }
                finally {
                    conn.Close();
                }
            }
            return klant;
        }
    }
}
