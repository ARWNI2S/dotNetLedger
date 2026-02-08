using NBitcoin;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotNetLedger.Bitcoin.Commons
{
    public interface IBlockRepository
    {
        Task<Block> GetBlockAsync(uint256 blockId, CancellationToken cancellationToken);
    }
}
