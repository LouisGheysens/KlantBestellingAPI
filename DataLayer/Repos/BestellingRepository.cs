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
        public bool BestaatBestelling(Bestelling bestelling) {
            SqlConnection conn = DBConnection.CreateConnection();
            string query = "SELECT COUNT(1) FROM [WebApi].[dbo].[Bestellingen] WHERE naam = @naam";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    bool result = false;
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new SqlParameter("@Product", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters["@Product"].Value = bestelling.Product;

                    cmd.Parameters.Add(new SqlParameter("@Aantal", System.Data.SqlDbType.Int));
                    cmd.Parameters["@Aantal"].Value = bestelling.Aantal;

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
            string query = "SELECT * FROM [WebApi].[dbo].[Bestelingen] WHERE BestelID = @id";
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

        public Klant GetBestellingFromSpecificKlant(Klant klant) {
            throw new NotImplementedException();
        }

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
                        Bestelling bestelling = new Bestelling((int)datareader["Id"], BusinessLayer.Enums.Bier.Leffe, (int)datareader["Aantal"], k);
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

        public void UpdateBestelling(Bestelling bestelling) {
            var conn = DBConnection.CreateConnection();
            conn.Open();
            SqlCommand comm = new SqlCommand($"update Bestellingen set BesteID= '" + bestelling.BestellingID + "', Product= " + bestelling.Product + " , Aantal=' " + bestelling.Aantal + "' where Klant = " + bestelling.Klant + "", conn);
            try {
                comm.ExecuteNonQuery();
                Console.WriteLine("Bestelling is bewerkt!");

            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Bestelling werd niet bewerkt!");
            }
            finally {
                conn.Close();
            }
        }

        public void VerwijderBestelling(Bestelling bestelling) {
            SqlConnection conn = DBConnection.CreateConnection();
            try {
                conn.Open();
                string sql = $"DELETE FROM Bestellingen WHERE ID = " + bestelling.BestellingID;
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

        public void VoegBestellingToe(Bestelling bestelling) {
            SqlConnection conn = DBConnection.CreateConnection();
            string query = "INSERT INTO Bestellingen VALUES(@BestlelID, @Product, @Aantal)";
            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@BestlelID", bestelling.BestellingID);
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
