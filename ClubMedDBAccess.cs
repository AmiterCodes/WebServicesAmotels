using System;
using System.Collections.Generic;
using Amotels.POCO;
using System.Data;
namespace Amotels
{
    /// <summary>
    /// The class that writes the sql to be sent to the dataBase, the paramaters used here come from
    /// the class "UI".
    /// </summary>
   public class ClubMedDBAccess
    {
        private static string PROVIDER = @"Microsoft.ACE.OLEDB.12.0";
        private static string PATH = @"..\..\DBAccess\GroupProjectDB.accdb";
        
        /// <summary>
        /// An exeption regarding whether a connection was successful or not        
        /// </summary>
        private class ConnectionException : Exception
        {
            public ConnectionException() : base("Could not open connection.")
            {
                
            }
        }

        /// <summary>
        /// Gets the name of the room coresponding the to given number
        /// </summary>
        /// <param name="roomType">The number representing the room</param>
        /// <returns>The name of the room</returns>
        public static string GetRoomType(int roomType)
        {
            DBHelper dbHelper = new DBHelper(PROVIDER, PATH);
            if (!dbHelper.OpenConnection()) throw new ConnectionException();
            DataTable tb = dbHelper.GetDataTable("SELECT roomTypes.description FROM roomTypes WHERE roomTypes.roomType = " + roomType);
            if (tb.Rows.Count == 0) throw new ArgumentException($"Room type '{roomType}' does not exist.");
            dbHelper.CloseConnection();
            return (string) tb.Rows[0]["description"];
        }

        /// <summary>
        /// This method checks if the id sent to it belongs to a customer in the database and returns true if yes and false otherwise.
        /// </summary>
        /// <param name="id">The id that we check</param>
        /// <returns>True if there is a customer for that id, false otherwise</returns>
        public static bool IsCustomer(int id)
        {
            DBHelper helper = new DBHelper(PROVIDER, PATH);
            helper.OpenConnection();
            DataTable tb = helper.GetDataTable($"SELECT costumers.id FROM costumers WHERE costumers.id = {id}");
            helper.CloseConnection();
            if (tb.Rows.Count < 1) return false;
            return true;
        }

        /// <summary>
        /// A method that inserts a customer into the dataBase
        /// </summary>
        /// <param name="customer">An object representing a customer (see POCO for more details)</param>
        public static void InsertCustomer(Customer customer)
        {
            DBHelper helper = new DBHelper(PROVIDER, PATH);
            if (!helper.OpenConnection()) throw new ConnectionException();
            int data = helper.WriteData($@"INSERT INTO costumers (id, firstName, lastName) VALUES(""{customer.Id}"", ""{customer.FirstName}"", ""{customer.LastName}"");");
            if (data == -1) throw new ArgumentException("Insertion failed");
            helper.CloseConnection();
        }

