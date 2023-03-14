using System.Net.Sockets;

namespace ConsoleApp1;

internal sealed class SecureUdpServer
{
    private readonly UdpClient _listener;
    private readonly RsaDecriptor _rsaDecriptor;

    public SecureUdpServer(RsaKey key)
    {
        _rsaDecriptor = new RsaDecriptor(key);
        _listener = new UdpClient("localhost", 8000);
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested) 
        {
            var result = await _listener.ReceiveAsync(cancellationToken);
            
            // Tratar o resultado...
        }
    }
}