using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BuffettCodeAddin.Processor.UnitTests
{
    [TestClass]
    public class SemaphoreTaskProcessorTest
    {
        [TestMethod]
        public void TestProcessSequential()
        {
            var processor = new SemaphoreTaskProcessor<string>(1);
            var tasks = CreateTaskList(processor, 10, 1000);

            // 同時実行数1で10タスク実行
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string[] results = Task.WhenAll(tasks).Result;
            stopwatch.Stop();

            // 約10秒かかってるはず
            var elapsed = stopwatch.ElapsedMilliseconds;
            System.Console.WriteLine(elapsed + " milliseconds elapsed.");
            Assert.IsTrue(20000 > elapsed && elapsed > 8000);
        }

        [TestMethod]
        public void TestProcessParallel()
        {
            var processor = new SemaphoreTaskProcessor<string>(3);
            var tasks = CreateTaskList(processor, 10, 1000);

            // 同時実行数3で10タスク実行
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string[] results = Task.WhenAll(tasks).Result;
            stopwatch.Stop();

            // 約4秒かかってるはず
            var elapsed = stopwatch.ElapsedMilliseconds;
            System.Console.WriteLine(elapsed + " milliseconds elapsed.");
            Assert.IsTrue(6000 > elapsed && elapsed > 2000);
        }

        private List<Task<string>> CreateTaskList(SemaphoreTaskProcessor<string> processor, int size, int delay)
        {
            var tasks = new List<Task<string>>();
            for (int i = 0; i < size; i++)
            {
                tasks.Add(Task.Run(() => processor.Process(CreateTask(delay))));
            }
            return tasks;
        }

        private Task<string> CreateTask(int delay)
        {
            return Task.Run(async () =>
            {
                await Task.Delay(delay);
                return "dummy";
            });
        }
    }
}
