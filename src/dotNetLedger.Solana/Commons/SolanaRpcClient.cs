using Microsoft.Extensions.Logging;
using Solnet.Rpc;
using Solnet.Rpc.Core.Http;
using Solnet.Rpc.Messages;
using Solnet.Rpc.Models;
using Solnet.Rpc.Types;
using Solnet.Rpc.Utilities;

namespace dotNetLedger.Solana.Commons
{
    internal class SolanaRpcClient : JsonRpcClient, IRpcClient
    {
        private readonly IdGenerator _idGenerator = new IdGenerator();

        internal SolanaRpcClient(string url, ILogger logger, HttpClient httpClient = null, IRateLimiter rateLimiter = null)
            : base(url, logger, httpClient, rateLimiter)
        {
        }

        private JsonRpcRequest BuildRequest<T>(string method, IList<object> parameters)
        {
            return new JsonRpcRequest(_idGenerator.GetNextId(), method, parameters);
        }

        private async Task<RequestResult<T>> SendRequestAsync<T>(string method)
        {
            JsonRpcRequest req = BuildRequest<T>(method, null);
            return await SendRequest<T>(req);
        }

        private async Task<RequestResult<T>> SendRequestAsync<T>(string method, IList<object> parameters)
        {
            JsonRpcRequest req = BuildRequest<T>(method, parameters);
            return await SendRequest<T>(req);
        }

        private KeyValue HandleCommitment(Commitment parameter, Commitment defaultValue = Commitment.Finalized)
        {
            if (parameter == defaultValue)
            {
                return null;
            }

            return KeyValue.Create("commitment", parameter);
        }

        private KeyValue HandleTransactionDetails(TransactionDetailsFilterType parameter, TransactionDetailsFilterType defaultValue = TransactionDetailsFilterType.Full)
        {
            if (parameter == defaultValue)
            {
                return null;
            }

            return KeyValue.Create("transactionDetails", parameter);
        }

        public async Task<RequestResult<ResponseValue<TokenMintInfo>>> GetTokenMintInfoAsync(string pubKey, Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<ResponseValue<TokenMintInfo>>("getAccountInfo", Parameters.Create(pubKey, ConfigObject.Create(KeyValue.Create("encoding", "jsonParsed"), HandleCommitment(commitment))));
        }

        public RequestResult<ResponseValue<TokenMintInfo>> GetTokenMintInfo(string pubKey, Commitment commitment = Commitment.Finalized)
        {
            return GetTokenMintInfoAsync(pubKey, commitment).Result;
        }

        public async Task<RequestResult<ResponseValue<TokenAccountInfo>>> GetTokenAccountInfoAsync(string pubKey, Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<ResponseValue<TokenAccountInfo>>("getAccountInfo", Parameters.Create(pubKey, ConfigObject.Create(KeyValue.Create("encoding", "jsonParsed"), HandleCommitment(commitment))));
        }

        public RequestResult<ResponseValue<TokenAccountInfo>> GetTokenAccountInfo(string pubKey, Commitment commitment = Commitment.Finalized)
        {
            return GetTokenAccountInfoAsync(pubKey, commitment).Result;
        }

        public async Task<RequestResult<ResponseValue<AccountInfo>>> GetAccountInfoAsync(string pubKey, Commitment commitment = Commitment.Finalized, BinaryEncoding encoding = BinaryEncoding.Base64)
        {
            return await SendRequestAsync<ResponseValue<AccountInfo>>("getAccountInfo", Parameters.Create(pubKey, ConfigObject.Create(KeyValue.Create("encoding", encoding), HandleCommitment(commitment))));
        }

        public RequestResult<ResponseValue<AccountInfo>> GetAccountInfo(string pubKey, Commitment commitment = Commitment.Finalized, BinaryEncoding encoding = BinaryEncoding.Base64)
        {
            return GetAccountInfoAsync(pubKey, commitment, encoding).Result;
        }

        public async Task<RequestResult<List<AccountKeyPair>>> GetProgramAccountsAsync(string pubKey, Commitment commitment = Commitment.Finalized, int? dataSize = null, IList<MemCmp> memCmpList = null)
        {
            List<object> list = Parameters.Create(ConfigObject.Create(KeyValue.Create("dataSize", dataSize)));
            if (memCmpList != null)
            {
                if (list == null)
                {
                    list = new List<object>();
                }

                list.AddRange(memCmpList.Select((MemCmp filter) => ConfigObject.Create(KeyValue.Create("memcmp", ConfigObject.Create(KeyValue.Create("offset", filter.Offset), KeyValue.Create("bytes", filter.Bytes))))));
            }

            return await SendRequestAsync<List<AccountKeyPair>>("getProgramAccounts", Parameters.Create(pubKey, ConfigObject.Create(KeyValue.Create("encoding", "base64"), KeyValue.Create("filters", list), HandleCommitment(commitment))));
        }

        public RequestResult<List<AccountKeyPair>> GetProgramAccounts(string pubKey, Commitment commitment = Commitment.Finalized, int? dataSize = null, IList<MemCmp> memCmpList = null)
        {
            return GetProgramAccountsAsync(pubKey, commitment, dataSize, memCmpList).Result;
        }

