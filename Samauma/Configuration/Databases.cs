using MongoDB.Driver;

namespace Samauma.Configuration
{
	public class Databases
    {
        public static void Configure(WebApplicationBuilder builder)
        {
			var Database = new MongoClient(builder.Configuration["Database:ConnectionString"]).GetDatabase(builder.Configuration["Database:Name"]);
			builder.Services.AddSingleton(x => Database);
		}
	}
}

