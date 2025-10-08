using System;

class Program
{
    static int N;
    static int[] numbers;
    static int[] operators = new int[4]; // +, -, *, /
    static int maxValue = int.MinValue;
    static int minValue = int.MaxValue;

    static void Main()
    {
        N = int.Parse(Console.ReadLine());
        numbers = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        var ops = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        for (int i = 0; i < 4; i++) operators[i] = ops[i];

        DFS(1, numbers[0]); // 첫 번째 숫자부터 시작

        Console.WriteLine(maxValue);
        Console.WriteLine(minValue);
    }

    static void DFS(int idx, int current)
    {
        if (idx == N) // 모든 숫자 사용
        {
            maxValue = Math.Max(maxValue, current);
            minValue = Math.Min(minValue, current);
            return;
        }

        for (int i = 0; i < 4; i++)
        {
            if (operators[i] > 0)
            {
                operators[i]--;

                int next = 0;
                switch (i)
                {
                    case 0: next = current + numbers[idx]; break; // +
                    case 1: next = current - numbers[idx]; break; // -
                    case 2: next = current * numbers[idx]; break; // *
                    case 3: // /
                        if (current < 0)
                            next = - (Math.Abs(current) / numbers[idx]);
                        else
                            next = current / numbers[idx];
                        break;
                }

                DFS(idx + 1, next);
                operators[i]++; // 복구
            }
        }
    }
}
