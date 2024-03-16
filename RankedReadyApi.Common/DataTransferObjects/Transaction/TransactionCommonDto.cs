namespace RankedReadyApi.Common.DataTransferObjects.Transaction
{
    public class TransactionCommonDto
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }

        public static TransactionCommonDto CreateDto(TransactionStripeDto model)
        {
            return new TransactionCommonDto
            {
                Id = model.Id,
                AccountId = model.AccountId,
                UserId = model.UserId,
                Email = model.Email,
                Amount = model.Amount.ToString(),
                CreatedDate = model.CreatedDate,
                Status = model.IsCompleted == true ? "Completed" : "Failed"
            };
        }

        public static TransactionCommonDto CreateDto(TransactionDto model)
        {
            return new TransactionCommonDto
            {
                Id = model.Id,
                AccountId = model.AccountId,
                UserId = model.UserId,
                Email = model.Email,
                Amount = model.CartAmount.ToString(),
                CreatedDate = model.DateTransaction,
                Status = model.IsSucceed != null && model.IsSucceed == true ? "Completed" : "Failed"
            };
        }
    }
}
