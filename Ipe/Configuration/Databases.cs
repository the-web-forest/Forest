using Ipe.Domain.Models;
using MongoDB.Driver;

namespace Ipe.Configuration
{
	public class Databases
    {
        public static void Configure(WebApplicationBuilder builder)
        {
			var Database = new MongoClient(builder.Configuration["Database:ConnectionString"]).GetDatabase(builder.Configuration["Database:Name"]);
			ConfigureUserCollection(Database);
			ConfigureMailVerificationCollection(Database);
			ConfigurePasswordResetCollection(Database);
			ConfigureTreeCollection(Database);
			ConfigureOrderCollection(Database);
			ConfigurePlantCollection(Database);
			builder.Services.AddSingleton(x => Database);
		}

		private static void ConfigureUserCollection(IMongoDatabase Database)
        {
			var UserCollection = Database.GetCollection<User>(typeof(User).Name);
			var indexOptions = new CreateIndexOptions();
			var indexKeys = Builders<User>.IndexKeys.Ascending(x => x.CreatedAt);
			var indexModel = new CreateIndexModel<User>(indexKeys, indexOptions);
			UserCollection.Indexes.CreateOne(indexModel);
		}

		private static void ConfigureMailVerificationCollection(IMongoDatabase Database)
		{
			var MailVerificationCollection = Database.GetCollection<MailVerification>(typeof(MailVerification).Name);
			var indexOptions = new CreateIndexOptions();
			var indexKeys = Builders<MailVerification>.IndexKeys.Ascending(x => x.CreatedAt);
			var indexModel = new CreateIndexModel<MailVerification>(indexKeys, indexOptions);
			MailVerificationCollection.Indexes.CreateOne(indexModel);
		}

		private static void ConfigurePasswordResetCollection(IMongoDatabase Database)
		{
			var PasswordResetCollection = Database.GetCollection<PasswordReset>(typeof(PasswordReset).Name);
			var indexOptions = new CreateIndexOptions();
			var indexKeys = Builders<PasswordReset>.IndexKeys.Ascending(x => x.CreatedAt);
			var indexModel = new CreateIndexModel<PasswordReset>(indexKeys, indexOptions);
			PasswordResetCollection.Indexes.CreateOne(indexModel);
		}

		private static void ConfigureTreeCollection(IMongoDatabase Database)
		{
			var treeCollection = Database.GetCollection<Tree>(typeof(Tree).Name);
			var indexOptions = new CreateIndexOptions();
			var indexKeys = Builders<Tree>.IndexKeys.Ascending(x => x.CreatedAt);
			var indexModel = new CreateIndexModel<Tree>(indexKeys, indexOptions);
			treeCollection.Indexes.CreateOne(indexModel);
		}

		private static void ConfigureOrderCollection(IMongoDatabase Database)
		{
			var orderCollection = Database.GetCollection<Order>(typeof(Order).Name);
			var indexOptions = new CreateIndexOptions();
			var indexKeys = Builders<Order>.IndexKeys.Ascending(x => x.CreatedAt);
			var indexModel = new CreateIndexModel<Order>(indexKeys, indexOptions);
			orderCollection.Indexes.CreateOne(indexModel);
		}

        private static void ConfigurePlantCollection(IMongoDatabase Database)
        {
            var PasswordResetCollection = Database.GetCollection<Plant>(typeof(Plant).Name);
            var indexOptions = new CreateIndexOptions();
            var indexKeys = Builders<Plant>.IndexKeys.Ascending(x => x.CreatedAt);
            var indexModel = new CreateIndexModel<Plant>(indexKeys, indexOptions);
            PasswordResetCollection.Indexes.CreateOne(indexModel);
        }
    }
}

