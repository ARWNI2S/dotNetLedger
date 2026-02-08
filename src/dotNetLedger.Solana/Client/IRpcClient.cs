using Solnet.Rpc.Core.Http;
using Solnet.Rpc.Messages;
using Solnet.Rpc.Models;
using Solnet.Rpc.Types;

namespace dotNetLedger.Solana.Client
{
    public interface IRpcClient
    {
        //
        // Resumen:
        //     The address this client connects to.
        Uri NodeAddress { get; }

        //
        // Resumen:
        //     Gets the token mint info. This method only works if the target account is a SPL
        //     token mint. The commitment parameter is optional, the default value Solnet.Rpc.Types.Commitment.Finalized
        //     is not sent.
        //
        // Parámetros:
        //   pubKey:
        //     The token mint public key.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     A task which may return a request result holding the context and account info.
        Task<RequestResult<ResponseValue<TokenMintInfo>>> GetTokenMintInfoAsync(string pubKey, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the token mint info. This method only works if the target account is a SPL
        //     token mint. The commitment parameter is optional, the default value Solnet.Rpc.Types.Commitment.Finalized
        //     is not sent.
        //
        // Parámetros:
        //   pubKey:
        //     The token mint public key.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ResponseValue<TokenMintInfo>> GetTokenMintInfo(string pubKey, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the token account info. The commitment parameter is optional, the default
        //     value Solnet.Rpc.Types.Commitment.Finalized is not sent.
        //
        // Parámetros:
        //   pubKey:
        //     The token account public key.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     A task which may return a request result holding the context and account info.
        Task<RequestResult<ResponseValue<TokenAccountInfo>>> GetTokenAccountInfoAsync(string pubKey, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the token account info. The commitment parameter is optional, the default
        //     value Solnet.Rpc.Types.Commitment.Finalized is not sent.
        //
        // Parámetros:
        //   pubKey:
        //     The token account public key.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ResponseValue<TokenAccountInfo>> GetTokenAccountInfo(string pubKey, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the account info. The commitment parameter is optional, the default value
        //     Solnet.Rpc.Types.Commitment.Finalized is not sent.
        //
        // Parámetros:
        //   pubKey:
        //     The account public key.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        //   encoding:
        //     The encoding of the account data.
        //
        // Devuelve:
        //     A task which may return a request result holding the context and account info.
        Task<RequestResult<ResponseValue<AccountInfo>>> GetAccountInfoAsync(string pubKey, Commitment commitment = Commitment.Finalized, BinaryEncoding encoding = BinaryEncoding.Base64);

        //
        // Resumen:
        //     Gets the account info. The commitment parameter is optional, the default value
        //     Solnet.Rpc.Types.Commitment.Finalized is not sent.
        //
        // Parámetros:
        //   pubKey:
        //     The account public key.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        //   encoding:
        //     The encoding of the account data.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ResponseValue<AccountInfo>> GetAccountInfo(string pubKey, Commitment commitment = Commitment.Finalized, BinaryEncoding encoding = BinaryEncoding.Base64);

        //
        // Resumen:
        //     Gets the balance asynchronously for a certain public key. The commitment parameter
        //     is optional, the default value Solnet.Rpc.Types.Commitment.Finalized is not sent.
        //
        //
        // Parámetros:
        //   pubKey:
        //     The public key.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     A task which may return a request result holding the context and address balance.
        Task<RequestResult<ResponseValue<ulong>>> GetBalanceAsync(string pubKey, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the balance synchronously for a certain public key. The commitment parameter
        //     is optional, the default value Solnet.Rpc.Types.Commitment.Finalized is not sent.
        //
        //
        // Parámetros:
        //   pubKey:
        //     The public key.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ResponseValue<ulong>> GetBalance(string pubKey, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns identity and transaction information about a block in the ledger.
        //
        //     The commitment parameter is optional, Solnet.Rpc.Types.Commitment.Processed is
        //     not supported, the default value Solnet.Rpc.Types.Commitment.Finalized is not
        //     sent.
        //
        //     The transactionDetails parameter is optional, the default value Solnet.Rpc.Types.TransactionDetailsFilterType.Full
        //     is not sent.
        //
        //     The blockRewards parameter is optional, the default value, false, is not sent.
        //
        //
        // Parámetros:
        //   slot:
        //     The slot.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        //   transactionDetails:
        //     The level of transaction detail to return, see Solnet.Rpc.Types.TransactionDetailsFilterType.
        //
        //
        //   blockRewards:
        //     Whether to populate the rewards array, the default includes rewards.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<BlockInfo>> GetBlockAsync(ulong slot, Commitment commitment = Commitment.Finalized, TransactionDetailsFilterType transactionDetails = TransactionDetailsFilterType.Full, bool blockRewards = false);

        //
        // Resumen:
        //     Returns identity and transaction information about a confirmed block in the ledger.
        //
        //
        //     The commitment parameter is optional, Solnet.Rpc.Types.Commitment.Processed is
        //     not supported, the default value Solnet.Rpc.Types.Commitment.Finalized is not
        //     sent.
        //
        //     The transactionDetails parameter is optional, the default value Solnet.Rpc.Types.TransactionDetailsFilterType.Full
        //     is not sent.
        //
        //     The blockRewards parameter is optional, the default value, false, is not sent.
        //
        //
        // Parámetros:
        //   slot:
        //     The slot.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        //   transactionDetails:
        //     The level of transaction detail to return, see Solnet.Rpc.Types.TransactionDetailsFilterType.
        //
        //
        //   blockRewards:
        //     Whether to populate the rewards array, the default includes rewards.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        [Obsolete("Please use GetBlockAsync whenever possible instead. This method is expected to be removed in solana-core v1.8.")]
        Task<RequestResult<BlockInfo>> GetConfirmedBlockAsync(ulong slot, Commitment commitment = Commitment.Finalized, TransactionDetailsFilterType transactionDetails = TransactionDetailsFilterType.Full, bool blockRewards = false);

        //
        // Resumen:
        //     Returns identity and transaction information about a block in the ledger.
        //
        //     The commitment parameter is optional, Solnet.Rpc.Types.Commitment.Processed is
        //     not supported, the default value Solnet.Rpc.Types.Commitment.Finalized is not
        //     sent.
        //
        //     The transactionDetails parameter is optional, the default value Solnet.Rpc.Types.TransactionDetailsFilterType.Full
        //     is not sent.
        //
        //     The blockRewards parameter is optional, the default value, false, is not sent.
        //
        //
        // Parámetros:
        //   slot:
        //     The slot.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        //   transactionDetails:
        //     The level of transaction detail to return, see Solnet.Rpc.Types.TransactionDetailsFilterType.
        //
        //
        //   blockRewards:
        //     Whether to populate the rewards array, the default includes rewards.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<BlockInfo> GetBlock(ulong slot, Commitment commitment = Commitment.Finalized, TransactionDetailsFilterType transactionDetails = TransactionDetailsFilterType.Full, bool blockRewards = false);

        //
        // Resumen:
        //     Returns identity and transaction information about a confirmed block in the ledger.
        //
        //
        //     The commitment parameter is optional, Solnet.Rpc.Types.Commitment.Processed is
        //     not supported, the default value Solnet.Rpc.Types.Commitment.Finalized is not
        //     sent.
        //
        //     The transactionDetails parameter is optional, the default value Solnet.Rpc.Types.TransactionDetailsFilterType.Full
        //     is not sent.
        //
        //     The blockRewards parameter is optional, the default value, false, is not sent.
        //
        //
        // Parámetros:
        //   slot:
        //     The slot.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        //   transactionDetails:
        //     The level of transaction detail to return, see Solnet.Rpc.Types.TransactionDetailsFilterType.
        //
        //
        //   blockRewards:
        //     Whether to populate the rewards array, the default includes rewards.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        [Obsolete("Please use GetBlock whenever possible instead. This method is expected to be removed in solana-core v1.8.")]
        RequestResult<BlockInfo> GetConfirmedBlock(ulong slot, Commitment commitment = Commitment.Finalized, TransactionDetailsFilterType transactionDetails = TransactionDetailsFilterType.Full, bool blockRewards = false);

        //
        // Resumen:
        //     Gets the block commitment of a certain block, identified by slot.
        //
        // Parámetros:
        //   slot:
        //     The slot.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<BlockCommitment>> GetBlockCommitmentAsync(ulong slot);

        //
        // Resumen:
        //     Gets the block commitment of a certain block, identified by slot.
        //
        // Parámetros:
        //   slot:
        //     The slot.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<BlockCommitment> GetBlockCommitment(ulong slot);

        //
        // Resumen:
        //     Gets the current block height of the node.
        //
        // Parámetros:
        //   commitment:
        //     The commitment state to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ulong>> GetBlockHeightAsync(Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the current block height of the node.
        //
        // Parámetros:
        //   commitment:
        //     The commitment state to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ulong> GetBlockHeight(Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns recent block production information from the current or previous epoch.
        //     All the arguments are optional, but the lastSlot must be paired with a firstSlot
        //     argument.
        //
        // Parámetros:
        //   identity:
        //     Filter production details only for this given validator.
        //
        //   firstSlot:
        //     The first slot to return production information (inclusive).
        //
        //   lastSlot:
        //     The last slot to return production information (inclusive and optional).
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ResponseValue<BlockProductionInfo>>> GetBlockProductionAsync(string? identity = null, ulong? firstSlot = null, ulong? lastSlot = null, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns recent block production information from the current or previous epoch.
        //
        //
        // Parámetros:
        //   identity:
        //     Filter production details only for this given validator.
        //
        //   firstSlot:
        //     The first slot to return production information (inclusive).
        //
        //   lastSlot:
        //     The last slot to return production information (inclusive and optional).
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        //
        //
        // Comentarios:
        //     All the arguments are optional, but the lastSlot must be paired with a firstSlot
        //     argument.
        RequestResult<ResponseValue<BlockProductionInfo>> GetBlockProduction(string? identity = null, ulong? firstSlot = null, ulong? lastSlot = null, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns a list of blocks between two slots.
        //
        // Parámetros:
        //   startSlot:
        //     The start slot (inclusive).
        //
        //   endSlot:
        //     The start slot (inclusive and optional).
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<List<ulong>>> GetBlocksAsync(ulong startSlot, ulong endSlot = 0uL, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns a list of confirmed blocks between two slots.
        //
        // Parámetros:
        //   startSlot:
        //     The start slot (inclusive).
        //
        //   endSlot:
        //     The start slot (inclusive and optional).
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        //
        // Comentarios:
        //     The commitment parameter is optional, Solnet.Rpc.Types.Commitment.Processed is
        //     not supported, the default value Solnet.Rpc.Types.Commitment.Finalized is not
        //     sent.
        [Obsolete("Please use GetBlocksAsync whenever possible instead. This method is expected to be removed in solana-core v1.8.")]
        Task<RequestResult<List<ulong>>> GetConfirmedBlocksAsync(ulong startSlot, ulong endSlot = 0uL, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns a list of blocks between two slots.
        //
        // Parámetros:
        //   startSlot:
        //     The start slot (inclusive).
        //
        //   endSlot:
        //     The start slot (inclusive and optional).
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<List<ulong>> GetBlocks(ulong startSlot, ulong endSlot = 0uL, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns a list of confirmed blocks between two slots.
        //
        // Parámetros:
        //   startSlot:
        //     The start slot (inclusive).
        //
        //   endSlot:
        //     The start slot (inclusive and optional).
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        //
        //
        // Comentarios:
        //     The commitment parameter is optional, Solnet.Rpc.Types.Commitment.Processed is
        //     not supported, the default value Solnet.Rpc.Types.Commitment.Finalized is not
        //     sent.
        [Obsolete("Please use GetBlocks whenever possible instead. This method is expected to be removed in solana-core v1.8.")]
        RequestResult<List<ulong>> GetConfirmedBlocks(ulong startSlot, ulong endSlot = 0uL, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns a list of confirmed blocks starting at the given slot.
        //
        // Parámetros:
        //   startSlot:
        //     The start slot (inclusive).
        //
        //   limit:
        //     The max number of blocks to return.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<List<ulong>>> GetBlocksWithLimitAsync(ulong startSlot, ulong limit, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns a list of confirmed blocks starting at the given slot.
        //
        // Parámetros:
        //   startSlot:
        //     The start slot (inclusive).
        //
        //   limit:
        //     The max number of blocks to return.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        [Obsolete("Please use GetBlocksWithLimitAsync whenever possible instead. This method is expected to be removed in solana-core v1.8.")]
        Task<RequestResult<List<ulong>>> GetConfirmedBlocksWithLimitAsync(ulong startSlot, ulong limit, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns a list of confirmed blocks starting at the given slot.
        //
        // Parámetros:
        //   startSlot:
        //     The start slot (inclusive).
        //
        //   limit:
        //     The max number of blocks to return.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<List<ulong>> GetBlocksWithLimit(ulong startSlot, ulong limit, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns a list of confirmed blocks starting at the given slot.
        //
        // Parámetros:
        //   startSlot:
        //     The start slot (inclusive).
        //
        //   limit:
        //     The max number of blocks to return.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        [Obsolete("Please use GetBlocksWithLimit whenever possible instead. This method is expected to be removed in solana-core v1.8.")]
        RequestResult<List<ulong>> GetConfirmedBlocksWithLimit(ulong startSlot, ulong limit, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the estimated production time for a certain block, identified by slot.
        //
        // Parámetros:
        //   slot:
        //     The slot.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ulong>> GetBlockTimeAsync(ulong slot);

        //
        // Resumen:
        //     Gets the estimated production time for a certain block, identified by slot.
        //
        // Parámetros:
        //   slot:
        //     The slot.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ulong> GetBlockTime(ulong slot);

        //
        // Resumen:
        //     Gets the cluster nodes.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<List<ClusterNode>>> GetClusterNodesAsync();

        //
        // Resumen:
        //     Gets the cluster nodes.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<List<ClusterNode>> GetClusterNodes();

        //
        // Resumen:
        //     Gets information about the current epoch.
        //
        // Parámetros:
        //   commitment:
        //     The commitment state to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<EpochInfo>> GetEpochInfoAsync(Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets information about the current epoch.
        //
        // Parámetros:
        //   commitment:
        //     The commitment state to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<EpochInfo> GetEpochInfo(Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets epoch schedule information from this cluster's genesis config.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<EpochScheduleInfo>> GetEpochScheduleAsync();

        //
        // Resumen:
        //     Gets epoch schedule information from this cluster's genesis config.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<EpochScheduleInfo> GetEpochSchedule();

        //
        // Resumen:
        //     Gets the fee calculator associated with the query blockhash, or null if the blockhash
        //     has expired.
        //
        // Parámetros:
        //   blockhash:
        //     The blockhash to query, as base-58 encoded string.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ResponseValue<FeeCalculatorInfo>>> GetFeeCalculatorForBlockhashAsync(string blockhash, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the fee calculator associated with the query blockhash, or null if the blockhash
        //     has expired.
        //
        // Parámetros:
        //   blockhash:
        //     The blockhash to query, as base-58 encoded string.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ResponseValue<FeeCalculatorInfo>> GetFeeCalculatorForBlockhash(string blockhash, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the fee rate governor information from the root bank.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ResponseValue<FeeRateGovernorInfo>>> GetFeeRateGovernorAsync();

        //
        // Resumen:
        //     Gets the fee rate governor information from the root bank.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ResponseValue<FeeRateGovernorInfo>> GetFeeRateGovernor();

        //
        // Resumen:
        //     Gets a recent block hash from the ledger, a fee schedule that can be used to
        //     compute the cost of submitting a transaction using it, and the last slot in which
        //     the blockhash will be valid.
        //
        // Parámetros:
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ResponseValue<FeesInfo>>> GetFeesAsync(Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets a recent block hash from the ledger, a fee schedule that can be used to
        //     compute the cost of submitting a transaction using it, and the last slot in which
        //     the blockhash will be valid.
        //
        // Parámetros:
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ResponseValue<FeesInfo>> GetFees(Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Get the fee the network will charge for a particular Message.
        //
        // Parámetros:
        //   message:
        //     The base-64 encoded message.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ResponseValue<ulong>>> GetFeeForMessageAsync(string message, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Get the fee the network will charge for a particular Message.
        //
        // Parámetros:
        //   message:
        //     The base-64 encoded message.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ResponseValue<ulong>> GetFeeForMessage(string message, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns the slot of the lowest confirmed block that has not been purged from
        //     the ledger.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ulong>> GetFirstAvailableBlockAsync();

        //
        // Resumen:
        //     Returns the slot of the lowest confirmed block that has not been purged from
        //     the ledger.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ulong> GetFirstAvailableBlock();

        //
        // Resumen:
        //     Gets the genesis hash of the ledger.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<string>> GetGenesisHashAsync();

        //
        // Resumen:
        //     Gets the genesis hash of the ledger.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<string> GetGenesisHash();

        //
        // Resumen:
        //     Returns the current health of the node. This method should return the string
        //     'ok' if the node is healthy, or the error code along with any information provided
        //     otherwise.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<string>> GetHealthAsync();

        //
        // Resumen:
        //     Returns the current health of the node. This method should return the string
        //     'ok' if the node is healthy, or the error code along with any information provided
        //     otherwise.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<string> GetHealth();

        //
        // Resumen:
        //     Gets the identity pubkey for the current node.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<NodeIdentity>> GetIdentityAsync();

        //
        // Resumen:
        //     Gets the identity pubkey for the current node.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<NodeIdentity> GetIdentity();

        //
        // Resumen:
        //     Gets the current inflation governor.
        //
        // Parámetros:
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<InflationGovernor>> GetInflationGovernorAsync(Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the current inflation governor.
        //
        // Parámetros:
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<InflationGovernor> GetInflationGovernor(Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the specific inflation values for the current epoch.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<InflationRate>> GetInflationRateAsync();

        //
        // Resumen:
        //     Gets the specific inflation values for the current epoch.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<InflationRate> GetInflationRate();

        //
        // Resumen:
        //     Gets the inflation reward for a list of addresses for an epoch.
        //
        // Parámetros:
        //   addresses:
        //     The list of addresses to query, as base-58 encoded strings.
        //
        //   epoch:
        //     The epoch.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<List<InflationReward>>> GetInflationRewardAsync(IList<string> addresses, ulong epoch = 0uL, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the inflation reward for a list of addresses for an epoch.
        //
        // Parámetros:
        //   addresses:
        //     The list of addresses to query, as base-58 encoded strings.
        //
        //   epoch:
        //     The epoch.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<List<InflationReward>> GetInflationReward(IList<string> addresses, ulong epoch = 0uL, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the 20 largest accounts, by lamport balance.
        //
        // Parámetros:
        //   filter:
        //     Filter results by account type.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        //
        // Comentarios:
        //     Results may be cached up to two hours.
        Task<RequestResult<ResponseValue<List<LargeAccount>>>> GetLargestAccountsAsync(AccountFilterType? filter = null, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the 20 largest accounts, by lamport balance.
        //
        // Parámetros:
        //   filter:
        //     Filter results by account type.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        //
        //
        // Comentarios:
        //     Results may be cached up to two hours.
        RequestResult<ResponseValue<List<LargeAccount>>> GetLargestAccounts(AccountFilterType? filter = null, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns the leader schedule for an epoch.
        //
        // Parámetros:
        //   slot:
        //     Fetch the leader schedule for the epoch that corresponds to the provided slot.
        //     If unspecified, the leader schedule for the current epoch is fetched.
        //
        //   identity:
        //     Filter results for this validator only (base 58 encoded string and optional).
        //
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<Dictionary<string, List<ulong>>>> GetLeaderScheduleAsync(ulong slot = 0uL, string? identity = null, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns the leader schedule for an epoch.
        //
        // Parámetros:
        //   slot:
        //     Fetch the leader schedule for the epoch that corresponds to the provided slot.
        //     If unspecified, the leader schedule for the current epoch is fetched.
        //
        //   identity:
        //     Filter results for this validator only (base 58 encoded string and optional).
        //
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<Dictionary<string, List<ulong>>> GetLeaderSchedule(ulong slot = 0uL, string? identity = null, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the maximum slot seen from retransmit stage.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ulong>> GetMaxRetransmitSlotAsync();

        //
        // Resumen:
        //     Gets the maximum slot seen from retransmit stage.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ulong> GetMaxRetransmitSlot();

        //
        // Resumen:
        //     Gets the maximum slot seen from after shred insert.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ulong>> GetMaxShredInsertSlotAsync();

        //
        // Resumen:
        //     Gets the maximum slot seen from after shred insert.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ulong> GetMaxShredInsertSlot();

        //
        // Resumen:
        //     Gets the minimum balance required to make account rent exempt.
        //
        // Parámetros:
        //   accountDataSize:
        //     The account data size.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ulong>> GetMinimumBalanceForRentExemptionAsync(long accountDataSize, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the minimum balance required to make account rent exempt.
        //
        // Parámetros:
        //   accountDataSize:
        //     The account data size.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ulong> GetMinimumBalanceForRentExemption(long accountDataSize, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the lowest slot that the node has information about in its ledger. This
        //     value may decrease over time if a node is configured to purging data.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ulong>> GetMinimumLedgerSlotAsync();

        //
        // Resumen:
        //     Gets the lowest slot that the node has information about in its ledger. This
        //     value may decrease over time if a node is configured to purging data.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ulong> GetMinimumLedgerSlot();

        //
        // Resumen:
        //     Gets the account info for multiple accounts.
        //
        // Parámetros:
        //   accounts:
        //     The list of the accounts public keys.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     A task which may return a request result holding the context and account info.
        Task<RequestResult<ResponseValue<List<AccountInfo>>>> GetMultipleAccountsAsync(IList<string> accounts, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the account info for multiple accounts.
        //
        // Parámetros:
        //   accounts:
        //     The list of the accounts public keys.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ResponseValue<List<AccountInfo>>> GetMultipleAccounts(IList<string> accounts, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns all accounts owned by the provided program Pubkey. Accounts must meet
        //     all filter criteria to be included in the results.
        //
        // Parámetros:
        //   pubKey:
        //     The program public key.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        //   dataSize:
        //     The data size of the account to compare against the program account data.
        //
        //   memCmpList:
        //     The list of comparisons to match against the program account data.
        //
        // Devuelve:
        //     A task which may return a request result holding the context and account info.
        Task<RequestResult<List<AccountKeyPair>>> GetProgramAccountsAsync(string pubKey, Commitment commitment = Commitment.Finalized, int? dataSize = null, IList<MemCmp>? memCmpList = null);

        //
        // Resumen:
        //     Returns all accounts owned by the provided program Pubkey. Accounts must meet
        //     all filter criteria to be included in the results.
        //
        // Parámetros:
        //   pubKey:
        //     The program public key.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        //   dataSize:
        //     The data size of the account to compare against the program account data.
        //
        //   memCmpList:
        //     The list of comparisons to match against the program account data.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<List<AccountKeyPair>> GetProgramAccounts(string pubKey, Commitment commitment = Commitment.Finalized, int? dataSize = null, IList<MemCmp>? memCmpList = null);

        //
        // Resumen:
        //     Gets a recent block hash.
        //
        // Parámetros:
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        [Obsolete("DEPRECATED: Please use GetLatestBlockhashAsync instead. This method is expected to be removed in solana-core v2.0")]
        Task<RequestResult<ResponseValue<BlockHash>>> GetRecentBlockHashAsync(Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets a recent block hash.
        //
        // Parámetros:
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        [Obsolete("DEPRECATED: Please use GetLatestBlockhash instead. This method is expected to be removed in solana-core v2.0")]
        RequestResult<ResponseValue<BlockHash>> GetRecentBlockHash(Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the latest block hash.
        //
        // Parámetros:
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ResponseValue<LatestBlockHash>>> GetLatestBlockHashAsync(Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the latest block hash.
        //
        // Parámetros:
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ResponseValue<LatestBlockHash>> GetLatestBlockHash(Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns whether a blockhash is still valid or not.
        //
        // Parámetros:
        //   blockHash:
        //     The Blockhash to validate, as a base58 encoded string.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ResponseValue<bool>>> IsBlockHashValidAsync(string blockHash, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns whether a blockhash is still valid or not.
        //
        // Parámetros:
        //   blockHash:
        //     The Blockhash to validate, as a base58 encoded string.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ResponseValue<bool>> IsBlockHashValid(string blockHash, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets a list of recent performance samples. Unless searchTransactionHistory is
        //     included, this method only searches the recent status cache of signatures.
        //
        // Parámetros:
        //   limit:
        //     Maximum transaction signatures to return, between 1-720. Default is 720.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<List<PerformanceSample>>> GetRecentPerformanceSamplesAsync(ulong limit = 720uL);

        //
        // Resumen:
        //     Gets a list of recent performance samples. Unless searchTransactionHistory is
        //     included, this method only searches the recent status cache of signatures.
        //
        // Parámetros:
        //   limit:
        //     Maximum transaction signatures to return, between 1-720. Default is 720.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<List<PerformanceSample>> GetRecentPerformanceSamples(ulong limit = 720uL);

        //
        // Resumen:
        //     Gets signatures with the given commitment for transactions involving the address.
        //     Unless searchTransactionHistory is included, this method only searches the recent
        //     status cache of signatures.
        //
        // Parámetros:
        //   accountPubKey:
        //     The account address as base-58 encoded string.
        //
        //   limit:
        //     Maximum transaction signatures to return, between 1-1000. Default is 1000.
        //
        //   before:
        //     Start searching backwards from this transaction signature.
        //
        //   until:
        //     Search until this transaction signature, if found before limit is reached.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<List<SignatureStatusInfo>>> GetSignaturesForAddressAsync(string accountPubKey, ulong limit = 1000uL, string? before = null, string? until = null, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets confirmed signatures for transactions involving the address. Unless searchTransactionHistory
        //     is included, this method only searches the recent status cache of signatures.
        //
        //
        // Parámetros:
        //   accountPubKey:
        //     The account address as base-58 encoded string.
        //
        //   limit:
        //     Maximum transaction signatures to return, between 1-1000. Default is 1000.
        //
        //   before:
        //     Start searching backwards from this transaction signature.
        //
        //   until:
        //     Search until this transaction signature, if found before limit is reached.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        [Obsolete("Please use GetSignaturesForAddressAsync whenever possible instead. This method is expected to be removed in solana-core v1.8.")]
        Task<RequestResult<List<SignatureStatusInfo>>> GetConfirmedSignaturesForAddress2Async(string accountPubKey, ulong limit = 1000uL, string? before = null, string? until = null, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets signatures with the given commitment for transactions involving the address.
        //     Unless searchTransactionHistory is included, this method only searches the recent
        //     status cache of signatures.
        //
        // Parámetros:
        //   accountPubKey:
        //     The account address as base-58 encoded string.
        //
        //   limit:
        //     Maximum transaction signatures to return, between 1-1000. Default is 1000.
        //
        //   before:
        //     Start searching backwards from this transaction signature.
        //
        //   until:
        //     Search until this transaction signature, if found before limit is reached.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<List<SignatureStatusInfo>> GetSignaturesForAddress(string accountPubKey, ulong limit = 1000uL, string? before = null, string? until = null, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets confirmed signatures for transactions involving the address. Unless searchTransactionHistory
        //     is included, this method only searches the recent status cache of signatures.
        //
        //
        // Parámetros:
        //   accountPubKey:
        //     The account address as base-58 encoded string.
        //
        //   limit:
        //     Maximum transaction signatures to return, between 1-1000. Default is 1000.
        //
        //   before:
        //     Start searching backwards from this transaction signature.
        //
        //   until:
        //     Search until this transaction signature, if found before limit is reached.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        [Obsolete("Please use GetSignaturesForAddress whenever possible instead. This method is expected to be removed in solana-core v1.8.")]
        RequestResult<List<SignatureStatusInfo>> GetConfirmedSignaturesForAddress2(string accountPubKey, ulong limit = 1000uL, string? before = null, string? until = null, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the status of a list of signatures. Unless searchTransactionHistory is included,
        //     this method only searches the recent status cache of signatures.
        //
        // Parámetros:
        //   transactionHashes:
        //     The list of transactions to search status info for.
        //
        //   searchTransactionHistory:
        //     If the node should search for signatures in it's ledger cache.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ResponseValue<List<SignatureStatusInfo>>>> GetSignatureStatusesAsync(List<string> transactionHashes, bool searchTransactionHistory = false);

        //
        // Resumen:
        //     Gets the status of a list of signatures. Unless searchTransactionHistory is included,
        //     this method only searches the recent status cache of signatures.
        //
        // Parámetros:
        //   transactionHashes:
        //     The list of transactions to search status info for.
        //
        //   searchTransactionHistory:
        //     If the node should search for signatures in it's ledger cache.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ResponseValue<List<SignatureStatusInfo>>> GetSignatureStatuses(List<string> transactionHashes, bool searchTransactionHistory = false);

        //
        // Resumen:
        //     Gets the current slot the node is processing
        //
        // Parámetros:
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ulong>> GetSlotAsync(Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the current slot the node is processing
        //
        // Parámetros:
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ulong> GetSlot(Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the current slot leader.
        //
        // Parámetros:
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<string>> GetSlotLeaderAsync(Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the current slot leader.
        //
        // Parámetros:
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<string> GetSlotLeader(Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the slot leaders for a given slot range.
        //
        // Parámetros:
        //   start:
        //     The start slot.
        //
        //   limit:
        //     The result limit.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<List<string>>> GetSlotLeadersAsync(ulong start, ulong limit, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the slot leaders for a given slot range.
        //
        // Parámetros:
        //   start:
        //     The start slot.
        //
        //   limit:
        //     The result limit.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<List<string>> GetSlotLeaders(ulong start, ulong limit, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the highest slot that the node has a snapshot for.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ulong>> GetSnapshotSlotAsync();

        //
        // Resumen:
        //     Gets the highest slot that the node has a snapshot for.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ulong> GetSnapshotSlot();

        //
        // Resumen:
        //     Gets the highest slot that the node has a snapshot for.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<SnapshotSlotInfo>> GetHighestSnapshotSlotAsync();

        //
        // Resumen:
        //     Gets the highest slot that the node has a snapshot for.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<SnapshotSlotInfo> GetHighestSnapshotSlot();

        //
        // Resumen:
        //     Gets the epoch activation information for a stake account.
        //
        // Parámetros:
        //   publicKey:
        //     Public key of account to query, as base-58 encoded string
        //
        //   epoch:
        //     Epoch for which to calculate activation details.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<StakeActivationInfo>> GetStakeActivationAsync(string publicKey, ulong epoch = 0uL, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the epoch activation information for a stake account.
        //
        // Parámetros:
        //   publicKey:
        //     Public key of account to query, as base-58 encoded string
        //
        //   epoch:
        //     Epoch for which to calculate activation details.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<StakeActivationInfo> GetStakeActivation(string publicKey, ulong epoch = 0uL, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets information about the current supply.
        //
        // Parámetros:
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ResponseValue<Supply>>> GetSupplyAsync(Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets information about the current supply.
        //
        // Parámetros:
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ResponseValue<Supply>> GetSupply(Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the token balance of an SPL Token account.
        //
        // Parámetros:
        //   splTokenAccountPublicKey:
        //     Public key of Token account to query, as base-58 encoded string.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ResponseValue<TokenBalance>>> GetTokenAccountBalanceAsync(string splTokenAccountPublicKey, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the token balance of an SPL Token account.
        //
        // Parámetros:
        //   splTokenAccountPublicKey:
        //     Public key of Token account to query, as base-58 encoded string.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ResponseValue<TokenBalance>> GetTokenAccountBalance(string splTokenAccountPublicKey, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets all SPL Token accounts by approved delegate.
        //
        // Parámetros:
        //   ownerPubKey:
        //     Public key of account owner query, as base-58 encoded string.
        //
        //   tokenMintPubKey:
        //     Public key of the specific token Mint to limit accounts to, as base-58 encoded
        //     string.
        //
        //   tokenProgramId:
        //     Public key of the Token program ID that owns the accounts, as base-58 encoded
        //     string.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ResponseValue<List<TokenAccount>>>> GetTokenAccountsByDelegateAsync(string ownerPubKey, string? tokenMintPubKey = null, string? tokenProgramId = null, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets all SPL Token accounts by approved delegate.
        //
        // Parámetros:
        //   ownerPubKey:
        //     Public key of account owner query, as base-58 encoded string.
        //
        //   tokenMintPubKey:
        //     Public key of the specific token Mint to limit accounts to, as base-58 encoded
        //     string.
        //
        //   tokenProgramId:
        //     Public key of the Token program ID that owns the accounts, as base-58 encoded
        //     string.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ResponseValue<List<TokenAccount>>> GetTokenAccountsByDelegate(string ownerPubKey, string? tokenMintPubKey = null, string? tokenProgramId = null, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets all SPL Token accounts by token owner.
        //
        // Parámetros:
        //   ownerPubKey:
        //     Public key of account owner query, as base-58 encoded string.
        //
        //   tokenMintPubKey:
        //     Public key of the specific token Mint to limit accounts to, as base-58 encoded
        //     string.
        //
        //   tokenProgramId:
        //     Public key of the Token program ID that owns the accounts, as base-58 encoded
        //     string.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ResponseValue<List<TokenAccount>>>> GetTokenAccountsByOwnerAsync(string ownerPubKey, string? tokenMintPubKey = null, string? tokenProgramId = null, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets all SPL Token accounts by token owner.
        //
        // Parámetros:
        //   ownerPubKey:
        //     Public key of account owner query, as base-58 encoded string.
        //
        //   tokenMintPubKey:
        //     Public key of the specific token Mint to limit accounts to, as base-58 encoded
        //     string.
        //
        //   tokenProgramId:
        //     Public key of the Token program ID that owns the accounts, as base-58 encoded
        //     string.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ResponseValue<List<TokenAccount>>> GetTokenAccountsByOwner(string ownerPubKey, string? tokenMintPubKey = null, string? tokenProgramId = null, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the 20 largest token accounts of a particular SPL Token.
        //
        // Parámetros:
        //   tokenMintPubKey:
        //     Public key of Token Mint to query, as base-58 encoded string.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ResponseValue<List<LargeTokenAccount>>>> GetTokenLargestAccountsAsync(string tokenMintPubKey, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the 20 largest token accounts of a particular SPL Token.
        //
        // Parámetros:
        //   tokenMintPubKey:
        //     Public key of Token Mint to query, as base-58 encoded string.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ResponseValue<List<LargeTokenAccount>>> GetTokenLargestAccounts(string tokenMintPubKey, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Get the token supply of an SPL Token type.
        //
        // Parámetros:
        //   tokenMintPubKey:
        //     Public key of Token Mint to query, as base-58 encoded string.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ResponseValue<TokenBalance>>> GetTokenSupplyAsync(string tokenMintPubKey, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Get the token supply of an SPL Token type.
        //
        // Parámetros:
        //   tokenMintPubKey:
        //     Public key of Token Mint to query, as base-58 encoded string.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ResponseValue<TokenBalance>> GetTokenSupply(string tokenMintPubKey, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns transaction details for a confirmed transaction.
        //
        //     The commitment parameter is optional, Solnet.Rpc.Types.Commitment.Processed is
        //     not supported, the default value Solnet.Rpc.Types.Commitment.Finalized is not
        //     sent.
        //
        // Parámetros:
        //   signature:
        //     Transaction signature as base-58 encoded string.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<TransactionMetaSlotInfo>> GetTransactionAsync(string signature, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns transaction details for a confirmed transaction.
        //
        //     The commitment parameter is optional, Solnet.Rpc.Types.Commitment.Processed is
        //     not supported, the default value Solnet.Rpc.Types.Commitment.Finalized is not
        //     sent.
        //
        // Parámetros:
        //   signature:
        //     Transaction signature as base-58 encoded string.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        [Obsolete("Please use GetTransactionAsync whenever possible instead. This method is expected to be removed in solana-core v1.8.")]
        Task<RequestResult<TransactionMetaSlotInfo>> GetConfirmedTransactionAsync(string signature, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns transaction details for a confirmed transaction.
        //
        //     The commitment parameter is optional, Solnet.Rpc.Types.Commitment.Processed is
        //     not supported, the default value Solnet.Rpc.Types.Commitment.Finalized is not
        //     sent.
        //
        // Parámetros:
        //   signature:
        //     Transaction signature as base-58 encoded string.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<TransactionMetaSlotInfo> GetTransaction(string signature, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Returns transaction details for a confirmed transaction.
        //
        //     The commitment parameter is optional, Solnet.Rpc.Types.Commitment.Processed is
        //     not supported, the default value Solnet.Rpc.Types.Commitment.Finalized is not
        //     sent.
        //
        // Parámetros:
        //   signature:
        //     Transaction signature as base-58 encoded string.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        [Obsolete("Please use GetTransaction whenever possible instead. This method is expected to be removed in solana-core v1.8.")]
        RequestResult<TransactionMetaSlotInfo> GetConfirmedTransaction(string signature, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the total transaction count of the ledger.
        //
        // Parámetros:
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ulong>> GetTransactionCountAsync(Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the total transaction count of the ledger.
        //
        // Parámetros:
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ulong> GetTransactionCount(Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the current node's software version info.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<NodeVersion>> GetVersionAsync();

        //
        // Resumen:
        //     Gets the current node's software version info.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<NodeVersion> GetVersion();

        //
        // Resumen:
        //     Gets the account info and associated stake for all voting accounts in the current
        //     bank.
        //
        // Parámetros:
        //   votePubKey:
        //     Filter by validator vote address, base-58 encoded string.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<VoteAccounts>> GetVoteAccountsAsync(string? votePubKey = null, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Gets the account info and associated stake for all voting accounts in the current
        //     bank.
        //
        // Parámetros:
        //   votePubKey:
        //     Filter by validator vote address, base-58 encoded string.
        //
        //   commitment:
        //     The state commitment to consider when querying the ledger state.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<VoteAccounts> GetVoteAccounts(string? votePubKey = null, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Requests an airdrop to the passed pubKey of the passed lamports amount. The commitment
        //     parameter is optional, the default Solnet.Rpc.Types.Commitment.Finalized is used.
        //
        //
        // Parámetros:
        //   pubKey:
        //     The public key of to receive the airdrop.
        //
        //   lamports:
        //     The amount of lamports to request.
        //
        //   commitment:
        //     The block commitment used to retrieve block hashes and verify success.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<string>> RequestAirdropAsync(string pubKey, ulong lamports, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Requests an airdrop to the passed pubKey of the passed lamports amount. The commitment
        //     parameter is optional, the default Solnet.Rpc.Types.Commitment.Finalized is used.
        //
        //
        // Parámetros:
        //   pubKey:
        //     The public key of to receive the airdrop.
        //
        //   lamports:
        //     The amount of lamports to request.
        //
        //   commitment:
        //     The block commitment used to retrieve block hashes and verify success.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<string> RequestAirdrop(string pubKey, ulong lamports, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Sends a transaction.
        //
        // Parámetros:
        //   transaction:
        //     The signed transaction as base-64 encoded string.
        //
        //   skipPreflight:
        //     If true skip the prflight transaction checks (default false).
        //
        //   preFlightCommitment:
        //     The block commitment used for preflight.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<string>> SendTransactionAsync(string transaction, bool skipPreflight = false, Commitment preFlightCommitment = Commitment.Finalized);

        //
        // Resumen:
        //     Sends a transaction.
        //
        // Parámetros:
        //   transaction:
        //     The signed transaction as base-64 encoded string.
        //
        //   skipPreflight:
        //     If true skip the prflight transaction checks (default false).
        //
        //   preFlightCommitment:
        //     The block commitment used for preflight.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<string> SendTransaction(string transaction, bool skipPreflight = false, Commitment preFlightCommitment = Commitment.Finalized);

        //
        // Resumen:
        //     Sends a transaction.
        //
        // Parámetros:
        //   transaction:
        //     The signed transaction as byte array.
        //
        //   skipPreflight:
        //     If true skip the prflight transaction checks (default false).
        //
        //   commitment:
        //     The block commitment used to retrieve block hashes and verify success.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<string>> SendTransactionAsync(byte[] transaction, bool skipPreflight = false, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Sends a transaction.
        //
        // Parámetros:
        //   transaction:
        //     The signed transaction as byte array.
        //
        //   skipPreflight:
        //     If true skip the prflight transaction checks (default false).
        //
        //   commitment:
        //     The block commitment used to retrieve block hashes and verify success.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<string> SendTransaction(byte[] transaction, bool skipPreflight = false, Commitment commitment = Commitment.Finalized);

        //
        // Resumen:
        //     Simulate sending a transaction.
        //
        // Parámetros:
        //   transaction:
        //     The signed transaction as a base-64 encoded string.
        //
        //   sigVerify:
        //     If the transaction signatures should be verified (default false, conflicts with
        //     replaceRecentBlockHash.
        //
        //   commitment:
        //     The block commitment used to retrieve block hashes and verify success.
        //
        //   replaceRecentBlockhash:
        //     If the transaction recent blockhash should be replaced with the most recent blockhash
        //     (default false, conflicts with sigVerify
        //
        //   accountsToReturn:
        //     List of accounts to return, as base-58 encoded strings.
        //
        // Devuelve:
        //     Returns a task that holds the asynchronous operation result and state.
        Task<RequestResult<ResponseValue<SimulationLogs>>> SimulateTransactionAsync(string transaction, bool sigVerify = false, Commitment commitment = Commitment.Finalized, bool replaceRecentBlockhash = false, IList<string>? accountsToReturn = null);

        //
        // Resumen:
        //     Simulate sending a transaction.
        //
        // Parámetros:
        //   transaction:
        //     The signed transaction base-64 encoded string.
        //
        //   sigVerify:
        //     If the transaction signatures should be verified (default false, conflicts with
        //     replaceRecentBlockHash.
        //
        //   commitment:
        //     The block commitment used to retrieve block hashes and verify success.
        //
        //   replaceRecentBlockhash:
        //     If the transaction recent blockhash should be replaced with the most recent blockhash
        //     (default false, conflicts with sigVerify
        //
        //   accountsToReturn:
        //     List of accounts to return, as base-58 encoded strings.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ResponseValue<SimulationLogs>> SimulateTransaction(string transaction, bool sigVerify = false, Commitment commitment = Commitment.Finalized, bool replaceRecentBlockhash = false, IList<string>? accountsToReturn = null);

        //
        // Resumen:
        //     Simulate sending a transaction.
        //
        // Parámetros:
        //   transaction:
        //     The signed transaction as a byte array.
        //
        //   sigVerify:
        //     If the transaction signatures should be verified (default false, conflicts with
        //     replaceRecentBlockHash.
        //
        //   commitment:
        //     The block commitment used to retrieve block hashes and verify success.
        //
        //   replaceRecentBlockhash:
        //     If the transaction recent blockhash should be replaced with the most recent blockhash
        //     (default false, conflicts with sigVerify
        //
        //   accountsToReturn:
        //     List of accounts to return, as base-58 encoded strings.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        Task<RequestResult<ResponseValue<SimulationLogs>>> SimulateTransactionAsync(byte[] transaction, bool sigVerify = false, Commitment commitment = Commitment.Finalized, bool replaceRecentBlockhash = false, IList<string>? accountsToReturn = null);

        //
        // Resumen:
        //     Simulate sending a transaction.
        //
        // Parámetros:
        //   transaction:
        //     The signed transaction as a byte array.
        //
        //   sigVerify:
        //     If the transaction signatures should be verified (default false, conflicts with
        //     replaceRecentBlockHash.
        //
        //   commitment:
        //     The block commitment used to retrieve block hashes and verify success.
        //
        //   replaceRecentBlockhash:
        //     If the transaction recent blockhash should be replaced with the most recent blockhash
        //     (default false, conflicts with sigVerify
        //
        //   accountsToReturn:
        //     List of accounts to return, as base-58 encoded strings.
        //
        // Devuelve:
        //     Returns an object that wraps the result along with possible errors with the request.
        RequestResult<ResponseValue<SimulationLogs>> SimulateTransaction(byte[] transaction, bool sigVerify = false, Commitment commitment = Commitment.Finalized, bool replaceRecentBlockhash = false, IList<string>? accountsToReturn = null);

        //
        // Resumen:
        //     Low-level method to send a batch of JSON RPC requests
        //
        // Parámetros:
        //   reqs:
        Task<RequestResult<JsonRpcBatchResponse>> SendBatchRequestAsync(JsonRpcBatchRequest reqs);

        //
        // Resumen:
        //     Generates the next unique id for the request.
        //
        // Devuelve:
        //     The id.
        int GetNextIdForReq();
    }
}
