using Raven.Client;
using Raven.Client.Document;

namespace RavenTodo2
{
    public static class Database
    {
        private const string Url = "http://localhost:23456";
        private const string DatabaseName = "RavenTodo2";

        private static DocumentStore _documentStore;

        static Database()
        {
            // Connect to RavenDB, there should only be one instance of Documentstore.
            if (_documentStore == null)
                Initialize();
        }

        private static void Initialize()
        {
            _documentStore = (DocumentStore)new DocumentStore
            {
                Url = Url,
                DefaultDatabase = DatabaseName
            }.Initialize();
        }

        public static IDocumentSession OpenSession()
        {
            if (_documentStore == null)
                Initialize();

            return _documentStore.OpenSession(DatabaseName);
        }
    }
}