using Bandit.NBS.Daemon.Models;

namespace Bandit.NBS.Daemon.Mappers
{
    public static class TransactionMapper
    {
        public static NpgsqlRepository.Models.Transaction ToModel(this AnalyticsTransaction transaction) => new()
        {
            Id = transaction.Id,
            DebitBank = transaction.DebitBank,
            CreditBank = transaction.CreditBank,
            ClientId = transaction.ClientId,
            ClientGender = transaction.ClientGender,
            ClientBirthDate = transaction.ClientBirthDate,
            ClientAge = transaction.ClientAge,
            ClientMaritalStatus = transaction.ClientMaritalStatus,
            ClientMonthlySalary = transaction.ClientMonthlySalary,
            TransactionDate = transaction.TransactionDate,
            MerchantId = transaction.MerchantId,
            MerchantActivity = transaction.MerchantActivity,
            AuthenticationMethod = transaction.AuthenticationMethod,
            TransferredAmount = transaction.TransferredAmount
        };
    }
}
