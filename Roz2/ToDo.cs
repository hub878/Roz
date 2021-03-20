using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace DemoSource
{
    internal class ToDo
    {
        public IEnumerable<(Account, Person)>
        MatchPersonToAccount(
        IEnumerable<Group> groups,
        IEnumerable<Account> accounts,
        IEnumerable<string> emails)
        {
            HashSet<Person> hpeople = new HashSet<Person>();
            HashSet<Account> haccounts = new HashSet<Account>(accounts);
            HashSet<string> hemails = new HashSet<string>(emails);

            //groups.ToList().ForEach(x => hpeople.A(x.));

            List<Person> list = new List<Person>();
            foreach (var item in groups)
            {
                list.AddRange(item.People);
            }

            hpeople = new HashSet<Person>(list);
            List<(Account, Person)> ret = new List<(Account, Person)>();

            foreach (var email in emails)
            {
                var pers = hpeople.SingleOrDefault( x => 
                    x.Emails.Any(y => y.Email == email)
                );
                var acc = haccounts.Where(x => x.EmailAddress.Email == email).ToList();

                acc.ToList().ForEach(x => ret.Add( (x,pers)));
            }
            return ret;

        }
    }
}