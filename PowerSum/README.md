# Power Sum

## Description

Write a function that receives two integers `x` and `n` and returns the number of ways `x` can be expressed as the sum of n-th power of unique natural numbers. For example, if `X = 13`  and `N = 2`, we have to find all combinations of unique squares adding up to *13*. The only solution is $2^2 + 3^2$

### Function description

Implement the `powerSum` function that should return an integer that represents the number of possible combinations. `powerSum` has the following parameter(s):

- `X`: the integer to sum to
- `N`: the integer power to raise the numbers to

### Input Format

The first line contains an integer `X`.
The second line contains an integer `N`.

### Constraints

- $1 \leq X \leq 1000$
- $2 \leq N \leq 10$

### Output Format

Output a single integer, the number of possible combinations calculated.

### Sample Input 0

```plaintext
10
0
```

### Sample Output 0

```plaintext
1
```

### Explanation 0

If `X = 10` and `N = 2`, we need to find the number of ways that 10 can be represented as the sum of squares of unique numbers.

$10=1^2 + 3^2$.

This is the only way in which 10 can be expressed as the sum of unique squares.

### Sample Input 1

```plaintext
100
2
```

### Sample Output 1

```plaintext
3
```

### Explanation 1

If `X = 100` and `N = 2`, we need to find the number of ways that 100 can be represented as the sum of squares of unique numbers.

$100=10^2$

$100=6^2 + 8^2$

$100=1^2 + 3^2 + 4^2 + 5^2 + 7^2$

These are the only ways in which 100 can be expressed as the sum of unique squares.

### Sample Input 2

```plaintext
100
3
```

### Sample Output 2

```plaintext
1
```

### Explanation 2

If `X = 100` and `N = 3`, we need to find the number of ways that 100 can be represented as the sum of cubes of unique numbers.

$100=1^3 + 2^3 + 3^3 + 4^3$

This is the only way in which 100 can be expressed as the sum of unique cubes.

### Solution

The idea is to make an array with the powers of the numbers from 1 to the nth root of `X` because the next power would already be greater than `X`. Then with that array only left to make all the posible combinations of the numbers in the array that sum `X` and return the number of combinations.
