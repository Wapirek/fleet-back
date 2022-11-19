using System.Threading.Tasks;
using Fleet.Core.ApiModels;
using Fleet.Core.Dtos;

namespace Fleet.Core.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<ApiResponse<TransactionDto>> CreateTransaction( TransactionDto transactionDto );
    }
}