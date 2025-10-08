using System;

class Program
{
    static void Main()
    {
        int N = int.Parse(Console.ReadLine());
        string[] inputA = Console.ReadLine().Split();
        int[] A = Array.ConvertAll(inputA, int.Parse);

        string[] inputBC = Console.ReadLine().Split();
        int B = int.Parse(inputBC[0]); // 총감독관 감시 가능 인원
        int C = int.Parse(inputBC[1]); // 부감독관 감시 가능 인원

        long result = 0; // 결과값 (감독관 수) → int 범위를 넘어갈 수 있으므로 long 사용

        for (int i = 0; i < N; i++)
        {
            int students = A[i];

            // 총감독관 1명은 무조건 배치
            result++;

            // 남은 학생 수
            students -= B;

            if (students > 0)
            {
                // 부감독관 수 계산
                result += students / C;
                if (students % C != 0) result++;
            }
        }

        Console.WriteLine(result);
    }
}