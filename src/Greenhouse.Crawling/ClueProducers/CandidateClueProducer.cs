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
    public class CandidateClueProducer : BaseClueProducer<Candidate>
    {
        private readonly IClueFactory _factory;
        private readonly ILogger<GreenhouseClient> _log;

        public CandidateClueProducer([NotNull] IClueFactory factory, ILogger<GreenhouseClient> _log)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this._log = _log;
        }

        protected override Clue MakeClueImpl(Candidate input, Guid id)
        {

            var clue = _factory.Create("/Person/Candidate", input.id.ToString(), id);
            var data = clue.Data.EntityData;
            var vocab = new CandidateVocabulary();
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.PROPERTIES_002_Unknown_VocabularyKey_Used);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_002_Uri_MustBeSet);
            if (!string.IsNullOrEmpty(input.first_name) && !string.IsNullOrEmpty(input.last_name))
            {
                data.Name = $"{input.first_name}{input.last_name}";
            }
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
            if (input.tags != null)
            {
                foreach (var tag in input.tags)
                {
                    data.Tags.Add(new Tag(tag));
                }
            }
            if (input.social_media_addresses != null)
            {
                foreach (var socialMediaAddress in input.social_media_addresses)
                {
                    data.Aliases.Add(socialMediaAddress.ToString());
                }
            }
            if (input.phone_numbers != null)
            {
                foreach (var phoneNumbers in input.phone_numbers)
                {
                    data.Aliases.Add(phoneNumbers.ToString());
                }
            }
            if (input.email_addresses != null)
            {
                foreach (var emailAddress in input.email_addresses)
                {
                    var code = new EntityCode("/Candidate", "CluedIn", emailAddress.value);
                    data.Codes.Add(code);
                }
            }
            if (input.educations != null)
            {
                foreach (var education in input.educations)
                {
                    _factory.CreateOutgoingEntityReference(clue, CommonEntityTypes.School, EntityEdgeType.Attended,
                        education, education.GetKey());
                }
            }
            if (input.employments != null)
                foreach (var employment in input.employments)
                {
                    _factory.CreateOutgoingEntityReference(clue, "/Placement", EntityEdgeType.Attended, employment,
                        employment.GetKey());
                }
            if (input.company != null)
            {
                _factory.CreateOutgoingEntityReference(clue, EntityType.Organization, EntityEdgeType.WorksFor, input,
                    input.company);
            }
            if (input.attachments != null)
            {
                foreach (var attachment in input.attachments)
                {
                    //You might need to parse this. 
                    _factory.CreateOutgoingEntityReference(clue, EntityType.Files.File, EntityEdgeType.PartOf,
                        attachment, attachment.url);
                }
            }
            if (input.applications != null)
            {
                foreach (var application in input.application_ids)
                {
                    _factory.CreateOutgoingEntityReference(clue, "/Application", EntityEdgeType.For, application,
                        application);
                }
            }
            if (input.addresses != null)
            {
                foreach (var address in input.addresses)
                {
                    _factory.CreateOutgoingEntityReference(clue, EntityType.Location, EntityEdgeType.For, address,
                        address.value);
                }
            }
            data.Properties[vocab.Title] = input.title.PrintIfAvailable();
            data.Properties[vocab.LastName] = input.last_name.PrintIfAvailable();
            data.Properties[vocab.LastActivity] = input.last_activity.ToString().PrintIfAvailable();
            data.Properties[vocab.Coordinator] = input.coordinator.PrintIfAvailable();
            data.Properties[vocab.CanEmail] = input.can_email.PrintIfAvailable();
            data.Properties[vocab.FirstName] = input.first_name.PrintIfAvailable();
            data.Properties[vocab.Email] = input.email_addresses.PrintIfAvailable(JsonUtility.Serialize);
            data.Properties[vocab.Company] = input.company.PrintIfAvailable();
            if (!data.OutgoingEdges.Any())
            {
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);
            }
            return clue;
        }
    }
}
