internal sealed class KeyGenerator
{
    public RsaKey GenerateKey()
    {

        long p = this.GeneratePrimeNumbers();
        long q = this.GeneratePrimeNumbers();

        while (p == q)
            q = this.GeneratePrimeNumbers();

        long tot = Totient(p, q);

        return null;
    }

    private long Euler(long totient)
    {
        return 0;
    }

    private long Totient(long p, long q) => (p - 1) * (q - 1);

    private long GeneratePrimeNumbers()
    {
        var random = new Random();
        long number = 0;

        while (true)
        {
            number = random.Next(3, 50);
            var isPrime = true;

            for (var i = 2; i < number; i++)
            {
                if (i % number == 0)
                {
                    isPrime = false;
                    break;
                }
            }

            if (isPrime)
                break;
        }

        return number;
    }
}