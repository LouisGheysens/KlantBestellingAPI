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

        private string connectionString;

        public KlantRepository(string connectionString) {
            this.connectionString = connectionString;
        }

        private SqlConnection getConnection() {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        public bool BestaatKlantId(int id) {
            SqlConnection conn = getConnection();
            string query = "SELECT COUNT(*) FROM klanten WHERE Id=@Id";
            using (SqlCommand cmd = new(query, conn)) {
                try {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    int r = (int)cmd.ExecuteScalar();
                    if(r > 0) {
                        return true;
                    }
                    return false;
                }catch(Exception ex) {
                    throw new KlantRepositoryADOException("KlantRepository: BestaatKlatnId(id) - gefaald", ex);
                }
                finally {
                    conn.Close();
                }
            }
        }

        public Klant GetKlant(int id) {
            SqlConnection connection = getConnection();
            string query = "SELECT * FROM dbo.Klanten WHERE KlantId=@Id";
            using (SqlCommand command = new(query,connection)) {
                try {
                    connection.Open();
                    command.Parameters.AddWithValue("@Id", id);
                    IDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Klant klant = new Klant((int)reader["KlantId"], (string)reader["Naam"], (string)reader["Adres"]);
                    reader.Close();
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

        public void UpdateKlant(Klant klant) {
            SqlConnection conn = getConnection();
            string query = "UPDATE Klanten SET Naam=@Naam, Adres=@Adres WHERE Id=@Id";
            using(SqlCommand cmd = new(query, conn)) {
                try {
                    conn.Open();
                    cmd.Parameters.Add("@Naam", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Adres", SqlDbType.NVarChar);
                    cmd.Parameters["@Naam"].Value = klant.Naam;
                    cmd.Parameters["@Adres"].Value = klant.Adres;
                    cmd.Parameters["@Id"].Value = klant.KlantID;
                    cmd.ExecuteNonQuery();
                }catch(Exception ex) {
                    throw new KlantRepositoryADOException("KlantRepository: UpdateKlant - gefaald!", ex);
                }
                finally {
                    conn.Close();
                }
            }
        }

        //Klopt!
        public void VerwijderKlant(int id) {
            SqlConnection conn = getConnection();
            string query = "DELETE FROM Klanten WHERE Id=@Id";
            using (SqlCommand comm = new(query, conn)) {
                try {
                    conn.Open();
                    comm.Parameters.AddWithValue("@Id", id);
                    comm.ExecuteNonQuery();
                }
                catch (Exception ex) {
                    throw new KlantRepositoryADOException("Klantrepository: VerwijderKlant - gefaald!", ex);
                }
                finally {
                    conn.Close();
                }
            }
        }

        public Klant VoegKlantToe(Klant klant) {
            SqlConnection conn = getConnection();
            string query = "INSERT INTO Klanten(Naam, Adres) VALUES(@Naam, @Adres) SELECT SCOPE_IDENTITY();";
            using (SqlCommand cmd = new(query, conn)) {
                try {
                    conn.Open();
                    cmd.Parameters.Add("@Naam", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Adres", SqlDbType.NVarChar);
                    cmd.Parameters["@Naam"].Value = klant.Naam;
                    cmd.Parameters["@Adres"].Value = klant.Adres;
                    int id = decimal.ToInt32((decimal)cmd.ExecuteScalar());
                    return new Klant(id, klant.Naam, klant.Adres);
                }
                catch (Exception ex) {
                    throw new KlantRepositoryADOException("Klantrepository: VoegKlantToe - gefaald!", ex);

                }
                finally {
                    conn.Close();
                }
            }
        }
    }
}
