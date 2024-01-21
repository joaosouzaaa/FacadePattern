using FacadePattern.API.Enums;
using FacadePattern.API.Extensions;

namespace UnitTests.ExtensionTests;
public sealed class MessageExtensionTests
{
    [Fact]
    public void Description_SuccessfulScenario()
    {
        // A
        var messageToGetDescription = EMessage.NotFound;

        // A
        var messageDescription = messageToGetDescription.Description();

        // A
        Assert.Equal("{0} was not found.", messageDescription);
    }
}
