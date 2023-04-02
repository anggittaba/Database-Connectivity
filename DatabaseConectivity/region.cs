using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseConectivity
{
    class region
    {
        static string ConnectionString = "Data Source=DESKTOP-T8JR52S;Database=db_hr_dts;Integrated Security=True;Connect Timeout=30;";

        static SqlConnection connection;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("=== MENU ===");
                Console.WriteLine("1. Tampilkan semua data region");
                Console.WriteLine("2. Tampilkan data region berdasarkan ID");
                Console.WriteLine("3. Tambah data region");
                Console.WriteLine("4. Update data region");
                Console.WriteLine("5. Hapus data region");
                Console.WriteLine("6. Keluar");

                Console.Write("Pilih menu: ");
                int menu = Convert.ToInt32(Console.ReadLine());

                switch (menu)
                {
                    case 1:
                        GetAllRegion();
                        break;
                    case 2:
                        Console.Write("Masukkan ID region: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        GetRegionById(id);
                        break;
                    case 3:
                        Console.Write("Masukkan nama region baru: ");
                        string name = Console.ReadLine();
                        InsertRegion(name);
                        break;
                    case 4:
                        GetAllRegion();
                        Console.Write("Masukkan ID region yang akan diupdate: ");
                        int updateId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Masukkan nama region baru: ");
                        string updateName = Console.ReadLine();
                        UpdateRegion(updateId, updateName);
                        break;
                    case 5:
                        GetAllRegion();
                        Console.Write("Masukkan ID region yang akan dihapus: ");
                        int deleteId = Convert.ToInt32(Console.ReadLine());
                        DeleteRegion(deleteId);
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Menu tidak tersedia!");
                        break;
                }

                Console.WriteLine();
            }
        }

        // GETALL : REGION (Command)
        // Fungsi untuk menampilkan semua data region menggunakan SqlCommand
        public static void GetAllRegion()
        {
            connection = new SqlConnection(ConnectionString);

            //Membuat instance untuk command
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM tb_region";

            //Membuka koneksi
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                Console.WriteLine("===================================");
                Console.WriteLine("| ID\t| Region Name\t|");
                Console.WriteLine("===================================");
                while (reader.Read())
                {
                    Console.WriteLine($"| {reader[0]}\t| {reader[1]}\t|");
                }
                Console.WriteLine("===================================");
            }
            else
            {
                Console.WriteLine("Data not found!");
            }
            reader.Close();
            connection.Close();
        }

        // GETBYID : REGION (Command)
        // Fungsi untuk menampilkan data region berdasarkan ID menggunakan SqlCommand
        public static void GetRegionById(int id)
        {
            connection = new SqlConnection(ConnectionString);

            //Membuat instance untuk command
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM tb_region WHERE region_id = @region_id";

            //Membuat parameter
            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@region_id";
            pId.Value = id;
            pId.SqlDbType = SqlDbType.Int;

            //Menambahkan parameter ke command
            command.Parameters.Add(pId);

            //Membuka koneksi
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                Console.WriteLine("===================================");
                Console.WriteLine("| ID\t| Region Name\t|");
                Console.WriteLine("===================================");
                while (reader.Read())
                {
                    Console.WriteLine($"| {reader[0]}\t| {reader[1]}\t|");
                }
                Console.WriteLine("===================================");
            }
            else
            {
                Console.WriteLine("Data not found!");
            }
            reader.Close();
            connection.Close();
        }


        // INSERT : REGION (Transaction)
        // Fungsi untuk menambahkan data region menggunakan SqlTransaction
        public static void InsertRegion(string name)
        {
            connection = new SqlConnection(ConnectionString);

            //Membuka koneksi
            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //Membuat instance untuk command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO tb_region (region_name) VALUES (@region_name)";
                command.Transaction = transaction;

                //Membuat parameter
                SqlParameter pName = new SqlParameter();
                pName.ParameterName = "@region_name";
                pName.Value = name;
                pName.SqlDbType = SqlDbType.VarChar;

                //Menambahkan parameter ke command
                command.Parameters.Add(pName);

                //Menjalankan command
                int result = command.ExecuteNonQuery();
                transaction.Commit();

                if (result > 0)
                {
                    Console.WriteLine("Data berhasil ditambahkan!");
                }
                else
                {
                    Console.WriteLine("Data gagal ditambahkan!");
                }

                //Menutup koneksi
                connection.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                try
                {
                    transaction.Rollback();
                }
                catch (Exception rollback)
                {
                    Console.WriteLine(rollback.Message);
                }
            }

        }

        // UPDATE : REGION (Transaction)
        // Fungsi untuk mengupdate data region menggunakan SqlTransaction
        public static void UpdateRegion(int id, string name)
        {
            connection = new SqlConnection(ConnectionString);

            //Membuka koneksi
            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //Membuat instance untuk command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "UPDATE tb_region SET region_name = @region_name WHERE region_id = @region_id";
                command.Transaction = transaction;

                //Membuat parameter
                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@region_id";
                pId.Value = id;
                pId.SqlDbType = SqlDbType.Int;

                SqlParameter pName = new SqlParameter();
                pName.ParameterName = "@region_name";
                pName.Value = name;
                pName.SqlDbType = SqlDbType.VarChar;

                //Menambahkan parameter ke command
                command.Parameters.Add(pId);
                command.Parameters.Add(pName);

                //Menjalankan command
                int result = command.ExecuteNonQuery();
                transaction.Commit();

                if (result > 0)
                {
                    Console.WriteLine("Data berhasil diupdate!");
                }
                else
                {
                    Console.WriteLine("Data gagal diupdate!");
                }

                //Menutup koneksi
                connection.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                try
                {
                    transaction.Rollback();
                }
                catch (Exception rollback)
                {
                    Console.WriteLine(rollback.Message);
                }
            }
        }

        // DELETE : REGION (Transaction)
        // Fungsi untuk menghapus data region menggunakan SqlTransaction
        public static void DeleteRegion(int id)
        {
            connection = new SqlConnection(ConnectionString);

            //Membuka koneksi
            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //Membuat instance untuk command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "DELETE FROM tb_region WHERE region_id = @region_id";
                command.Transaction = transaction;

                //Membuat parameter
                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@region_id";
                pId.Value = id;
                pId.SqlDbType = SqlDbType.Int;

                //Menambahkan parameter ke command
                command.Parameters.Add(pId);

                //Menjalankan command
                int result = command.ExecuteNonQuery();
                transaction.Commit();

                if (result > 0)
                {
                    Console.WriteLine("Data berhasil dihapus!");
                }
                else
                {
                    Console.WriteLine("Data gagal dihapus!");
                }

                //Menutup koneksi
                connection.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                try
                {
                    transaction.Rollback();
                }
                catch (Exception rollback)
                {
                    Console.WriteLine(rollback.Message);
                }
            }
        }
    }
}
