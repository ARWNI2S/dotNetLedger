using dotNetLedger.Commons;

namespace dotNetLedger.Database.Commons
{
    public class DatabaseLedgerAdapter : ILedgerAdapter
    {
        //private readonly RPCClient _rpcClient = default!;

        //public HttpClient? HttpClient => _rpcClient.HttpClient;
        //public Uri Address => _rpcClient.Address;
        //public LedgerConnectionString CredentialString => _rpcClient.CredentialString;
        //public Network Network { get; }
        //public RPCCapabilities Capabilities { get; set; }
        //public string Authentication { get; }
        //public bool AllowBatchFallback { get; set; }

        //public RPCClient(Network network);

        //public RPCClient(RPCCredentialString credentials, Network network);

        //public RPCClient(RPCCredentialString credentials, string host, Network network);

        //public RPCClient(RPCCredentialString credentials, Uri address, Network network);

        //public async Task<RPCCapabilities> ScanRPCCapabilitiesAsync(CancellationToken cancellationToken = default(CancellationToken));

        //public RPCCapabilities ScanRPCCapabilities();

        //public static string GetDefaultCookieFilePath(Network network);

        //public static string TryGetDefaultCookieFilePath(Network network);

        //public RPCClient(string authenticationString, string hostOrUri, Network network);

        //public RPCClient(NetworkCredential credentials, Uri address, Network network = null);

        //public RPCClient(string authenticationString, Uri address, Network network = null);

        //public RPCClient PrepareBatch();

        //public RPCClient Clone();

        //public RPCResponse SendCommand(RPCOperations commandName, params object[] parameters);

        //public RPCResponse SendCommand(string commandName, params object[] parameters);

        //public RPCResponse SendCommand(RPCOperations commandName, CancellationToken cancellationToken, params object[] parameters);

        //public RPCResponse SendCommand(string commandName, CancellationToken cancellationToken, params object[] parameters);

        //public BitcoinAddress GetNewAddress(CancellationToken cancellationToken = default(CancellationToken));

        //public BitcoinAddress GetNewAddress(GetNewAddressRequest request);

        //public async Task<BitcoinAddress> GetNewAddressAsync(CancellationToken cancellationToken = default(CancellationToken));

        //public async Task<GetBlockFromPeerResult> GetBlockFromPeer(uint256 blockHash, int peerId, CancellationToken cancellationToken = default(CancellationToken));

        //public async Task<BitcoinAddress> GetNewAddressAsync(GetNewAddressRequest request, CancellationToken cancellationToken = default(CancellationToken));

        //public BitcoinAddress GetRawChangeAddress();

        //public async Task<BitcoinAddress> GetRawChangeAddressAsync();

        //public Task<RPCResponse> SendCommandAsync(RPCOperations commandName, params object[] parameters);

        //public Task<RPCResponse> SendCommandAsync(RPCOperations commandName, CancellationToken cancellationToken, params object[] parameters);

        //public RPCResponse SendCommandWithNamedArgs(string commandName, Dictionary<string, object> parameters, CancellationToken cancellationToken);

        //public Task<RPCResponse> SendCommandWithNamedArgsAsync(string commandName, Dictionary<string, object> parameters, CancellationToken cancellationToken);

        //public Task<RPCResponse> SendCommandAsync(string commandName, params object[] parameters);

        //public Task<RPCResponse> SendCommandAsync(string commandName, CancellationToken cancellationToken, params object[] parameters);

        //public void SendBatch();

        //public void CancelBatch();

        //public async Task StopAsync(CancellationToken cancellationToken = default(CancellationToken));

        //public void Stop(CancellationToken cancellationToken = default(CancellationToken));

        //public TimeSpan Uptime();

        //public async Task<TimeSpan> UptimeAsync(CancellationToken cancellationToken = default(CancellationToken));

        //public async Task<ImportDescriptorResult[]> ImportDescriptors(IEnumerable<ImportDescriptorParameters> descriptors, CancellationToken cancellationToken = default(CancellationToken));

        //public ScanTxoutSetResponse StartScanTxoutSet(ScanTxoutSetParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        //public async Task<ScanTxoutSetResponse> StartScanTxoutSetAsync(ScanTxoutSetParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        //public async Task<decimal?> GetStatusScanTxoutSetAsync(CancellationToken cancellationToken = default(CancellationToken));

        //public decimal? GetStatusScanTxoutSet();

