using System;
using System.Reactive;
using System.Reactive.Linq;
using MessageBusLite.Event;
using Microsoft.AspNetCore.Mvc;

namespace MessageBusLite.ApiSample.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly IEventBus _eventBus;

        public TestController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpGet]
        public OkResult Get()
        {
            // fire and forget
            _eventBus.Publish(new TestEvent {Name = "hello"});
            
            return Ok();
        }
    }

    public class TestEvent : IMessage
    {
        public string Name { get; set; }
    }

    public class FirstTestHandler : IEventBusHandler<TestEvent>
    {
        public IObservable<Unit> Handle(TestEvent message)
        {
            return Observable.Return(new Unit());
        }
    }

    public class SecondTestHandler : IEventBusHandler<TestEvent>
    {
        public IObservable<Unit> Handle(TestEvent message)
        {
            return Observable.Return(new Unit());
        }
    } 
}