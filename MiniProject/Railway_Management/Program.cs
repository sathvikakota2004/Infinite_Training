using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace RailwayReservationSystem
{
    class Program
    {
        static int currentUserId = -1;
        static string currentUserRole = "";
        static string connectionString = "Data Source=anahbbrdba-102\\SATHVIKA;Initial Catalog=RailwayReservationDB;Integrated Security=True;TrustServerCertificate=True";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("================= Railway Reservation System =================");
                Console.WriteLine("1. Admin Login");
                Console.WriteLine("2. User Login");
                Console.WriteLine("3. Admin SignUp");
                Console.WriteLine("4.User Signup");
                Console.WriteLine("5. Exit");
                Console.Write("Select Option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Login("admin");
                        break;
                    case "2":
                        Login("user");
                        break;
                    case "3":
                        RegisterAdmin();
                        break;
                    case "4":RegisterUser();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
        static void RegisterAdmin()
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

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_register_user", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mailid", email);
                cmd.Parameters.AddWithValue("@passwordhash", password);
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@role", "admin"); 

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader["message"]);
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

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_register_user", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mailid", email);
                cmd.Parameters.AddWithValue("@passwordhash", password);
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@role", "user");

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader["message"]);
                }
            }
        }


       
        static void Login(string expectedRole)
        {
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_authenticate_user", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mailid", email);
                cmd.Parameters.AddWithValue("@passwordhash", password);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string role = reader["role"].ToString().ToLower();
                    if (role != expectedRole)
                    {
                        Console.WriteLine($"Access denied! This account is not a {expectedRole}.");
                        return;
                    }

                    currentUserId = Convert.ToInt32(reader["userid"]);
                    currentUserRole = role;
                    Console.WriteLine($"Login Successful! Welcome {reader["username"]} ({role})");

                    if (role == "admin")
                        ShowAdminMenu();
                    else
                        ShowUserMenu();
                }
                else
                {
                    Console.WriteLine("Please enter valid username and password!!!!!.");
                }
            }
        }

        // Admin Menu
        static void ShowAdminMenu()
        {
            while (true)
            {
                Console.WriteLine("==== Admin Menu ====");
                Console.WriteLine("1.View all Trains");
                Console.WriteLine("2. Add Train");
                Console.WriteLine("3. Update Train");
                Console.WriteLine("4. Delete Train");
                Console.WriteLine("5. View All Bookings");
                Console.WriteLine("6. View All Cancellations");
                Console.WriteLine("7. Logout");
                Console.Write("Select Option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":AvailableTrains();
                        break;
                    case "2": 
                        AddTrain();
                        break;
                    case "3": 
                        UpdateTrain(); 
                        break;
                    case "4":
                        DeleteTrain(); 
                        break;
                    case "5": 
                        
                        
                       ViewAllBookings();
                        break;
                    case "6":
                        ViewAllCancellations(); 
                        break;
                    case "7":
                        return;
                    default: 
                        Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        // User Menu
        static void ShowUserMenu()
        {
            while (true)
            {
                Console.WriteLine("\n==== User Menu ====");
                Console.WriteLine("1. View Available Trains");
                Console.WriteLine("2. Make Reservation");
                Console.WriteLine("3. Cancel Reservation");
                Console.WriteLine("4. Ticket Report");
                Console.WriteLine("5. Logout");
                Console.Write("Select Option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": 
                        
                        AvailableTrains(); 
                        break;
                    case "2": 
                        MakeReservation();
                        break;
                    case "3": 
                        CancelReservation();
                        break;
                    case "4": 
                        TicketReport(); 
                        
                        break;
                    case "5": 
                        return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        // ======================= Admin Functions =======================
        static void AddTrain()
        {
            Console.Write("Train No: ");
            int trainNo = Convert.ToInt32(Console.ReadLine());
            Console.Write("Train Name: ");
            string trainName = Console.ReadLine();
            Console.Write("Source: ");
            string source = Console.ReadLine();
            Console.Write("Destination: ");
            string destination = Console.ReadLine();
            Console.Write("Number of seat classes to add: ");
            int numClasses = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < numClasses; i++)
            {
                Console.Write("Class Code (SL, 3A, 2A, 1A, 2S): ");
                string classCode = Console.ReadLine();
                Console.Write("Max Seats: ");
                int maxSeats = Convert.ToInt32(Console.ReadLine());
                Console.Write("Available Seats: ");
                int availableSeats = Convert.ToInt32(Console.ReadLine());
                Console.Write("Cost Per Seat: ");
                decimal costPerSeat = Convert.ToDecimal(Console.ReadLine());

                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("sp_add_train", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@trainno", trainNo);
                    cmd.Parameters.AddWithValue("@trainname", trainName);
                    cmd.Parameters.AddWithValue("@source", source);
                    cmd.Parameters.AddWithValue("@destination", destination);
                    cmd.Parameters.AddWithValue("@classcode", classCode);
                    cmd.Parameters.AddWithValue("@maxseats", maxSeats);
                    cmd.Parameters.AddWithValue("@availableseats", availableSeats);
                    cmd.Parameters.AddWithValue("@costperseat", costPerSeat);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Train added successfully.");
                }
            }
        }

        static void UpdateTrain()
        {
            Console.WriteLine("Enter the Train no:");
            int trainno = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Train Name:");
            string trainName = Console.ReadLine();
            Console.WriteLine("Enter Source:");
            string source = Console.ReadLine();
            Console.WriteLine("Enter Destination:");
            string destination = Console.ReadLine();

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_update_train", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@trainno", trainno);

                
                cmd.Parameters.AddWithValue("@trainname", string.IsNullOrWhiteSpace(trainName) ?   (object)DBNull.Value : trainName);
                cmd.Parameters.AddWithValue("@source", string.IsNullOrWhiteSpace(source) ? (object)DBNull.Value : source);
                cmd.Parameters.AddWithValue("@destination", string.IsNullOrWhiteSpace(destination) ? (object)DBNull.Value : destination);

                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Train updated successfully.");
            }

        }

        static void DeleteTrain()
        {
            Console.Write("Train No to delete: ");
            int trainNo = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_delete_train", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@trainno", trainNo);
                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Train deleted successfully.");
            }
        }

        static void ViewAllBookings()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("sp_get_all_reservations", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("No bookings found.");
                            return;
                        }

                        
                        List<object[]> rows = new List<object[]>();
                        int colCount = reader.FieldCount;
                        int[] widths = new int[colCount];

                     
                        for (int i = 0; i < colCount; i++)
                            widths[i] = reader.GetName(i).Length;

                       
                        while (reader.Read())
                        {
                            object[] values = new object[colCount];
                            reader.GetValues(values);
                            rows.Add(values);

                            for (int i = 0; i < colCount; i++)
                            {
                                string val = values[i]?.ToString() ?? "";
                                if (val.Length > widths[i])
                                    widths[i] = val.Length;
                            }
                        }

                       
                        for (int i = 0; i < colCount; i++)
                            Console.Write(reader.GetName(i).PadRight(widths[i] + 2));
                        Console.WriteLine();

                      
                        for (int i = 0; i < colCount; i++)
                            Console.Write(new string('-', widths[i]) + "  ");
                        Console.WriteLine();

                       
                        foreach (var row in rows)
                        {
                            for (int i = 0; i < colCount; i++)
                            {
                                string val = row[i]?.ToString() ?? "";
                                Console.Write(val.PadRight(widths[i] + 2));
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void ViewAllCancellations()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_get_all_cancelled", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    Console.WriteLine("No cancellations found.");
                    return;
                }

                List<object[]> rows = new List<object[]>();
                int colCount = reader.FieldCount;
                int[] widths = new int[colCount];

                for (int i = 0; i < colCount; i++)
                    widths[i] = reader.GetName(i).Length;

                while (reader.Read())
                {
                    object[] values = new object[colCount];
                    reader.GetValues(values);
                    rows.Add(values);

                    for (int i = 0; i < colCount; i++)
                    {
                        string val = values[i]?.ToString() ?? "";
                        if (val.Length > widths[i])
                            widths[i] = val.Length;
                    }
                }

                for (int i = 0; i < colCount; i++)
                    Console.Write(reader.GetName(i).PadRight(widths[i] + 2));
                Console.WriteLine();

                for (int i = 0; i < colCount; i++)
                    Console.Write(new string('-', widths[i]) + "  ");
                Console.WriteLine();

                foreach (var row in rows)
                {
                    for (int i = 0; i < colCount; i++)
                    {
                        string val = row[i]?.ToString() ?? "";
                        Console.Write(val.PadRight(widths[i] + 2));
                    }
                    Console.WriteLine();
                }
            }
        }

        // ======================= User Functions =======================
        static void AvailableTrains()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_get_all_booking", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                  
                    Console.WriteLine(
                        $"{"trainno",-8} | {"trainname",-20} | {"source",-15} | {"destination",-15} | classes_info"
                    );
                    Console.WriteLine(new string('-', 90));

                    while (reader.Read())
                    {
                        string trainno = reader["trainno"].ToString();
                        string trainname = reader["trainname"].ToString();
                        string source = reader["source"].ToString();
                        string destination = reader["destination"].ToString();
                        string classesInfo = reader["classes_info"].ToString();

                        
                        string[] classParts = classesInfo.Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries);

                       
                        Console.WriteLine(
                            $"{trainno,-8} | {trainname,-20} | {source,-15} | {destination,-15} | {classParts[0]}"
                        );

                        
                        for (int i = 1; i < classParts.Length; i++)
                        {
                            Console.WriteLine(
                                $"{"",-8} | {"",-20} | {"",-15} | {"",-15} | {classParts[i]}"
                            );
                        }

                       
                        Console.WriteLine(new string('-', 90));
                    }
                }
            }
        }



        static void MakeReservation()
        {
            try
            {
               
                if (currentUserId == -1)
                {
                    Console.WriteLine("Please Login First");
                    return;
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("sp_get_all_booking", con)) 
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine(
                            $"{"trainno",-8} | {"trainname",-20} | {"source",-15} | {"destination",-15} | classes_info"
                        );
                        Console.WriteLine(new string('-', 90));

                        while (reader.Read())
                        {
                            string trainno = reader["trainno"].ToString();
                            string trainname = reader["trainname"].ToString();
                            string source = reader["source"].ToString();
                            string destination = reader["destination"].ToString();
                            string classesInfo = reader["classes_info"].ToString();

                            string[] classParts = classesInfo.Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries);

                            Console.WriteLine(
                                $"{trainno,-8} | {trainname,-20} | {source,-15} | {destination,-15} | {classParts[0]}"
                            );

                            for (int i = 1; i < classParts.Length; i++)
                            {
                                Console.WriteLine(
                                    $"{"",-8} | {"",-20} | {"",-15} | {"",-15} | {classParts[i]}"
                                );
                            }
                            Console.WriteLine(new string('-', 90));
                        }
                    }
                }

               
                Console.Write("Username: ");
                string username = Console.ReadLine();


                
                DateTime travelDate;

                while (true)
                {
                    Console.Write("Travel Date (yyyy-mm-dd): ");
                    if (DateTime.TryParse(Console.ReadLine(), out travelDate))
                    {
                        if (travelDate.Date >= DateTime.Today)
                        {
                            break; 
                        }
                        else
                        {
                            Console.WriteLine("Error: Travel date cannot be in the past. Please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format. Please enter in yyyy-mm-dd format.");
                    }
                }


                Console.Write("Train No: ");
                int trainNo = Convert.ToInt32(Console.ReadLine());

                Console.Write("Class Code: ");
                string classCode = Console.ReadLine();

                Console.Write("Seats Booked: ");
                int seatsBooked = Convert.ToInt32(Console.ReadLine());

                if (seatsBooked <= 0)
                {
                    Console.WriteLine("Seats booked must be at least 1.");
                    return;
                }

                List<string> passengerNames = new List<string>();
                for (int i = 1; i <= seatsBooked; i++)
                {
                    Console.Write($"Passenger Name {i}: ");
                    string passengerName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(passengerName))
                    {
                        Console.WriteLine("Passenger name cannot be empty.");
                        i--;
                        continue;
                    }
                    passengerNames.Add(passengerName);
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    foreach (string passengerName in passengerNames)
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_make_reservation", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@username", username);
                            cmd.Parameters.AddWithValue("@passengername", passengerName);
                            cmd.Parameters.AddWithValue("@traveldate", travelDate);
                            cmd.Parameters.AddWithValue("@trainno", trainNo);
                            cmd.Parameters.AddWithValue("@classcode", classCode);
                            cmd.Parameters.AddWithValue("@seatsbooked", 1);

                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                foreach (var col in Enumerable.Range(0, reader.FieldCount))
                                    Console.Write($"{reader.GetName(col)}: {reader[col]}   ");
                                Console.WriteLine();
                            }
                            reader.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                Console.WriteLine("=========Ticket Report================");
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
