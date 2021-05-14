using System;
using System.Linq;
using AutoFixture.Xunit2;
using CluedIn.Core.Data;
using CluedIn.Crawling.Greenhouse.ClueProducers;
using CluedIn.Crawling.Greenhouse.Core.Models;
using CluedIn.Crawling.Greenhouse.Factories;
using CluedIn.Crawling.Greenhouse.Infrastructure;
using CluedIn.Crawling.Greenhouse.Vocabularies;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Xunit;

namespace CluedIn.Crawling.Greenhouse.Unit.Test.ClueProducers
{
    public class CandidateClueProducerTests : BaseClueProducerTest<Candidate>
    {
        protected override BaseClueProducer<Candidate> Sut =>
            new CandidateClueProducer(new GreenhouseClueFactory(), new NullLogger<GreenhouseClient>());

        protected override EntityType ExpectedEntityType { get; } = "/Person/Candidate";

        [Theory]
        [AutoData]
        public void MakeClueCreatesClue()
        {
            var jsonCandidateString =
                "{\"website_addresses\":[],\"updated_at\":\"2021-05-09T11:00:08.859Z\",\"title\":\"Project Manager - Talent Acquisition Commercial Launch Excellence\",\"tags\":[],\"social_media_addresses\":[],\"recruiter\":{\"name\":\"Kristen Turner\",\"last_name\":\"Turner\",\"id\":4003481004,\"first_name\":\"Kristen\",\"employee_id\":null},\"photo_url\":null,\"phone_numbers\":[{\"value\":\"973 590-8601\",\"type\":\"home\"}],\"last_name\":\"Turner\",\"last_activity\":\"2021-05-09T11:00:08.833Z\",\"is_private\":false,\"id\":4093122004,\"first_name\":\"Kristen\",\"employments\":[],\"email_addresses\":[{\"value\":\"kristen.turner@springworkstx.com\",\"type\":\"personal\"}],\"educations\":[{\"start_date\":null,\"school_name\":\"Fairleigh Dickinson University\",\"id\":4013165004,\"end_date\":null,\"discipline\":null,\"degree\":\"Bachelor's Degree\"}],\"created_at\":\"2021-02-18T19:43:17.028Z\",\"coordinator\":{\"name\":\"Erika Clark\",\"last_name\":\"Clark\",\"id\":4003582004,\"first_name\":\"Erika\",\"employee_id\":null},\"company\":\"KornFerry International - Novartis Pharmaceuticals\",\"can_email\":true,\"attachments\":[{\"url\":\"https://grnhse-ghr-prod-s4.s3.amazonaws.com/person_attachments/data/467/675/800/original/Kristen_Turner_-_Offer_Packet_2021-05-05_%28Private%29.pdf?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVQGOLGY3T6RAOEN7%2F20210514%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20210514T022613Z&X-Amz-Expires=604800&X-Amz-SignedHeaders=host&X-Amz-Signature=6ce7e62cca3d9b22505bf94295712fc5d639b2b37bf10924dbec6835ce4bc522\",\"type\":\"offer_packet\",\"filename\":\"Kristen_Turner_-_Offer_Packet_2021-05-05_(Private).pdf\"},{\"url\":\"https://grnhse-ghr-prod-s4.s3.amazonaws.com/person_attachments/data/411/381/700/original/Offer_Document_Kristen_Turner.docx?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVQGOLGY3T6RAOEN7%2F20210514%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20210514T022613Z&X-Amz-Expires=604800&X-Amz-SignedHeaders=host&X-Amz-Signature=4a0fcb792caf511d8bf1817d181eaa55a0de2f6cf13a48d8fbb129ad69389ee8\",\"type\":\"offer_letter\",\"filename\":\"Offer_Document_Kristen_Turner.docx\"}],\"applications\":[{\"status\":\"hired\",\"source\":{\"public_name\":\"Internal Applicant\",\"id\":4000177004},\"rejection_reason\":null,\"rejection_details\":null,\"rejected_at\":null,\"prospective_office\":null,\"prospective_department\":null,\"prospect_detail\":{\"prospect_stage\":null,\"prospect_pool\":null,\"prospect_owner\":null},\"prospect\":false,\"location\":null,\"last_activity_at\":\"2021-05-09T11:00:08.833Z\",\"keyed_custom_fields\":{\"work_authorization\":null,\"relocate\":null},\"jobs\":[{\"name\":\"MASTER JOB TEMPLATE\",\"id\":4002000004}],\"job_post_id\":null,\"id\":4093837004,\"custom_fields\":{\"work_authorization\":null,\"relocation\":null},\"current_stage\":null,\"credited_to\":{\"name\":\"Kristen Turner\",\"last_name\":\"Turner\",\"id\":4003481004,\"first_name\":\"Kristen\",\"employee_id\":null},\"candidate_id\":4093122004,\"attachments\":[{\"url\":\"https://grnhse-ghr-prod-s4.s3.amazonaws.com/person_attachments/data/467/675/800/original/Kristen_Turner_-_Offer_Packet_2021-05-05_%28Private%29.pdf?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVQGOLGY3T6RAOEN7%2F20210514%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20210514T022613Z&X-Amz-Expires=604800&X-Amz-SignedHeaders=host&X-Amz-Signature=6ce7e62cca3d9b22505bf94295712fc5d639b2b37bf10924dbec6835ce4bc522\",\"type\":\"offer_packet\",\"filename\":\"Kristen_Turner_-_Offer_Packet_2021-05-05_(Private).pdf\"},{\"url\":\"https://grnhse-ghr-prod-s4.s3.amazonaws.com/person_attachments/data/411/381/700/original/Offer_Document_Kristen_Turner.docx?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVQGOLGY3T6RAOEN7%2F20210514%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20210514T022613Z&X-Amz-Expires=604800&X-Amz-SignedHeaders=host&X-Amz-Signature=4a0fcb792caf511d8bf1817d181eaa55a0de2f6cf13a48d8fbb129ad69389ee8\",\"type\":\"offer_letter\",\"filename\":\"Offer_Document_Kristen_Turner.docx\"}],\"applied_at\":\"2021-02-18T20:04:38.417Z\",\"answers\":[]}],\"application_ids\":[4093837004],\"addresses\":[{\"value\":\"151 Andover Mohawk Road\\nAndover, NJ 07821\\nUS\",\"type\":\"home\"}]}";
            var candidate = JsonConvert.DeserializeObject<Candidate>(jsonCandidateString);
            var prod = new CandidateClueProducer(new GreenhouseClueFactory(), new NullLogger<GreenhouseClient>());
            var clue = prod.MakeClue(candidate, Guid.NewGuid());
            Assert.NotNull(clue);
            Assert.Equal(clue.Data.EntityData.Properties[GreenhouseVocabulary.Candidate.FirstName],
                candidate.first_name);
            Assert.Equal(clue.Data.EntityData.Properties[GreenhouseVocabulary.Candidate.LastName], candidate.last_name);
            Assert.Equal(clue.Data.EntityData.Properties[GreenhouseVocabulary.Candidate.Email],
                JsonConvert.SerializeObject(candidate.email_addresses));
            Assert.Equal(clue.Data.EntityData.Properties[GreenhouseVocabulary.Candidate.Company], candidate.company);
            Assert.True(clue.Data.EntityData.OutgoingEdges.Count > 0);
            var attendedEducationSchool =
                clue.Data.EntityData.OutgoingEdges.FirstOrDefault(edge =>
                    edge.EdgeType.Equals(EntityEdgeType.Attended));
            Assert.NotNull(attendedEducationSchool);
            Assert.Equal(EntityEdgeType.Attended, attendedEducationSchool.EdgeType);
        }
    }
}