        //public async Task<bool> AbortScanTxoutSetAsync(CancellationToken cancellationToken = default(CancellationToken));

        //public bool AbortScanTxoutSet();

        //public async Task SendBatchAsync(CancellationToken cancellationToken = default(CancellationToken));

        //public async Task<RPCResponse> SendCommandAsync(RPCRequest request, CancellationToken cancellationToken = default(CancellationToken));

        //public PeerInfo[] GetPeersInfo();

        //public async Task<PeerInfo[]> GetPeersInfoAsync(CancellationToken cancellationToken = default(CancellationToken));

        //public Task DisconnectNode(EndPoint endPoint, CancellationToken cancellationToken = default(CancellationToken));

        //public Task DisconnectNode(int peerId, CancellationToken cancellationToken = default(CancellationToken));

        //public void AddNode(EndPoint nodeEndPoint, bool onetry = false);

        //public async Task AddNodeAsync(EndPoint nodeEndPoint, bool onetry = false, CancellationToken cancellationToken = default(CancellationToken));

        //public void RemoveNode(EndPoint nodeEndPoint);

        //public async Task RemoveNodeAsync(EndPoint nodeEndPoint, CancellationToken cancellationToken = default(CancellationToken));

        //public async Task<AddedNodeInfo[]> GetAddedNodeInfoAsync(bool detailed, CancellationToken cancellationToken = default(CancellationToken));

        //public AddedNodeInfo[] GetAddedNodeInfo(bool detailed);

        //public AddedNodeInfo GetAddedNodeInfo(bool detailed, EndPoint nodeEndPoint);

        //public async Task<AddedNodeInfo> GetAddedNodeInfoAync(bool detailed, EndPoint nodeEndPoint, CancellationToken cancellationToken = default(CancellationToken));

        //public async Task<BlockchainInfo> GetBlockchainInfoAsync(CancellationToken cancellationToken = default(CancellationToken));

        //public BlockchainInfo GetBlockchainInfo();

        //public uint256 GetBestBlockHash();

        //public async Task<uint256> GetBestBlockHashAsync(CancellationToken cancellationToken = default(CancellationToken));

        //public BlockHeader GetBlockHeader(int height);

        //public BlockHeader GetBlockHeader(uint height);

        //public async Task<BlockHeader> GetBlockHeaderAsync(int height, CancellationToken cancellationToken = default(CancellationToken));

        //public async Task<BlockHeader> GetBlockHeaderAsync(uint height, CancellationToken cancellationToken = default(CancellationToken));

        //public async Task<Block> GetBlockAsync(uint256 blockId, CancellationToken cancellationToken = default(CancellationToken));

        //public Block GetBlock(uint256 blockId);

        //public Block GetBlock(int height);

        //public Block GetBlock(uint height);

        //public async Task<Block> GetBlockAsync(int height, CancellationToken cancellationToken = default(CancellationToken));

        //public async Task<Block> GetBlockAsync(uint height, CancellationToken cancellationToken = default(CancellationToken));

        //public BlockHeader GetBlockHeader(uint256 blockHash);

        //public async Task<BlockHeader> GetBlockHeaderAsync(uint256 blockHash, CancellationToken cancellationToken = default(CancellationToken));

        //public GetBlockRPCResponse GetBlock(uint256 blockHash, GetBlockVerbosity verbosity);

        //public async Task<GetBlockRPCResponse> GetBlockAsync(uint256 blockHash, GetBlockVerbosity verbosity, CancellationToken cancellationToken = default(CancellationToken));

        //public uint256 GetBlockHash(int height);

        //public uint256 GetBlockHash(uint height);

        //public async Task<uint256> GetBlockHashAsync(int height, CancellationToken cancellationToken = default(CancellationToken));

        //public async Task<uint256> GetBlockHashAsync(uint height, CancellationToken cancellationToken = default(CancellationToken));

        //public BlockFilter GetBlockFilter(uint256 blockHash);

        //public async Task<BlockFilter> GetBlockFilterAsync(uint256 blockHash, CancellationToken cancellationToken = default(CancellationToken));

        //public BlockStats GetBlockStats(uint256 blockHash, string[] stats = null);

        //public Task<BlockStats> GetBlockStatsAsync(uint256 blockHash, CancellationToken cancellationToken = default(CancellationToken));

        //public async Task<BlockStats> GetBlockStatsAsync(uint256 blockHash, string[] stats, CancellationToken cancellationToken = default(CancellationToken));

        //public int GetBlockCount();

