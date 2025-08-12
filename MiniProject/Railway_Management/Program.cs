using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

namespace RailwayReservationSystem
{
    

    class Program
    {
        static int currentUserId = -1;
        static string connectionString = "Data Source=anahbbrdba-102\\SATHVIKA;Initial Catalog=RailwayReservationDB;Integrated Security=True;TrustServerCertificate=True";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n=== Railway Reservation System ===");
                Console.WriteLine("1. Register User");
                Console.WriteLine("2. Login User");
                Console.WriteLine("3.Available Trains");
                Console.WriteLine("4. Make Reservation");
                Console.WriteLine("5. Cancel Reservation");
                Console.WriteLine("6.Ticket Report");
                Console.WriteLine("7. Exit");
                Console.Write("Select Option: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1: RegisterUser(); break;
                    case 2: LoginUser(); break;
                    case 3:AvailableTrains();break;
                    case 4: MakeReservation(); break;
                    case 5: CancelReservation(); break;
                    case 6: TicketReport(); break;
                    case 7: return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        static void RegisterUser()
        {
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Console.Write("Salt: ");
            string salt = Console.ReadLine();
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Phone: ");
            string phone = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_register_user", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mailid", email);
                cmd.Parameters.AddWithValue("@passwordhash", password); 
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@phone", phone);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader["message"]);
                }
            }
        }

        static void LoginUser()
        {
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_authenticate_user", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mailid", email);
                cmd.Parameters.AddWithValue("@passwordhash", password);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    currentUserId = Convert.ToInt32(reader["userid"]); 
                    Console.WriteLine($"Login Successful! Welcome {reader["username"]}");
                }
                else
                {
                    Console.WriteLine("Invalid credentials.");
                }
            }
        }
        static void AvailableTrains()
        {
            if (currentUserId == -1)
            {
                Console.WriteLine("Please Login First");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_get_available_trains", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write($"{reader.GetName(i),-15}"); 
                    }
                    Console.WriteLine();
                    Console.WriteLine(new string('-', reader.FieldCount * 15)); 

                    
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write($"{reader[i],-15}");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }






        static void MakeReservation()
        {
            if (currentUserId == -1)
            {
                Console.WriteLine("Please Login Firts");
                return;
            }

            Console.Write("Username ");
            string username = Console.ReadLine();
            Console.Write("Passenger Name: ");
            string passengerName = Console.ReadLine();
            Console.Write("Travel Date (yyyy-mm-dd): ");
            DateTime travelDate = Convert.ToDateTime(Console.ReadLine());
            if (travelDate.Date < DateTime.Today)
            {
                Console.WriteLine("Error: Travel date cannot be in the past.");
                return;  
            }

            Console.Write("Train No: ");
            int trainNo = Convert.ToInt32(Console.ReadLine());
            Console.Write("Class Code: ");
            string classCode = Console.ReadLine();
            Console.Write("Seats Booked: ");
            int seatsBooked = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_make_reservation", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@passengername", passengerName);
                cmd.Parameters.AddWithValue("@traveldate", travelDate);
                cmd.Parameters.AddWithValue("@trainno", trainNo);
                cmd.Parameters.AddWithValue("@classcode", classCode);
                cmd.Parameters.AddWithValue("@seatsbooked", seatsBooked);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    foreach (var col in Enumerable.Range(0, reader.FieldCount))
                        Console.Write($"{reader.GetName(col)}: {reader[col]}   ");
                    Console.WriteLine();
                }
            }
        }

        static void CancelReservation()
        {
            if (currentUserId == -1)
            {
                Console.WriteLine("Please login first!");
                return;
            }

            Console.Write("Booking ID: ");
            int bookingId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Are you sure you want to cancel the ticket? (Y/N): ");
            string confirmation = Console.ReadLine().Trim().ToUpper();

            if (confirmation != "Y")
            {
                Console.WriteLine("Cancellation aborted.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_cancel_reservation", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bookingid", bookingId);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                            Console.Write($"{reader.GetName(i)}: {reader[i]}   ");
                        Console.WriteLine();
                    }
                }
            }
        }

        static void TicketReport()
        {
            if (currentUserId == -1)
            {
                Console.WriteLine("Please login first!");
                return;
            }

            Console.Write("Booking ID: ");
            int bookingId = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_get_booking_details", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bookingid", bookingId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    foreach (var col in Enumerable.Range(0, reader.FieldCount))
                        Console.WriteLine($"{reader.GetName(col)}: {reader[col]}");
                    Console.WriteLine();
                }
            }
        }
    }
}

