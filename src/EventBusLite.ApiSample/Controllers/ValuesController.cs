using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Reactive;

namespace EventBusLite.ApiSample.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IEventBus _eventBus;

        public ValuesController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _eventBus.Publish(new TestEvent {Name = "abc"});

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

    public class TestEvent : IEvent
    {
        public string Name { get; set; }
    }

    public class TestHandler : IEventBusHandler<TestEvent>
    {
        public IObservable<Unit> Handle(TestEvent message)
        {
            return Observable.Return(new Unit());
        }
    }
}