        public async Task<RequestResult<ResponseValue<List<AccountInfo>>>> GetMultipleAccountsAsync(IList<string> accounts, Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<ResponseValue<List<AccountInfo>>>("getMultipleAccounts", Parameters.Create(accounts, ConfigObject.Create(KeyValue.Create("encoding", "base64"), HandleCommitment(commitment))));
        }

        public RequestResult<ResponseValue<List<AccountInfo>>> GetMultipleAccounts(IList<string> accounts, Commitment commitment = Commitment.Finalized)
        {
            return GetMultipleAccountsAsync(accounts, commitment).Result;
        }

        public async Task<RequestResult<ResponseValue<ulong>>> GetBalanceAsync(string pubKey, Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<ResponseValue<ulong>>("getBalance", Parameters.Create(pubKey, ConfigObject.Create(HandleCommitment(commitment))));
        }

        public RequestResult<ResponseValue<ulong>> GetBalance(string pubKey, Commitment commitment = Commitment.Finalized)
        {
            return GetBalanceAsync(pubKey, commitment).Result;
        }

        public async Task<RequestResult<BlockInfo>> GetBlockAsync(ulong slot, Commitment commitment = Commitment.Finalized, TransactionDetailsFilterType transactionDetails = TransactionDetailsFilterType.Full, bool blockRewards = false)
        {
            if (commitment == Commitment.Processed)
            {
                throw new ArgumentException("Commitment.Processed is not supported for this method.");
            }

            return await SendRequestAsync<BlockInfo>("getBlock", Parameters.Create(slot, ConfigObject.Create(KeyValue.Create("encoding", "json"), HandleTransactionDetails(transactionDetails), KeyValue.Create("rewards", blockRewards ? ((object)blockRewards) : null), HandleCommitment(commitment))));
        }

        public RequestResult<BlockInfo> GetBlock(ulong slot, Commitment commitment = Commitment.Finalized, TransactionDetailsFilterType transactionDetails = TransactionDetailsFilterType.Full, bool blockRewards = false)
        {
            return GetBlockAsync(slot, commitment, transactionDetails, blockRewards).Result;
        }

        public async Task<RequestResult<List<ulong>>> GetBlocksAsync(ulong startSlot, ulong endSlot = 0uL, Commitment commitment = Commitment.Finalized)
        {
            if (commitment == Commitment.Processed)
            {
                throw new ArgumentException("Commitment.Processed is not supported for this method.");
            }

            return await SendRequestAsync<List<ulong>>("getBlocks", Parameters.Create(startSlot, (endSlot != 0) ? ((object)endSlot) : null, ConfigObject.Create(HandleCommitment(commitment))));
        }

        public async Task<RequestResult<BlockInfo>> GetConfirmedBlockAsync(ulong slot, Commitment commitment = Commitment.Finalized, TransactionDetailsFilterType transactionDetails = TransactionDetailsFilterType.Full, bool blockRewards = false)
        {
            if (commitment == Commitment.Processed)
            {
                throw new ArgumentException("Commitment.Processed is not supported for this method.");
            }

            return await SendRequestAsync<BlockInfo>("getConfirmedBlock", Parameters.Create(slot, ConfigObject.Create(KeyValue.Create("encoding", "json"), HandleTransactionDetails(transactionDetails), KeyValue.Create("rewards", blockRewards ? ((object)blockRewards) : null), HandleCommitment(commitment))));
        }

        public RequestResult<BlockInfo> GetConfirmedBlock(ulong slot, Commitment commitment = Commitment.Finalized, TransactionDetailsFilterType transactionDetails = TransactionDetailsFilterType.Full, bool blockRewards = false)
        {
            return GetConfirmedBlockAsync(slot, commitment, transactionDetails, blockRewards).Result;
        }

        public RequestResult<List<ulong>> GetBlocks(ulong startSlot, ulong endSlot = 0uL, Commitment commitment = Commitment.Finalized)
        {
            return GetBlocksAsync(startSlot, endSlot, commitment).Result;
        }

        public async Task<RequestResult<List<ulong>>> GetConfirmedBlocksAsync(ulong startSlot, ulong endSlot = 0uL, Commitment commitment = Commitment.Finalized)
        {
            if (commitment == Commitment.Processed)
            {
                throw new ArgumentException("Commitment.Processed is not supported for this method.");
            }

            return await SendRequestAsync<List<ulong>>("getConfirmedBlocks", Parameters.Create(startSlot, (endSlot != 0) ? ((object)endSlot) : null, ConfigObject.Create(HandleCommitment(commitment))));
        }

        public RequestResult<List<ulong>> GetConfirmedBlocks(ulong startSlot, ulong endSlot = 0uL, Commitment commitment = Commitment.Finalized)
        {
            return GetConfirmedBlocksAsync(startSlot, endSlot, commitment).Result;
        }

        public async Task<RequestResult<List<ulong>>> GetConfirmedBlocksWithLimitAsync(ulong startSlot, ulong limit, Commitment commitment = Commitment.Finalized)
        {
            if (commitment == Commitment.Processed)
            {
                throw new ArgumentException("Commitment.Processed is not supported for this method.");
            }

            return await SendRequestAsync<List<ulong>>("getConfirmedBlocksWithLimit", Parameters.Create(startSlot, limit, ConfigObject.Create(HandleCommitment(commitment))));
        }

