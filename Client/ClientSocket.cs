using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class ClientSocket
    {
        private readonly Socket _clientSocket;

        public ClientSocket()
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect(string remoteIp, int remotePort)
        {
            var ipAddress = IPAddress.Parse(remoteIp);
            var endPoint = new IPEndPoint(ipAddress, remotePort);

            try
            {
                _clientSocket.Connect(endPoint);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error connecting: {e.Message}");
            }
        }

        public void SendLoop()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Message: ");
                    string text = Console.ReadLine() ?? "";

                    byte[] buffer = Encoding.UTF8.GetBytes(text);
                    _clientSocket.Send(buffer);

                    buffer = new byte[1024];
                    _clientSocket.Receive(buffer);

                    string message = Encoding.UTF8.GetString(buffer);
                    Console.WriteLine(message);

                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error sending: {e.Message}");
                }
            }
        }
    }
}