        //public async Task<int> GetBlockCountAsync(CancellationToken cancellationToken = default(CancellationToken));

        //public MemPoolInfo GetMemPool();

        //public async Task<MemPoolInfo> GetMemPoolAsync(CancellationToken cancellationToken = default(CancellationToken));

        //public uint256[] GetRawMempool();

        //public async Task<uint256[]> GetRawMempoolAsync(CancellationToken cancellationToken = default(CancellationToken));

        //public MempoolEntry GetMempoolEntry(uint256 txid, bool throwIfNotFound = true);

        //public async Task<MempoolEntry> GetMempoolEntryAsync(uint256 txid, bool throwIfNotFound = true, CancellationToken cancellationToken = default(CancellationToken));

        //public async Task SaveMempoolAsync(CancellationToken cancellationToken = default(CancellationToken));

        //public void SaveMempool();

        //public MempoolAcceptResult TestMempoolAccept(Transaction transaction, TestMempoolParameters? parameters, CancellationToken cancellationToken = default(CancellationToken));

        //public MempoolAcceptResult TestMempoolAccept(Transaction transaction, CancellationToken cancellationToken = default(CancellationToken));

        //public async Task<MempoolAcceptResult> TestMempoolAcceptAsync(Transaction transaction, CancellationToken cancellationToken = default(CancellationToken));

        //public async Task<MempoolAcceptResult> TestMempoolAcceptAsync(Transaction transaction, TestMempoolParameters? parameters, CancellationToken cancellationToken = default(CancellationToken));

        //public GetTxOutResponse GetTxOut(uint256 txid, int index, bool includeMempool = true);

        //public async Task<GetTxOutResponse> GetTxOutAsync(uint256 txid, int index, bool includeMempool = true, CancellationToken cancellationToken = default(CancellationToken));

        //public GetTxOutSetInfoResponse GetTxoutSetInfo();

        //public async Task<GetTxOutSetInfoResponse> GetTxoutSetInfoAsync(CancellationToken cancellationToken = default(CancellationToken));

        //public IEnumerable<Transaction> GetTransactions(uint256 blockHash, CancellationToken cancellationToken = default(CancellationToken));

        //public IEnumerable<Transaction> GetTransactions(int height);

        //public Transaction DecodeRawTransaction(string rawHex);

        //public Transaction DecodeRawTransaction(byte[] raw);

        //public Task<Transaction> DecodeRawTransactionAsync(string rawHex);

        //public Task<Transaction> DecodeRawTransactionAsync(byte[] raw);

        //public Transaction GetRawTransaction(uint256 txid, bool throwIfNotFound = true);

        //public Task<Transaction> GetRawTransactionAsync(uint256 txid, bool throwIfNotFound = true, CancellationToken cancellationToken = default(CancellationToken));

        //public Transaction GetRawTransaction(uint256 txid, uint256 blockId, bool throwIfNotFound = true);

        //public async Task<Transaction> GetRawTransactionAsync(uint256 txid, uint256 blockId, bool throwIfNotFound = true, CancellationToken cancellationToken = default(CancellationToken));

        //public RawTransactionInfo GetRawTransactionInfo(uint256 txid);

        //public async Task<RawTransactionInfo> GetRawTransactionInfoAsync(uint256 txId, CancellationToken cancellationToken = default(CancellationToken));

        //public uint256 SendRawTransaction(Transaction tx);

        //public uint256 SendRawTransaction(byte[] bytes);

        //public Task<uint256> SendRawTransactionAsync(Transaction tx, CancellationToken cancellationToken = default(CancellationToken));

        //public async Task<uint256> SendRawTransactionAsync(byte[] bytes, CancellationToken cancellationToken = default(CancellationToken));

        //public BumpResponse BumpFee(uint256 txid);

        //public async Task<BumpResponse> BumpFeeAsync(uint256 txid, CancellationToken cancellationToken = default(CancellationToken));

        //public EstimateSmartFeeResponse EstimateSmartFee(int confirmationTarget, EstimateSmartFeeMode? estimateMode = null);

        //public async Task<EstimateSmartFeeResponse> TryEstimateSmartFeeAsync(int confirmationTarget, EstimateSmartFeeMode? estimateMode = null, CancellationToken cancellationToken = default(CancellationToken));

        //public EstimateSmartFeeResponse TryEstimateSmartFee(int confirmationTarget, EstimateSmartFeeMode? estimateMode = null);