        public RequestResult<List<ulong>> GetBlocksWithLimit(ulong startSlot, ulong limit, Commitment commitment = Commitment.Finalized)
        {
            return GetBlocksWithLimitAsync(startSlot, limit, commitment).Result;
        }

        public RequestResult<List<ulong>> GetConfirmedBlocksWithLimit(ulong startSlot, ulong limit, Commitment commitment = Commitment.Finalized)
        {
            return GetConfirmedBlocksWithLimitAsync(startSlot, limit, commitment).Result;
        }

        public async Task<RequestResult<List<ulong>>> GetBlocksWithLimitAsync(ulong startSlot, ulong limit, Commitment commitment = Commitment.Finalized)
        {
            if (commitment == Commitment.Processed)
            {
                throw new ArgumentException("Commitment.Processed is not supported for this method.");
            }

            return await SendRequestAsync<List<ulong>>("getBlocksWithLimit", Parameters.Create(startSlot, limit, ConfigObject.Create(HandleCommitment(commitment))));
        }

        public RequestResult<ulong> GetFirstAvailableBlock()
        {
            return GetFirstAvailableBlockAsync().Result;
        }

        public async Task<RequestResult<ulong>> GetFirstAvailableBlockAsync()
        {
            return await SendRequestAsync<ulong>("getFirstAvailableBlock");
        }

        public async Task<RequestResult<ResponseValue<BlockProductionInfo>>> GetBlockProductionAsync(string identity = null, ulong? firstSlot = null, ulong? lastSlot = null, Commitment commitment = Commitment.Finalized)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (commitment != Commitment.Finalized)
            {
                dictionary.Add("commitment", commitment);
            }

            if (!string.IsNullOrEmpty(identity))
            {
                dictionary.Add("identity", identity);
            }

            if (firstSlot.HasValue)
            {
                Dictionary<string, object> dictionary2 = new Dictionary<string, object> { { "firstSlot", firstSlot.Value } };
                if (lastSlot.HasValue)
                {
                    dictionary2.Add("lastSlot", lastSlot.Value);
                }

                dictionary.Add("range", dictionary2);
            }
            else if (lastSlot.HasValue)
            {
                throw new ArgumentException("Range parameters are optional, but the lastSlot argument must be paired with a firstSlot.");
            }

            List<object> parameters = ((dictionary.Count > 0) ? new List<object> { dictionary } : null);
            return await SendRequestAsync<ResponseValue<BlockProductionInfo>>("getBlockProduction", parameters);
        }

        public RequestResult<ResponseValue<BlockProductionInfo>> GetBlockProduction(string identity = null, ulong? firstSlot = null, ulong? lastSlot = null, Commitment commitment = Commitment.Finalized)
        {
            return GetBlockProductionAsync(identity, firstSlot, lastSlot, commitment).Result;
        }

        public RequestResult<string> GetHealth()
        {
            return GetHealthAsync().Result;
        }

        public async Task<RequestResult<string>> GetHealthAsync()
        {
            return await SendRequestAsync<string>("getHealth");
        }

        public RequestResult<Dictionary<string, List<ulong>>> GetLeaderSchedule(ulong slot = 0uL, string identity = null, Commitment commitment = Commitment.Finalized)
        {
            return GetLeaderScheduleAsync(slot, identity, commitment).Result;
        }

        public async Task<RequestResult<Dictionary<string, List<ulong>>>> GetLeaderScheduleAsync(ulong slot = 0uL, string identity = null, Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<Dictionary<string, List<ulong>>>("getLeaderSchedule", Parameters.Create((slot != 0) ? ((object)slot) : null, ConfigObject.Create(HandleCommitment(commitment), KeyValue.Create("identity", identity))));
        }

        public async Task<RequestResult<TransactionMetaSlotInfo>> GetTransactionAsync(string signature, Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<TransactionMetaSlotInfo>("getTransaction", Parameters.Create(signature, ConfigObject.Create(KeyValue.Create("encoding", "json"), HandleCommitment(commitment))));
        }

        public async Task<RequestResult<TransactionMetaSlotInfo>> GetConfirmedTransactionAsync(string signature, Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<TransactionMetaSlotInfo>("getConfirmedTransaction", Parameters.Create(signature, ConfigObject.Create(KeyValue.Create("encoding", "json"), HandleCommitment(commitment))));
        }

        public RequestResult<TransactionMetaSlotInfo> GetTransaction(string signature, Commitment commitment = Commitment.Finalized)
        {
            return GetTransactionAsync(signature, commitment).Result;
        }

        public RequestResult<TransactionMetaSlotInfo> GetConfirmedTransaction(string signature, Commitment commitment = Commitment.Finalized)
        {
            return GetConfirmedTransactionAsync(signature, commitment).Result;
        }

        public async Task<RequestResult<ulong>> GetBlockHeightAsync(Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<ulong>("getBlockHeight", Parameters.Create(ConfigObject.Create(HandleCommitment(commitment))));
        }

