using BusinessLayer;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repos {
    public class BestellingRepository: IBestellingRepository {
        private SqlConnection sqlConnection;

        public BestellingRepository(SqlConnection sqlConnection) {
            this.sqlConnection = sqlConnection;
        }

        public bool BestaatBestelling(Bestelling bestelling) {
            SqlConnection conn = DBConnection.CreateConnection();
            string query = "SELECT COUNT(1) FROM [WebApi].[dbo].[Bestellingen] WHERE BestellingId = @BestellingId";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    bool result = false;
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new SqlParameter("@BestellingId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BestellingId"].Value = bestelling.BestellingID;

                    Console.WriteLine();
                    result = (int)cmd.ExecuteScalar() == 1 ? true : false;
                    return result;
                }
                catch (Exception ex) {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void GetBestelling(int id) {
            SqlConnection conn = DBConnection.CreateConnection();
            string query = "SELECT * FROM [WebApi].[dbo].[Bestelingen] WHERE BestellingID = @id";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.CommandText = query;
                    cmd.Parameters.Add(new SqlParameter("@BestelID", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BestelID"].Value = id;
                    Console.WriteLine("GetBestelling - geslaagd!");
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("GetBestelling - Niet geslaagd");
                }
            }
        }

        public List<Bestelling> GetBestellingKlant(int id) {
            throw new NotImplementedException();
        }

        //Klopt!
        public List<Bestelling> SelecteerBestellingen(int klantId) {
            SqlConnection conn = DBConnection.CreateConnection();
            string query = "SELECT * FROM [dbo].[Klanten] g INNER JOIN [dbo].[Bestellingen] s on g.KlantId = s.KlantId" +
                "WHERE g.KlantId = @klantId";
            using(SqlCommand comm = conn.CreateCommand()) {
                try {
                    List<Bestelling> klantlijst = new List<Bestelling>();
                    conn.Open();
                    comm.Parameters.AddWithValue("@klantId", klantId);
                    IDataReader datareader = comm.ExecuteReader();
                    Klant k = null;
                    while (datareader.Read()) {
                        if(k == null) {
                            k = new Klant((string)datareader["Naam"], (string)datareader["Adres"]);
                        }
                        Bestelling bestelling = new Bestelling((int)datareader["Id"], BusinessLayer.Enums.Bier.Duvel, (int)datareader["Aantal"], k); ;
                        klantlijst.Add(bestelling);
                    }
                    datareader.Close();
                    return klantlijst;
               }catch(Exception ex) {
                    throw new BestellingRepositoryADOException("Bestellingrepository: SelecteeerBestellingen - gefaald!");
                }
                finally {
                    conn.Close();
                }
            }
        }

        //Klopt!
        public void UpdateBestelling(Bestelling bestelling) {
            var conn = DBConnection.CreateConnection();
            string query = "UPDATE Bestellingen SET BestellingId=@BestellingId, KlantId=@KlantId, Product=@Product, " +
                "Aantal=@Aantal WHERE BestellingId=@BestellingId";
            using(SqlCommand comm = conn.CreateCommand()) {
                try {
                    conn.Open();
                    comm.Parameters.Add("@BestellingId", SqlDbType.Int);
                    comm.Parameters.Add("@KlantId", SqlDbType.Int);
                    comm.Parameters.Add("@Product", SqlDbType.NVarChar);
                    comm.Parameters.Add("@Aantal", SqlDbType.Int);
                    comm.CommandText = query;
                    comm.Parameters["@BestellingId"].Value = bestelling.BestellingID; ;
                    comm.Parameters["@KlantId"].Value = bestelling.Klant.KlantID;
                    comm.Parameters["@Product"].Value = bestelling.Product;
                    comm.Parameters["@Aantal"].Value = bestelling.Aantal;
                    comm.ExecuteNonQuery();
                }catch(Exception ex) {
                    throw new BestellingRepositoryADOException("BestellingRepository: UpdateBestelling - gefaald!");
                }
                finally {
                    conn.Close();
                }
            }
        }

        //Klopt!
        public void VerwijderBestelling(Bestelling bestelling) {
            SqlConnection conn = DBConnection.CreateConnection();
            try {
                conn.Open();
                string sql = $"DELETE FROM Bestellingen WHERE BestellingId = " + bestelling.BestellingID;
                var updateCommand = new SqlCommand(sql, conn);
                updateCommand.ExecuteNonQuery();
                Console.WriteLine("Bestelling werd verwijderd!");
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine("Bestelling werd niet verwijderd");
            }
            finally {
                conn.Close();
                conn.Dispose();
            }
        }

        //Klopt
        public void VoegBestellingToe(Bestelling bestelling) {
            SqlConnection conn = DBConnection.CreateConnection();
            string query = "INSERT INTO Bestellingen(KlantId, Product, Aantal)) VALUES(@KlantId, @Product, @Aantal)";
            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@KlantId", bestelling.Klant.KlantID);
                    cmd.Parameters.AddWithValue("@Product", bestelling.Product);
                    cmd.Parameters.AddWithValue("@Aantal", bestelling.Aantal);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Bestelling werd toegevoegd!");
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Bestelling werd niet toegevoegd!");
                }
                finally {
                    conn.Close();
                    }
                }
            }
        }
    }
