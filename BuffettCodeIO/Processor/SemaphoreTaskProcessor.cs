using System;
using System.Threading;
using System.Threading.Tasks;

namespace BuffettCodeIO.Processor
{
    /// <summary>
    /// <see cref="SemaphoreSlim"/>を利用した<see cref="ITaskProcessor"/>
    /// </summary>
    /// <typeparam name="Type">Taskの型</typeparam>
    public class SemaphoreTaskProcessor<Type> : ITaskProcessor<Type>
    {
        private SemaphoreSlim semaphore;
        private int maxDegreeOfParallelism;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="maxDegreeOfParallelism">同時に実行可能なタスクの数</param>
        public SemaphoreTaskProcessor(int maxDegreeOfParallelism)
        {
            if (maxDegreeOfParallelism < 1)
            {
                throw new ArgumentOutOfRangeException($"maxDegreeOfParallelism must be positive integer, but given {maxDegreeOfParallelism}");
            }
            this.maxDegreeOfParallelism = maxDegreeOfParallelism;
            semaphore = new SemaphoreSlim(maxDegreeOfParallelism);
        }

        public void UpdateMaxDegreeOfParallelism(int maxDegreeOfParallelism)
        {
            this.maxDegreeOfParallelism = maxDegreeOfParallelism;
            semaphore = new SemaphoreSlim(maxDegreeOfParallelism);
        }

        public int GetMaxDegreeOfParallelism() => maxDegreeOfParallelism;

        /// <inheritdoc/>
        public Type Process(Task<Type> task)
        {
            return CreateWrapperTask(task).Result;
        }

        private Task<Type> CreateWrapperTask(Task<Type> task)
        {
            return Task.Run(async () =>
            {
                try
                {
                    await semaphore.WaitAsync();
                    return task.Result;
                }
                finally
                {
                    semaphore.Release();
                }
            });
        }
    }
}