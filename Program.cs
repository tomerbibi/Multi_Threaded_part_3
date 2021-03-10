using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multi_Threaded_part_3
{
    class Program
    {
        // 33)
        static CancellationTokenSource tokenSource = new CancellationTokenSource();
        static CancellationToken token = tokenSource.Token;
        static int count = 0;
        public static void MyTimer()
        {
            while (!tokenSource.IsCancellationRequested)
            {
                Thread.Sleep(1000);
                count++;
            }
        }

        // 39)
        public static async Task ReadFromFile()
        {
            await Task.Run(() => 
            {
                var reader = File.OpenText("TextFile1.txt");
                Console.WriteLine(reader.ReadToEnd());
            });
        }

        static void Main(string[] args)
        {
            // 31)

            // a)
            Task<int> t1 = new Task<int>(() =>
            {
                int x = 0;
                return 6 / x;
            });
            t1.RunSynchronously();

            // b)
            //Console.WriteLine(t1.Result);

            // c)
            try
            {
                Task<int> t2 = new Task<int>(() =>
                {
                    int x = 0;
                    return 6 / x;
                });
                t2.RunSynchronously();
                Console.WriteLine(t2.Result);
            }

            catch (Exception)
            {
                Console.WriteLine("cannot divede by zero");
            }

            // 33)

            Console.WriteLine("timer is starting");
            Task t3 = Task.Run(() => MyTimer(), token);
            Console.ReadLine();
            tokenSource.Cancel();
            Thread.Sleep(5000);
            Console.WriteLine($"timer is stopped: {count}");

            // 39)
            Task t10 = ReadFromFile();
            t10.Wait();
        }
    }
}
