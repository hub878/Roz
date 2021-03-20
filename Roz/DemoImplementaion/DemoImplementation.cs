using System.Collections.Generic;
using DemoTarget;
using DemoSource;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Linq;
using System;

namespace DemoImplementation
{
    /*
     Odpowiedź na pytanie: Mapowanie tego ypu może zostać użyte podczas mapowania objektu bazodanowego
     np. z EntityFramework na objekt modelu widkou w MVC. Potrzebna jest wowczas sanityzacja tekstu w 
     celu zapobierzenia atakom XSS. Nie wiem o co chodzi EmailType zrobiłem założenie że chodzi o serwer(wiem że raczej błędnie).

         */
    public class DemoImplementation
    {
        public static IEnumerable<PersonWithEmail> Flatten(IEnumerable<Person> people)
        {
            List<PersonWithEmail> pwe = new List<PersonWithEmail>();
            
            foreach (var item in people)
            {
                var email = item.Emails?.FirstOrDefault();
                pwe.Add(new PersonWithEmail() { 
                 SanitizedNameWithId = $"{sanitizeString(item.Name)}{sanitizeString(item.Id)}",
                 FormattedEmail = getEmail(email)//$"{email.Email??""}@{email.EmailType??""}"
                });
            }

            return pwe;
        }

        private static string getEmail(EmailAddress email)
        {
            if (email != null && !String.IsNullOrEmpty(email.Email) 
                && !String.IsNullOrEmpty(email.EmailType))
            {
                return $"{email.Email}@{email.EmailType}";
            }
            else
                return "";
        }

        private static string sanitizeString(string input)
        {
            Regex lt = new Regex("<");
            Regex gt = new Regex(">");
            Regex am = new Regex("&");
            Regex ap = new Regex("\'");
            Regex qt = new Regex("\"");

            input = am.Replace(input, "&amp;");
            input = lt.Replace(input, "&lt;");
            input = gt.Replace(input, "&gt;");
            input = ap.Replace(input, "&apos;");
            input = qt.Replace(input, "	&quot;");



            return input;
        }

    }
}