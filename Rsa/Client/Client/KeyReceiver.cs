using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Text.Json;

namespace Client;

internal sealed class KeyReceiver
{
    private readonly UdpClient _listener;

    public KeyReceiver()
    {
        _listener = new UdpClient();
        
        _listener
            .Client
            .SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

        _listener.Client.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000));
    }
    
    public async Task<RsaKey> ReceiveAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested) 
        {
            var result = await _listener.ReceiveAsync(cancellationToken);
            
            var keyDto = JsonSerializer.Deserialize<RsaKeyDto>(Encoding.Default.GetString(result.Buffer));
            var key = new RsaKey(
                BigInteger.Parse(keyDto!.P),
                BigInteger.Parse(keyDto.Q),
                BigInteger.Parse(keyDto.N),
                BigInteger.Parse(keyDto.E));
            
            _listener.Dispose();
            
            return key;
        }

        return default!;
    }
    
    private sealed class RsaKeyDto
    {
        public string P { get; set; } = null!;
        public string Q { get; set; } = null!;
        public string N { get; set; } = null!;
        public string E { get; set; } = null!;
    }
}