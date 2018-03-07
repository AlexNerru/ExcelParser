﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLib
{
    public struct Person
    {
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
            catch (IndexOutOfRangeException e)
            {
                throw new NotFullNameProvidedException("You have not 3 words in your full name", e);
                }

        }
        public override string ToString()
        {
            return $"{Name} {SecondName} {Surname}";
        }
    }

    [Serializable]
    public class NotFullNameProvidedException : Exception
    {
        public NotFullNameProvidedException() { }
        public NotFullNameProvidedException(string message) : base(message) { }
        public NotFullNameProvidedException(string message, Exception inner) : base(message, inner) { }
        protected NotFullNameProvidedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}