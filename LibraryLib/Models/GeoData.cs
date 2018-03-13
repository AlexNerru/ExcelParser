using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using System.Text.RegularExpressions;

namespace LibraryLib
{
    /// <summary>
    /// Class for geo information
    /// </summary>
    public class GeoData
    {
        /// <summary>
        /// String that need to be removed
        /// </summary>
        string[] toRemove = new string[5] { "{type=MultiPoint, coordinates=" , "{type=MultiPoint,coordinates=" , "]}","[","]"};

        /// <summary>
        /// All coordinates of library
        /// </summary>
        List<GeoCoordinate> coords = new List<GeoCoordinate>();
        /// <summary>
        /// Returns coors
        /// </summary>
        public List<GeoCoordinate> Coordinates { get => coords; }
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="geoData"></param>
        public GeoData(string geoData)
        {
            if (geoData == string.Empty)
                geoData = "{type = MultiPoint, coordinates =[[37.512935741337, 55.792942379791]]}";
            foreach (var item in toRemove)
                geoData = geoData.Replace(item, "");
            var allGeo = Regex.Split(geoData, ", ");
            for (int i = 0; i < allGeo.Length; i++)
            {
                try
                {                    
                    coords.Add(new GeoCoordinate(double.Parse(allGeo[i]), double.Parse(allGeo[++i])));
                }
                catch (FormatException e)
                {
                    throw new GeoParseException("Problem with coords", e);
                }
                catch (Exception e)
                {
                    throw new GeoParseException("Something strange", e);
                }
            }
        }
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        public GeoData(double latitude, double longitude)
        {
            try
            {
                coords.Add(new GeoCoordinate(latitude, longitude));
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw new CoordsException("Coords are not between -90 and 90", e);
            }
        }
        /// <summary>
        /// To string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string newStr = @"{type=MultiPoint,coordinates=";
            if (coords.Count > 1)
            {
                newStr += "[";
                for (int i = 0; i < coords.Count - 1; i++)
                    newStr += $"[{coords[i].Latitude}, {coords[i].Longitude}], ";
                newStr += $"[{coords.Last().Latitude}, {coords.Last().Longitude}]";
                newStr += "]}";
                return newStr;
            }
            else return newStr+$"[[{coords.First().Latitude}, {coords.First().Longitude}]]" + "}";
        }
    }

    /// <summary>
    /// Thrown when string is not in right format
    /// </summary>
    [Serializable]
    public class GeoParseException : Exception
    {
        public GeoParseException() { }
        public GeoParseException(string message) : base(message) { }
        public GeoParseException(string message, Exception inner) : base(message, inner) { }
        protected GeoParseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// Thrown when coordinates values are incorrect
    /// </summary>
    [Serializable]
    public class CoordsException : Exception
    {
        public CoordsException() { }
        public CoordsException(string message) : base(message) { }
        public CoordsException(string message, Exception inner) : base(message, inner) { }
        protected CoordsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
