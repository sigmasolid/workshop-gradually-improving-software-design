namespace Bookstore.Domain;

public class PrimeGenerator
{
    public static IEnumerable<int> CoPilotGeneratesPrimes()
    {
        var primes = new List<int>();
        for (var candidate = 2; candidate<10000 ; candidate++)
        {
            var isPrime = true;
            foreach (var divisor in primes)
            {
                if (candidate % divisor == 0)
                {
                    isPrime = false;
                    break;
                }
            }
            if (isPrime)
            {
                primes.Add(candidate);
                yield return candidate;
            }
        }
    }

    public static IEnumerable<int> MyGeneratesPrimes(int stopAt)
    {
        var primes = new List<int>();
        for (var candidate = 2; candidate <= stopAt; candidate++)
        {
            var isPrime = true;
            foreach (var prime in primes)
            {
                if (candidate % prime == 0)
                {
                    isPrime = false;
                    break;
                }
            }

            if (isPrime)
                primes.Add(candidate);
        }

        return primes;
    }

    public static IEnumerable<int> HorvarGeneratePrimes() =>
        GetPrimeCandidates().Where(IsPrime);

    private static bool IsPrime(int candidate) =>
        !GetDivisorsOf(candidate).Any();

    private static IEnumerable<int> GetDivisorsOf(int candidate) =>
        Enumerable.Range(2, (int)Math.Sqrt(candidate) - 1)
            .Where(divisor => candidate % divisor == 0);

    private static IEnumerable<int> GetPrimeCandidates() =>
        Enumerable.Range(2, int.MaxValue - 1);
}   