        public RequestResult<ulong> GetBlockHeight(Commitment commitment = Commitment.Finalized)
        {
            return GetBlockHeightAsync(commitment).Result;
        }

        public async Task<RequestResult<BlockCommitment>> GetBlockCommitmentAsync(ulong slot)
        {
            return await SendRequestAsync<BlockCommitment>("getBlockCommitment", Parameters.Create(slot));
        }

        public RequestResult<BlockCommitment> GetBlockCommitment(ulong slot)
        {
            return GetBlockCommitmentAsync(slot).Result;
        }

        public async Task<RequestResult<ulong>> GetBlockTimeAsync(ulong slot)
        {
            return await SendRequestAsync<ulong>("getBlockTime", Parameters.Create(slot));
        }

        public RequestResult<ulong> GetBlockTime(ulong slot)
        {
            return GetBlockTimeAsync(slot).Result;
        }

        public async Task<RequestResult<List<ClusterNode>>> GetClusterNodesAsync()
        {
            return await SendRequestAsync<List<ClusterNode>>("getClusterNodes");
        }

        public RequestResult<List<ClusterNode>> GetClusterNodes()
        {
            return GetClusterNodesAsync().Result;
        }

        public async Task<RequestResult<EpochInfo>> GetEpochInfoAsync(Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<EpochInfo>("getEpochInfo", Parameters.Create(ConfigObject.Create(HandleCommitment(commitment))));
        }

        public RequestResult<EpochInfo> GetEpochInfo(Commitment commitment = Commitment.Finalized)
        {
            return GetEpochInfoAsync(commitment).Result;
        }

        public async Task<RequestResult<EpochScheduleInfo>> GetEpochScheduleAsync()
        {
            return await SendRequestAsync<EpochScheduleInfo>("getEpochSchedule");
        }

        public RequestResult<EpochScheduleInfo> GetEpochSchedule()
        {
            return GetEpochScheduleAsync().Result;
        }

        public async Task<RequestResult<ResponseValue<FeeCalculatorInfo>>> GetFeeCalculatorForBlockhashAsync(string blockhash, Commitment commitment = Commitment.Finalized)
        {
            List<object> parameters = Parameters.Create(blockhash, ConfigObject.Create(HandleCommitment(commitment)));
            return await SendRequestAsync<ResponseValue<FeeCalculatorInfo>>("getFeeCalculatorForBlockhash", parameters);
        }

        public RequestResult<ResponseValue<FeeCalculatorInfo>> GetFeeCalculatorForBlockhash(string blockhash, Commitment commitment = Commitment.Finalized)
        {
            return GetFeeCalculatorForBlockhashAsync(blockhash, commitment).Result;
        }

        public async Task<RequestResult<ResponseValue<FeeRateGovernorInfo>>> GetFeeRateGovernorAsync()
        {
            return await SendRequestAsync<ResponseValue<FeeRateGovernorInfo>>("getFeeRateGovernor");
        }

        public RequestResult<ResponseValue<FeeRateGovernorInfo>> GetFeeRateGovernor()
        {
            return GetFeeRateGovernorAsync().Result;
        }

        public async Task<RequestResult<ResponseValue<FeesInfo>>> GetFeesAsync(Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<ResponseValue<FeesInfo>>("getFees", Parameters.Create(ConfigObject.Create(HandleCommitment(commitment))));
        }

        public RequestResult<ResponseValue<FeesInfo>> GetFees(Commitment commitment = Commitment.Finalized)
        {
            return GetFeesAsync(commitment).Result;
        }

        public async Task<RequestResult<ResponseValue<ulong>>> GetFeeForMessageAsync(string message, Commitment commitment = Commitment.Finalized)
        {
            List<object> parameters = Parameters.Create(message, ConfigObject.Create(HandleCommitment(commitment)));
            return await SendRequestAsync<ResponseValue<ulong>>("getFeeForMessage", parameters);
        }

        public RequestResult<ResponseValue<ulong>> GetFeeForMessage(string message, Commitment commitment = Commitment.Finalized)
        {
            return GetFeeForMessageAsync(message, commitment).Result;
        }

        public async Task<RequestResult<ResponseValue<BlockHash>>> GetRecentBlockHashAsync(Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<ResponseValue<BlockHash>>("getRecentBlockhash", Parameters.Create(ConfigObject.Create(HandleCommitment(commitment))));
        }

        public RequestResult<ResponseValue<LatestBlockHash>> GetLatestBlockHash(Commitment commitment = Commitment.Finalized)
        {
            return GetLatestBlockHashAsync(commitment).Result;
        }

        public async Task<RequestResult<ResponseValue<LatestBlockHash>>> GetLatestBlockHashAsync(Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<ResponseValue<LatestBlockHash>>("getLatestBlockhash", Parameters.Create(ConfigObject.Create(HandleCommitment(commitment))));
        }

        public async Task<RequestResult<ResponseValue<bool>>> IsBlockHashValidAsync(string blockHash, Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<ResponseValue<bool>>("isBlockhashValid", Parameters.Create(blockHash, ConfigObject.Create(HandleCommitment(commitment))));
        }

