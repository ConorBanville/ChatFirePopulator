namespace ChatFirePoupulator
{
    public static class Information
    {

        public static string KEY_PATH = "/home/conor/Dev/ChatFirePopulator/chatapp-key.json";
        public static string Help =
            @"
------------------------------------------------
h[elp]      |   Display a list of commands.
k[ill]      |   Close the program.
c[ls]       |   Clear the screen

users               |   target the 'Users' collection.
groups              |   target the 'groups' collection.

* The following tokens must be prefixed with [users | groups] *
g[enerate] x<int>   |   generate x random documents.
d[isplay]           |   diplay a currrently stored collection.
p[ublish]           |   publish a currrently stored collection to the firestore.
------------------------------------------------";

        public static string Welcome =
            @"
    Welcome to ChatFirePopulator. This is a console application that helps to
    populate ChatApp's Firestore with randomly generated data. For a list of
    commands, try the command h[elp].
    --------------------------------------------------------------------------
    ";

        public static string ConorUid = "Tzc2GPkswMTU9mqHHjLFVCjGZCF3";
        public static string ConorName = "@conormbanville";
        public static string UsersColl = "users";
        public static string GroupsColl = "groups";
    }
}