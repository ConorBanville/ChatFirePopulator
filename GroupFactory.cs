namespace ChatFirePoupulator
{
    internal class GroupFactory : IFactory
    {
        public static List<User> Users { get; set; } = new List<User>();

        public IDocument GetDocument()
        {
            Group group = new Group();    // Create a new Group
            group.AddMember(Information.ConorUid);      // Add Conor's Uid and Name to the group members
            string[] owner = GetRandomOwner(Users); // Get the Uid and Display name of a random User
            AddOwnerToGroup(group, owner[0], owner[1]); // Add the Owners Uid and Name to the group
            AddGroupMembers(group, Users);  // Add a random number of users to the group
            AddMessages(group); // Add some random messages to the group
            return group;   // Return the created group
        }

        public static void AddMessages(Group group)
        {
            Random rnd = new Random();
            RandomDateTime RDT = new RandomDateTime();
            List<string> members = group.GetMembersUids();
            for (int i = 0; i < rnd.Next(3, 15); i++)
            {
                string uid = members[rnd.Next(members.Count)];
                Message msg = new Message(uid, RDT.Next());
                group.AddMessage(msg);
            }
        }

        public static bool MemberExists(Group group, string uid)
        {
            foreach (string user in group.GetMembersUids())
            {
                if (uid == user) return true;
            }
            return false;
        }

        private static void AddOwnerToGroup(Group group, string ownerUid, string ownerName)
        {
            group.SetOwner(ownerUid);
            group.AddMember(ownerUid);
        }

        private static void AddGroupMembers(Group group, List<User> users)
        {           
            Random rnd = new Random();

            for (int i = 0; i < rnd.Next(2, Users.Count); i++)
            {
                int num = rnd.Next(Users.Count);
                while (MemberExists(group, users[num].Uid))
                {
                    num = rnd.Next(Users.Count);
                }
                group.AddMember(users[num].Uid);
            }
        }

        private static string[] GetRandomOwner(List<User> users)
        {
            Random rnd = new Random();
            int num = rnd.Next(users.Count);
            return new string[] { users[num].Uid, users[num].DisplayName };
        }
    }
}