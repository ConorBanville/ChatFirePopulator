using System.Text.Json;

namespace ChatFirePoupulator
{
    public class User : IDocument
    {
        public override string Uid {get; set;}
        public string DisplayName {get; set;}
        public string Email {get; set;}
        public bool EmailVerfied {get; set;}
        public string[] Friends {get; set;}
        public string PhotoUrl {get; set;}

        public User()
        {
            Uid = "";
            DisplayName = "@"+Faker.Name.FullName();
            Email = Faker.Internet.Email();
            EmailVerfied = false;
            Friends = new string[]{};
            PhotoUrl = "default-assets/profile.svg";
        }

        public override string Serialize()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(this, options);
        }

        public override Dictionary<string, object> ToDocument()
        {
            return new Dictionary<string, object>{
                {"displayName", DisplayName},
                {"email", Email},
                {"emailVerified", EmailVerfied},
                {"friends", Friends},
                {"photoUrl", PhotoUrl},
                {"uid", Uid}
            };
        }
    }
}