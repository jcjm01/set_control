using System;
using System.Data.SQLite;
using System.IO;

namespace SetControl_WPF
{
    public class DatabaseManager
    {
        private string connectionString;
        private string logPath;

        public DatabaseManager()
        {
            // Usamos una ruta genérica segura para cualquier máquina
            string dbFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SetControlApp");
            Directory.CreateDirectory(dbFolderPath); // Crear el directorio si no existe

            string dbPath = Path.Combine(dbFolderPath, "SetControlDatabase.db");
            logPath = Path.Combine(dbFolderPath, "log.txt");

            try
            {
                // Si no existe la base de datos, la creamos
                if (!File.Exists(dbPath))
                {
                    Logger.Log(logPath, $"La base de datos no se encontró en: {dbPath}. Se creará una nueva base de datos.");
                    SQLiteConnection.CreateFile(dbPath);
                }

                connectionString = $"Data Source={dbPath};Version=3;";
                Logger.Log(logPath, "Iniciando inicialización de la base de datos.");
                InitializeDatabase();
            }
            catch (Exception ex)
            {
                Logger.Log(logPath, $"Error al inicializar la base de datos: {ex.Message}");
                throw;
            }
        }

        private void InitializeDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    Logger.Log(logPath, "Conexión a la base de datos abierta correctamente.");

                    string createUsersTable = @"CREATE TABLE IF NOT EXISTS Users (
                                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                Username TEXT NOT NULL,
                                                Password TEXT NOT NULL)";

                    string createLoginsTable = @"CREATE TABLE IF NOT EXISTS Logins (
                                                 Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                 UserId INTEGER NOT NULL,
                                                 LoginTimeJulian REAL NOT NULL,
                                                 FOREIGN KEY(UserId) REFERENCES Users(Id))";

                    string createEventsTable = @"CREATE TABLE IF NOT EXISTS Events (
                                                 Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                 UserId INTEGER NOT NULL,
                                                 EventType TEXT NOT NULL,
                                                 EventTimeJulian REAL NOT NULL,
                                                 FOREIGN KEY(UserId) REFERENCES Users(Id))";

                    using (var command = new SQLiteCommand(createUsersTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (var command = new SQLiteCommand(createLoginsTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (var command = new SQLiteCommand(createEventsTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    Logger.Log(logPath, "Tablas de base de datos creadas correctamente.");
                }
            }
            catch (SQLiteException ex)
            {
                Logger.Log(logPath, $"Error al inicializar la base de datos SQLite: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log(logPath, $"Error inesperado al inicializar la base de datos: {ex.Message}");
                throw;
            }
        }

        public void AddUser(string username, string password)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.ExecuteNonQuery();
                    }
                }

                Logger.Log(logPath, $"Usuario {username} agregado correctamente.");
            }
            catch (SQLiteException ex)
            {
                Logger.Log(logPath, $"Error al agregar usuario a la base de datos: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log(logPath, $"Error inesperado al agregar usuario: {ex.Message}");
                throw;
            }
        }

        public void LogLogin(int userId, double julianDate)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Logins (UserId, LoginTimeJulian) VALUES (@UserId, @JulianDate)";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@JulianDate", julianDate);
                        command.ExecuteNonQuery();
                    }
                }

                Logger.Log(logPath, $"Login registrado correctamente para el usuario {userId}.");
            }
            catch (SQLiteException ex)
            {
                Logger.Log(logPath, $"Error al registrar login: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log(logPath, $"Error inesperado al registrar login: {ex.Message}");
                throw;
            }
        }

        public void LogEvent(int userId, string eventType, double julianDate)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Events (UserId, EventType, EventTimeJulian) VALUES (@UserId, @EventType, @JulianDate)";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@EventType", eventType);
                        command.Parameters.AddWithValue("@JulianDate", julianDate);
                        command.ExecuteNonQuery();
                    }
                }

                Logger.Log(logPath, $"Evento {eventType} registrado correctamente para el usuario {userId}.");
            }
            catch (SQLiteException ex)
            {
                Logger.Log(logPath, $"Error al registrar evento: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log(logPath, $"Error inesperado al registrar evento: {ex.Message}");
                throw;
            }
        }
    }

    // Clase Logger para escribir logs en un archivo de texto
    public static class Logger
    {
        public static void Log(string filePath, string message)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }
    }
}