        public RequestResult<ResponseValue<bool>> IsBlockHashValid(string blockHash, Commitment commitment = Commitment.Finalized)
        {
            return IsBlockHashValidAsync(blockHash, commitment).Result;
        }

        public RequestResult<ResponseValue<BlockHash>> GetRecentBlockHash(Commitment commitment = Commitment.Finalized)
        {
            return GetRecentBlockHashAsync(commitment).Result;
        }

        public async Task<RequestResult<ulong>> GetMaxRetransmitSlotAsync()
        {
            return await SendRequestAsync<ulong>("getMaxRetransmitSlot");
        }

        public RequestResult<ulong> GetMaxRetransmitSlot()
        {
            return GetMaxRetransmitSlotAsync().Result;
        }

        public async Task<RequestResult<ulong>> GetMaxShredInsertSlotAsync()
        {
            return await SendRequestAsync<ulong>("getMaxShredInsertSlot");
        }

        public RequestResult<ulong> GetMaxShredInsertSlot()
        {
            return GetMaxShredInsertSlotAsync().Result;
        }

        public async Task<RequestResult<ulong>> GetMinimumBalanceForRentExemptionAsync(long accountDataSize, Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<ulong>("getMinimumBalanceForRentExemption", Parameters.Create(accountDataSize, ConfigObject.Create(HandleCommitment(commitment))));
        }

        public RequestResult<ulong> GetMinimumBalanceForRentExemption(long accountDataSize, Commitment commitment = Commitment.Finalized)
        {
            return GetMinimumBalanceForRentExemptionAsync(accountDataSize, commitment).Result;
        }

        public async Task<RequestResult<string>> GetGenesisHashAsync()
        {
            return await SendRequestAsync<string>("getGenesisHash");
        }

        public RequestResult<string> GetGenesisHash()
        {
            return GetGenesisHashAsync().Result;
        }

        public async Task<RequestResult<NodeIdentity>> GetIdentityAsync()
        {
            return await SendRequestAsync<NodeIdentity>("getIdentity");
        }

        public RequestResult<NodeIdentity> GetIdentity()
        {
            return GetIdentityAsync().Result;
        }

        public async Task<RequestResult<InflationGovernor>> GetInflationGovernorAsync(Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<InflationGovernor>("getInflationGovernor", Parameters.Create(ConfigObject.Create(HandleCommitment(commitment))));
        }

        public RequestResult<InflationGovernor> GetInflationGovernor(Commitment commitment = Commitment.Finalized)
        {
            return GetInflationGovernorAsync(commitment).Result;
        }

        public async Task<RequestResult<InflationRate>> GetInflationRateAsync()
        {
            return await SendRequestAsync<InflationRate>("getInflationRate");
        }

        public RequestResult<InflationRate> GetInflationRate()
        {
            return GetInflationRateAsync().Result;
        }

        public async Task<RequestResult<List<InflationReward>>> GetInflationRewardAsync(IList<string> addresses, ulong epoch = 0uL, Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<List<InflationReward>>("getInflationReward", Parameters.Create(addresses, ConfigObject.Create(HandleCommitment(commitment), KeyValue.Create("epoch", (epoch != 0) ? ((object)epoch) : null))));
        }

        public RequestResult<List<InflationReward>> GetInflationReward(IList<string> addresses, ulong epoch = 0uL, Commitment commitment = Commitment.Finalized)
        {
            return GetInflationRewardAsync(addresses, epoch, commitment).Result;
        }

        public async Task<RequestResult<ResponseValue<List<LargeAccount>>>> GetLargestAccountsAsync(AccountFilterType? filter = null, Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<ResponseValue<List<LargeAccount>>>("getLargestAccounts", Parameters.Create(ConfigObject.Create(HandleCommitment(commitment), KeyValue.Create("filter", filter))));
        }

        public RequestResult<ResponseValue<List<LargeAccount>>> GetLargestAccounts(AccountFilterType? filter = null, Commitment commitment = Commitment.Finalized)
        {
            return GetLargestAccountsAsync(filter, commitment).Result;
        }

        public async Task<RequestResult<ulong>> GetSnapshotSlotAsync()
        {
            return await SendRequestAsync<ulong>("getSnapshotSlot");
        }

        public RequestResult<ulong> GetSnapshotSlot()
        {
            return GetSnapshotSlotAsync().Result;
        }

        public async Task<RequestResult<SnapshotSlotInfo>> GetHighestSnapshotSlotAsync()
        {
            return await SendRequestAsync<SnapshotSlotInfo>("getHighestSnapshotSlot");
        }

        public RequestResult<SnapshotSlotInfo> GetHighestSnapshotSlot()
        {
            return GetHighestSnapshotSlotAsync().Result;
        }

        public async Task<RequestResult<List<PerformanceSample>>> GetRecentPerformanceSamplesAsync(ulong limit = 720uL)
        {
            return await SendRequestAsync<List<PerformanceSample>>("getRecentPerformanceSamples", new List<object> { limit });
        }

        public RequestResult<List<PerformanceSample>> GetRecentPerformanceSamples(ulong limit = 720uL)
        {
            return GetRecentPerformanceSamplesAsync(limit).Result;
        }

