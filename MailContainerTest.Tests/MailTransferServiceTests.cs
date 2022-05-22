namespace MailContainerTest.Tests;

public class MailTransferServiceTests
{
    private readonly Mock<IContainerDataStore> _containerDataStoreMock = new();

    [Fact]
    public void MakeMailTransfer_WhenMailContainerIsNull_ThenResultShouldFail()
    {
        MailContainer? mailContainer = null;

        var mailTransferRequest = new MakeMailTransferRequest()
        {
            DestinationMailContainerNumber = "0",
            MailType = MailType.StandardLetter,
            NumberOfMailItems = 1,
            SourceMailContainerNumber = "1"
        };
        _containerDataStoreMock.Setup(x => x.GetMailContainer("1"))
                                         .Returns(mailContainer);

        MailTransferService sut = new(_containerDataStoreMock.Object);

        var result = sut.MakeMailTransfer(mailTransferRequest);

        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
    }

    [Fact]
    [Trait("Category", "StandardMail")]
    public void MakeMailTransfer_WhenMailTypeIsStandardAndAllowedMailTypeIsStandard_ThenResultShouldSucceed()
    {
        MailContainer mailContainer = new()
        {
            AllowedMailType = MailType.StandardLetter,
            Capacity = 2,
            MailContainerNumber = "0",
            Status = MailContainerStatus.Operational
        };

        var mailTransferRequest = new MakeMailTransferRequest()
        {
            DestinationMailContainerNumber = "0",
            MailType = MailType.StandardLetter,
            NumberOfMailItems = 1,
            SourceMailContainerNumber = "0"
        };
        _containerDataStoreMock.Setup(x => x.GetMailContainer("0"))
                                         .Returns(mailContainer);

        MailTransferService sut = new(_containerDataStoreMock.Object);

        var result = sut.MakeMailTransfer(mailTransferRequest);

        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "LargeLetter")]
    public void MakeMailTransfer_WhenMailTypeIsStandardAndAllowedMailTypeIsNotStandard_ThenResultShouldFail()
    {
        MailContainer mailContainer = new()
        {
            AllowedMailType = MailType.LargeLetter,
            Capacity = 2,
            MailContainerNumber = "0",
            Status = MailContainerStatus.Operational
        };

        var mailTransferRequest = new MakeMailTransferRequest()
        {
            DestinationMailContainerNumber = "0",
            MailType = MailType.StandardLetter,
            NumberOfMailItems = 1,
            SourceMailContainerNumber = "1"
        };
        _containerDataStoreMock.Setup(x => x.GetMailContainer("1"))
                                         .Returns(mailContainer);

        MailTransferService sut = new(_containerDataStoreMock.Object);

        var result = sut.MakeMailTransfer(mailTransferRequest);

        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
    }

    [Fact]
    [Trait("Category", "LargeLetter")]
    public void MakeMailTransfer_WhenLargeMailTypeHasCapacityLessThanNumberOfMailItems_ThenResultShouldFail()
    {
        MailContainer mailContainer = new()
        {
            AllowedMailType = MailType.LargeLetter,
            Capacity = 2,
            MailContainerNumber = "0",
            Status = MailContainerStatus.Operational
        };

        var mailTransferRequest = new MakeMailTransferRequest()
        {
            DestinationMailContainerNumber = "0",
            MailType = MailType.LargeLetter,
            NumberOfMailItems = 3,
            SourceMailContainerNumber = "1"
        };
        _containerDataStoreMock.Setup(x => x.GetMailContainer("1"))
                                         .Returns(mailContainer);

        MailTransferService sut = new(_containerDataStoreMock.Object);

        var result = sut.MakeMailTransfer(mailTransferRequest);

        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
    }

    [Fact]
    [Trait("Category", "LargeLetter")]
    public void MakeMailTransfer_WhenLargeMailTypeHasCapacityMoreThanNumberOfMailItems_ThenResultShouldPass()
    {
        MailContainer mailContainer = new()
        {
            AllowedMailType = MailType.LargeLetter,
            Capacity = 4,
            MailContainerNumber = "0",
            Status = MailContainerStatus.Operational
        };

        var mailTransferRequest = new MakeMailTransferRequest()
        {
            DestinationMailContainerNumber = "0",
            MailType = MailType.LargeLetter,
            NumberOfMailItems = 3,
            SourceMailContainerNumber = "1"
        };
        _containerDataStoreMock.Setup(x => x.GetMailContainer("1"))
                                         .Returns(mailContainer);

        MailTransferService sut = new(_containerDataStoreMock.Object);

        var result = sut.MakeMailTransfer(mailTransferRequest);

        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "SmallParcel")]
    public void MakeMailTransfer_WhenSmallParcelSatusIsOperational_ThenResultShouldPass()
    {
        MailContainer mailContainer = new()
        {
            AllowedMailType = MailType.SmallParcel,
            Capacity = 4,
            MailContainerNumber = "0",
            Status = MailContainerStatus.Operational
        };

        var mailTransferRequest = new MakeMailTransferRequest()
        {
            DestinationMailContainerNumber = "0",
            MailType = MailType.SmallParcel,
            NumberOfMailItems = 3,
            SourceMailContainerNumber = "1"
        };
        _containerDataStoreMock.Setup(x => x.GetMailContainer("1"))
                                         .Returns(mailContainer);

        MailTransferService sut = new(_containerDataStoreMock.Object);

        var result = sut.MakeMailTransfer(mailTransferRequest);

        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "SmallParcel")]
    public void MakeMailTransfer_WhenSmallParcelSatusIsNotOperational_ThenResultShouldFail()
    {
        MailContainer mailContainer = new()
        {
            AllowedMailType = MailType.SmallParcel,
            Capacity = 4,
            MailContainerNumber = "0",
            Status = MailContainerStatus.OutOfService
        };

        var mailTransferRequest = new MakeMailTransferRequest()
        {
            DestinationMailContainerNumber = "0",
            MailType = MailType.SmallParcel,
            NumberOfMailItems = 3,
            SourceMailContainerNumber = "1"
        };
        _containerDataStoreMock.Setup(x => x.GetMailContainer("1"))
                                         .Returns(mailContainer);

        MailTransferService sut = new(_containerDataStoreMock.Object);

        var result = sut.MakeMailTransfer(mailTransferRequest);

        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
    }
}