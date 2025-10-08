using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int N = int.Parse(Console.ReadLine());

        // 최대 힙 (왼쪽)
        var leftHeap = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
        // 최소 힙 (오른쪽)
        var rightHeap = new PriorityQueue<int, int>();

        for (int i = 0; i < N; i++)
        {
            int num = int.Parse(Console.ReadLine());

            // 삽입: leftHeap 크기 <= rightHeap 크기 → leftHeap에 넣음
            if (leftHeap.Count <= rightHeap.Count)
                leftHeap.Enqueue(num, num);
            else
                rightHeap.Enqueue(num, num);

            // 균형 조정: left의 최대 > right의 최소면 swap
            if (leftHeap.Count > 0 && rightHeap.Count > 0 && leftHeap.Peek() > rightHeap.Peek())
            {
                int leftTop = leftHeap.Dequeue();
                int rightTop = rightHeap.Dequeue();

                leftHeap.Enqueue(rightTop, rightTop);
                rightHeap.Enqueue(leftTop, leftTop);
            }

            // 현재 중앙값 출력
            Console.WriteLine(leftHeap.Peek());
        }
    }
}