        public async Task<RequestResult<List<SignatureStatusInfo>>> GetSignaturesForAddressAsync(string accountPubKey, ulong limit = 1000uL, string before = null, string until = null, Commitment commitment = Commitment.Finalized)
        {
            if (commitment == Commitment.Processed)
            {
                throw new ArgumentException("Commitment.Processed is not supported for this method.");
            }

            return await SendRequestAsync<List<SignatureStatusInfo>>("getSignaturesForAddress", Parameters.Create(accountPubKey, ConfigObject.Create(KeyValue.Create("limit", (limit != 1000) ? ((object)limit) : null), KeyValue.Create("before", before), KeyValue.Create("until", until), HandleCommitment(commitment))));
        }

        public async Task<RequestResult<List<SignatureStatusInfo>>> GetConfirmedSignaturesForAddress2Async(string accountPubKey, ulong limit = 1000uL, string before = null, string until = null, Commitment commitment = Commitment.Finalized)
        {
            if (commitment == Commitment.Processed)
            {
                throw new ArgumentException("Commitment.Processed is not supported for this method.");
            }

            return await SendRequestAsync<List<SignatureStatusInfo>>("getConfirmedSignaturesForAddress2", Parameters.Create(accountPubKey, ConfigObject.Create(KeyValue.Create("limit", (limit != 1000) ? ((object)limit) : null), KeyValue.Create("before", before), KeyValue.Create("until", until), HandleCommitment(commitment))));
        }

        public RequestResult<List<SignatureStatusInfo>> GetSignaturesForAddress(string accountPubKey, ulong limit = 1000uL, string before = null, string until = null, Commitment commitment = Commitment.Finalized)
        {
            return GetSignaturesForAddressAsync(accountPubKey, limit, before, until, commitment).Result;
        }

        public RequestResult<List<SignatureStatusInfo>> GetConfirmedSignaturesForAddress2(string accountPubKey, ulong limit = 1000uL, string before = null, string until = null, Commitment commitment = Commitment.Finalized)
        {
            return GetConfirmedSignaturesForAddress2Async(accountPubKey, limit, before, until, commitment).Result;
        }

        public async Task<RequestResult<ResponseValue<List<SignatureStatusInfo>>>> GetSignatureStatusesAsync(List<string> transactionHashes, bool searchTransactionHistory = false)
        {
            return await SendRequestAsync<ResponseValue<List<SignatureStatusInfo>>>("getSignatureStatuses", Parameters.Create(transactionHashes, ConfigObject.Create(KeyValue.Create("searchTransactionHistory", searchTransactionHistory ? ((object)searchTransactionHistory) : null))));
        }

        public RequestResult<ResponseValue<List<SignatureStatusInfo>>> GetSignatureStatuses(List<string> transactionHashes, bool searchTransactionHistory = false)
        {
            return GetSignatureStatusesAsync(transactionHashes, searchTransactionHistory).Result;
        }

        public async Task<RequestResult<ulong>> GetSlotAsync(Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<ulong>("getSlot", Parameters.Create(ConfigObject.Create(HandleCommitment(commitment))));
        }

        public RequestResult<ulong> GetSlot(Commitment commitment = Commitment.Finalized)
        {
            return GetSlotAsync(commitment).Result;
        }

        public async Task<RequestResult<string>> GetSlotLeaderAsync(Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<string>("getSlotLeader", Parameters.Create(ConfigObject.Create(HandleCommitment(commitment))));
        }

        public RequestResult<string> GetSlotLeader(Commitment commitment = Commitment.Finalized)
        {
            return GetSlotLeaderAsync(commitment).Result;
        }

        public async Task<RequestResult<List<string>>> GetSlotLeadersAsync(ulong start, ulong limit, Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<List<string>>("getSlotLeaders", Parameters.Create(start, limit, ConfigObject.Create(HandleCommitment(commitment))));
        }

        public RequestResult<List<string>> GetSlotLeaders(ulong start, ulong limit, Commitment commitment = Commitment.Finalized)
        {
            return GetSlotLeadersAsync(start, limit, commitment).Result;
        }

        public async Task<RequestResult<StakeActivationInfo>> GetStakeActivationAsync(string publicKey, ulong epoch = 0uL, Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<StakeActivationInfo>("getStakeActivation", Parameters.Create(publicKey, ConfigObject.Create(HandleCommitment(commitment), KeyValue.Create("epoch", (epoch != 0) ? ((object)epoch) : null))));
        }

        public RequestResult<StakeActivationInfo> GetStakeActivation(string publicKey, ulong epoch = 0uL, Commitment commitment = Commitment.Finalized)
        {
            return GetStakeActivationAsync(publicKey, epoch, commitment).Result;
        }

        public async Task<RequestResult<ResponseValue<Supply>>> GetSupplyAsync(Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<ResponseValue<Supply>>("getSupply", Parameters.Create(ConfigObject.Create(HandleCommitment(commitment))));
        }

        public RequestResult<ResponseValue<Supply>> GetSupply(Commitment commitment = Commitment.Finalized)
        {
            return GetSupplyAsync(commitment).Result;
        }

