using System;
using System.Linq;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Greenhouse.Core.Models;
using CluedIn.Crawling.Greenhouse.Infrastructure;
using Microsoft.Extensions.Logging;

namespace CluedIn.Crawling.Greenhouse.ClueProducers
{
    public class JobClueProducer : BaseClueProducer<Job>
    {
        private readonly IClueFactory _factory;
        private readonly ILogger<GreenhouseClient> _log;

        public JobClueProducer([NotNull] IClueFactory factory, ILogger<GreenhouseClient> _log)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this._log = _log;
        }

        protected override Clue MakeClueImpl(Job input, Guid id)
        {
            var clue = _factory.Create("/Job", input.id.ToString(), id);
            var data = clue.Data.EntityData;

            if (!string.IsNullOrEmpty(input.name))
            {
                data.Name = input.name;
                
            }

            


            foreach (var hiringManager in input.hiring_team.hiring_managers)
            {
                _factory.CreateOutgoingEntityReference(clue, EntityType.Infrastructure.User, GreenhouseEntityTypes.Employee, input,
                    hiringManager.GetKey());
                
            }


            if (!data.OutgoingEdges.Any())
            {
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);
            }

            return clue;
        }
    }
}
