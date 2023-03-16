using System.Numerics;
using System.Text;

internal class RsaDecriptor
{
    private readonly RsaKey _key;

    public RsaDecriptor(RsaKey key)
    {
        _key = key;
    }

    public string Decript(byte[] stream)
    {
        var aux = Encoding.Default.GetString(stream);
        var number = BigInteger.Parse(aux);
        var clean = BigInteger.ModPow(
            number, 
            BigInteger.Parse(_key.D.ToString()),
            BigInteger.Parse(_key.N.ToString()));

        Console.WriteLine("Log:");
        Console.WriteLine("D (bytes): " + string.Join(",", _key.D.ToByteArray().Reverse().ToArray()));
        Console.WriteLine("D (number): " + new BigInteger(_key.D.ToByteArray().Reverse().ToArray()));
        Console.WriteLine("N (bytes): " + string.Join(",", _key.N.ToByteArray().Reverse().ToArray()));
        Console.WriteLine("N (number): " + new BigInteger(_key.N.ToByteArray().Reverse().ToArray()));
        Console.WriteLine("Number cifer: " + number.ToString());
        Console.WriteLine("Clean bytes: " + string.Join(",", clean.ToByteArray()));
        
        return Encoding.Default.GetString(clean.ToByteArray());
    }
}
