using Org.BouncyCastle.Math;

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
        var e = Euler(totient);
        var d = GetD(e, totient);

        return new RsaKey(p, q, n, e, d);
    }

    private BigInteger GetN(BigInteger p, BigInteger q)
    {
        return p.Multiply(q);
    }

    private BigInteger Euler(BigInteger totient)
    {
        var euler = new BigInteger("3");

        while (totient.Gcd(euler).IntValue > 1)
        {
            euler = euler.Add(new BigInteger("2"));
        }
        
        return euler;
    }

    private BigInteger GetTotient(BigInteger p, BigInteger q) 
        => p
            .Subtract(BigInteger.One)
            .Multiply(
                q.Subtract(BigInteger.One));

    private BigInteger GetD(BigInteger euler, BigInteger totiente)
    {
        var result = euler.ModInverse(totiente);

        return result;
    }

    private BigInteger GeneratePrimeNumbers()
    {
        var random = new Random();

        while (true)
        {
            var bytes = new byte[96];
            random.NextBytes(bytes);

            var number = new BigInteger(1, bytes);

            if (number.IsProbablePrime(100))
                return number;
        }
    }
}