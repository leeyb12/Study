using System;
using System.Linq;

class Program
{
    static int N, M;
    static int[,] board;
    static bool[,] visited;
    static int maxSum = 0;

    // 상하좌우 이동
    static int[] dx = { -1, 1, 0, 0 };
    static int[] dy = { 0, 0, -1, 1 };

    static void Main()
    {
        var nm = Console.ReadLine().Split().Select(int.Parse).ToArray();
        N = nm[0]; M = nm[1];

        board = new int[N, M];
        visited = new bool[N, M];

        for (int i = 0; i < N; i++)
        {
            var row = Console.ReadLine().Split().Select(int.Parse).ToArray();
            for (int j = 0; j < M; j++)
                board[i, j] = row[j];
        }

        // 모든 칸에서 시작
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                visited[i, j] = true;
                DFS(i, j, board[i, j], 1);
                visited[i, j] = false;

                CheckExtraShape(i, j); // ㅗ 모양 체크
            }
        }

        Console.WriteLine(maxSum);
    }

    // DFS로 가능한 4칸 합 탐색
    static void DFS(int x, int y, int sum, int depth)
    {
        if (depth == 4)
        {
            maxSum = Math.Max(maxSum, sum);
            return;
        }

        for (int d = 0; d < 4; d++)
        {
            int nx = x + dx[d];
            int ny = y + dy[d];

            if (nx < 0 || ny < 0 || nx >= N || ny >= M) continue;
            if (visited[nx, ny]) continue;

            visited[nx, ny] = true;
            DFS(nx, ny, sum + board[nx, ny], depth + 1);
            visited[nx, ny] = false;
        }
    }

    // ㅗ, ㅜ, ㅏ, ㅓ 모양 체크
    static void CheckExtraShape(int x, int y)
    {
        // 중심에 (x,y) 두고 네 방향 중 세 방향 선택
        int center = board[x, y];

        for (int i = 0; i < 4; i++)
        {
            int sum = center;
            bool ok = true;

            for (int j = 0; j < 3; j++)
            {
                int d = (i + j) % 4;
                int nx = x + dx[d];
                int ny = y + dy[d];

                if (nx < 0 || ny < 0 || nx >= N || ny >= M)
                {
                    ok = false;
                    break;
                }
                sum += board[nx, ny];
            }

            if (ok) maxSum = Math.Max(maxSum, sum);
        }
    }
}

