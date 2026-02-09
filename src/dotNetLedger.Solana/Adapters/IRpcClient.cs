using Solnet.Rpc.Core.Http;
using Solnet.Rpc.Messages;
using Solnet.Rpc.Models;
using Solnet.Rpc.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotNetLedger.Solana.Commons
{
    public interface IRpcClient
    {
        Uri NodeAddress { get; }

        Task<RequestResult<ResponseValue<TokenMintInfo>>> GetTokenMintInfoAsync(string pubKey, Commitment commitment = Commitment.Finalized);

        RequestResult<ResponseValue<TokenMintInfo>> GetTokenMintInfo(string pubKey, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ResponseValue<TokenAccountInfo>>> GetTokenAccountInfoAsync(string pubKey, Commitment commitment = Commitment.Finalized);

        RequestResult<ResponseValue<TokenAccountInfo>> GetTokenAccountInfo(string pubKey, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ResponseValue<AccountInfo>>> GetAccountInfoAsync(string pubKey, Commitment commitment = Commitment.Finalized, BinaryEncoding encoding = BinaryEncoding.Base64);

        RequestResult<ResponseValue<AccountInfo>> GetAccountInfo(string pubKey, Commitment commitment = Commitment.Finalized, BinaryEncoding encoding = BinaryEncoding.Base64);

        Task<RequestResult<ResponseValue<ulong>>> GetBalanceAsync(string pubKey, Commitment commitment = Commitment.Finalized);

        RequestResult<ResponseValue<ulong>> GetBalance(string pubKey, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<BlockInfo>> GetBlockAsync(ulong slot, Commitment commitment = Commitment.Finalized, TransactionDetailsFilterType transactionDetails = TransactionDetailsFilterType.Full, bool blockRewards = false);

        RequestResult<BlockInfo> GetBlock(ulong slot, Commitment commitment = Commitment.Finalized, TransactionDetailsFilterType transactionDetails = TransactionDetailsFilterType.Full, bool blockRewards = false);

        Task<RequestResult<BlockCommitment>> GetBlockCommitmentAsync(ulong slot);

        RequestResult<BlockCommitment> GetBlockCommitment(ulong slot);

        Task<RequestResult<ulong>> GetBlockHeightAsync(Commitment commitment = Commitment.Finalized);

        RequestResult<ulong> GetBlockHeight(Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ResponseValue<BlockProductionInfo>>> GetBlockProductionAsync(string identity = null, ulong? firstSlot = null, ulong? lastSlot = null, Commitment commitment = Commitment.Finalized);

        RequestResult<ResponseValue<BlockProductionInfo>> GetBlockProduction(string identity = null, ulong? firstSlot = null, ulong? lastSlot = null, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<List<ulong>>> GetBlocksAsync(ulong startSlot, ulong endSlot = 0uL, Commitment commitment = Commitment.Finalized);

        RequestResult<List<ulong>> GetBlocks(ulong startSlot, ulong endSlot = 0uL, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<List<ulong>>> GetBlocksWithLimitAsync(ulong startSlot, ulong limit, Commitment commitment = Commitment.Finalized);

        RequestResult<List<ulong>> GetBlocksWithLimit(ulong startSlot, ulong limit, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ulong>> GetBlockTimeAsync(ulong slot);

        RequestResult<ulong> GetBlockTime(ulong slot);

        Task<RequestResult<List<ClusterNode>>> GetClusterNodesAsync();

        RequestResult<List<ClusterNode>> GetClusterNodes();

        Task<RequestResult<EpochInfo>> GetEpochInfoAsync(Commitment commitment = Commitment.Finalized);

        RequestResult<EpochInfo> GetEpochInfo(Commitment commitment = Commitment.Finalized);

        Task<RequestResult<EpochScheduleInfo>> GetEpochScheduleAsync();

        RequestResult<EpochScheduleInfo> GetEpochSchedule();

        Task<RequestResult<ResponseValue<FeeCalculatorInfo>>> GetFeeCalculatorForBlockhashAsync(string blockhash, Commitment commitment = Commitment.Finalized);

        RequestResult<ResponseValue<FeeCalculatorInfo>> GetFeeCalculatorForBlockhash(string blockhash, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ResponseValue<FeeRateGovernorInfo>>> GetFeeRateGovernorAsync();

        RequestResult<ResponseValue<FeeRateGovernorInfo>> GetFeeRateGovernor();

        Task<RequestResult<ResponseValue<FeesInfo>>> GetFeesAsync(Commitment commitment = Commitment.Finalized);

        RequestResult<ResponseValue<FeesInfo>> GetFees(Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ResponseValue<ulong>>> GetFeeForMessageAsync(string message, Commitment commitment = Commitment.Finalized);

        RequestResult<ResponseValue<ulong>> GetFeeForMessage(string message, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ulong>> GetFirstAvailableBlockAsync();

        RequestResult<ulong> GetFirstAvailableBlock();

        Task<RequestResult<string>> GetGenesisHashAsync();

        RequestResult<string> GetGenesisHash();

        Task<RequestResult<string>> GetHealthAsync();

        RequestResult<string> GetHealth();

        Task<RequestResult<NodeIdentity>> GetIdentityAsync();

        RequestResult<NodeIdentity> GetIdentity();

        Task<RequestResult<InflationGovernor>> GetInflationGovernorAsync(Commitment commitment = Commitment.Finalized);

        RequestResult<InflationGovernor> GetInflationGovernor(Commitment commitment = Commitment.Finalized);

        Task<RequestResult<InflationRate>> GetInflationRateAsync();

        RequestResult<InflationRate> GetInflationRate();

        Task<RequestResult<List<InflationReward>>> GetInflationRewardAsync(IList<string> addresses, ulong epoch = 0uL, Commitment commitment = Commitment.Finalized);

        RequestResult<List<InflationReward>> GetInflationReward(IList<string> addresses, ulong epoch = 0uL, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ResponseValue<List<LargeAccount>>>> GetLargestAccountsAsync(AccountFilterType? filter = null, Commitment commitment = Commitment.Finalized);

        RequestResult<ResponseValue<List<LargeAccount>>> GetLargestAccounts(AccountFilterType? filter = null, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<Dictionary<string, List<ulong>>>> GetLeaderScheduleAsync(ulong slot = 0uL, string identity = null, Commitment commitment = Commitment.Finalized);

        RequestResult<Dictionary<string, List<ulong>>> GetLeaderSchedule(ulong slot = 0uL, string identity = null, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ulong>> GetMaxRetransmitSlotAsync();

        RequestResult<ulong> GetMaxRetransmitSlot();

        Task<RequestResult<ulong>> GetMaxShredInsertSlotAsync();

        RequestResult<ulong> GetMaxShredInsertSlot();

        Task<RequestResult<ulong>> GetMinimumBalanceForRentExemptionAsync(long accountDataSize, Commitment commitment = Commitment.Finalized);

        RequestResult<ulong> GetMinimumBalanceForRentExemption(long accountDataSize, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ulong>> GetMinimumLedgerSlotAsync();

        RequestResult<ulong> GetMinimumLedgerSlot();

        Task<RequestResult<ResponseValue<List<AccountInfo>>>> GetMultipleAccountsAsync(IList<string> accounts, Commitment commitment = Commitment.Finalized);

        RequestResult<ResponseValue<List<AccountInfo>>> GetMultipleAccounts(IList<string> accounts, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<List<AccountKeyPair>>> GetProgramAccountsAsync(string pubKey, Commitment commitment = Commitment.Finalized, int? dataSize = null, IList<MemCmp> memCmpList = null);

        RequestResult<List<AccountKeyPair>> GetProgramAccounts(string pubKey, Commitment commitment = Commitment.Finalized, int? dataSize = null, IList<MemCmp> memCmpList = null);

        Task<RequestResult<ResponseValue<LatestBlockHash>>> GetLatestBlockHashAsync(Commitment commitment = Commitment.Finalized);

        RequestResult<ResponseValue<LatestBlockHash>> GetLatestBlockHash(Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ResponseValue<bool>>> IsBlockHashValidAsync(string blockHash, Commitment commitment = Commitment.Finalized);

        RequestResult<ResponseValue<bool>> IsBlockHashValid(string blockHash, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<List<PerformanceSample>>> GetRecentPerformanceSamplesAsync(ulong limit = 720uL);

        RequestResult<List<PerformanceSample>> GetRecentPerformanceSamples(ulong limit = 720uL);

        Task<RequestResult<List<SignatureStatusInfo>>> GetSignaturesForAddressAsync(string accountPubKey, ulong limit = 1000uL, string before = null, string until = null, Commitment commitment = Commitment.Finalized);

        RequestResult<List<SignatureStatusInfo>> GetSignaturesForAddress(string accountPubKey, ulong limit = 1000uL, string before = null, string until = null, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ResponseValue<List<SignatureStatusInfo>>>> GetSignatureStatusesAsync(List<string> transactionHashes, bool searchTransactionHistory = false);

        RequestResult<ResponseValue<List<SignatureStatusInfo>>> GetSignatureStatuses(List<string> transactionHashes, bool searchTransactionHistory = false);

        Task<RequestResult<ulong>> GetSlotAsync(Commitment commitment = Commitment.Finalized);

        RequestResult<ulong> GetSlot(Commitment commitment = Commitment.Finalized);

        Task<RequestResult<string>> GetSlotLeaderAsync(Commitment commitment = Commitment.Finalized);

        RequestResult<string> GetSlotLeader(Commitment commitment = Commitment.Finalized);

        Task<RequestResult<List<string>>> GetSlotLeadersAsync(ulong start, ulong limit, Commitment commitment = Commitment.Finalized);

        RequestResult<List<string>> GetSlotLeaders(ulong start, ulong limit, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ulong>> GetSnapshotSlotAsync();

        RequestResult<ulong> GetSnapshotSlot();

        Task<RequestResult<SnapshotSlotInfo>> GetHighestSnapshotSlotAsync();

        RequestResult<SnapshotSlotInfo> GetHighestSnapshotSlot();

        Task<RequestResult<StakeActivationInfo>> GetStakeActivationAsync(string publicKey, ulong epoch = 0uL, Commitment commitment = Commitment.Finalized);

        RequestResult<StakeActivationInfo> GetStakeActivation(string publicKey, ulong epoch = 0uL, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ResponseValue<Supply>>> GetSupplyAsync(Commitment commitment = Commitment.Finalized);

        RequestResult<ResponseValue<Supply>> GetSupply(Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ResponseValue<TokenBalance>>> GetTokenAccountBalanceAsync(string splTokenAccountPublicKey, Commitment commitment = Commitment.Finalized);

        RequestResult<ResponseValue<TokenBalance>> GetTokenAccountBalance(string splTokenAccountPublicKey, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ResponseValue<List<TokenAccount>>>> GetTokenAccountsByDelegateAsync(string ownerPubKey, string tokenMintPubKey = null, string tokenProgramId = null, Commitment commitment = Commitment.Finalized);

        RequestResult<ResponseValue<List<TokenAccount>>> GetTokenAccountsByDelegate(string ownerPubKey, string tokenMintPubKey = null, string tokenProgramId = null, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ResponseValue<List<TokenAccount>>>> GetTokenAccountsByOwnerAsync(string ownerPubKey, string tokenMintPubKey = null, string tokenProgramId = null, Commitment commitment = Commitment.Finalized);

        RequestResult<ResponseValue<List<TokenAccount>>> GetTokenAccountsByOwner(string ownerPubKey, string tokenMintPubKey = null, string tokenProgramId = null, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ResponseValue<List<LargeTokenAccount>>>> GetTokenLargestAccountsAsync(string tokenMintPubKey, Commitment commitment = Commitment.Finalized);

        RequestResult<ResponseValue<List<LargeTokenAccount>>> GetTokenLargestAccounts(string tokenMintPubKey, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ResponseValue<TokenBalance>>> GetTokenSupplyAsync(string tokenMintPubKey, Commitment commitment = Commitment.Finalized);

        RequestResult<ResponseValue<TokenBalance>> GetTokenSupply(string tokenMintPubKey, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<TransactionMetaSlotInfo>> GetTransactionAsync(string signature, Commitment commitment = Commitment.Finalized);

        RequestResult<TransactionMetaSlotInfo> GetTransaction(string signature, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ulong>> GetTransactionCountAsync(Commitment commitment = Commitment.Finalized);

        RequestResult<ulong> GetTransactionCount(Commitment commitment = Commitment.Finalized);

        Task<RequestResult<NodeVersion>> GetVersionAsync();

        RequestResult<NodeVersion> GetVersion();

        Task<RequestResult<VoteAccounts>> GetVoteAccountsAsync(string votePubKey = null, Commitment commitment = Commitment.Finalized);

        RequestResult<VoteAccounts> GetVoteAccounts(string votePubKey = null, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<string>> RequestAirdropAsync(string pubKey, ulong lamports, Commitment commitment = Commitment.Finalized);

        RequestResult<string> RequestAirdrop(string pubKey, ulong lamports, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<string>> SendTransactionAsync(string transaction, bool skipPreflight = false, Commitment preFlightCommitment = Commitment.Finalized);

        RequestResult<string> SendTransaction(string transaction, bool skipPreflight = false, Commitment preFlightCommitment = Commitment.Finalized);

        Task<RequestResult<string>> SendTransactionAsync(byte[] transaction, bool skipPreflight = false, Commitment commitment = Commitment.Finalized);

        RequestResult<string> SendTransaction(byte[] transaction, bool skipPreflight = false, Commitment commitment = Commitment.Finalized);

        Task<RequestResult<ResponseValue<SimulationLogs>>> SimulateTransactionAsync(string transaction, bool sigVerify = false, Commitment commitment = Commitment.Finalized, bool replaceRecentBlockhash = false, IList<string> accountsToReturn = null);

        RequestResult<ResponseValue<SimulationLogs>> SimulateTransaction(string transaction, bool sigVerify = false, Commitment commitment = Commitment.Finalized, bool replaceRecentBlockhash = false, IList<string> accountsToReturn = null);

        Task<RequestResult<ResponseValue<SimulationLogs>>> SimulateTransactionAsync(byte[] transaction, bool sigVerify = false, Commitment commitment = Commitment.Finalized, bool replaceRecentBlockhash = false, IList<string> accountsToReturn = null);

        RequestResult<ResponseValue<SimulationLogs>> SimulateTransaction(byte[] transaction, bool sigVerify = false, Commitment commitment = Commitment.Finalized, bool replaceRecentBlockhash = false, IList<string> accountsToReturn = null);

        Task<RequestResult<JsonRpcBatchResponse>> SendBatchRequestAsync(JsonRpcBatchRequest reqs);

        int GetNextIdForReq();
    }
}
