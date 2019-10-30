using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jokes;
/// <summerVacation>
/// www.Amotels.co.il 
/// Amotels = Amit + Hotels (Revolutionary, I know).
/// </winterVacation>
namespace Amotels
{
    /// <summary>
    /// This class represents an exception where the ratings entered by a user is invalid.
    /// </summary>
    public class InvalidRatingExeption : Exception
    {
        /// <summary>
        /// The constructor for the InvalidRatingException class.
        /// </summary>
        public InvalidRatingExeption() : base("Rating must be between 1 and 5")
        {
        }
    }
    /// <summary>
    /// This class represents an exception where The time entered for a village is invalid 
    /// (the starting time must be smaller then the ending time and both must be between 1 and 52).
    /// </summary>
    public class InvalidTimeExeption : Exception
    {
        /// <summary>
        /// The constructor for the InvalidTimeException.
        /// </summary>
        public InvalidTimeExeption() : base("Time inserted is invalid. time is a week between 1 and 52, make sure starting week and ending week ain't the same")
        {

        }
    }
}
