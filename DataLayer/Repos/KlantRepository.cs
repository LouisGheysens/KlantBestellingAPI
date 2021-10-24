using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using Microsoft.Data.SqlClient;

namespace DataLayer.Repos {
    public class KlantRepository : IKlantRepository {
        public bool BestaatKlant(Klant klant) {
            SqlConnection conn = DBConnection.CreateConnection();
            string query = "SELECT COUNT(1) FROM [WebApi].[dbo].[Klanten] WHERE naam = @naam";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    bool result = false;
                    cmd.CommandText = query;
                    cmd.Parameters.Add(new SqlParameter("@KlantId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@KlantId"].Value = klant.KlantID;

                    cmd.Parameters.Add(new SqlParameter("@Naam", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters["@naam"].Value = klant.Naam;

                    cmd.Parameters.Add(new SqlParameter("@Adres", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters["@adres"].Value = klant.Adres;

                    Console.WriteLine();
                    result = (int)cmd.ExecuteScalar() == 1 ? true : false;
                    return result;
                    Console.WriteLine("BestaatKlant - Geslaagd");
                }
                catch (Exception ex) {
                    throw new Exception(ex.Message);
                    Console.WriteLine("BestaatKlant - Niet geslaagd");
                }
            }
        }

        public void GetKlant(int id) {
            SqlConnection conn = DBConnection.CreateConnection();
            string query = "SELECT * FROM [WebApi].[dbo].[Klanten] WHERE KlantId = @id";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.CommandText = query;
                    cmd.Parameters.Add(new SqlParameter("@KlantId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@KlantId"].Value = id;
                    Console.WriteLine("GetKlant - geslaagd!");
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("GetKlant - Niet geslaagd");
                }
            }
        }

        public List<Klant> SelecteerKlanten() {
            throw new NotImplementedException();
        }

        public void UpdateKlant(Klant klant) {
            var conn = DBConnection.CreateConnection();
            conn.Open();
            SqlCommand comm = new SqlCommand($"update Klanten set KlantId= '" + klant.KlantID + "', Naam= " + klant.Naam + " , Adres=' " + klant.Adres + "' where Naam = " + klant.Naam + "", conn);
            try {
                comm.ExecuteNonQuery();
                Console.WriteLine("Klant is bewerkt!");

            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Klant werd niet bewerkt!");
            }
            finally {
                conn.Close();
            }
        }

        public void VerwijderKlant(Klant klant) {
            SqlConnection conn = DBConnection.CreateConnection();
            try {
                conn.Open();
                string sql = $"DELETE FROM Klanten WHERE Naam = " + klant.Naam;
                var updateCommand = new SqlCommand(sql, conn);
                updateCommand.ExecuteNonQuery();
                Console.WriteLine("Klant werd verwijderd!");
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine("Klant werd niet verwijderd");
            }
            finally {
                conn.Close();
                conn.Dispose();
            }
        }

        public void VoegKlantToe(Klant klant) {
            SqlConnection conn = DBConnection.CreateConnection();
            string query = "INSERT INTO Klanten VALUES(@Naam, @Adres)";
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
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Niet opgeslagen!");
                }
                finally {
                    conn.Close();
                }
            }
        }
    }
}
