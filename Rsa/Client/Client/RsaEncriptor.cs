using System.Numerics;
using System.Text;

internal sealed class RsaEncriptor
{
    public byte[] Encript(RsaKey key, byte[] msg)
    {
        var cifer = BigInteger.ModPow(new BigInteger(msg), key.E, key.N);
            
        Console.ForegroundColor = ConsoleColor.White;
        
        Console.WriteLine("Log: ");
        Console.WriteLine("Clean Number: " + new BigInteger(msg));
        Console.WriteLine("Number Encripted: " + cifer.ToString());
        Console.WriteLine("Number Encripted (bytes): " + string.Join(",", cifer.ToByteArray().Select(b => b)));
        Console.WriteLine("Original (bytes): " + string.Join(",", msg.Select(b => b)));
        
        return Encoding.Default.GetBytes(cifer.ToString());
    }
}