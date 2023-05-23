using Client;

ClientSocket client = new ClientSocket();

client.Connect("192.168.1.156", 5050);
client.SendLoop();