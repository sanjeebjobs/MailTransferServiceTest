namespace MailContainerTest.Types
{
    public class MakeMailTransferRequest
    {
        public string? SourceMailContainerNumber { get; set; }
        public string? DestinationMailContainerNumber { get; set; }
        public int NumberOfMailItems { get; set; }
        public DateTime TransferDate { get; set; }
        public MailType MailType { get; set; }
    }

    public static class MakeMailTransferRequestExtension
    {
        public static MakeMailTransferResult MailTransferResult(this MakeMailTransferRequest request, MailContainer? mailContainer)
        {
            MakeMailTransferResult result = new();
            if (mailContainer is null)
                return result.Failed();

            if (mailContainer.AllowedMailType != request.MailType)
                return result.Failed();

            switch (request.MailType)
            {
                case MailType.LargeLetter:

                    if (mailContainer.Capacity < request.NumberOfMailItems)
                        return result.Failed();
                    break;

                case MailType.SmallParcel:
                    if (mailContainer.Status != MailContainerStatus.Operational)
                        return result.Failed();
                    break;
            }

            return result.Succeed();
        }
    }
}