using DotnetWhoisCLI.Services;



namespace DotnetWhoisCLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args?.Length > 0)
            {
               string response = WhoisService.GetDomainWhoisInfo(args[0]);

                Console.WriteLine(response);
            }
            else
            {
                Console.WriteLine("Please enter a domain; example: whois github.com");
            }
        }
    }
}

