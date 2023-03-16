using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace ConsoleApp1;

internal sealed class RsaKeySender
{
    public async Task SendAsync(RsaKey key)
    {
        var udpClient = new UdpClient();
        
        udpClient
            .Client
            .SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

        var jsonKey = GetJson(key);

        await udpClient.SendAsync(
            Encoding.Default.GetBytes(jsonKey),
            new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000));
        
        udpClient.Dispose();
    }

    private string GetJson(RsaKey rsaKey)
    {
        var keyDto = new RsaKeyDto()
        {
            P = rsaKey.P.ToString(),
            Q = rsaKey.Q.ToString(),
            N = rsaKey.N.ToString(),
            E = rsaKey.E.ToString()
        };

        return JsonSerializer.Serialize(keyDto);
    }

    private sealed class RsaKeyDto
    {
        public string P { get; set; } = null!;
        public string Q { get; set; } = null!;
        public string N { get; set; } = null!;
        public string E { get; set; } = null!;
    }
}