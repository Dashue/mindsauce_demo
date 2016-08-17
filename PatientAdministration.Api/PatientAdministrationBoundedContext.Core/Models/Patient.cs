
using System;

namespace PatientAdministrationBoundedContext.Core
{
    public class Patient
    {
        private Guid Id;

        public string Firstname { get; private set; }
        public string Surname { get; private set; }
        public DateTime Birthdate { get; private set; }
        public string Note { get; private set; }

        private Patient(Guid id, string firstname, string surname, DateTime birthdate, string note)
        {
            /* Private constructor to enforce data consistency. Used by exposed factory methods */
            Id = id;
            Firstname = firstname;
            Surname = surname;
            Birthdate = birthdate;
            Note = note;
        }

        public static Patient New(string firstname, string surname, DateTime birthdate, string note = "")
        {
            return new Patient(Guid.NewGuid(), firstname, surname, birthdate, note);
        }

        public static Patient Rehydrate(Guid id, string firstname, string lastname, DateTime birthdate, string note)
        {
            /*
             * Usually using Entityframework which allows for rehydrating through private properties through reflection
             * rendering this manual rehydration unnecessary
             */

            return new Patient(id, firstname, lastname, birthdate, note);
        }
    }
}
