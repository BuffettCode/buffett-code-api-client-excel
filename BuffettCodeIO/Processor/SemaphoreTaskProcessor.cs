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
        private readonly SemaphoreSlim semaphore;

        /// <summary>
        /// 最大同時実行数のデフォルト値
        /// </summary>
        /// <remarks>
        /// API Gatewayの設定値が20リクエスト数/秒。
        /// 1リクエストあたりのレスポンスタイムをざっくり0.5秒として10並列。少し余裕を見て8。
        /// </remarks>
        private static readonly int DEFAULT_MAX_DEGREE_OF_PARALLELISM = 8;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="maxDegreeOfParallelism">同時に実行可能なタスクの数</param>
        public SemaphoreTaskProcessor(int maxDegreeOfParallelism)
        {
            if (maxDegreeOfParallelism == Configuration.USE_DEFAULT_MAX_DEGREE_OF_PARALLELISM)
            {
                maxDegreeOfParallelism = DEFAULT_MAX_DEGREE_OF_PARALLELISM;
            }
            semaphore = new SemaphoreSlim(maxDegreeOfParallelism);
        }

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
