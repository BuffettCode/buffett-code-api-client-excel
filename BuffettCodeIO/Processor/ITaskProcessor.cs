using System.Threading.Tasks;

namespace BuffettCodeIO.Processor
{
    /// <summary>
    /// タスクプロセッサインタフェース
    /// </summary>
    /// <remarks>
    /// WebAPIのAPIコールを実行するTaskを一元的に受け取り、同時実行数の制御をするためのインタフェース。
    /// 以下のようなモチベーションで作成してます。
    /// <list>
    /// <item>
    /// <description>ユーザのPCスペックによってはExcelアドインの実行でPCに負担が掛かるかもしれず、
    /// その場合に負荷を抑えたい</description>
    /// </item>
    /// <item>
    /// <description>バフェットコードのWebAPIには「秒あたりxx件」という形でスロットリングの制限があり、
    /// この制限に引っかかる場合に実行ペースを一定以下にしたい。</description>
    /// </item>
    /// </list>
    /// </remarks>
    interface ITaskProcessor<Type>
    {
        /// <summary>
        /// タスクを実行します。
        /// </summary>
        /// <param name="task">タスク</param>
        /// <returns>タスクの実行結果</returns>
        Type Process(Task<Type> task);
    }
}
