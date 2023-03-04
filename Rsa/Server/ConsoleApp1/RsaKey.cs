internal sealed class RsaKey
{
    public RsaKey(long @public, long @private)
    {
        Public = @public;
        Private = @private;
    }

    long Public { get; }
    long Private { get; }
}
