using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Text.RegularExpressions;

namespace DotnetWhoisCLI.Services
{
    internal class WhoisService
    {
        private readonly static string ianaWhoisURL = "whois.iana.org";

        private static string GetTldWhoisServer(string domain)
        {
            string resp = GetWhoisInformation(ianaWhoisURL, domain);

            string whoisServer = "";

            if(!string.IsNullOrWhiteSpace(resp))
            {
                var reg = Regex.Match(resp, @"whois:\s+([a-zA-Z0-9._-]+)");

                if (reg?.Success == true && reg.Groups?.Count > 1)
                    whoisServer = reg.Groups[1].Value;
            }

            return whoisServer;
        }

        private static string GetWhoisInformation(string whoisServer, string url)
        {
            StringBuilder stringBuilderResult = new StringBuilder();
            TcpClient tcpClinetWhois = new TcpClient(whoisServer, 43);
            NetworkStream networkStreamWhois = tcpClinetWhois.GetStream();
            BufferedStream bufferedStreamWhois = new BufferedStream(networkStreamWhois);
            StreamWriter streamWriter = new StreamWriter(bufferedStreamWhois);

            streamWriter.WriteLine(url);
            streamWriter.Flush();

            StreamReader streamReaderReceive = new StreamReader(bufferedStreamWhois);

            while (!streamReaderReceive.EndOfStream)
                stringBuilderResult.AppendLine(streamReaderReceive.ReadLine());

            tcpClinetWhois?.Close();
            networkStreamWhois?.Dispose();
            bufferedStreamWhois?.Dispose();

            return stringBuilderResult.ToString();
        }

        public static string GetDomainWhoisInfo(string domain)
        {
            string whoisServer = GetTldWhoisServer(domain);

            if (!string.IsNullOrWhiteSpace(whoisServer))
            {
                string resp = GetWhoisInformation(whoisServer, domain);

                return resp;
            }
            else
            {
                return "Failed to query on whois server. Please try again later.";
            }
        }
    }
}
