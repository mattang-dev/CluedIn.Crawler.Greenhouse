using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.Greenhouse.Vocabularies
{
    public class OfferVocabulary : SimpleVocabulary
    {
        public OfferVocabulary()
        {
            VocabularyName = "Greenhouse Offer";
            KeyPrefix = "greenhouse.Offer";
            KeySeparator = ".";
            Grouping = "/Offer";
            AddGroup("Greenhouse Details", group =>
            {
                Version = group.Add(new VocabularyKey(nameof(Version), VocabularyKeyDataType.Integer,
                    VocabularyKeyVisibility.Visible));
                Status = group.Add(new VocabularyKey(nameof(Status), VocabularyKeyDataType.Text,
                    VocabularyKeyVisibility.Visible));
                JobId = group.Add(new VocabularyKey(nameof(JobId), VocabularyKeyDataType.Integer,
                    VocabularyKeyVisibility.Visible));
                Id = group.Add(new VocabularyKey(nameof(Id), VocabularyKeyDataType.Integer,
                    VocabularyKeyVisibility.Visible));
                CandidateId = group.Add(new VocabularyKey(nameof(CandidateId), VocabularyKeyDataType.Integer,
                    VocabularyKeyVisibility.Visible));
                ApplicationId = group.Add(new VocabularyKey(nameof(ApplicationId), VocabularyKeyDataType.Integer,
                    VocabularyKeyVisibility.Visible));
                SentAt = group.Add(new VocabularyKey(nameof(SentAt), VocabularyKeyDataType.Text,
                    VocabularyKeyVisibility.Visible));
                StartsAt = group.Add(new VocabularyKey(nameof(StartsAt), VocabularyKeyDataType.Text,
                    VocabularyKeyVisibility.Visible));
            });
        }

        public VocabularyKey Version { get; internal set; }
        public VocabularyKey Status { get; internal set; }
        public VocabularyKey JobId { get; internal set; }
        public VocabularyKey Id { get; internal set; }
        public VocabularyKey CandidateId { get; internal set; }
        public VocabularyKey ApplicationId { get; internal set; }
        public VocabularyKey SentAt { get; internal set; }
        public VocabularyKey StartsAt { get; internal set; }
    }
}