        public async Task<RequestResult<ResponseValue<TokenBalance>>> GetTokenAccountBalanceAsync(string splTokenAccountPublicKey, Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<ResponseValue<TokenBalance>>("getTokenAccountBalance", Parameters.Create(splTokenAccountPublicKey, ConfigObject.Create(HandleCommitment(commitment))));
        }

        public RequestResult<ResponseValue<TokenBalance>> GetTokenAccountBalance(string splTokenAccountPublicKey, Commitment commitment = Commitment.Finalized)
        {
            return GetTokenAccountBalanceAsync(splTokenAccountPublicKey, commitment).Result;
        }

        public async Task<RequestResult<ResponseValue<List<TokenAccount>>>> GetTokenAccountsByDelegateAsync(string ownerPubKey, string tokenMintPubKey = null, string tokenProgramId = null, Commitment commitment = Commitment.Finalized)
        {
            if (string.IsNullOrWhiteSpace(tokenMintPubKey) && string.IsNullOrWhiteSpace(tokenProgramId))
            {
                throw new ArgumentException("either tokenProgramId or tokenMintPubKey must be set");
            }

            return await SendRequestAsync<ResponseValue<List<TokenAccount>>>("getTokenAccountsByDelegate", Parameters.Create(ownerPubKey, ConfigObject.Create(KeyValue.Create("mint", tokenMintPubKey), KeyValue.Create("programId", tokenProgramId)), ConfigObject.Create(HandleCommitment(commitment), KeyValue.Create("encoding", "jsonParsed"))));
        }

        public RequestResult<ResponseValue<List<TokenAccount>>> GetTokenAccountsByDelegate(string ownerPubKey, string tokenMintPubKey = null, string tokenProgramId = null, Commitment commitment = Commitment.Finalized)
        {
            return GetTokenAccountsByDelegateAsync(ownerPubKey, tokenMintPubKey, tokenProgramId, commitment).Result;
        }

        public async Task<RequestResult<ResponseValue<List<TokenAccount>>>> GetTokenAccountsByOwnerAsync(string ownerPubKey, string tokenMintPubKey = null, string tokenProgramId = null, Commitment commitment = Commitment.Finalized)
        {
            if (string.IsNullOrWhiteSpace(tokenMintPubKey) && string.IsNullOrWhiteSpace(tokenProgramId))
            {
                throw new ArgumentException("either tokenProgramId or tokenMintPubKey must be set");
            }

            return await SendRequestAsync<ResponseValue<List<TokenAccount>>>("getTokenAccountsByOwner", Parameters.Create(ownerPubKey, ConfigObject.Create(KeyValue.Create("mint", tokenMintPubKey), KeyValue.Create("programId", tokenProgramId)), ConfigObject.Create(HandleCommitment(commitment), KeyValue.Create("encoding", "jsonParsed"))));
        }

        public RequestResult<ResponseValue<List<TokenAccount>>> GetTokenAccountsByOwner(string ownerPubKey, string tokenMintPubKey = null, string tokenProgramId = null, Commitment commitment = Commitment.Finalized)
        {
            return GetTokenAccountsByOwnerAsync(ownerPubKey, tokenMintPubKey, tokenProgramId, commitment).Result;
        }

        public async Task<RequestResult<ResponseValue<List<LargeTokenAccount>>>> GetTokenLargestAccountsAsync(string tokenMintPubKey, Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<ResponseValue<List<LargeTokenAccount>>>("getTokenLargestAccounts", Parameters.Create(tokenMintPubKey, ConfigObject.Create(HandleCommitment(commitment))));
        }

        public RequestResult<ResponseValue<List<LargeTokenAccount>>> GetTokenLargestAccounts(string tokenMintPubKey, Commitment commitment = Commitment.Finalized)
        {
            return GetTokenLargestAccountsAsync(tokenMintPubKey, commitment).Result;
        }

        public async Task<RequestResult<ResponseValue<TokenBalance>>> GetTokenSupplyAsync(string tokenMintPubKey, Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<ResponseValue<TokenBalance>>("getTokenSupply", Parameters.Create(tokenMintPubKey, ConfigObject.Create(HandleCommitment(commitment))));
        }

        public RequestResult<ResponseValue<TokenBalance>> GetTokenSupply(string tokenMintPubKey, Commitment commitment = Commitment.Finalized)
        {
            return GetTokenSupplyAsync(tokenMintPubKey, commitment).Result;
        }

        public async Task<RequestResult<ulong>> GetTransactionCountAsync(Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<ulong>("getTransactionCount", Parameters.Create(ConfigObject.Create(HandleCommitment(commitment))));
        }

        public RequestResult<ulong> GetTransactionCount(Commitment commitment = Commitment.Finalized)
        {
            return GetTransactionCountAsync(commitment).Result;
        }

        public async Task<RequestResult<NodeVersion>> GetVersionAsync()
        {
            return await SendRequestAsync<NodeVersion>("getVersion");
        }

        public RequestResult<NodeVersion> GetVersion()
        {
            return GetVersionAsync().Result;
        }

        public async Task<RequestResult<VoteAccounts>> GetVoteAccountsAsync(string votePubKey = null, Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<VoteAccounts>("getVoteAccounts", Parameters.Create(ConfigObject.Create(HandleCommitment(commitment), KeyValue.Create("votePubkey", votePubKey))));
        }

