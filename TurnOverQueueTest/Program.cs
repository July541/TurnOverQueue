using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turnover_Queue;
using System.Threading;

namespace TurnOverQueueTest
{
    class Program
    {
        static TurnOverQueue<int> q = new TurnOverQueue<int>();
        private static int ProID;
        static void Main(string[] args)
        {
            Thread t1 = new Thread(new ThreadStart(Product));
            Thread t2 = new Thread(new ThreadStart(Consumer));
            t2.Start();
            t1.Start();
            Console.ReadLine();
        }
        public static void Product()
        {
            for(int i = 0; i < 30; i++)
            {
                Console.WriteLine("product No." + (i + 1) + " has been producted");
                q.Add(i + 1);
            }
        }
        public static void Consumer()
        {
            while(true)
            {
                if(q.Get(out ProID))
                {
                    Console.WriteLine("Product No." + ProID + " has been consumed");
                    if (ProID == 30)
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("has no product yet.");
                }
            }
        }
    }
}
