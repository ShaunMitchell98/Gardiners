using Gardiners.Core.Deliveries;
using Gardiners.Core.Models;
using Moq;
using System;
using Xunit;

namespace Gardiners.Core.Tests.Deliveries;

public class DeliveryServiceTests : IntegrationTest
{
    [Fact]
    public void Service_GivenInvalidModeId_DoesNotCompleteOrder()
    {
        var service = GetService<IDeliveryService>();

        var request = new DeliverOrderRequest() { DeliveryModeId = -1, PartnerId = 3 };

        var response = service!.DeliverOrder(request);

        AssertResponse(false, response, request, null);
    }

    [Fact]
    public void Service_GivenInvalidPartnerId_DoesNotCompleteOrder()
    {
        var service = GetService<IDeliveryService>();

        var request = new DeliverOrderRequest() { DeliveryModeId = 3, PartnerId = -1 };

        var response = service!.DeliverOrder(request);

        AssertResponse(false, response, request, null);
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public void Service_GivenWhetherDeliveryPartnerSupportsMode_CompletesOrder(bool supportsMode, bool success)
    {
        var deliveryMode = new DeliveryMode(Guid.NewGuid().ToString(), false);

        var partner = new DeliveryPartner();

        if (supportsMode)
        {
            partner.AllowedDeliveryModes.Add(deliveryMode);
        }

        var service = GetDeliveryService(deliveryMode, partner);

        var request = new DeliverOrderRequest();

        var response = service.DeliverOrder(request);

        AssertResponse(success, response, request, partner);
    }

    [Fact]
    public void Service_GivenOrderTotalBelowLimitAndApplyOrderLimitFalse_CompletesOrder()
    {
        var deliveryMode = new DeliveryMode(Guid.NewGuid().ToString(), false) { ApplyOrderLimit = false };

        var partner = new DeliveryPartner() { OrderValueLimit = Convert.ToDecimal(new Random().NextDouble()) };
        partner.AllowedDeliveryModes.Add(deliveryMode);

        var service = GetDeliveryService(deliveryMode, partner);

        var request = new DeliverOrderRequest() { OrderTotal = partner.OrderValueLimit / 2 };

        var response = service.DeliverOrder(request);

        AssertResponse(true, response, request, partner);
    }

    [Fact]
    public void Service_GivenOrderTotalBelowLimitAndApplyOrderLimitTrue_CompletesOrder()
    {
        var deliveryMode = new DeliveryMode(Guid.NewGuid().ToString(), false) { ApplyOrderLimit = true };

        var partner = new DeliveryPartner() { OrderValueLimit = Convert.ToDecimal(new Random().NextDouble()) };
        partner.AllowedDeliveryModes.Add(deliveryMode);

        var service = GetDeliveryService(deliveryMode, partner);

        var request = new DeliverOrderRequest() { OrderTotal = partner.OrderValueLimit / 2 };

        var response = service.DeliverOrder(request);

        AssertResponse(true, response, request, partner);
    }

    [Fact]
    public void Service_GivenOrderTotalAboveLimitAndApplyOrderLimitFalse_CompletesOrder()
    {
        var deliveryMode = new DeliveryMode(Guid.NewGuid().ToString(), false) { ApplyOrderLimit = false };

        var partner = new DeliveryPartner() { OrderValueLimit = Convert.ToDecimal(new Random().NextDouble()) };
        partner.AllowedDeliveryModes.Add(deliveryMode);

        var service = GetDeliveryService(deliveryMode, partner);

        var request = new DeliverOrderRequest() { OrderTotal = partner.OrderValueLimit * 2 };

        var response = service.DeliverOrder(request);

        AssertResponse(true, response, request, partner);
    }

    [Fact]
    public void Service_GivenOrderTotalAboveLimitAndApplyOrderLimitTrue_DoesNotCompleteOrder()
    {
        var deliveryMode = new DeliveryMode(Guid.NewGuid().ToString(), false) { ApplyOrderLimit = true };

        var partner = new DeliveryPartner() { OrderValueLimit = Convert.ToDecimal(new Random().NextDouble()) };
        partner.AllowedDeliveryModes.Add(deliveryMode);

        var service = GetDeliveryService(deliveryMode, partner);

        var request = new DeliverOrderRequest() { OrderTotal = partner.OrderValueLimit * 2 };

        var response = service.DeliverOrder(request);

        AssertResponse(false, response, request, partner);
    }

    [Theory]
    [InlineData(Status.Live, true, true)]
    [InlineData(Status.Disabled, true, false)]
    [InlineData(Status.Live, false, true)]
    [InlineData(Status.Disabled, false, true)]
    public void Service_GivenRequiresLive_ReturnsCorrectResult(Status status, bool requiresLive, bool success)
    {
        var deliveryMode = new DeliveryMode(Guid.NewGuid().ToString(), false) { RequiresLive = requiresLive };

        var partner = new DeliveryPartner() { Status = status };
        partner.AllowedDeliveryModes.Add(deliveryMode);

        var service = GetDeliveryService(deliveryMode, partner);

        var request = new DeliverOrderRequest();

        var response = service.DeliverOrder(request);

        AssertResponse(success, response, request, partner);
    }

    private static void AssertResponse(bool success, DeliverOrderResponse response, DeliverOrderRequest request, DeliveryPartner? partner)
    {
        if (success)
        {
            Assert.True(response.Success);
            Assert.Equal(request.OrderTotal, partner!.TotalOrderValueProcessedTillDate);
        }
        else
        {
            Assert.False(response.Success);

            if (partner != null)
            {
                Assert.Equal(0, partner.TotalOrderValueProcessedTillDate);
            }
        }
    }

    private static IDeliveryService GetDeliveryService(DeliveryMode mode, DeliveryPartner partner)
    {
        var mockDeliveryModeDataStore = new Mock<IDeliveryModeDataStore>();
        mockDeliveryModeDataStore.Setup(x => x.GetDeliveryMode(It.IsAny<int>())).Returns(mode);

        var mockDeliveryPartnerDataStore = new Mock<IDeliveryPartnerDataStore>();
        mockDeliveryPartnerDataStore.Setup(x => x.GetDeliveryPartner(It.IsAny<int>())).Returns(partner);
        return new DeliveryService(mockDeliveryModeDataStore.Object, mockDeliveryPartnerDataStore.Object);
    }
}
