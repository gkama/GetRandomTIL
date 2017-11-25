using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

using Newtonsoft.Json;

namespace GetRandomTIL
{
    public class Get
    {
        public string table { get; set; }
        public List<string> ids = new List<string>();
        public ILambdaContext context { get; set; }

        //Constructor
        public Get(string table, ILambdaContext context)
        {
            this.table = table;
            this.context = context;
            this.ids = new List<string>();
        }

        //Scan the table
        public async Task<Data.data> Child()
        {

            var client = new AmazonDynamoDBClient();
            Table reddit_til_table = Table.LoadTable(client, table);

            //Scan
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("id", ScanOperator.IsNotNull);
            ScanOperationConfig scanConfig = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.SpecificAttributes,
                AttributesToGet = new List<string> { "id" }
            };

            //Search
            Search tableSearch = reddit_til_table.Scan(scanConfig);

            //Loop of all documents - al items
            List<Document> all_TILs = new List<Document>();
            do
            {
                all_TILs = await tableSearch.GetNextSetAsync();
                foreach (var document in all_TILs)
                    ids.Add(document["id"].ToString());
            } while (!tableSearch.IsDone);

            //Return
            Document doc = await getChild(client, reddit_til_table);
            return new Data().CastToData(doc);
        }

        //Get random child
        private async Task<Document> getChild(AmazonDynamoDBClient client, Table reddit_til_table)
        {
            string randomID = this.ids[getRandomInt()];

            //Get the child via get async
            Document child = await reddit_til_table.GetItemAsync(randomID);

            //Log
            context.Logger.Log(JsonConvert.SerializeObject(child, Formatting.Indented));

            //Return the child
            return child;
        }

        //Random number
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        private int getRandomInt()
        {
            lock (syncLock)
                return random.Next(0, this.ids.Count + 1);
        }
    }
}
