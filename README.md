# Aplikasi Database Connectivity

Aplikasi ini adalah contoh sederhana untuk mengakses database menggunakan bahasa pemrograman C#. Aplikasi ini menggunakan database SQL Server dan menggunakan library `System.Data.SqlClient` untuk mengakses database.

## Fitur

Aplikasi ini memiliki beberapa fitur, yaitu:

1. Menampilkan semua data region
2. Menampilkan data region berdasarkan ID
3. Menambahkan data region baru
4. Mengupdate data region
5. Menghapus data region
6. Keluar dari aplikasi

## Cara Menggunakan

1. Pastikan sudah terinstall SQL Server dan sudah terbuat database `db_hr_dts` link database (https://github.com/anggittaba/db_hr_dts)
2. Clone repository ini
3. Buka project menggunakan Visual Studio
4. Pastikan sudah terkoneksi dengan database dengan mengubah nilai variabel `ConnectionString` pada file `region.cs`
5. Jalankan program

## Fungsi Syntax

Berikut adalah fungsi syntax yang digunakan pada aplikasi ini:

1. `SqlConnection`: untuk membuat koneksi ke database
2. `SqlCommand`: untuk membuat perintah SQL
3. `SqlDataReader`: untuk membaca data dari database
4. `SqlTransaction`: untuk melakukan transaksi pada database

## Fungsi-Fungsi Utama

Berikut adalah fungsi-fungsi utama pada aplikasi ini:

1. `GetAllRegion()`: untuk menampilkan semua data region menggunakan `SqlCommand`
2. `GetRegionById(int id)`: untuk menampilkan data region berdasarkan ID menggunakan `SqlCommand`
3. `InsertRegion(string name)`: untuk menambahkan data region baru menggunakan `SqlTransaction`
4. `UpdateRegion(int id, string name)`: untuk mengupdate data region menggunakan `SqlTransaction`
5. `DeleteRegion(int id)`: untuk menghapus data region menggunakan `SqlTransaction`

## Kontributor

Hieronimus Anggit Taba
