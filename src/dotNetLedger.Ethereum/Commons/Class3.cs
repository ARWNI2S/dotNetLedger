using Nethereum.JsonRpc.Client;
using Nethereum.JsonRpc.Client.RpcMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotNetLedger.Ethereum.Commons
{
    public interface IClient : IBaseClient
    {
        Task<RpcRequestResponseBatch> SendBatchRequestAsync(RpcRequestResponseBatch rpcRequestResponseBatch);
        Task<T> SendRequestAsync<T>(RpcRequest request, string route = null);

        Task<RpcResponseMessage> SendAsync(RpcRequestMessage rpcRequestMessage, string route = null);

        Task<T> SendRequestAsync<T>(string method, string route = null, params object[] paramList);
    }

    public interface IBaseClient
    {
#if !DOTNET35
        RequestInterceptor OverridingRequestInterceptor { get; set; }

        T DecodeResult<T>(RpcResponseMessage rpcResponseMessage);
#endif
        Task SendRequestAsync(RpcRequest request, string route = null);
        Task SendRequestAsync(string method, string route = null, params object[] paramList);
    }
}
