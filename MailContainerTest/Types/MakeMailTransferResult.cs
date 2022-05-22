namespace MailContainerTest.Types
{
    public class MakeMailTransferResult
    {
        public bool Success { get; set; }
    }

    public static class MakeMailTransferResultExtension
    {
        public static MakeMailTransferResult Succeed(this MakeMailTransferResult request)
        {
            request.Success = true;
            return request;
        }

        public static MakeMailTransferResult Failed(this MakeMailTransferResult request)
        {
            request.Success = false;
            return request;
        }
    }
}