        //public async Task<EstimateSmartFeeResponse> EstimateSmartFeeAsync(int confirmationTarget, EstimateSmartFeeMode? estimateMode = null, CancellationToken cancellationToken = default(CancellationToken));

        //public uint256 SendToAddress(BitcoinAddress address, Money amount, SendToAddressParameters? parameters, CancellationToken cancellationToken = default(CancellationToken));

        //public uint256 SendToAddress(BitcoinAddress address, Money amount, CancellationToken cancellationToken = default(CancellationToken));

        //public uint256 SendToAddress(Script scriptPubKey, Money amount, CancellationToken cancellationToken = default(CancellationToken));

        //public Task<uint256> SendToAddressAsync(Script scriptPubKey, Money amount, SendToAddressParameters? parameters, CancellationToken cancellationToken = default(CancellationToken));

        //public Task<uint256> SendToAddressAsync(Script scriptPubKey, Money amount, CancellationToken cancellationToken = default(CancellationToken));

        //public uint256 SendToAddress(Script scriptPubKey, Money amount, SendToAddressParameters? parameters, CancellationToken cancellationToken = default(CancellationToken));

        //public Task<uint256> SendToAddressAsync(BitcoinAddress address, Money amount, CancellationToken cancellationToken = default(CancellationToken));

        //public async Task<uint256> SendToAddressAsync(BitcoinAddress address, Money amount, SendToAddressParameters? parameters, CancellationToken cancellationToken = default(CancellationToken));

        //public bool SetTxFee(FeeRate feeRate, CancellationToken cancellationToken = default(CancellationToken));

        //public async Task<uint256[]> GenerateAsync(int nBlocks, CancellationToken cancellationToken = default(CancellationToken));

        //public uint256[] Generate(int nBlocks);

        //public async Task<uint256[]> GenerateToAddressAsync(int nBlocks, BitcoinAddress address, CancellationToken cancellationToken = default(CancellationToken));

        //public uint256[] GenerateToAddress(int nBlocks, BitcoinAddress address);

        //public void InvalidateBlock(uint256 blockhash, CancellationToken cancellationToken = default(CancellationToken));

        //public async Task InvalidateBlockAsync(uint256 blockhash, CancellationToken cancellationToken = default(CancellationToken));

        //public bool AddPeerAddress(IPAddress ip, int port);

        //public async Task<bool> AddPeerAddressAsync(IPAddress ip, int port, CancellationToken cancellationToken = default(CancellationToken));

        //public RPCClient SetWalletContext(string? walletName);

        //public async Task<RPCClient> CreateWalletAsync(string walletNameOrPath, CreateWalletOptions? options = null, CancellationToken cancellationToken = default(CancellationToken));

        //public RPCClient CreateWallet(string walletNameOrPath, CreateWalletOptions? options = null);

        //public Task<RPCClient> LoadWalletAsync(bool? loadOnStartup = null);

        //public RPCClient LoadWallet(bool? loadOnStartup = null);

        //public async Task<RPCClient> LoadWalletAsync(string? walletName, bool? loadOnStartup = null, CancellationToken cancellationToken = default(CancellationToken));

        //public RPCClient LoadWallet(string? walletName, bool? loadOnStartup = null);

        //public void UnloadWallet();

        //public void UnloadWallet(bool? loadOnStartup = null);

        //public Task UnloadWalletAsync(bool? loadOnStartup = null);

        //public Task UnloadWalletAsync(string? walletName, bool? loadOnStartup = null, CancellationToken cancellationToken = default(CancellationToken));

        //public void BackupWallet(string path);

        //public async Task BackupWalletAsync(string path);

        //public BitcoinSecret DumpPrivKey(BitcoinAddress address, CancellationToken cancellationToken = default(CancellationToken));

        //public async Task<BitcoinSecret> DumpPrivKeyAsync(BitcoinAddress address)
        //{
        //    RPCResponse rPCResponse = await SendCommandAsync(RPCOperations.dumpprivkey, address.ToString()).ConfigureAwait(continueOnCapturedContext: false);
        //    return Network.Parse<BitcoinSecret>((string?)rPCResponse.Result);
        //}

        //public FundRawTransactionResponse FundRawTransaction(Transaction transaction, FundRawTransactionOptions options = null);

        //public GetAddressInfoResponse GetAddressInfo(IDestination address);

        //public async Task<GetAddressInfoResponse> GetAddressInfoAsync(IDestination address);

        //public Money GetBalance(int minConf, bool includeWatchOnly);

