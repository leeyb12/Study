using System;
using System.Collections.Generic;

class MainClass
{
    static int N;
    static int answer = 0;

    static void Main()
    {
        N = int.Parse(Console.ReadLine());
        int[,] board = new int[N, N];
        for (int i = 0; i < N; i++)
        {
            var parts = Console.ReadLine().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < N; j++) board[i, j] = int.Parse(parts[j]);
        }

        DFS(0, board);
        Console.WriteLine(answer);
    }

    static void DFS(int depth, int[,] cur)
    {
        // 현재 보드의 최댓값 갱신
        for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
                if (cur[i, j] > answer) answer = cur[i, j];

        if (depth == 5) return;

        for (int dir = 0; dir < 4; dir++)
        {
            int[,] next = Move(cur, dir);
            DFS(depth + 1, next);
        }
    }

    // dir: 0 = up, 1 = down, 2 = left, 3 = right
    static int[,] Move(int[,] b, int dir)
    {
        int[,] res = new int[N, N];
        // 초기화 0 (default)

        if (dir == 0)
        { // up
            for (int col = 0; col < N; col++)
            {
                List<int> vals = new List<int>();
                for (int row = 0; row < N; row++) if (b[row, col] != 0) vals.Add(b[row, col]);
                List<int> merged = MergeList(vals);
                for (int i = 0; i < merged.Count; i++) res[i, col] = merged[i];
            }
        }
        else if (dir == 1)
        { // down
            for (int col = 0; col < N; col++)
            {
                List<int> vals = new List<int>();
                for (int row = N - 1; row >= 0; row--) if (b[row, col] != 0) vals.Add(b[row, col]);
                List<int> merged = MergeList(vals);
                for (int i = 0; i < merged.Count; i++) res[N - 1 - i, col] = merged[i];
            }
        }
        else if (dir == 2)
        { // left
            for (int row = 0; row < N; row++)
            {
                List<int> vals = new List<int>();
                for (int col = 0; col < N; col++) if (b[row, col] != 0) vals.Add(b[row, col]);
                List<int> merged = MergeList(vals);
                for (int i = 0; i < merged.Count; i++) res[row, i] = merged[i];
            }
        }
        else
        { // right
            for (int row = 0; row < N; row++)
            {
                List<int> vals = new List<int>();
                for (int col = N - 1; col >= 0; col--) if (b[row, col] != 0) vals.Add(b[row, col]);
                List<int> merged = MergeList(vals);
                for (int i = 0; i < merged.Count; i++) res[row, N - 1 - i] = merged[i];
            }
        }

        return res;
    }

    // 값 리스트를 받아서 한 번의 이동에서 합쳐진 결과 리스트를 반환
    static List<int> MergeList(List<int> vals)
    {
        List<int> merged = new List<int>();
        int i = 0;
        while (i < vals.Count)
        {
            if (i + 1 < vals.Count && vals[i] == vals[i + 1])
            {
                merged.Add(vals[i] * 2);
                i += 2; // 한 번 합쳐졌으면 다음 값은 스킵
            }
            else
            {
                merged.Add(vals[i]);
                i++;
            }
        }
        return merged;
    }
}