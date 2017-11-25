using System;
using System.Collections.Generic;
using System.Text;

using Amazon.DynamoDBv2.DocumentModel;

namespace GetRandomTIL
{
    public class Data
    {
        public data CastToData(Document doc)
        {
            data data = new data
            {
                id = doc["id"],
                score = Convert.ToInt32(doc["score"]),
                title = doc["title"],
                url = doc["url"],
                subreddit = doc["subreddit"],
                thumbnail = doc["thumbnail"],
                subreddit_id = doc["subreddit_id"],
                gilded = Convert.ToInt32(doc["gilded"]),
                name = doc["name"],
                permalink = doc["permalink"],
                link = doc["link"],
                author = doc["author"],
                ups = Convert.ToInt32(doc["ups"]),
                downs = Convert.ToInt32(doc["downs"]),
                num_comments = Convert.ToInt32(doc["num_comments"]),
                last_updated = doc["last_updated"]
            };
            return data;
        }
        public class data
        {
            public string title { get; set; }
            public string url { get; set; }
            public int score { get; set; }
            public string subreddit { get; set; }
            public string id { get; set; }
            public string thumbnail { get; set; }
            public string subreddit_id { get; set; }
            public string name { get; set; }
            public string permalink { get; set; }
            public string link { get; set; }
            public string author { get; set; }
            public string last_updated { get; set; }
            public int ups { get; set; }
            public int downs { get; set; }
            public int gilded { get; set; }
            public int num_comments { get; set; }
        }
    }
}
