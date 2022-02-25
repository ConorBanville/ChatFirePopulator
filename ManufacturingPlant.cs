namespace ChatFirePoupulator
{
    internal class ManufacturingPlant
    {
        private static UserFactory UserFactory = new UserFactory();
        private static GroupFactory GroupFactory = new GroupFactory();

        public User GetNewUser()
        {
            // Get a new User from the factory
            return (User)UserFactory.GetDocument();
        }

        public Group GetNewGroup()
        {          
            // Get a new Group from the factory
            return (Group)GroupFactory.GetDocument();
        }

        public void AddCreatedUsersToGroupFactory()
        {
            // Set the active Users in the group factory
            GroupFactory.Users = UserFactory.GetActiveUsers();
        }
    }
}