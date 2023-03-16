using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp1;

internal sealed class MessageReceiver
{
    private readonly UdpClient _listener;
    private readonly RsaDecriptor _rsaDecriptor;

    public MessageReceiver(RsaKey key)
    {
        _rsaDecriptor = new RsaDecriptor(key);
        _listener = new UdpClient();
        
        _listener
            .Client
            .SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

        _listener.Client.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000));
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested) 
        {
            var result = await _listener.ReceiveAsync(cancellationToken);
            
            Console.WriteLine("Resultado cifrado: " + Encoding.Default.GetString(result.Buffer));

            var msg = _rsaDecriptor.Decript(result.Buffer);
            
            Console.WriteLine("Resultado decifrado: " + msg);
        }
    }
}