        //public Money GetBalance();

        //public async Task<Money> GetBalanceAsync();

        //public async Task<Money> GetBalanceAsync(int minConf, bool includeWatchOnly);

        //public async Task<FundRawTransactionResponse> FundRawTransactionAsync(Transaction transaction, FundRawTransactionOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

        //public Money GetReceivedByAddress(BitcoinAddress address);

        //public async Task<Money> GetReceivedByAddressAsync(BitcoinAddress address);

        //public Money GetReceivedByAddress(BitcoinAddress address, int confirmations);

        //public async Task<Money> GetReceivedByAddressAsync(BitcoinAddress address, int confirmations);

        //public IEnumerable<AddressGrouping> ListAddressGroupings();

        //public IEnumerable<BitcoinSecret> ListSecrets();

        //public UnspentCoin[] ListUnspent();

        //public UnspentCoin[] ListUnspent(int minconf, int maxconf, params BitcoinAddress[] addresses);

        //public async Task<UnspentCoin[]> ListUnspentAsync();

        //public async Task<UnspentCoin[]> ListUnspentAsync(int minconf, int maxconf, params BitcoinAddress[] addresses);

        //public async Task<UnspentCoin[]> ListUnspentAsync(ListUnspentOptions options, params BitcoinAddress[] addresses);

        //public async Task<UnspentCoin[]> ListUnspentAsync(ListUnspentOptions options, CancellationToken cancellationToken, params BitcoinAddress[] addresses);

        //public async Task<OutPoint[]> ListLockUnspentAsync();

        //public OutPoint[] ListLockUnspent();

        //public void AbandonTransaction(uint256 txId);

        //public async Task AbandonTransactionAsync(uint256 txId);

        //public void LockUnspent(params OutPoint[] outpoints);

        //public void UnlockUnspent(params OutPoint[] outpoints);

        //public Task LockUnspentAsync(params OutPoint[] outpoints);

        //public Task UnlockUnspentAsync(params OutPoint[] outpoints);

        //public void WalletPassphrase(string passphrase, int timeout);

        //public async Task WalletPassphraseAsync(string passphrase, int timeout);

        //public Transaction SignRawTransaction(Transaction tx);

        //public async Task<Transaction> SignRawTransactionAsync(Transaction tx);

        //public SignRawTransactionResponse SignRawTransactionWithKey(SignRawTransactionWithKeyRequest request);

        //public async Task<SignRawTransactionResponse> SignRawTransactionWithKeyAsync(SignRawTransactionWithKeyRequest request, CancellationToken cancellationToken = default(CancellationToken));

        //public SignRawTransactionResponse SignRawTransactionWithWallet(SignRawTransactionRequest request);

        //public async Task<SignRawTransactionResponse> SignRawTransactionWithWalletAsync(SignRawTransactionRequest request, CancellationToken cancellationToken = default(CancellationToken));

        //public WalletProcessPSBTResponse WalletProcessPSBT(PSBT psbt, bool sign = true, SigHash hashType = SigHash.All, bool bip32derivs = false);

        //public async Task<WalletProcessPSBTResponse> WalletProcessPSBTAsync(PSBT psbt, bool sign = true, SigHash sighashType = SigHash.All, bool bip32derivs = false);

        //public WalletCreateFundedPSBTResponse WalletCreateFundedPSBT(TxIn[] inputs, Tuple<Dictionary<BitcoinAddress, Money>, Dictionary<string, string>> outputs, LockTime locktime, FundRawTransactionOptions options = null, bool bip32derivs = false);

        //public async Task<WalletCreateFundedPSBTResponse> WalletCreateFundedPSBTAsync(TxIn[] inputs, Tuple<Dictionary<BitcoinAddress, Money>, Dictionary<string, string>> outputs, LockTime locktime = default(LockTime), FundRawTransactionOptions options = null, bool bip32derivs = false, CancellationToken cancellationToken = default(CancellationToken));

        //public WalletCreateFundedPSBTResponse WalletCreateFundedPSBT(TxIn[] inputs, Dictionary<BitcoinAddress, Money> outputs, LockTime locktime, FundRawTransactionOptions options = null, bool bip32derivs = false);

        //public WalletCreateFundedPSBTResponse WalletCreateFundedPSBT(TxIn[] inputs, Dictionary<string, string> outputs, LockTime locktime, FundRawTransactionOptions options = null, bool bip32derivs = false);

        //public string SigHashToString(SigHash value);
    }

}
