﻿using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Greenhouse.Core.Models;
using CluedIn.Crawling.Greenhouse.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Linq;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.Greenhouse.Vocabularies;

namespace CluedIn.Crawling.Greenhouse.ClueProducers
{
    public class ApplicationClueProducer : BaseClueProducer<Application>
    {
        private readonly IClueFactory _factory;
        private readonly ILogger<GreenhouseClient> _log;

        public ApplicationClueProducer([NotNull] IClueFactory factory, ILogger<GreenhouseClient> _log)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this._log = _log;
        }

        protected override Clue MakeClueImpl(Application input, Guid id)
        {
            var clue = _factory.Create("/Application", input.id.ToString(), id);
            var data = clue.Data.EntityData;

            var vocab = new ApplicationVocabulary();

            data.Properties[vocab.Status] = input.status.PrintIfAvailable();
            data.Properties[vocab.RejectionReason] = input.rejection_reason.PrintIfAvailable();
            data.Properties[vocab.RejectionDetails] = input.rejection_details.PrintIfAvailable();
            data.Properties[vocab.RejectedAt] = input.rejected_at.ToString().PrintIfAvailable();
            data.Properties[vocab.ProspectiveOffice] = input.prospective_office.PrintIfAvailable();
            data.Properties[vocab.ProspectiveDepartment] = input.prospective_department.PrintIfAvailable();

            data.Properties[vocab.Prospect] = input.prospect.PrintIfAvailable();
            data.Properties[vocab.Location] = input.location.PrintIfAvailable();
            data.Properties[vocab.LastActivityAt] = input.last_activity_at.PrintIfAvailable();
            data.Properties[vocab.CandidateId] = input.candidate_id.ToString().PrintIfAvailable();
            data.Properties[vocab.AppliedAt] = input.applied_at.PrintIfAvailable();


            if (!data.OutgoingEdges.Any())
            {
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);
            }

            return clue;
        }
    }
}