        /// <summary>
        /// Insert a village into the data table
        /// </summary>
        /// <param name="village">The village being inserted</param>
        /// <param name="details">The details regarding the diffrent room types the village has</param>
        public static void InsertVillage(Village village, params VillageDetails[] details)
        {

            DBHelper dbHelper = new DBHelper(PROVIDER, PATH);



            if (dbHelper.OpenConnection())
            {
                    string sql = "INSERT INTO village (villageID, villageName, country, city," +  
                                "street, houseNumber, mainActivityID, startingTime, endingTime, rating)" +
                                $@" VALUES (""{village.Id}"", ""{village.Name}"", ""{village.Country}"", ""{village.City}"", ""{
                                village.Street}"", ""{village.HouseNumber}"", ""{village.MainActivityID}"", ""{village.StartingTime}"", ""{village.EndingTime
                                }"", ""{village.Rating}"");";
                    int writeData = dbHelper.WriteData(sql);
                dbHelper.CloseConnection();
                if (writeData == -1) throw new Exception("Something went wrong, failed to insert the village");
                if (dbHelper.OpenConnection())
                {
                    foreach (VillageDetails det in details)
                    {
                        
                        sql = "INSERT INTO villageDetails (villageID, roomType, numOfRooms, roomPrice)" +
                           "VALUES (" + det.VillageID + ", " + det.RoomType + ", " + det.NumOfRooms + ", " + det.RoomPrice + ");";
                        writeData = dbHelper.WriteData(sql);
                        if (writeData == -1) throw new Exception("Something went wrong, failed to insert the village");
                    }
                    dbHelper.CloseConnection();
                }
            } else
            {
                dbHelper.CloseConnection();
                throw new ConnectionException();
            }
            dbHelper.CloseConnection();

        }
        /// <summary>
        /// Returns a list of all the possible acticitys in the database in the form of a string.
        /// </summary>
        /// <returns>The list of all activitys</returns>
        public static string GetActivities()
        {
            DBHelper dbHelper = new DBHelper(PROVIDER, PATH);
            if (!dbHelper.OpenConnection()) throw new ConnectionException();
            DataTable tb = dbHelper.GetDataTable("SELECT * FROM activities");

            string output = "Available Activities: ";

            for(int i = 0; i<  tb.Rows.Count; i++)
            {
                
               output +=  " - " + tb.Rows[i]["activityID"] + " - " + tb.Rows[i]["description"] + "\n";
            }
            dbHelper.CloseConnection();
            return output;
        }

        /// <summary>
        /// Gets the name that coresponds to the number given.
        /// </summary>
        /// <param name="activity">The number representing the wanted activity</param>
        /// <returns>The name of the activity</returns>
        public static string GetActivityName(int activity)
        {

            DBHelper dbHelper = new DBHelper(PROVIDER, PATH);
            if (!dbHelper.OpenConnection()) throw new ConnectionException();
            string activitys = (string)dbHelper.GetDataTable("SELECT description FROM" + 
                "activities WHERE activityID = " + activity).Rows[0]["description"];
            dbHelper.CloseConnection();
            return activitys;
        }
        /// <summary>
        /// Searchs for a village/villages with an avalible room using the given paramaters.
        /// </summary>
        /// <param name="country">The country where the villages are</param>
        /// <param name="week">The week you would like to order in</param>
        /// <param name="roomID">The number representing the type of room you like find</param>
        /// <returns>The list of villages that met the conditions</returns>
        public static List<Village> SearchVillage(string country, int week, int roomID)
        {
            List<Village> list = new List<Village>();
            DBHelper dbHelper = new DBHelper(PROVIDER, PATH);

            if (!dbHelper.OpenConnection()) throw new ConnectionException();

            DataTable db = dbHelper.GetDataTable("SELECT village.*, villageDetails.numOfRooms " +
                            "FROM villageDetails " +
                            " INNER JOIN village" +
                            " ON villageDetails.villageID = village.villageID" +
                            $" WHERE village.[country] = '{country.ToLower()}' " +
                            $" AND villageDetails.[roomType] = {roomID} " +
                            $" AND {week} >= village.[startingTime] " +
                            $" AND {week} <= village.[endingTime]");

            for (int i = 0; i < db.Rows.Count; i++)
            {
                DataRow row = db.Rows[i];

                int villageID = (int)row["villageID"];

                DataTable tb = dbHelper.GetDataTable($"SELECT orders.orderID FROM orders WHERE week = {week} AND villageID = {villageID}");
                if(tb.Rows.Count < (int) row["numOfRooms"])
                list.Add(ParseVillage(row));  
            }
            dbHelper.CloseConnection();
            return list;
        }
            /// <summary>
            /// Parses a data row into a village object (see POCO for the class).
            /// </summary>
            /// <param name="row">The row that represend a village in the data table</param>
            /// <returns>A village object</returns>
            public static Village ParseVillage(DataRow row)
            {
            return new Village(
                (int)row["villageID"],
                (string)row["villageName"],
                (string)row["country"],
                (string)row["city"],
                (string)row["street"],
                (int)row["houseNumber"],
                (int)row["mainActivityID"],
                (int)row["startingTime"],
                (int)row["endingTime"],
                (int)row["rating"]);
            }
        /// <summary>
        /// Orders a room.
        /// </summary>
        /// <param name="villageID">ID of the village</param>
        /// <param name="customerId">Customer ID of the customer ordering</param>
        /// <param name="week">Week of stay</param>
        /// <param name="roomTypeID"></param>
        /// <returns>Whether the ordering procces was successful or not</returns>
        public static bool OrderRoom(int villageID, int customerId ,int week, int roomTypeID)
        {
        DBHelper dbHelper = new DBHelper(PROVIDER, PATH);
        if (dbHelper.OpenConnection())
        {
            
                DataTable db = dbHelper.GetDataTable($"SELECT * FROM village WHERE villageID = {villageID}");

                if (db.Rows.Count == 0)
                {
                    throw new ArgumentException("village does not exist.");
                }

                int startingTime = (int)db.Rows[0]["startingTime"];
                int endingTime = (int)db.Rows[0]["endingTime"];

                if (week < startingTime || week > endingTime)
                {
                    throw new ArgumentException("the village is closed in this week");
                }
                string sql = "SELECT * FROM villageDetails WHERE villageID = " + villageID
                    + " AND roomType = " + roomTypeID;
                db = dbHelper.GetDataTable(sql);
                DataTable tb = dbHelper.GetDataTable($"SELECT orders.orderID FROM orders WHERE week = {week} AND villageID = {villageID}");
                if (tb.Rows.Count >= (int)db.Rows[0]["numOfRooms"])
{
                    throw new ArgumentException("there are no rooms available.");
                }
                sql = "INSERT INTO orders (villageID, costumerID, week, roomType, price)" +
                "VALUES (" + villageID + ", " + customerId + ", " + week + ", " + roomTypeID + "," +
                "" + (int)db.Rows[0]["roomPrice"] + ")";
                int data = dbHelper.WriteData(sql);
                if (data == -1) throw new ArgumentException("Data Insertion Error");
                dbHelper.CloseConnection();
                return true;
        }
            dbHelper.CloseConnection();
            return false;

    }

        /// <summary>
        /// Returns a summary of All villages, the amout of orders from each village and 
        /// the average amout of rooms ordered for each week in precent.
        /// </summary>
        /// <returns>A ReportDetails object that contains all the information of stated above 
        /// (see POCO for the object).</returns>
        public static List<ReportDetails> YearlyReport ()
        {
            List<ReportDetails> reportDetails = new List<ReportDetails>();     
            DBHelper dbHelper = new DBHelper(@"Microsoft.ACE.OLEDB.12.0", 
                @"..\..\DBAccess\GroupProjectDB.accdb");
            if (dbHelper.OpenConnection())
            {
                string[] sql = { "SELECT * FROM orders;", "SELECT * FROM village"
                        , "SELECT * FROM villageDetails" };
                DataSet db = dbHelper.GetDataSet(sql);


                Village[] villages = new Village[db.Tables[1].Rows.Count]; //An array of all the villages in the table
                int[] numOfOrders = new int[villages.Length]; //An array of the number of orders from each village, regardless of time or what room was ordered.
                                                              // The index of each position corresponds to that of the corresponding village in the villages array.
                double[] averageOccupied = new double[villages.Length]; //An array that keeps the average amout of rooms ordered from each hotel, by precent. 
                                                                        // The index of each position corresponds to that of the corresponding number of orders/number of rooms in their arrays 
                                                                        //(Both of which corospond to the same village in their index).
                int[] totalNumOfRooms = new int[villages.Length]; //An array that keeps the total number of rooms each village has, regardless of size.
                                                                  // The index of each position corresponds to that of the corresponding village in the villages array.
                for (int i = 0; i < villages.Length; i++)
                {
                    villages[i] = ParseVillage(db.Tables[1].Rows[i]);
                    
                }
                for (int i = 0; i < db.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < villages.Length; j++)
                    {
                        if ((int)db.Tables[0].Rows[i]["villageID"] == villages[j].Id)
                            numOfOrders[j]++;  
                    }
                }
                for (int i = 0; i < db.Tables[2].Rows.Count; i++)
                {
                    for (int j = 0; j < villages.Length; j++)
                    {

                        if ((int)db.Tables[2].Rows[i]["villageID"] == villages[j].Id)
                        {
                            totalNumOfRooms[j] += (int)db.Tables[2].Rows[i]["numOfRooms"];
                        }
                    }
                }
                for (int i = 0; i < averageOccupied.Length; i++)
                {

                    averageOccupied[i] = ((double)numOfOrders[i] / (villages[i].EndingTime - villages[i].StartingTime)) * (100.0 / totalNumOfRooms[i]);
                }
                for (int i = 0; i < villages.Length; i++)
                {
                    reportDetails.Add(new ReportDetails(new Village(villages[i]), averageOccupied[i], numOfOrders[i]));
                }
            }
            dbHelper.CloseConnection();
            return reportDetails;
        }
    }
}