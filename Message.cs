namespace ChatFirePoupulator
{
    public class Message
    {
        public string Uid {get; set;}
        public string Timestamp {get; set;}
        public string Content {get; set;}
        private Random rnd = new Random();

        public Message(string uid, string timestamp)
        {
            Uid = uid;
            Timestamp = timestamp;
            Content = "";
            string[] cont = Faker.Lorem.Sentences(rnd.Next(1,5)).ToArray();
            foreach(String s in cont) {
                Content += s;
            }
        }

        public Dictionary<string, object> ToDictionary() 
        {
            return new Dictionary<string, object>{
                {"uid", Uid},
                {"timestamp", Timestamp},
                {"content", Content}
            };
        }
    }
}