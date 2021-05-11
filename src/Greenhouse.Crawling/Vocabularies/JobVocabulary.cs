using CluedIn.Core.Data.Vocabularies;
using Mattang.Common.Vocabulary;


namespace CluedIn.Crawling.Greenhouse.Vocabularies
{
    public class JobVocabulary : SimpleVocabulary
    {
        public JobVocabulary()
        {
            VocabularyName = "Greenhouse Job"; 
            KeyPrefix = "greenhouse.Job"; 
            KeySeparator = ".";
            Grouping = "/Job";

            AddGroup("Greenhouse Details", group =>
            {
                RequisitionId = group.Add(new VocabularyKey("RequisitionId", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Id = group.Add(new VocabularyKey("Id", VocabularyKeyDataType.Integer, VocabularyKeyVisibility.Visible));
                Name = group.Add(new VocabularyKey("Name", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Notes = group.Add(new VocabularyKey("Notes", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));

                Confidential = group.Add(new VocabularyKey("Confidential", VocabularyKeyDataType.Boolean,
                    VocabularyKeyVisibility.Visible));
                Status = group.Add(new VocabularyKey("Status", VocabularyKeyDataType.Boolean,
                    VocabularyKeyVisibility.Visible));

            });


            AddMapping(Id, CommonVocabularies.Employee.Id);




        }

        public VocabularyKey RequisitionId { get; internal set; }
        public VocabularyKey Id { get; internal set; }
        public VocabularyKey Name { get; internal set; }
        public VocabularyKey Notes { get; internal set; }
        public VocabularyKey Confidential { get; internal set; }
        public VocabularyKey Status { get; internal set; }

    }
}
