using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLib
{
    public struct Person
    {
        //TODO: Check what's the hell is happening here
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Surname { get; set; }
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
        public Person(string name, string secondName, string surname)
        {
            this.Name = name;
            this.SecondName = secondName;
            this.Surname = surname;
        }
        public override string ToString()
        {
            return $"{Name} {SecondName} {Surname}";
        }
    }

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
