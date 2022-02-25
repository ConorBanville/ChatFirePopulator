using System.Collections.Generic;
using System.Text.Json;

namespace ChatFirePoupulator
{
    public class Group : IDocument
    {
        public string Name { get; set; }
        public override string Uid { get; set; }
        public string OwnerUid { get; set; }
        public List<string> Members { get; set; }
        public List<Message> Messages { get; set; }

        public Group()
        {
            Name = Faker.Company.Name();
            Uid = "";
            OwnerUid = "";
            Members = new List<string>();
            Messages = new List<Message>();
        }

        public void SetOwner(string uid)
        {
            OwnerUid = uid;
        }

        public void AddMessage(Message msg)
        {
            Messages.Add(msg);
        }

        public void AddMember(string uid)
        {
            Members.Add(uid);
        }

        public List<string> GetMembersUids()
        {
            return Members;
        }

        public override string Serialize()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(this, options);
        }

        public override Dictionary<string, object> ToDocument()
        {
            List<Dictionary<string, object>> messages = new List<Dictionary<string, object>>();
            foreach (Message msg in Messages)
            {
                messages.Add(msg.ToDictionary());
            }

            return new Dictionary<string, object>
            {
                { "name", Name },
                { "uid", Uid },
                { "ownerUid", OwnerUid },
                { "members", Members},
                { "messages", messages}
            };
        }
    }
}