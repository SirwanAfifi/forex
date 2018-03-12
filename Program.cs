using System;
using System.Collections;
using System.Linq;

namespace forex
{
    class Program
    {
        private static int[] RSI = {63, 66, 69, 74, 75, 79, 89, 72, 63};
        /*
         *    63, 66, 69, 74, 75, 79, 89, 72, 63 = 89
         *    63, 66, 69, 74, 75, 79, 72, 63     = 79
         *    63, 66, 69, 74, 75, 72, 63         = 75
         *    63, 66, 69, 75, 72, 63             = 74
         *    63, 66, 69                         = 69
         *    63, 66                             = 66
         *    63                                 = 63
         * 
         */
        private static int OverBought = 70;
        
        static void Main(string[] args)
        {
           /* for (int i = 0; i < RSI.Length; i++)
            {
                ProcessBearish(i);
            }*/
            //GetMax();
            FindMax(RSI.Length - 1);
        }

        private static bool IsPeak(int bar)
        {
            var i = 0;
            if (RSI[bar] > OverBought && RSI[bar] > RSI[bar + 1] && RSI[bar] > RSI[bar - 1]) 
            {
                for (i = bar + 1; i < RSI.Length; i++) {
                    if (RSI[i] < OverBought)
                    {
                        Console.WriteLine($"Bar: {bar}, Value: {RSI[bar]}");
                        return true;
                    }
                    else {
                        if (RSI[bar] < RSI[i])
                            return false;
                    }
                }
            }

            return false;
        }

        private static int PrevPeak(int bar)
        {
            var i = 0;
            for (i = bar + 5; i < RSI.Length; i++)
            {
                if (RSI[i] >= RSI[i + 1] && RSI[i] > RSI[i + 2] && RSI[i] >= RSI[i - 1] && RSI[i] > RSI[i - 2])
                {
                    return i;
                }
            }
            return -1;
        }

        private static void ProcessBearish(int bar)
        {
            if (IsPeak(bar))
            {
                int curr, prev;
                curr = bar;
                prev = PrevPeak(bar);
                if (prev != -1)
                {
                    if (RSI[curr] < RSI[prev])
                        Console.WriteLine($"start: {prev}, end: {curr}");
                    else if (RSI[curr] > RSI[prev])
                        Console.WriteLine($"start: {prev}, end: {curr}");
                    }
                }
                
            }

        private static void GetMax()
        {
            var max = 0;
            max = FetchMax(max);

            Console.WriteLine(max);
            Console.WriteLine(string.Join(", ", RSI));
        }

        private static int FetchMax(int max)
        {
            for (var i = 0; i < RSI.Length; i++)
            {
                if (RSI[i] > max)
                    max = RSI[i];
            }
            return max;
        }
        
        private static int FindMax(int size)
        {
            if (size == 1)
                return RSI[0];
            var max = FindMax(size-1);
            var s = RSI[size - 1] > max ? RSI[size - 1] : max;
            Console.WriteLine(max);
            return s;
        }
    }
}

