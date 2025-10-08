using System;
using System.Linq;

class Program
{
    static void Main()
    {
        string[] input = Console.ReadLine().Split();
        int N = int.Parse(input[0]);
        int M = int.Parse(input[1]);
        int x = int.Parse(input[2]);
        int y = int.Parse(input[3]);
        int K = int.Parse(input[4]);

        int[,] map = new int[N, M];
        for (int i = 0; i < N; i++)
        {
            int[] row = Console.ReadLine().Split().Select(int.Parse).ToArray();
            for (int j = 0; j < M; j++)
                map[i, j] = row[j];
        }

        int[] commands = Console.ReadLine().Split().Select(int.Parse).ToArray();

        // 주사위 (0=위, 1=아래, 2=북, 3=남, 4=서, 5=동)
        int[] dice = new int[6];

        // 방향 (동, 서, 북, 남)
        int[] dx = { 0, 0, -1, 1 };
        int[] dy = { 1, -1, 0, 0 };

        foreach (int cmd in commands)
        {
            int dir = cmd - 1; // (동=0, 서=1, 북=2, 남=3)
            int nx = x + dx[dir];
            int ny = y + dy[dir];

            // 지도 바깥이면 무시
            if (nx < 0 || nx >= N || ny < 0 || ny >= M) continue;

            // 주사위 굴리기
            Roll(dice, dir);

            // 지도와 주사위 상호작용
            if (map[nx, ny] == 0)
            {
                map[nx, ny] = dice[1]; // 바닥 복사
            }
            else
            {
                dice[1] = map[nx, ny]; // 지도값 주사위 바닥에 복사
                map[nx, ny] = 0;
            }

            // 위치 갱신
            x = nx; y = ny;

            // 윗면 출력
            Console.WriteLine(dice[0]);
        }
    }

    static void Roll(int[] dice, int dir)
    {
        int top = dice[0], bottom = dice[1], north = dice[2], south = dice[3], west = dice[4], east = dice[5];

        if (dir == 0) // 동
        {
            dice[0] = west;
            dice[1] = east;
            dice[4] = bottom;
            dice[5] = top;
        }
        else if (dir == 1) // 서
        {
            dice[0] = east;
            dice[1] = west;
            dice[4] = top;
            dice[5] = bottom;
        }
        else if (dir == 2) // 북
        {
            dice[0] = south;
            dice[1] = north;
            dice[2] = top;
            dice[3] = bottom;
        }
        else if (dir == 3) // 남
        {
            dice[0] = north;
            dice[1] = south;
            dice[2] = bottom;
            dice[3] = top;
        }
    }
}