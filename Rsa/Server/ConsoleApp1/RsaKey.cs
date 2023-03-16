using Org.BouncyCastle.Math;

internal sealed class RsaKey
{
    public RsaKey(
        BigInteger p,
        BigInteger q,
        BigInteger n,
        BigInteger e,
        BigInteger d)
    {
        P = p;
        Q = q;
        N = n;
        E = e;
        D = d;
    }

    public BigInteger P { get; }
    public BigInteger Q { get; }
    public BigInteger N { get; }
    public BigInteger E { get; }
    public BigInteger D { get; }
}
