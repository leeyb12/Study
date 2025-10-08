using System;
using System.Collections.Generic;

class Program
{
    static int N, M;
    static int[,] lab;
    static int[,] copy;
    static List<(int, int)> viruses = new List<(int, int)>();
    static int maxSafe = 0;

    static int[] dx = { -1, 1, 0, 0 };
    static int[] dy = { 0, 0, -1, 1 };

    static void Main()
    {
        var nm = Console.ReadLine().Split();
        N = int.Parse(nm[0]);
        M = int.Parse(nm[1]);

        lab = new int[N, M];

        for (int i = 0; i < N; i++)
        {
            var row = Console.ReadLine().Split();
            for (int j = 0; j < M; j++)
            {
                lab[i, j] = int.Parse(row[j]);
                if (lab[i, j] == 2)
                    viruses.Add((i, j));
            }
        }

        // 벽 세우기 시작
        BuildWalls(0);

        Console.WriteLine(maxSafe);
    }

    static void BuildWalls(int count)
    {
        if (count == 3)
        {
            // 벽 3개 세워졌으면 바이러스 퍼뜨리기
            SpreadVirus();
            return;
        }

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                if (lab[i, j] == 0)
                {
                    lab[i, j] = 1; // 벽 세움
                    BuildWalls(count + 1);
                    lab[i, j] = 0; // 원상복구
                }
            }
        }
    }

    static void SpreadVirus()
    {
        copy = (int[,])lab.Clone();
        Queue<(int, int)> q = new Queue<(int, int)>();

        foreach (var v in viruses)
            q.Enqueue(v);

        while (q.Count > 0)
        {
            var (x, y) = q.Dequeue();
            for (int dir = 0; dir < 4; dir++)
            {
                int nx = x + dx[dir];
                int ny = y + dy[dir];

                if (nx >= 0 && ny >= 0 && nx < N && ny < M)
                {
                    if (copy[nx, ny] == 0)
                    {
                        copy[nx, ny] = 2;
                        q.Enqueue((nx, ny));
                    }
                }
            }
        }

        CountSafe();
    }

    static void CountSafe()
    {
        int safe = 0;
        for (int i = 0; i < N; i++)
            for (int j = 0; j < M; j++)
                if (copy[i, j] == 0)
                    safe++;

        if (safe > maxSafe)
            maxSafe = safe;
    }
}
