namespace PowerSum;

public static class Program
{
    static int count;
    static void Main(string[] args)
    {
        int x= Int32.Parse(Console.ReadLine());
        int n= Int32.Parse(Console.ReadLine());
        Console.WriteLine(powerSum(x, n));
    }

    public static int powerSum(int x, int n)
    {
        int[] powers = GetPowers(x, n);
        bool[] set = new bool[powers.Length];
        count = 0;
        GeneratePowerSet(x, powers, set, 0);
        return count;
    }

    // Generates the power set of the powers array and 
    // checks if the sum of the set equals x
    static void GeneratePowerSet(int x, int[] powers, bool[] set, int index)
    {
        if (index == set.Length)
        {
            count = CheckSum(x, powers, set) ? count + 1 : count;
        }
        else
        {
            GeneratePowerSet(x, powers, set, index + 1);
            set[index] = true;
            GeneratePowerSet(x, powers, set, index + 1);
            set[index] = false;
        }
    }

    // Returns true if the sum of the powers in the set equals x
    static bool CheckSum(int x, int[] powers, bool[] set)
    {
        int sum = 0;
        for (int i = 0; i < powers.Length; i++)
        {
            if (set[i])
            {
                sum += powers[i];
            }
        }
        return sum == x;
    }

    // Returns an array of all powers of x up to x^(1/n)    
    static int[] GetPowers(int x, int n)
    {
        int[] powers = new int[(int)Math.Pow(x, 1/(double)n)];
        for (int i = 0; i < powers.Length; i++)
        {
            powers[i] = (int)Math.Pow(i + 1, n);
        }
        return powers;
    }
}
