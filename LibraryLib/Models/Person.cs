using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLib
{
    /// <summary>
    /// Struct representing someone full name
    /// </summary>
    public struct Person
    {
        //TODO: Check what's the hell is happening here
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Surname { get; set; }

        /// <summary>
        /// Initializes a new instance of Person struct by string from excel file 
        /// </summary>
        /// <param name="FullName"></param>
        public Person(string FullName)
        {
            var names = FullName.Split(' ');
            try
            {
                this.Name = names[0];
                this.SecondName = names[1];
                this.Surname = names[2];
            }
            catch (IndexOutOfRangeException)
            {
                this.Name = "Иван";
                this.SecondName = "Иванович";
                this.Surname = "Иванов";
                //throw new PersonParseException("You have not 3 words in your full name", e);
            }

        }

        /// <summary>
        /// Initializes a new instance of the Person struct to the value indicated by three string values
        /// </summary>
        /// <param name="name"></param>
        /// <param name="secondName"></param>
        /// <param name="surname"></param>
        public Person(string name, string secondName, string surname)
        {
            this.Name = name;
            this.SecondName = secondName;
            this.Surname = surname;
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>"Name SecondName Surname" string</returns>
        public override string ToString()
        {
            return $"{Name} {SecondName} {Surname}";
        }
    }

    /// <summary>
    /// Thrown when something happened while full name parsing
    /// </summary>
    [Serializable]
    public class PersonParseException : Exception
    {
        public PersonParseException() { }
        public PersonParseException(string message) : base(message) { }
        public PersonParseException(string message, Exception inner) : base(message, inner) { }
        protected PersonParseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