        public RequestResult<VoteAccounts> GetVoteAccounts(string votePubKey = null, Commitment commitment = Commitment.Finalized)
        {
            return GetVoteAccountsAsync(votePubKey, commitment).Result;
        }

        public async Task<RequestResult<ulong>> GetMinimumLedgerSlotAsync()
        {
            return await SendRequestAsync<ulong>("minimumLedgerSlot");
        }

        public RequestResult<ulong> GetMinimumLedgerSlot()
        {
            return GetMinimumLedgerSlotAsync().Result;
        }

        public async Task<RequestResult<string>> RequestAirdropAsync(string pubKey, ulong lamports, Commitment commitment = Commitment.Finalized)
        {
            return await SendRequestAsync<string>("requestAirdrop", Parameters.Create(pubKey, lamports, ConfigObject.Create(HandleCommitment(commitment))));
        }

        public RequestResult<string> RequestAirdrop(string pubKey, ulong lamports, Commitment commitment = Commitment.Finalized)
        {
            return RequestAirdropAsync(pubKey, lamports, commitment).Result;
        }

        public async Task<RequestResult<string>> SendTransactionAsync(byte[] transaction, bool skipPreflight = false, Commitment preflightCommitment = Commitment.Finalized)
        {
            return await SendTransactionAsync(Convert.ToBase64String(transaction), skipPreflight, preflightCommitment).ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<RequestResult<string>> SendTransactionAsync(string transaction, bool skipPreflight = false, Commitment preflightCommitment = Commitment.Finalized)
        {
            return await SendRequestAsync<string>("sendTransaction", Parameters.Create(transaction, ConfigObject.Create(KeyValue.Create("skipPreflight", skipPreflight ? ((object)skipPreflight) : null), KeyValue.Create("preflightCommitment", (preflightCommitment == Commitment.Finalized) ? null : ((object)preflightCommitment)), KeyValue.Create("encoding", BinaryEncoding.Base64))));
        }

        public RequestResult<string> SendTransaction(string transaction, bool skipPreFlight = false, Commitment preFlightCommitment = Commitment.Finalized)
        {
            return SendTransactionAsync(transaction, skipPreFlight, preFlightCommitment).Result;
        }

        public RequestResult<string> SendTransaction(byte[] transaction, bool skipPreFlight = false, Commitment preFlightCommitment = Commitment.Finalized)
        {
            return SendTransactionAsync(transaction, skipPreFlight, preFlightCommitment).Result;
        }

        public async Task<RequestResult<ResponseValue<SimulationLogs>>> SimulateTransactionAsync(string transaction, bool sigVerify = false, Commitment commitment = Commitment.Finalized, bool replaceRecentBlockhash = false, IList<string> accountsToReturn = null)
        {
            if (sigVerify && replaceRecentBlockhash)
            {
                throw new ArgumentException("Parameters sigVerify and replaceRecentBlockhash are incompatible, only one can be set to true.");
            }

            return await SendRequestAsync<ResponseValue<SimulationLogs>>("simulateTransaction", Parameters.Create(transaction, ConfigObject.Create(KeyValue.Create("sigVerify", sigVerify ? ((object)sigVerify) : null), HandleCommitment(commitment), KeyValue.Create("encoding", BinaryEncoding.Base64), KeyValue.Create("replaceRecentBlockhash", replaceRecentBlockhash ? ((object)replaceRecentBlockhash) : null), KeyValue.Create("accounts", (accountsToReturn != null) ? ConfigObject.Create(KeyValue.Create("encoding", BinaryEncoding.Base64), KeyValue.Create("addresses", accountsToReturn)) : null))));
        }

        public async Task<RequestResult<ResponseValue<SimulationLogs>>> SimulateTransactionAsync(byte[] transaction, bool sigVerify = false, Commitment commitment = Commitment.Finalized, bool replaceRecentBlockhash = false, IList<string> accountsToReturn = null)
        {
            return await SimulateTransactionAsync(Convert.ToBase64String(transaction), sigVerify, commitment, replaceRecentBlockhash, accountsToReturn).ConfigureAwait(continueOnCapturedContext: false);
        }

        public RequestResult<ResponseValue<SimulationLogs>> SimulateTransaction(string transaction, bool sigVerify = false, Commitment commitment = Commitment.Finalized, bool replaceRecentBlockhash = false, IList<string> accountsToReturn = null)
        {
            return SimulateTransactionAsync(transaction, sigVerify, commitment, replaceRecentBlockhash, accountsToReturn).Result;
        }

        public RequestResult<ResponseValue<SimulationLogs>> SimulateTransaction(byte[] transaction, bool sigVerify = false, Commitment commitment = Commitment.Finalized, bool replaceRecentBlockhash = false, IList<string> accountsToReturn = null)
        {
            return SimulateTransactionAsync(transaction, sigVerify, commitment, replaceRecentBlockhash, accountsToReturn).Result;
        }

        int IRpcClient.GetNextIdForReq()
        {
            return _idGenerator.GetNextId();
        }
    }
}
