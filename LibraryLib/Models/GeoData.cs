﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using System.Text.RegularExpressions;

namespace LibraryLib
{
    public class GeoData
    {
        List<GeoCoordinate> coords = new List<GeoCoordinate>();
        public List<GeoCoordinate> Coordinates { get; }
        public GeoData(string geoData)
        {
            geoData = geoData.Replace("{type=MultiPoint, coordinates=", "");
            geoData = geoData.Replace("]}", "");
            geoData = geoData.Replace("[", "");
            geoData = geoData.Replace("]", "");
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
        public override string ToString()
        {
            string newStr = @"{type = MultiPoint,coordinates =";
            if (coords.Count > 1)
            {
                newStr += "[";
                for (int i = 0; i < coords.Count - 1; i++)
                    newStr += $"[{coords[i].Latitude}, {coords[i].Longitude}], ";
                newStr += $"[{coords.Last().Latitude}, {coords.Last().Longitude}]";
                newStr += "}";
                return newStr;
            }
            else return $"[[{coords.First().Latitude}, {coords.First().Longitude}]]" + "}";
        }
    }

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
}
