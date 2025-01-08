using System;
using System.Threading;

namespace GameOfVlad.Utils
{
    public static class RandomV
    {
        public static int GetRandom1(int min, int max)
        {
            Thread.Sleep(1);
            return new Random(DateTime.Now.Millisecond).Next(min,max);
        }
        public static int GetRandom5(int min, int max)
        {
            Thread.Sleep(20);
            return new Random(DateTime.Now.Millisecond).Next(min, max);
        }

        //public static float RandomFloat(float min, float max)
        //{
        //    Thread.Sleep(1);
        //    return new Random(DateTime.Now.Millisecond).
        //}
    }
}
