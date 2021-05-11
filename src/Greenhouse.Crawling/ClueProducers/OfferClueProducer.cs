using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Greenhouse.Core;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Greenhouse.Core.Models;
using CluedIn.Crawling.Greenhouse;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Greenhouse.Infrastructure;
using Microsoft.Extensions.Logging;
using CluedIn.Crawling.Helpers;
using System.Linq;
using CluedIn.Crawling.Greenhouse.Vocabularies;
using CluedIn.Core.Data.Parts;
using RestSharp;

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

            if (!string.IsNullOrEmpty(input.first_name) && !string.IsNullOrEmpty(input.last_name))
            {
                data.Name = string.Format("{0}{1}", input.first_name, input.last_name);
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
            _factory.CreateOutgoingEntityReference(clue, "/User", EntityEdgeType.Attended, input.candidate_id, education.GetKey());

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
                    var code = new EntityCode("/Email", "CluedIn", emailAddress.value);
                    data.Codes.Add(code);
                }
            }
            if (input.educations != null)
            {

               


                foreach (var education in input.educations)
                {
                    _factory.CreateOutgoingEntityReference(clue, "/School", EntityEdgeType.Attended, education, education.GetKey());
                }
            }

            if (input.employments != null)
                foreach (var employment in input.employments)
                {
                    _factory.CreateOutgoingEntityReference(clue, "/Placement", EntityEdgeType.Attended, employment, employment.GetKey());
                }

            if (input.company != null)
            {
                _factory.CreateOutgoingEntityReference(clue, EntityType.Organization, EntityEdgeType.WorksFor, input, input.company);
            }
            if (input.attachments != null)
            {
                foreach (var attachment in input.attachments)
                {
                    //You might need to parse this. 
                    _factory.CreateOutgoingEntityReference(clue, EntityType.Files.File, EntityEdgeType.PartOf, attachment, attachment.url);
                }
            }
            if (input.applications != null)
            {
                foreach (var application in input.application_ids)
                {
                    _factory.CreateOutgoingEntityReference(clue, "/Application", EntityEdgeType.For, application, application.ToString());
                }
            }
            if (input.addresses != null)
            {
                foreach (var address in input.addresses)
                {
                    _factory.CreateOutgoingEntityReference(clue, EntityType.Location, EntityEdgeType.For, address, address.value);
                }
            }
            data.Properties[vocab.Title] = input.title.PrintIfAvailable();
            


            data.Properties[vocab.LastName] = input.last_name.PrintIfAvailable();
            data.Properties[vocab.LastActivity] = input.last_activity.ToString().PrintIfAvailable();
            data.Properties[vocab.Coordinator] = input.coordinator.PrintIfAvailable();
            data.Properties[vocab.CanEmail] = input.can_email.PrintIfAvailable();

            data.Properties[vocab.FirstName] = input.first_name.PrintIfAvailable();


            if (!data.OutgoingEdges.Any())
            {
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);
            }

            return clue;
        }
    }
}
