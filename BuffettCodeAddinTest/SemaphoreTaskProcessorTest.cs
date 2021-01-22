using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BuffettCodeAddin.Processor.UnitTests
{
    [TestClass]
    public class SemaphoreTaskProcessorTest
    {
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
