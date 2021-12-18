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
            string query = "SELECT COUNT(*) FROM Bestellingen WHERE Id = @Id";
            using(SqlCommand cmd = new(query, conn)) {
                try {
                    conn.Open();
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
                finally {
                    conn.Close();
                }
            }
        }

        public void VerwijderBestelling(int id) {
            SqlConnection conn = getConnection();
            string sql = "DELETE FROM bestellingen WHERE Id = @Id";
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
            string sql = "INSERT INTO [dbo].[Bestellingen] (Product, Aantal, KlantId) VALUES (@Product, @Aantal, @KlantId) SELECT SCOPE_IDENTITY();";
            using SqlCommand command = new(sql, conn);
            conn.Open();
            SqlTransaction sqlTransaction = conn.BeginTransaction();
            try {
                command.Transaction = sqlTransaction;
                command.Parameters.Add("@Product", SqlDbType.Int);
                command.Parameters.Add("@Aantal", SqlDbType.Int);
                command.Parameters.Add("@KlantId", SqlDbType.Int);
                command.Parameters["@Product"].Value = (int)bestelling.Product;
                command.Parameters["@Aantal"].Value = bestelling.Aantal;
                command.Parameters["@KlantId"].Value = bestelling.Klant.KlantID;
                int id = Decimal.ToInt32((decimal)command.ExecuteScalar());
                sqlTransaction.Commit();
                return new Bestelling(id, (int)bestelling.Product, bestelling.Aantal, bestelling.Klant);
            }
            catch (Exception ex) {
                sqlTransaction.Rollback();
                throw new BestellingRepositoryADOException("BestellingRepository - VoegBestellingToe", ex);
            }
        }

       public void UpdateBestelling(Bestelling bestelling) {
            SqlConnection connection = getConnection();
            string sql = "UPDATE [dbo].[bestellingen] SET Product = @Product, Aantal = @Aantal, KlantId = @KlantId WHERE Id = @Id";
            using SqlCommand command = new(sql, connection);
            connection.Open();
            SqlTransaction sqlTransaction = connection.BeginTransaction();
            try {
                command.Transaction = sqlTransaction;
                command.Parameters.Add("@Product", SqlDbType.Int);
                command.Parameters.Add("@Aantal", SqlDbType.Int);
                command.Parameters.Add("@KlantId", SqlDbType.Int);
                command.Parameters.Add("@Id", SqlDbType.Int);
                command.Parameters["@Product"].Value = (int)bestelling.Product;
                command.Parameters["@Aantal"].Value = bestelling.Aantal;
                command.Parameters["@KlantId"].Value = bestelling.Klant.KlantID;
                command.Parameters["@Id"].Value = bestelling.BestellingID;
                command.ExecuteNonQuery();
                sqlTransaction.Commit();
            }
            catch (Exception ex) {
                sqlTransaction.Rollback();
                throw new BestellingRepositoryADOException("BestellingRepository - UpdateBestelling", ex);
            }
        }

        public IEnumerable<Bestelling> GetBestellingKlant(int id) {
            SqlConnection conn = getConnection();
            string sql = "SELECT k.*, b.Id AS BestellingId, " +
                "b.KlantId, b.Product, b.Aantal FROM " +
                "[dbo].[klanten] k INNER JOIN [dbo].[bestellingen] b ON k.KlantId = b.KlantId WHERE k.KlantId = @Id"; ;
            using(SqlCommand comm = new(sql, conn)) {
                try {
                    conn.Open();
                    comm.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = comm.ExecuteReader();
                    Klant k = null;
                    List<Bestelling> bestellingen = new List<Bestelling>();
                    while (reader.Read()) {
                        if(k == null)  k = new Klant((int)reader["KlantId"], (string)reader["Naam"], (string)reader["Adres"]);
                        
                        Bestelling b = new(
                            (int)reader["Id"], (int)reader["Product"], (int)reader["Aantal"], k);
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

        public Bestelling GeefBestellingWeer(int klantid, int id) {
            SqlConnection conn = getConnection();
            string sql = "SELECT k.*, b.Id AS BestellingId, b.KlantId, b.Product, b.Aantal FROM [dbo].[klanten] " +
                "k INNER JOIN [dbo].[bestellingen] b ON k.KlantId = b.KlantId WHERE k.KlantID = @KlantId AND b.Id = @Id";
            Klant klant = null;
            using SqlCommand cmd = new(sql, conn);
                try {
                    conn.Open();
                    cmd.Parameters.Add("@KlantId", SqlDbType.Int);
                    cmd.Parameters["@KlantId"].Value = klantid;
                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters["@Id"].Value = id;
                    IDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    if (klant == null) {
                        klant = new((int)reader["KlantId"], (string)reader["Naam"], (string)reader["Adres"]);
                    }
                    Bestelling bestelling = new((int)reader["BestellingId"], (int)reader["Product"], (int)reader["Aantal"], klant);
                    reader.Close();
                    return bestelling;
                }
                catch (Exception ex) {
                    throw new BestellingRepositoryADOException("BestellingWeergeven niet gelukt", ex);
                }
                finally {
                    conn.Close();
                }
        }

        public bool BestaatBestelling(Bestelling bestelling) {
            SqlConnection conn = getConnection();
            string query = "SELECT COUNT(*) FROM bestellingen WHERE Id = @Id";
            using (SqlCommand cmd = new(query, conn)) {
                try {
                    conn.Open();
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
                    throw new BestellingRepositoryADOException("BestellingRepository: BestaatBestellingBijKlant(id) - gefaald", ex);
                }
                finally {
                    conn.Close();
                }
            }
        }

        public bool HeeftBestelling(int id) {
            SqlConnection conn = getConnection();
            string sql = "SELECT COUNT(*) FROM Bestellingen WHERE KlantId = @Id";
            using(SqlCommand cmd = new(sql, conn)) {
                try {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    int n = (int)cmd.ExecuteScalar();
                    if(n > 0) {
                        return true;
                    }
                    return false;
                }catch(Exception ex) {
                    throw new BestellingRepositoryADOException("BestellingRepository: HeeftBestelling(id) - gefaald", ex);
                }
                finally {
                    conn.Close();
                }
            }
        }

        public bool BestaatBestelling(int id) {
            throw new NotImplementedException();
        }

        public bool HeeftBestellingenKlant(int id) {
            throw new NotImplementedException();
        }
    }
    }
