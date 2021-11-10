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


        public bool BestaatBestellingBijKlant(int BestellingId) {
            SqlConnection conn = getConnection();
            string query = "SELECT COUNT(*) FROM [WebApi].Bestellingen] WHERE Id = @id";
            using(SqlCommand cmd = new(query, conn)) {
                try {
                    cmd.Parameters.AddWithValue("@Id", BestellingId);
                    int newInt = (int)cmd.ExecuteScalar();
                    if (newInt > 0) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }catch(Exception ex) {
                    throw new KlantRepositoryADOException("KlantRepository: BestaatBestellingBijKlant(id) - gefaald", ex);
                }
            }
        }

        public void VerwijderBestelling(int id) {
            SqlConnection conn = getConnection();
            string sql = "DELETE FROM klanten WHERE Id = @Id";
            using(SqlCommand cmd = new(sql, conn)) {
                try {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }catch(Exception ex) {
                    throw new KlantRepositoryADOException("BestellingRepository: VerwijderBestelling(id) gefaald", ex);
                }
                finally {
                    conn.Close();
                }
            }
        }


        public Bestelling VoegBestellingToe(Bestelling bestelling) {
            SqlConnection conn = getConnection();
            string sql = "INSERT INTO Bestellingen(BestellingId, KlantId, Product, Aantal) VALUES (@BestellingId, @KlantId, @Product, @Aantal)";
            using(SqlCommand cmd = new(sql, conn)) {
                try {
                    conn.Open();
                    cmd.Parameters.Add("@BestellingId", SqlDbType.Int);
                    cmd.Parameters.Add("@KlantId", SqlDbType.Int);
                    cmd.Parameters.Add("@Product", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Aantal", SqlDbType.Int);
                    cmd.Parameters["@BestellingId"].Value = bestelling.BestellingID;
                    cmd.Parameters["@KlantId"].Value = bestelling.Klant.KlantID;
                    cmd.Parameters["@Product"].Value = bestelling.Product;
                    cmd.Parameters["@Aantal"].Value = bestelling.Aantal;
                    int integer = decimal.ToInt32((decimal)cmd.ExecuteScalar());
                    return new Bestelling(bestelling.BestellingID, bestelling.Product, bestelling.Aantal, bestelling.Klant);
                }catch(Exception ex) {
                    throw new KlantRepositoryADOException("KlantRepository: VoegBestellingToe - gefaald", ex);
                }
                finally {
                    conn.Close();
                }
            }
        }

       public void UpdateBestelling(Bestelling bestelling) {
            SqlConnection conn = getConnection();
            string sql = "UPDATE Bestellingen SET BestellingId = @BestellingId, Aantal = @Aantal, KlantId = @KlantId WHERE id = @Id";
            using (SqlCommand cmd = new(sql, conn)) {
                try {
                    conn.Open();
                    cmd.Parameters.Add("@BestellingId", SqlDbType.Int);
                    cmd.Parameters.Add("@KlantId", SqlDbType.Int);
                    cmd.Parameters.Add("@Product", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Aantal", SqlDbType.Int);
                    cmd.Parameters["@BestellingId"].Value = bestelling.BestellingID;
                    cmd.Parameters["@KlantId"].Value = bestelling.Klant.KlantID;
                    cmd.Parameters["@Product"].Value = bestelling.Product;
                    cmd.Parameters["@Aantal"].Value = bestelling.Aantal;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) {
                    throw new KlantRepositoryADOException("KlantRepository: VoegBestellingToe - gefaald", ex);
                }
                finally {
                    conn.Close();
                }
            }
        }

        public IEnumerable<Bestelling> GetBestellingKlant(int id) {
            SqlConnection conn = getConnection();
            string sql = "SELECT * FROM klanten k INNER JOIN Bestellingen b ON K.Id = b.KlantId WHERE k.Id = @id";
            using(SqlCommand comm = new(sql, conn)) {
                try {
                    conn.Open();
                    comm.Parameters.AddWithValue("@id", id);
                    IDataReader reader = comm.ExecuteReader();
                    Klant k = null;
                    List<Bestelling> bestellingen = new List<Bestelling>();
                    while (reader.Read()) {
                        if(k == null) {
                            k = new Klant((int)reader["Id"], (string)reader["Naam"], (string)reader["Adres"]);
                        }
                        var product = (Bier)Enum.Parse(typeof(Bier), reader["Product"].ToString(), true);
                        Bestelling b = new Bestelling((int)reader["Id"], product, (int)reader["Aantal"], k);
                        bestellingen.Add(b);
                    }
                    reader.Close();
                    return bestellingen;
                }catch(Exception ex) {
                    throw new BestellingRepositoryADOException("BestellingRepository: GetBestellingKlant(id) - gefaald", ex);
                }
                finally {
                    conn.Close();
                }
            }
        }

        public Bestelling GeefBestellingWeer(int id) {
            SqlConnection conn = getConnection();
            string sql = "SELECT * FROM klanten k INNER JOIN Bestellingen b  ON k.Id = b.KlantId WHERE k.Id = @id";
            Klant k = null;
            using(SqlCommand cmd = new(sql, conn)) {
                try {
                    conn.Open();
                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters["@Id"].Value = id;
                    IDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    if(k == null) {
                        k = new Klant((int)reader["Id"], (string)reader["Naam"], (string)reader["Adres"]);
                    }
                    var product = (Bier)Enum.Parse(typeof(Bier), reader["Product"].ToString(), true);
                    Bestelling b = new Bestelling((int)reader["Id"], product, (int)reader["Aantal"], k);
                    reader.Close();
                    return b;
                }catch(Exception ex) {
                    throw new KlantRepositoryADOException("BestellingRepository: GeefBestellingWeer(id) - gefaald", ex);
                }
                finally {
                    conn.Close();
                }
            }
        }

        public bool BestaatBestelling(Bestelling bestelling) {
            SqlConnection conn = getConnection();
            string query = "SELECT COUNT(*) FROM [WebApi].Bestellingen] WHERE Id = @id";
            using (SqlCommand cmd = new(query, conn)) {
                try {
                    cmd.Parameters.AddWithValue("@Id", bestelling.BestellingID);
                    int newInt = (int)cmd.ExecuteScalar();
                    if (newInt > 0) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                catch (Exception ex) {
                    throw new KlantRepositoryADOException("KlantRepository: BestaatBestellingBijKlant(id) - gefaald", ex);
                }
            }
        }
    }
    }
