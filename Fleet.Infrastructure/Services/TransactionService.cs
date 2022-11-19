using System.Threading.Tasks;
using Fleet.Core.ApiModels;
using Fleet.Core.Dtos;
using Fleet.Core.Entities;
using Fleet.Core.Interfaces.Repositories;
using Fleet.Core.Interfaces.Services;

namespace Fleet.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ApiResponse<TransactionDto>> CreateTransaction( TransactionDto transactionDto )
        {
            var transactionToCreate = new TransactionEntity();
            
            if( transactionDto.ProductId == null )
                transactionToCreate.Paid = transactionDto.Paid;
            else
            {
                var product = await _unitOfWork.Repository<ProductEntity>().GetByIdAsync ( (int) transactionDto.ProductId );
                transactionToCreate.Paid = product.Price * transactionDto.Quantity;
                transactionDto.Paid = transactionToCreate.Paid;
            }

            transactionToCreate.TransactionDirectionId = transactionDto.TransactionDirectionId;
            transactionToCreate.AccountId = transactionDto.AccountId;
            transactionToCreate.Currency = transactionDto.Currency;
            transactionToCreate.Quantity = transactionDto.Quantity;
            transactionToCreate.TransactionDate = transactionDto.TransactionDate;
            transactionToCreate.ProductId = transactionDto.ProductId;
            
            _unitOfWork.Repository<TransactionEntity>().Add ( transactionToCreate );
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<TransactionDto> ( 200, "", transactionDto );
        }
    }
}