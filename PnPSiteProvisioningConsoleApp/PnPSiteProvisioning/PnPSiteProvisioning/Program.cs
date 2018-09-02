using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PnPSiteProvisioning
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleColor defaultForeground = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Enter the source site : ");
            Console.ForegroundColor = defaultForeground;
            string sourceUrl = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Enter the target site : ");
            Console.ForegroundColor = defaultForeground;
            string targetUrl = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Enter the username : ");
            Console.ForegroundColor = defaultForeground;
            string username = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Enter the password : ");
            Console.ForegroundColor = defaultForeground;
            SecureString password = GetPasswordFromConsole();

            using (var context = new ClientContext(sourceUrl))
            {
                context.Credentials = new SharePointOnlineCredentials(username, password);
                Web web = context.Web;
                context.Load(web, w => w.Title);
                context.ExecuteQueryRetry();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Your site title is ",context.Web.Title);
                Console.ForegroundColor = defaultForeground;
            }
            Console.ReadLine();
        }

        private static SecureString GetPasswordFromConsole()
        {
            SecureString securePassword = new SecureString();
            ConsoleKeyInfo info;

            do
            {
                info = Console.ReadKey(true);

                if (info.Key != ConsoleKey.Enter)
                    securePassword.AppendChar(info.KeyChar);
            } while (info.Key != ConsoleKey.Enter);

            return securePassword;
        }
    }
}
