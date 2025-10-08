using System;
using System.Linq;

class Program
{
    static void Main()
    {
        var nk = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int N = nk[0]; // 물건 개수
        int K = nk[1]; // 최대 무게

        int[] W = new int[N + 1];
        int[] V = new int[N + 1];

        for (int i = 1; i <= N; i++)
        {
            var input = Console.ReadLine().Split().Select(int.Parse).ToArray();
            W[i] = input[0]; // 무게
            V[i] = input[1]; // 가치
        }

        // dp[i, j] = i번째 물건까지 고려했을 때, 무게 j를 넘지 않는 최대 가치
        int[,] dp = new int[N + 1, K + 1];

        for (int i = 1; i <= N; i++)
        {
            for (int j = 1; j <= K; j++)
            {
                if (W[i] > j)
                {
                    dp[i, j] = dp[i - 1, j]; // 현재 물건 못 넣음
                }
                else
                {
                    dp[i, j] = Math.Max(dp[i - 1, j], dp[i - 1, j - W[i]] + V[i]);
                }
            }
        }

        Console.WriteLine(dp[N, K]);
    }
}