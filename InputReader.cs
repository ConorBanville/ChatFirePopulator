using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatFirePoupulator
{
    internal class InputReader
    {
        private FirebaseHandler Firebase;
        private ManufacturingPlant MP;
        private List<User> Users;
        private List<Group> Groups;
        private int ExceptionCounter;

        public InputReader()
        {
            Firebase = FirebaseHandler.GetSingletonInstance();
            MP = new ManufacturingPlant();
            Users = new List<User>();
            Groups = new List<Group>();
            ExceptionCounter = 0;
        }

        public void Start()
        {
            Console.Title = "ChatFirePopulator";
            Console.Clear();
            Console.WriteLine(Information.Welcome);

            while (true)
            {
                try
                {
                    if (Run(Console.ReadLine()) == "k") return;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private string Run(string? input)
        {
            if(input == null)
            {
                throw NullInputException();
            }

            string Token;
            string Argument;

            if (input.Contains(" "))
            {
                Token = input.Substring(0, input.IndexOf(' '));
                Argument = input.Substring(input.IndexOf(' ') + 1, input.Length - (input.IndexOf(' ') + 1));
                return Parse(Token, Argument);
            }
            else
            {
                Token = input;
                return Parse(Token);
            }
        }

        private string Parse(string token)
        {
            switch (token.ToLower())
            {
                // Display the help screen
                case "h":
                    Console.WriteLine(Information.Help);
                    break;

                case "help":
                    Console.WriteLine(Information.Help);
                    break;

                // Close the program
                case "k":
                    return "k";

                case "kill":
                    return "k";

                // Clear the console
                case "c":
                    Console.Clear();
                    Console.WriteLine(Information.Welcome);
                    break;

                case "cls":
                    Console.Clear();
                    Console.WriteLine(Information.Welcome);
                    break;

                // Handle invalid usage of g
                case "g":
                    Parse(token, "");
                    break;
                // Handle invalid usage of generate
                case "generate":
                    Parse(token, "");
                    break;

                // Handle invalid usage of d
                case "d":
                    Parse(token, "");
                    break;
                // Handle invalid usage of display
                case "display":
                    Parse(token, "");
                    break;

                // Handle invalid usage of p
                case "p":
                    Parse(token, "");
                    break;
                // Handle invalid usage of publish
                case "publish":
                    Parse(token, "");
                    break;

                // Handle empty input
                case "":
                    break;

                // Handle invalid user inputs
                default:
                    throw InvalidExpression(token);
            }
            return "";
        }

        private string Parse(string token, string argument)
        {
            switch (token.ToLower())
            {
                // Target the users collection
                case "users":
                    // GENERATE NEW USERS //                                // check if 'users g[enerate] (x)' is passed
                    if (
                        // CONDITIONS //
                        argument.ToLower().Contains('g') &&                 // IF Generate token present AND
                        argument.Contains(" ") &&                           // the argument contains a " " AND
                        (argument.Length - argument.IndexOf(" ")) > 1       // There is at least one character between the " " and the end of the argument
                       )
                    {
                        // CODE BLOCK //
                        // generate X number of user documents and add them to the firestore
                        // where x == an integer passed by the user
                        string[] x = argument.Split(' ');
                        int _x = Convert.ToInt32(x[1].Replace("(", "").Replace(")", ""));

                        for (int i = 0; i < _x; i++)
                        {
                            Users.Add(MP.GetNewUser());
                        }
                    }
                    else
                    // DISPLAY THE STORED USERS //                      // check if 'users d[isplay]' is passed
                    if (
                        // CONDITIONS //
                        argument.ToLower().Contains("d")                // Full display token passed
                      )
                    {
                        // CODE BLOCK //
                        // diplay token present, display the applications currently stored user documents
                        foreach (User user in Users)
                        {
                            Console.WriteLine(user.Serialize());
                        }
                    }
                    else
                    // PUBLISH THE STORED USERS //                      // Check if 'users p[ublish]' is passed
                    if (
                        // CONDITIONS
                        argument.ToLower().Contains("p") &&             // IF publish token passed AND
                        !argument.ToLower().Contains("d")               // d is not contained in the token
                      )
                    {
                        // CODE BLOCK //
                        if (Users.Count == 0) throw NoUsersStoredException();
                        for (int i = 0; i < Users.Count; i++)
                        {
                            if (Users[i].Uid == "")
                            {
                                Firebase.AddDocumentWithAutoID(Users[i], Information.UsersColl);
                                UserFactory.Published = true;
                            }
                        }
                        MP.AddCreatedUsersToGroupFactory();
                    }
                    else throw InvalidExpression(token + " " + argument);
                    break;

                // Target the groups collection
                case "groups":
                    // check if 'groups g[enerate] (x)' is passed
                    if (
                        // CONDITIONS //
                        argument.ToLower().Contains('g') &&                 // IF Generate token present AND
                        argument.Contains(" ") &&                           // the argument contains a " " AND
                        (argument.Length - argument.IndexOf(" ")) > 1       // There is at least one character between the " " and the end of the argument
                       )
                    {
                        // CODE BLOCK //
                        if (Users.Count < 2) throw NotEnoughUsersStoredException();
                        if (!UserFactory.Published) throw NoUsersPublished();

                        string[] x = argument.Split(' ');
                        int _x = Convert.ToInt32(x[1].Replace("(", "").Replace(")", ""));

                        for (int i = 0; i < _x; i++)
                        {
                            Group group = MP.GetNewGroup();  // Get an new group from the Factory
                            if (group == null) throw NullGroupException();  // If the group comes back null then we don't add it
                            else Groups.Add(group); // If it isn't null then we do add it
                        }
                    }
                    else
                    // check if 'groups d[isplay]' is passed
                    if (
                        // CONDITIONS //
                        argument.ToLower().Contains("d")                    // Display token passed
                      )
                    {
                        // CODE BLOCK //
                        foreach (Group group in Groups)
                        {
                            Console.WriteLine(group.Serialize());
                        }
                    }
                    else
                    // Check if 'groups p[ublish]' is passed
                    if (
                        // CONDITIONS
                        argument.ToLower().Contains("p") &&             // IF publish token passed AND
                        !argument.ToLower().Contains("d")               // d is not contained in the token
                      )
                    {
                        // CODE BLOCK //
                        if (Groups.Count == 0) throw NoGroupsStoredException();

                        for (int i = 0; i < Groups.Count; i++)
                        {
                            if (Groups[i].Uid == "")
                            {
                                Firebase.AddDocumentWithAutoID(Groups[i], Information.GroupsColl);
                            }
                        }
                    }
                    else throw InvalidExpression(token + " " + argument);
                    break;

                // Handle invalid usage of g
                case "g":
                    throw PrefixException(token);

                // Handle invalid usage of generate
                case "generate":
                    throw PrefixException(token);

                // Handle invalid usage of d
                case "d":
                    throw PrefixException(token);

                // Handle invalid usage of display
                case "display":
                    throw PrefixException(token);

                // Handle invalid usage of p
                case "p":
                    throw PrefixException(token);

                // Handle invalid usage of publish
                case "publish":
                    throw PrefixException(token);

                // Handle all other invalid user inputs
                default:
                    throw InvalidExpression(token + " " + argument);
            }
            return "";
        }

        /*  Throws a new Exception object, this function is called when the user does not prefix
            relevent input with either users or groups */

        private Exception PrefixException(string variable)
        {
            ExceptionCounter++;
            if (ExceptionCounter % 5 == 0)
            {
                return new Exception("...Hmmm, seems like you're having some trouble, it might help to look at the list of commands again?");
            }
            return new Exception($"!Whoops, looks like there was an error in your input: \"{variable}\" must be prefixed with either users or groups");
        }

        /*  Throws a new Exception object, this function is called when the user enters input which cannot
            be handled by the system */

        private Exception InvalidExpression(string variable)
        {
            ExceptionCounter++;
            if (ExceptionCounter % 5 == 0)
            {
                return new Exception("...Hmmm, seems like you're having some trouble, it might help to look at the list of commands again?");
            }
            return new Exception($"!Sorry, I don't know what to do with that input: \"{variable}\"");
        }

        private Exception NoUsersStoredException()
        {
            return new Exception("Hmm... you might want to try adding some users before you try and publish!");
        }

        private Exception NotEnoughUsersStoredException()
        {
            return new Exception("!Oh dear... It does'nt look like you have added enough users yet, you will need at least 2 to create\n" +
                "a group, but I recommend adding more than that");
        }

        private Exception NoGroupsStoredException()
        {
            return new Exception("!Uh oh... Looks like you haven't generated any groups yet");
        }

        private Exception NoUsersPublished()
        {
            return new Exception("!No users have been published yet, you will need to publish some users first, " +
                "\nso that we can retrieve their Uid's from Firestore and set the group owner field in the generated groups");
        }

        private Exception NullGroupException()
        {
            return new Exception("!Fiddle sticks... It's seems we tried to add a null group to the firestore, honestly I didn't think this was possible.");
        }

        private Exception NullInputException()
        {
            return new Exception("Uh oh! Looks like that input was null :(");
        }
    }
}