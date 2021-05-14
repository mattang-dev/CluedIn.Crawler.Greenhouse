using System;
using System.Linq;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Greenhouse.Core.Models;
using CluedIn.Crawling.Greenhouse.Infrastructure;
using CluedIn.Crawling.Greenhouse.Vocabularies;
using CluedIn.Crawling.Helpers;
using Microsoft.Extensions.Logging;

namespace CluedIn.Crawling.Greenhouse.ClueProducers
{
    public class OfferClueProducer : BaseClueProducer<Offer>
    {
        private readonly IClueFactory _factory;
        private readonly ILogger<GreenhouseClient> _log;

        public OfferClueProducer([NotNull] IClueFactory factory, ILogger<GreenhouseClient> _log)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this._log = _log;
        }

        protected override Clue MakeClueImpl(Offer input, Guid id)
        {
            var clue = _factory.Create("/Offer", input.id.ToString(), id);
            var data = clue.Data.EntityData;
            var vocab = new OfferVocabulary();
            data.Name = $"offer-{input.id}";
            data.Description = "Job Offer";
            data.DisplayName = "Job Offer";
            DateTimeOffset modifiedDate;
            if (DateTimeOffset.TryParse(input.updated_at.ToString(), out modifiedDate))
            {
                data.ModifiedDate = modifiedDate;
            }
            DateTimeOffset createdDate;
            if (DateTimeOffset.TryParse(input.created_at.ToString(), out createdDate))
            {
                data.CreatedDate = createdDate;
            }
            data.Properties[vocab.Status] = input.status.PrintIfAvailable();
            data.Properties[vocab.ApplicationId] = input.application_id.PrintIfAvailable();
            data.Properties[vocab.JobId] = input.job_id.PrintIfAvailable();
            data.Properties[vocab.CandidateId] = input.candidate_id.PrintIfAvailable();
            data.Properties[vocab.Version] = input.version.PrintIfAvailable();
            data.Properties[vocab.SentAt] = input.sent_at.PrintIfAvailable();
            data.Properties[vocab.StartsAt] = input.starts_at.PrintIfAvailable();
            _factory.CreateOutgoingEntityReference(clue, "/Job", EntityEdgeType.Has, input.job_id, input.GetKey());
            _factory.CreateOutgoingEntityReference(clue, "/Candidate", EntityEdgeType.For, input.candidate_id,
                input.candidate_id.ToString());
            _factory.CreateOutgoingEntityReference(clue, "/Application", EntityEdgeType.For, input.application_id,
                input.application_id.ToString());
            if (!data.OutgoingEdges.Any())
            {
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);
            }
            return clue;
        }
    }
}
