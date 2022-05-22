using MailContainerTest.Data;
using MailContainerTest.Types;

namespace MailContainerTest.Services;

public class MailTransferService : IMailTransferService
{
    public IContainerDataStore ContainerDataStore { get; set; }

    public MailTransferService(IContainerDataStore containerDataStore)
    {
        ContainerDataStore = containerDataStore;
    }

    public MakeMailTransferResult MakeMailTransfer(MakeMailTransferRequest request)
    {
        MailContainer? mailContainer = ContainerDataStore.GetMailContainer(request.SourceMailContainerNumber!);

        var result = request.MailTransferResult(mailContainer);

        if (result.Success)
        {
            mailContainer!.Capacity -= request.NumberOfMailItems;
            ContainerDataStore.UpdateMailContainer(mailContainer);
        }

        return result;
    }
}