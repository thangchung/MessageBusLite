using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using Microsoft.AspNetCore.Mvc;

namespace MessageBusLite.ApiSample.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IMessageBus _eventBus;

        public ValuesController(IMessageBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _eventBus.Publish(new TestEvent { Name = "abc" });
            return new[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class TestEvent : IMessage
    {
        public string Name { get; set; }
    }

    public class FirstTestHandler : IMessageBusHandler<TestEvent>
    {
        public IObservable<Unit> Handle(TestEvent message)
        {
            return Observable.Return(new Unit());
        }
    }

    public class SecondTestHandler : IMessageBusHandler<TestEvent>
    {
        public IObservable<Unit> Handle(TestEvent message)
        {
            return Observable.Return(new Unit());
        }
    }
}
