using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCP_Chat
{
    public class ServerSocket
    {
        private readonly Socket _serverSocket;
        private readonly IPEndPoint _serverEndpoint;

        public ServerSocket(string ip, int port)
        {
            IPAddress ipAddress = IPAddress.Parse(ip);

            _serverSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            _serverEndpoint = new IPEndPoint(ipAddress, port);
        }

        public void BindAndListen(int queueLimit)
        {
            try
            {
                _serverSocket.Bind(_serverEndpoint);
                _serverSocket.Listen(queueLimit);
                Console.WriteLine($"Server listening on {_serverEndpoint}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error binding and listening: {e.Message}");
            }
        }

        public void AcceptAndReceive()
        {
            Socket? client = acceptClient();

            if (client != null)
            {
                receiveLoop(client);
            }
        }

        private Socket? acceptClient()
        {
            Socket? client = null;

            try
            {
                client = _serverSocket.Accept();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error accepting: {e.Message}");
            }

            return client;
        }


        private void receiveLoop(Socket client)
        {
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024]; //Pentru nota mai mare trebuie de facut sa primeasca mai mult de 1024 bytes

                    int bytesReceived = client.Receive(buffer);
                    string text = Encoding.UTF8.GetString(buffer);

                    Console.WriteLine($"From {client.RemoteEndPoint} - {text}");

                    client.Send(buffer);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error receiving {e.Message}");
                }

            }
        }


    }
}