using System.Numerics;
using System.Security.Cryptography;

internal sealed class KeyGenerator
{
    public RsaKey GenerateKey()
    {

        var p = this.GeneratePrimeNumbers();
        var q = this.GeneratePrimeNumbers();
        
        while (p.Equals(q))
            q = this.GeneratePrimeNumbers();

        var n = GetN(p, q);
        var totient = GetTotient(p, q);
        var e = Euler();
        var d = GetD(e, totient);

        return new RsaKey(p, q, n, e);
    }

    private BigInteger GetN(BigInteger p, BigInteger q)
    {
        return BigInteger.Multiply(p, q);
    }

    private int Euler()
    {
        return 2;
    }

    private BigInteger GetTotient(BigInteger p, BigInteger q) 
        => BigInteger.Multiply(
            BigInteger.Subtract(p, BigInteger.One), 
            BigInteger.Subtract(q, BigInteger.One));

    private BigInteger GetD(BigInteger euler, BigInteger totiente)
    {
        var aux = BigInteger.Add(totiente, BigInteger.One);
        
        return BigInteger.Divide(aux, euler);
    }

    private BigInteger GeneratePrimeNumbers()
    {
        var random = new Random();

        while (true)
        {
            var bytes = new byte[96];
            random.NextBytes(bytes);

            var number = new BigInteger(bytes);

            if (IsProbablePrime(number))
                return number;
        }
    }
    
    private bool IsProbablePrime(BigInteger source)
    {
        if (source == 2 || source == 3)
            return true;
        
        if (source < 2 || source % 2 == 0)
            return false;

        BigInteger d = source - 1;
        int s = 0;

        while (d % 2 == 0)
        {
            d /= 2;
            s += 1;
        }

        RandomNumberGenerator rng = RandomNumberGenerator.Create();
        byte[] bytes = new byte[source.ToByteArray().LongLength];

        for (int i = 0; i < 100; i++)
        {
            BigInteger number;
            
            do
            {
                rng.GetBytes(bytes);
                number = new BigInteger(bytes);
            }
            while (number < 2 || number >= source - 2);

            BigInteger aux = BigInteger.ModPow(number, d, source);
            
            if (aux == 1 || aux == source - 1)
                continue;

            for (int r = 1; r < s; r++)
            {
                aux = BigInteger.ModPow(aux, 2, source);
                
                if (aux == 1)
                    return false;
                
                if (aux == source - 1)
                    break;
            }

            if (aux != source - 1)
                return false;
        }

        return true;
    }
}