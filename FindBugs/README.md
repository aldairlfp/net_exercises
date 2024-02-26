# Find Bugs

## Description

Find the bug(s) and modify one line on the incorrect implementation of the function solution that is supposed to return the smallest element on the given non-empty list "numbers" which contains at most 1000 integers within the range [-1000..1000]

Notice that the example test case `var numbers = <int>[-1, 1, -2, 2];`  the attached code is already returning the correct answer (-2)

```c#
int Solution(int[] numbers)
{
    int small = 0;
    for (int i = 1; i < numbers.Length; i++)
    {
        if (numbers[i] < small)
        {
            small = numbers[i];
        }                
    }
    return small;
}
```

The solution should be the line `int small = numbers[0];` instead of `int small = 0;` because the initial value of `small` should be the first element of the array `numbers` and not 0, which is not even guaranteed to be in the array and starting with 0 the algorithm will not work with an array of positive numbers.