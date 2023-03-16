using System.Net;
using System.Net.Sockets;

namespace Client;

internal sealed class MessageSender
{
    public async Task<int> SendAsync(byte[] stream)
    {
        var udpClient = new UdpClient();
        
        udpClient
            .Client
            .SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

        var bytesSentLen = await udpClient.SendAsync(
            stream,
            new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000));

        udpClient.Dispose();
        
        return bytesSentLen;
    }
}