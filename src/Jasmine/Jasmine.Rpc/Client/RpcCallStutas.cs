namespace Jasmine.Rpc.Client
{
    public  enum RpcCallStutas
    {
        /// <summary>
        /// 已添加到发送队列
        /// </summary>
        Scheduled=1,
        /// <summary>
        /// 已发送
        /// </summary>
        Sended=2,
        /// <summary>
        /// 等待回复
        /// </summary>
        WaitForResponse=3,
        /// <summary>
        /// 超时
        /// </summary>
        Timeout=4,
        /// <summary>
        /// 已完成
        /// </summary>
        Finished=5,
        /// <summary>
        /// 连接断开
        /// </summary>
        ConnectionClosed,

    }
}
