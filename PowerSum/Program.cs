using System.Diagnostics.CodeAnalysis;

namespace PowerSum;

public static class Program
{
    static int count;
    static void Main(string[] args)
    {
        int x = Int32.Parse(Console.ReadLine());
        int n = Int32.Parse(Console.ReadLine());
        Console.WriteLine(powerSum(x, n));
    }

    public static int powerSum(int x, int n)
    {
        int[] powers = GetPowers(x, n);
        bool[] set = new bool[powers.Length];
        count = 0;
        GeneratePowerSet(x, powers, set, 0, 0);
        return count;
    }

    // Generates the power set of the powers array and 
    // checks if the sum of the set equals x
    static void GeneratePowerSet(int x, int[] powers, bool[] set, int index, int sum)
    {
        // If the sum is greater than x then we can stop generating the set 
        // since the powers array is sorted in ascending order and the sum 
        // will only increase from this point on
        if (sum > x)
        {
            return;
        }
        // If the index is equal to the length of the set array then we have
        // generated a complete set and we can check if the sum of the set
        // equals x
        if (index == set.Length)
        {
            if (sum == x)
            {
                count++;
            }
        }
        else
        {
            GeneratePowerSet(x, powers, set, index + 1, sum);
            set[index] = true;
            GeneratePowerSet(x, powers, set, index + 1, sum + powers[index]);
            set[index] = false;
        }
    }

    // Returns an array of all powers of x up to x^(1/n)    
    static int[] GetPowers(int x, int n)
    {
        int[] powers = new int[(int)Math.Pow(x, 1 / (double)n) + 1];
        for (int i = 0; i < powers.Length; i++)
        {
            powers[i] = (int)Math.Pow(i + 1, n);
        }
        return powers;
    }
}
