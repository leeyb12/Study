using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    // 🌍 조별리그 경기 일정표예요! 0,1,2,3번 팀이 각각 자기 자신을 제외한 팀과 한 번씩 붙어요.
    // (0,1), (0,2), (0,3), (1,2), (1,3), (2,3) 이렇게 총 6경기가 진행되죠!
    static readonly (int a, int b)[] matches = new (int, int)[]
    {
        (0,1),
        (0,2),
        (0,3),
        (1,2),
        (1,3),
        (2,3)
    };

    // 🎲 각 경기마다 {승리 확률, 무승부 확률, 패배 확률}을 저장할 곳이에요.
    // 전문가님들이 예측해 주신 소중한 정보!
    static double[][] probs = new double[6][]; 

    // 🏆 각 팀이 다음 라운드에 진출할 최종 확률을 저장할 곳!
    static double[] result = new double[4]; 

    // 📊 각 시나리오(모든 경기 결과)가 끝났을 때, 팀별 최종 승점을 저장할 곳이에요.
    static int[] points = new int[4]; 

    static void Main()
    {
        // 📥 전문가님들이 예측한 6경기 확률을 입력받아요!
        // 각 줄마다 '승리확률 무승부확률 패배확률' 이렇게 3개의 숫자를 입력해 주시면 돼요.
        Console.WriteLine("6경기의 승-무-패 확률을 입력해주세요. (각 경기마다 한 줄에 공백으로 구분)");
        for (int i = 0; i < 6; i++)
        {
            var parts = Console.ReadLine().Trim().Split(new[]{' ', '\t'}, StringSplitOptions.RemoveEmptyEntries)
                          .Select(double.Parse).ToArray();
            if (parts.Length < 3)
            {
                Console.WriteLine("에러! 각 경기마다 3개의 확률(승리, 무승부, 패배)을 정확히 입력해야 해요. 😥");
                return; // 입력 오류 시 프로그램 종료
            }
            // 확률의 합이 1이 되는지 확인하는 간단한 검증도 추가하면 더 좋겠죠?
            if (Math.Abs(parts[0] + parts[1] + parts[2] - 1.0) > 0.000001)
            {
                Console.WriteLine($"경고: {i+1}번째 경기의 확률 합이 1이 아니에요! 입력값을 확인해주세요. 🤔 (합: {parts[0] + parts[1] + parts[2]})");
            }
            probs[i] = parts;
        }

        // 🚀 이제 깊이 우선 탐색(DFS) 마법을 시작해서 모든 시나리오를 살펴볼 시간!
        // 첫 번째 경기부터 시작하고, 지금까지의 확률은 100%(1.0)으로 시작해요.
        DFS(0, 1.0);

        // 📤 모든 계산이 끝나면, 각 팀의 진출 확률을 소수점 10자리까지 정확하게 보여드려요!
        Console.WriteLine("\n✨ 각 팀의 다음 라운드 진출 확률입니다! ✨");
        for (int i = 0; i < 4; i++)
            Console.WriteLine($"팀 {i+1} : {result[i].ToString("F10")}"); // 팀 인덱스를 1부터 시작하도록 출력
    }

    // 🕵️‍♀️ 이 함수가 바로 모든 경기 결과 시나리오를 탐색하는 마법사예요!
    // idx: 현재 계산 중인 경기 번호 (0부터 5까지 총 6경기)
    // probSoFar: 현재까지 탐색한 경기 결과들의 확률을 모두 곱한 값
    static void DFS(int idx, double probSoFar)
    {
        // 🏁 모든 6경기의 결과가 다 결정되었다면, 하나의 시나리오가 완성된 거예요!
        if (idx == 6)
        {
            // 이제 이 시나리오의 확률(probSoFar)을 바탕으로 어떤 팀이 진출하는지 계산해요.
            DistributeScenarioProbability(probSoFar);
            return;
        }

        // 현재 경기의 두 팀을 불러와요!
        var (a, b) = matches[idx]; 
        // 그리고 이 경기의 승리, 무승부, 패배 확률도 불러와요.
        double pAwin = probs[idx][0];
        double pDraw = probs[idx][1];
        double pBwin = probs[idx][2];

        // 🌟 'A팀이 이기는 경우'를 가정해 봐요!
        // A팀 승점 3점 추가!
        points[a] += 3; 
        // 만약 이 확률이 0보다 크다면, 다음 경기로 넘어가서 탐색을 이어가요.
        if (pAwin > 0) DFS(idx + 1, probSoFar * pAwin);
        // 탐색이 끝났으면 점수를 다시 원상복구! (다른 경우의 수를 보기 위함)
        points[a] -= 3; 

        // 🤝 '무승부인 경우'를 가정해 봐요!
        // A, B팀 모두 승점 1점씩 추가!
        points[a] += 1; points[b] += 1; 
        // 이 확률이 0보다 크다면, 다음 경기로 넘어가서 탐색을 이어가요.
        if (pDraw > 0) DFS(idx + 1, probSoFar * pDraw);
        // 점수 원상복구!
        points[a] -= 1; points[b] -= 1; 

        // 📉 'B팀이 이기는 경우'를 가정해 봐요!
        // B팀 승점 3점 추가!
        points[b] += 3; 
        // 이 확률이 0보다 크다면, 다음 경기로 넘어가서 탐색을 이어가요.
        if (pBwin > 0) DFS(idx + 1, probSoFar * pBwin);
        // 점수 원상복구!
        points[b] -= 3; 
    }

    // 🤩 하나의 시나리오가 끝나면, 이 시나리오에서 어떤 팀이 다음 라운드에 진출하는지 결정하고,
    // 그 확률을 최종 결과에 더해주는 함수예요!
    static void DistributeScenarioProbability(double scenarioProb)
    {
        // 각 팀의 인덱스와 최종 승점을 묶어서 리스트에 넣어요.
        var list = new List<(int team, int pts)>();
        for (int i = 0; i < 4; i++) list.Add((i, points[i]));
        // 승점이 높은 순서대로 쭈르륵 정렬! (내림차순)
        list.Sort((x, y) => y.pts.CompareTo(x.pts)); 

        // 🕵️‍♀️ 상위 2팀을 뽑아야 하는데, 만약 동점 팀이 있다면 '추첨' 규칙을 적용해야 해요!
        // 이 부분은 아주 중요하답니다!
        int pos = 0; // 현재 순위 그룹의 시작 인덱스
        while (pos < 4) // 모든 팀을 살펴볼 때까지 반복!
        {
            int start = pos; // 현재 그룹의 시작점
            int score = list[pos].pts; // 현재 그룹의 점수
            // 같은 점수를 가진 팀들이 어디까지인지 찾아봐요!
            while (pos + 1 < 4 && list[pos + 1].pts == score) pos++;
            int end = pos; // 현재 그룹의 끝점
            int groupSize = end - start + 1; // 현재 점수 그룹에 속한 팀의 수

            // 👑 이 그룹에서 '상위 2팀' 안에 들어갈 수 있는 자리가 몇 개인지 계산해요.
            int topSlots = 0;
            // 예를 들어, 0위부터 1위까지가 상위 2팀이죠!
            int a = Math.Max(start, 0); // 그룹의 시작이 0위보다 앞에 있으면 0위부터
            int b = Math.Min(end, 1);   // 그룹의 끝이 1위보다 뒤에 있으면 1위까지
            if (b >= a) topSlots = b - a + 1; // 해당 그룹이 상위 2팀 자리에 얼마나 걸쳐있는지!
            else topSlots = 0;

            // 만약 현재 그룹에 상위 2팀 진출권에 해당하는 자리가 있다면!
            if (topSlots > 0)
            {
                // '추첨' 규칙에 따라 진출권을 가진 팀 수만큼 확률을 균등하게 나눠줘요!
                // (진출권 개수 / 그룹 팀 수) * 현재 시나리오 확률
                double share = (double)topSlots / groupSize * scenarioProb;
                // 해당 그룹에 속한 모든 팀에게 이 진출 확률을 더해줘요!
                for (int i = start; i <= end; i++)
                {
                    int teamIdx = list[i].team;
                    result[teamIdx] += share;
                }
            }

            pos++; // 다음 순위 그룹으로 넘어가요!
        }
    }
}