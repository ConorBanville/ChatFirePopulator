using Google.Cloud.Firestore;

namespace ChatFirePoupulator
{
    public sealed class FirebaseHandler
    {
        private static FirebaseHandler? Instance = null;
        private FirestoreDb DB { get; }

        private FirebaseHandler()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", Information.KEY_PATH);

            DB = FirestoreDb.Create("chatapp-799d9");
            Console.WriteLine("> Connection made successfully.");
        }

        public static FirebaseHandler GetSingletonInstance()
        {
            if (Instance == null)
            {
                Instance = new FirebaseHandler();
            }
            return Instance;
        }

        public async void AddDocumentWithAutoID(IDocument document, string collectionName)
        {
            CollectionReference collection = DB.Collection(collectionName);
            var res = await collection.AddAsync(document.ToDocument());
            document.Uid = res.Id;
            UpdateDocumentWithCollectionID(document, collectionName);
        }

        public async void UpdateDocumentWithCollectionID(IDocument document, string collectionName)
        {
            CollectionReference coll = DB.Collection(collectionName);
            await coll.Document(document.Uid).SetAsync(document.ToDocument(), SetOptions.MergeAll);
        }
    }
}