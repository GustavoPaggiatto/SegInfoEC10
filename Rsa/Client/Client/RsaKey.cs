using System.Numerics;

internal sealed class RsaKey
{
    public RsaKey(
        BigInteger p,
        BigInteger q,
        BigInteger n,
        BigInteger e)
    {
        P = p;
        Q = q;
        N = n;
        E = e;
    }

    public BigInteger P { get; }
    public BigInteger Q { get; }
    public BigInteger N { get; }
    public BigInteger E { get; }
}