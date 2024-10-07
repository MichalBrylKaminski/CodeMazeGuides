using MassTransit;
using NSubstitute;
using RebusVsNServiceBusVsMassTransit.Domain;

namespace RebusVsNServiceBusVsMassTransit.MassTransit.Tests;

public class MessageSenderTests
{
    [Test]
    public async Task GivenMassTransitMessageBus_WhenSendMessageAsync_ThenPublishIsCalled()
    {
        var bus = Substitute.For<IBus>();
        var sut = new MessageSender(bus);
        
        await sut.SendMessageAsync();

        await bus.Received(1).Publish(Arg.Any<Message>());
    }
}