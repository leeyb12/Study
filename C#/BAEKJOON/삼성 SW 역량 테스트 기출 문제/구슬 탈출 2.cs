using System;
using System.Collections.Generic;

class Program
{
    struct State
    {
        public int rx, ry, bx, by, d;
        public State(int rx, int ry, int bx, int by, int d)
        {
            this.rx = rx; this.ry = ry; this.bx = bx; this.by = by; this.d = d;
        }
    }

    static int N, M;
    static char[,] board;
    static bool[,,,] visited;
    static int[] dx = { -1, 1, 0, 0 }; // up, down, left, right
    static int[] dy = { 0, 0, -1, 1 };

    static (int nx, int ny, int cnt, bool inHole) Move(int x, int y, int dir)
    {
        int cx = x, cy = y;
        int cnt = 0;
        while (true)
        {
            int nx = cx + dx[dir];
            int ny = cy + dy[dir];
            if (board[nx, ny] == '#') break;
            cx = nx; cy = ny; cnt++;
            if (board[cx, cy] == 'O') return (cx, cy, cnt, true);
        }
        return (cx, cy, cnt, false);
    }

    static int Solve(int rx, int ry, int bx, int by)
    {
        visited = new bool[N, M, N, M];
        var q = new Queue<State>();
        q.Enqueue(new State(rx, ry, bx, by, 0));
        visited[rx, ry, bx, by] = true;

        while (q.Count > 0)
        {
            var s = q.Dequeue();
            if (s.d >= 10) continue; // 이미 10회 이상이면 더 못 확장 (다음이 11회)
            for (int dir = 0; dir < 4; dir++)
            {
                var rMove = Move(s.rx, s.ry, dir);
                var bMove = Move(s.bx, s.by, dir);

                // 파란 구슬이 구멍에 빠지면 실패
                if (bMove.inHole) continue;
                // 빨간 구슬만 빠지면 성공 (이동 후 횟수 = s.d + 1)
                if (rMove.inHole && !bMove.inHole) return s.d + 1;

                int nrx = rMove.nx, nry = rMove.ny;
                int nbx = bMove.nx, nby = bMove.ny;

                // 같은 칸이면 이동 거리 더 작은 쪽을 한 칸 뒤로 이동시킨다
                if (nrx == nbx && nry == nby)
                {
                    if (rMove.cnt > bMove.cnt)
                    {
                        // 빨간이 더 멀리 이동했으므로 빨간을 한칸 뒤로
                        nrx -= dx[dir];
                        nry -= dy[dir];
                    }
                    else
                    {
                        nbx -= dx[dir];
                        nby -= dy[dir];
                    }
                }

                if (!visited[nrx, nry, nbx, nby])
                {
                    visited[nrx, nry, nbx, nby] = true;
                    q.Enqueue(new State(nrx, nry, nbx, nby, s.d + 1));
                }
            }
        }
        return -1;
    }

    static void Main()
    {
        var parts = Console.ReadLine().Split();
        N = int.Parse(parts[0]);
        M = int.Parse(parts[1]);
        board = new char[N, M];

        int rx = -1, ry = -1, bx = -1, by = -1;
        for (int i = 0; i < N; i++)
        {
            var line = Console.ReadLine();
            for (int j = 0; j < M; j++)
            {
                board[i, j] = line[j];
                if (board[i, j] == 'R') { rx = i; ry = j; board[i, j] = '.'; }
                if (board[i, j] == 'B') { bx = i; by = j; board[i, j] = '.'; }
            }
        }

        Console.WriteLine(Solve(rx, ry, bx, by));
    }
}
