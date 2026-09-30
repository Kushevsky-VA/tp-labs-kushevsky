using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
      
        int N = 10_000_000;
        int[] original = new int[N];
        Random rnd = new Random();
        for (int i = 0; i < N; i++) original[i] = rnd.Next();

    
        int[] arrSeq = (int[])original.Clone();
        int[] arrPar = (int[])original.Clone();
        int[] arrBuiltIn = (int[])original.Clone();

        Stopwatch sw = new Stopwatch();

    
        sw.Start();
        MergeSort(arrSeq, 0, arrSeq.Length - 1);
        sw.Stop();
        long timeSeq = sw.ElapsedMilliseconds;

       
        sw.Restart();
        MergeSortParallel(arrPar, 0, arrPar.Length - 1);
        sw.Stop();
        long timePar = sw.ElapsedMilliseconds;

       
        sw.Restart();
        Array.Sort(arrBuiltIn);
        sw.Stop();
        long timeBuiltIn = sw.ElapsedMilliseconds;

      
        Console.WriteLine($"Ядер доступно: {Environment.ProcessorCount}");
        Console.WriteLine("---------------------------------------------");
        Console.WriteLine($"Последовательно:  {timeSeq} мс");
        Console.WriteLine($"Параллельно:      {timePar} мс (Ускорение: {(double)timeSeq / timePar:F2}x)");
        Console.WriteLine($"Array.Sort:       {timeBuiltIn} мс");
        Console.ReadKey();
    }

    static void MergeSort(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int mid = (left + right) / 2;
            MergeSort(arr, left, mid);
            MergeSort(arr, mid + 1, right);
            Merge(arr, left, mid, right);
        }
    }

   
    static void MergeSortParallel(int[] arr, int left, int right)
    {

        if (right - left < 100_000)
        {
            MergeSort(arr, left, right);
            return;
        }

        int mid = (left + right) / 2;

      
        Task t1 = Task.Run(() => MergeSortParallel(arr, left, mid));
        Task t2 = Task.Run(() => MergeSortParallel(arr, mid + 1, right));

        
        Task.WaitAll(t1, t2);

        
        Merge(arr, left, mid, right);
    }

   
    static void Merge(int[] arr, int left, int mid, int right)
    {
        int[] temp = new int[right - left + 1];
        int i = left, j = mid + 1, k = 0;

        while (i <= mid && j <= right)
            temp[k++] = (arr[i] <= arr[j]) ? arr[i++] : arr[j++];

        while (i <= mid) temp[k++] = arr[i++];
        while (j <= right) temp[k++] = arr[j++];

        Array.Copy(temp, 0, arr, left, temp.Length);
    }
}