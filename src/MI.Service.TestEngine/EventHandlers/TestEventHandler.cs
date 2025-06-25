using AutoMapper;
using MI.Service.TestEngine.Business.Tests;
using MI.Service.TestEngine.Business.Tests.Models;
using MI.Service.Shared.MongoDb.Resources.Events;
using MI.Service.Shared.RabbitMQ;

namespace MI.Service.TestEngine.EventHandlers;

/// <summary>
/// Test Engine Integration Event Handler.
/// </summary>
public class TestEventHandler : IIntegrationEventHandler<TestEngineActionResponse>
{
    private readonly TestService TestInternalService;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestEventHandler"/> class.
    /// </summary>
    /// <param name="mapper">The mapper.</param>
    /// <param name="TestInternalService">The Test service.</param>
    public TestEventHandler(IMapper mapper, TestService TestInternalService)
    {
        this.mapper = mapper;
        this.TestInternalService = TestInternalService;
    }

    /// <summary>
    /// Handles the specified event.
    /// </summary>
    /// <param name="event">The event.</param>
    /// <returns></returns>
    public async Task Handle(TestEngineActionResponse @event)
    {
        var Test = await this.TestInternalService.GetTestByRefReqIdAsync(@event.ReferenceRequestId);
        var TestSubmitModel = this.mapper.Map<TestSubmitModel>(@event);
        TestSubmitModel.TestId = Test.Id;
        await this.TestInternalService.SubmitTestAsync(TestSubmitModel.TestId, TestSubmitModel);
    }
}
