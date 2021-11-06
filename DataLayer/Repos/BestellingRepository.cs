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
using BusinessLayer.Enums;

namespace DataLayer.Repos {
    public class BestellingRepository: IBestellingRepository {

        private string connectionString;

        public BestellingRepository(string connectionString) {
            this.connectionString = connectionString;
        }

        private SqlConnection getConnection() {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        
        public bool BestaatBestelling(Bestelling bestelling) {
            SqlConnection conn = getConnection();
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
                    throw new BestellingRepositoryADOException("Bestellingrepository: BestaatBestelling - gefaald!", ex);
                }
                finally {
                    conn.Close();
                }
            }
        }


        public bool BestaatBestelling(int id) {
                SqlConnection conn = getConnection();
            string query = "SELECT COUNT(1) FROM [WebApi].[dbo].[Bestellingen] WHERE BestellingId = @id";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    bool result = false;
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int));
                    cmd.Parameters["@id"].Value = id;

                    Console.WriteLine();
                    result = (int)cmd.ExecuteScalar() == 1 ? true : false;
                    return result;
                }
                catch (Exception ex) {
                    throw new BestellingRepositoryADOException("Bestellingrepository: BestaatBestelling(id) - gefaald!", ex);
                }
                finally {
                    conn.Close();
                }
            }
        }

        public void GetBestelling(int id) {
            SqlConnection conn = getConnection();
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
                    throw new BestellingRepositoryADOException("Bestellingrepository: GetBestelling(id) - gefaald!",ex);
                }
                finally {
                    conn.Close();
                }
            }
        }

        public List<Bestelling> GetBestellingKlant(int id) {
            var dic = new Dictionary<int, Bestelling>();
            string query = "SELECT t1.*, t2.Naam FROM bestellingen t1"
                + " INNER JOIN Klanten t2 on t1.klantId=t2.Id WHERE t1.klantId=@klantId";
            SqlConnection conn = getConnection();
            using (SqlCommand cmd = new SqlCommand(query, conn)) {
                try {
                    List<Bestelling> bestellingen = new List<Bestelling>();
                    conn.Open();
                    cmd.Parameters.Add(new SqlParameter("@klantId", id));
                    IDataReader datareader = cmd.ExecuteReader();
                    Klant klant = null;
                    while (datareader.Read()) {
                        if (klant == null) {
                            klant = new Klant((string)datareader["Naam"], (string)datareader["Adres"]);
                        }

                        var prod = (Bier)Enum.Parse(typeof(Bier), datareader["Product"].ToString(), true);
                        Bestelling best = new Bestelling(prod, (int)datareader["aantal"], klant);

                    }
                    return bestellingen;
                }
                catch (Exception ex) {
                    throw new BestellingRepositoryADOException("Bestellingrepository: GetBestellingKlant(id) - gefaald", ex);
                }
                finally {
                    conn.Close();
                }
            }
        }


        //Klopt!
        public void UpdateBestelling(Bestelling bestelling) {
            SqlConnection conn = getConnection();
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
                    throw new BestellingRepositoryADOException("BestellingRepository: UpdateBestelling - gefaald!", ex);
                }
                finally {
                    conn.Close();
                }
            }
        }

        //Klopt!
        public void VerwijderBestelling(Bestelling bestelling) {
            SqlConnection conn = getConnection();
            try {
                conn.Open();
                string sql = $"DELETE FROM Bestellingen WHERE BestellingId = " + bestelling.BestellingID;
                var updateCommand = new SqlCommand(sql, conn);
                updateCommand.ExecuteNonQuery();
                Console.WriteLine("Bestelling werd verwijderd!");
            }
            catch (Exception ex) {
                throw new BestellingRepositoryADOException("Bestellingrepository: VerwijderBestelling - gefaald!", ex);
            }
            finally {
                conn.Close();
                conn.Dispose();
            }
        }

        //Klopt
        public void VoegBestellingToe(Bestelling bestelling) {
            SqlConnection conn = getConnection();
            string query = "INSERT INTO Bestellingen(KlantId, Product, Aantal)) VALUES(@KlantId, @Product, @Aantal)";
            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    cmd.CommandText = query;
                    cmd.Parameters.Add(new SqlParameter("@KlantId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@Product", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@Aantal", SqlDbType.Int));
                    cmd.Parameters["KlantId"].Value = bestelling.Klant.KlantID;
                    cmd.Parameters["@Product"].Value = bestelling.Product;
                    cmd.Parameters["@Aantal"].Value = bestelling.Aantal;
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Bestelling werd toegevoegd!");
                }
                catch (Exception ex) {
                    throw new BestellingRepositoryADOException("Bestellingrepository: VoegBestellingToe - gefaald!", ex);
                }
                finally {
                    conn.Close();
                    }
                }
            }
    }
    }
