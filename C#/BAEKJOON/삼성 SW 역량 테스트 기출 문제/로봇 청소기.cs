using System;

class Program
{
    static int N, M;
    static int[,] map;
    static int r, c, dir;
    // 북, 동, 남, 서
    static int[] dx = { -1, 0, 1, 0 };
    static int[] dy = { 0, 1, 0, -1 };

    static void Main()
    {
        var nm = Console.ReadLine().Split();
        N = int.Parse(nm[0]);
        M = int.Parse(nm[1]);

        var rcd = Console.ReadLine().Split();
        r = int.Parse(rcd[0]);
        c = int.Parse(rcd[1]);
        dir = int.Parse(rcd[2]);

        map = new int[N, M];
        for (int i = 0; i < N; i++)
        {
            var row = Console.ReadLine().Split();
            for (int j = 0; j < M; j++)
                map[i, j] = int.Parse(row[j]);
        }

        int cleaned = 0;

        while (true)
        {
            // 1. 현재 칸 청소
            if (map[r, c] == 0)
            {
                map[r, c] = 2;
                cleaned++;
            }

            bool moved = false;

            // 4방향 확인
            for (int i = 0; i < 4; i++)
            {
                dir = (dir + 3) % 4; // 반시계 90도 회전
                int nx = r + dx[dir];
                int ny = c + dy[dir];

                if (nx >= 0 && ny >= 0 && nx < N && ny < M && map[nx, ny] == 0)
                {
                    r = nx;
                    c = ny;
                    moved = true;
                    break;
                }
            }

            if (!moved)
            {
                // 뒤로 후진
                int back = (dir + 2) % 4;
                int nx = r + dx[back];
                int ny = c + dy[back];

                if (nx >= 0 && ny >= 0 && nx < N && ny < M && map[nx, ny] != 1)
                {
                    r = nx;
                    c = ny;
                }
                else
                {
                    // 후진 불가 → 종료
                    break;
                }
            }
        }

        Console.WriteLine(cleaned);
    }
}
