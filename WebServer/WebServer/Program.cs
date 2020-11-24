using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading.Tasks;

//Developed by Filippo Alessandrini

namespace WebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // gets THIS computer's IP addresses
            IPAddress ipAddress = ipHostInfo.AddressList[1]; // selects IPv4 IP address
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 57348); // new remote endpoint
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp); // new listener

            listener.Bind(remoteEP); // binds the remote EndPoint
            listener.Listen(10); // the server listens for a maximum of 10 clients connected at the same time
            while (true)
            {
                Socket connectedUser = listener.Accept(); // the connection is accepted, and a new user is created
                Console.WriteLine("New user connected: " + connectedUser.RemoteEndPoint); // writes the client's IP address
                StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/pages/page.html"); // streamreader that will read the HTML page
                string page = "HTTP/1.0 200 OK\r\n\r\n" + sr.ReadToEnd(); // reads the HTML page that has to be sent to the client and adds the HTTP header
                connectedUser.Send(Encoding.ASCII.GetBytes(page)); // sends the encoded webpage
                sr.Close(); // closes the streamreader
            }
        }
    }
}
