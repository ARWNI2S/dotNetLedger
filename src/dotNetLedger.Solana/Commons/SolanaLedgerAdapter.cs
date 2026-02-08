using dotNetLedger.Commons;
using Solnet.Rpc;

namespace dotNetLedger.Solana.Commons
{
    public class SolanaLedgerAdapter : ILedgerAdapter
    {
        IRpcClient _rpcClient = ClientFactory.GetClient(Cluster.MainNet);
    }
}
