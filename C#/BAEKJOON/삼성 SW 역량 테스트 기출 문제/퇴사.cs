using System;

class Program
{
    static void Main()
    {
        int N = int.Parse(Console.ReadLine());
        int[] T = new int[N + 1];
        int[] P = new int[N + 1];

        for (int i = 1; i <= N; i++)
        {
            var input = Console.ReadLine().Split();
            T[i] = int.Parse(input[0]);
            P[i] = int.Parse(input[1]);
        }

        int[] dp = new int[N + 2]; // dp[N+1]까지 필요
        for (int i = N; i >= 1; i--)
        {
            if (i + T[i] <= N + 1)
            {
                dp[i] = Math.Max(P[i] + dp[i + T[i]], dp[i + 1]);
            }
            else
            {
                dp[i] = dp[i + 1];
            }
        }

        Console.WriteLine(dp[1]);
    }
}
