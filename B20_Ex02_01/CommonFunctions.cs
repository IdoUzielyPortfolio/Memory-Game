using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex02_01
{
    class CommonFunctions
    {
        public static void Swap(ref GameCell i_GC1, ref GameCell i_GC2) //ready
        {
            GameCell temp = i_GC1;
            i_GC1 = i_GC2;
            i_GC2 = temp;
        }
        public static int Random(int i_Range)
        {
            Random r = new Random();
            return r.Next(i_Range);
        }
    }
}
