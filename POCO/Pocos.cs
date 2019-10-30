using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Amotels.POCO
{
    // Plain Old C# Object
        /// <summary>
        /// A class representing a village like it would apear in the data base.
        /// </summary>
    public class Village
    {
        /// <summary>
        /// A constructor for the Village class that recives a parameter for each of its propertys.
        /// </summary>
        /// <param name="id">The villages id, is not an auto num</param>
        /// <param name="name">The name of the village</param>
        /// <param name="country">The country where the village lies</param>
        /// <param name="city">The city where the village lies</param>
        /// <param name="street">The street where the village lies</param>
        /// <param name="houseNumber">The house number of the village</param>
        /// <param name="mainActivityID">The number representing the main activity of the village</param>
        /// <param name="startingTime">The week when the village opens</param>
        /// <param name="endingTime">The week when the village closes</param>
        /// <param name="rating">The rating of the village</param>
        public Village(int id, string name, string country, string city, string street, int houseNumber, int mainActivityID, int startingTime, int endingTime, double rating)
        {
            Id = id;
            Name = name;
            Country = country;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            MainActivityID = mainActivityID;
            StartingTime = startingTime;
            EndingTime = endingTime;
            Rating = rating;
        }


        /// <summary>
        /// A constructor for the Village class that recives another village and copy's it (a cloning method).
        /// </summary>
        /// <param name="other">The village that we copy</param>
        public Village(Village other)
        {
            Id = other.Id;
            Name = other.Name;
            Country = other.Country;
            City = other.City;
            Street = other.Street;
            HouseNumber = other.HouseNumber;
            MainActivityID = other.MainActivityID;
            StartingTime = other.StartingTime;
            EndingTime = other.EndingTime;
            Rating = other.Rating;
        }
        /// <summary>
        /// A method that returns a string representing the village.
        /// </summary>
        /// <returns>The village details in a string form</returns>
        public override string ToString()
        {
            return $@"
""{Name}"" Village 
- ID: {Id}
- Address: {Country}, {Street}, {HouseNumber}
- Main Activity: {ClubMedDBAccess.GetActivityName(MainActivityID)}
- Starting Week: {StartingTime}
- Ending Week: {EndingTime}
- Rating: {Rating}/5 stars
";
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int MainActivityID { get; set; }    
        public int StartingTime { get; set; }
        public int EndingTime { get; set; }
        public double Rating { get; set; }
    }

    /// <summary>
    /// A class representing a Customer like it would apear in the data base.
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        /// <summary>
        /// A constructor for the Customer class that recives a parameter for each of its propertys.
        /// </summary>
        /// <param name="id">The id of the customer</param>
        /// <param name="first">The first name of the customer</param>
        /// <param name="last">The last name of the customer</param>
        public Customer(int id, string first, string last)
        {
            Id = id;
            FirstName = first;
            LastName = last;
        }
    }


    /// <summary>
    /// A class representing the detaiks of a village like they would apear in the data base.
    /// </summary>
    public class VillageDetails
    {
        /// <summary>
        /// A constructor for the VillageDetails class that recives a paramater for each of its propertys.
        /// </summary>
        /// <param name="villageID">The id of the corresponding village</param>
        /// <param name="roomType">The number representing the room type</param>
        /// <param name="numOfRooms">The number of rooms of the corresponding room type</param>
        /// <param name="roomPrice">The price of the rooms of that type</param>
        public VillageDetails(int villageID, int roomType, int numOfRooms, int roomPrice)
        {
            VillageID = villageID;
            RoomType = roomType;
            NumOfRooms = numOfRooms;
            RoomPrice = roomPrice;
        }
        public int VillageID { get; set; }
        public int RoomType { get; set; }
        public int NumOfRooms { get; set; }
        public int RoomPrice { get; set; }


    }

    /// <summary>
    /// A class including all the details necessary for the ReportDetails method in ClubMedDBAccess.
    /// </summary>
    public class ReportDetails
    {
        /// <summary>
        /// A constructor for the ReportDetails class that recives a paramater for each of its propertys.
        /// </summary>
        /// <param name="village">A village from the database transformed into a village object in c#</param>
        /// <param name="averageOccupied">The average occupation of the village above, in precent</param>
        /// <param name="roomsOrdered">The total amout of orders the village above recived</param>
        public ReportDetails (Village village, double averageOccupied, int roomsOrdered)
        {
            this.village = new Village(village);
            this.averageOccupied = averageOccupied;
            this.roomsOrdered = roomsOrdered;
        }
        public Village village { get; set; }
        public double averageOccupied { get; set; }
        public int roomsOrdered { get; set; }

        /// <summary>
        /// A method that returns a string representing the ReportDetails.
        /// </summary>
        /// <returns>A string version of the ReportDetails class</returns>
        public override string ToString() {
            return $@"
Village {village.Name} #{village.Id}:
 - Average Occupation: {averageOccupied.ToString("F")} %
 - Rooms Ordered: {roomsOrdered}";


        }
    }

}

/// <summary>
/// random namespace we made as a joke
/// </summary>
namespace Jokes
{
    
}

