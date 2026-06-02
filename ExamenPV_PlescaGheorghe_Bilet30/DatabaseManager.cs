using System;
using System.Data;
using System.Data.SqlClient;

namespace AplicatieCafenea
{
    public class DatabaseManager
    {
        private readonly string connectionString = @"Data Source=DESKTOP-C24JO84;Initial Catalog=master;Integrated Security=True";

        public bool creazaBazaDeDate()
        {
            string query1 = @"CREATE DATABASE bioprinz";
            string query2 = @"USE bioprinz; CREATE TABLE comenzi (
                                id INT IDENTITY(1,1) PRIMARY KEY,
                                nrcomanda NVARCHAR(50) NOT NULL,
                                datacomanda DATE NOT NULL,
                                adresaclient NVARCHAR(250) NOT NULL,
                                felintai NVARCHAR(50) NOT NULL,
                                feldoi NVARCHAR(50) NOT NULL,
                                desert NVARCHAR(150) NOT NULL
                            );";
            try
            {
                SqlConnection conn1 = new SqlConnection(connectionString);
                SqlCommand cmd1 = new SqlCommand(query1, conn1);
                conn1.Open();
                cmd1.ExecuteNonQuery();
                conn1.Close();
           

                SqlConnection conn2 = new SqlConnection(connectionString);
                SqlCommand cmd2 = new SqlCommand(query2, conn2);
                conn2.Open();
                cmd2.ExecuteNonQuery();
                conn2.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Eroare SQL ( Probabil Baza de date deja exista ): {ex.Message}");
            }
            return false;
        }
        public bool InsereazaComanda(string nrComanda, DateTime data, string adresa, string felul1, string felul2, string desert)
        {
            string query = @"USE bioprinz; INSERT INTO comenzi (nrcomanda, datacomanda, adresaclient, felintai, feldoi, desert) 
                            VALUES (@NrComanda, @DataComanda, @AdresaClient, @FelulIntai, @FelulDoi, @Desert)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NrComanda", nrComanda);
                        cmd.Parameters.AddWithValue("@DataComanda", data);
                        cmd.Parameters.AddWithValue("@AdresaClient", adresa);
                        cmd.Parameters.AddWithValue("@FelulIntai", felul1);
                        cmd.Parameters.AddWithValue("@FelulDoi", felul2);
                        cmd.Parameters.AddWithValue("@Desert", desert);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Eroare SQL Inserare: {ex.Message}");
            }
        }

        public bool StergeComanda(string nrComanda)
        {
            string query = "USE bioprinz; DELETE FROM comenzi WHERE nrcomanda = @NrComanda";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NrComanda", nrComanda);

                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Eroare SQL Ștergere: {ex.Message}");
            }
        }

        public DataTable ToateComenzile()
        {
            string query = "USE bioprinz; SELECT id, nrcomanda, datacomanda, adresaclient, felintai, feldoi, desert FROM comenzi ORDER BY id";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Eroare SQL Interogare: {ex.Message}");
            }
        }
    }
}