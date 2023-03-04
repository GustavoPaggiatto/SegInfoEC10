using System.Net;
using System.Net.Sockets;

Console.ForegroundColor = ConsoleColor.Magenta;

Console.WriteLine("*************** Server UDP RSA ***************");

RsaKey key = default;

while (true)
{
    Console.ForegroundColor = ConsoleColor.Yellow;

    Console.WriteLine("Selecione uma opção:");

    Console.WriteLine("1 - Gerar chaves.");
    Console.WriteLine("2 - Habilitar servidor UDP.");
    Console.WriteLine("3 - Sair");

    Console.WriteLine("Escolha uma opção: ");
    int opt = Convert.ToInt32(Console.ReadLine());

    if (opt == 1)
    {
        key = new KeyGenerator().GenerateKey();
    }
    else if (opt == 2)
    {
        new RsaDecriptor(key);
    }
    else
    {
        break;
    }
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Saindo...");

var socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
socket.Bind(new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 8000));
socket.Listen();

var cancellationToken = new CancellationToken();

while (true) 
{
    if (cancellationToken.IsCancellationRequested)
        break;

    Memory<byte> buffer = new Memory<byte>();


    socket.ReceiveAsync(buffer, SocketFlags.None);
}
