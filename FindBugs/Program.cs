static int Solution(int[] numbers)
{
    // The line to modify was the following one 
    // because starting with 0 the algorithm will not work 
    // with an array of positive numbers    
    int small = numbers[0];
    for (int i = 1; i < numbers.Length; i++)
    {
        if (numbers[i] < small)
        {
            small = numbers[i];
        }                
    }
    return small;
}

static int[] ArrayGenerator(int size)
{
    Random random = new Random();
    int[] numbers = new int[size];
    for (int i = 0; i < size; i++)
    {
        numbers[i] = random.Next(-1000, 1000);
    }
    return numbers;
}

int size = 1000;
int[] array = ArrayGenerator(size);
int result = Solution(array);
Console.WriteLine(result == array.Min() ? "Test passed" : "Test failed");
