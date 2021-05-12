using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using PSQL.Data.Domain;
using PSQL.Data.Domain.Models;

//using Npgsql.EntityFrameworkCore.PostgreSQL.ex;


namespace PSQL.Debug.App
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            //await GenerateMessage();
            await TestPSQLFullTextSearch();
        }

        private static async Task TestPSQLFullTextSearch()
        {
            //// the following text were copied from the db
            //// they were cut arbitrary
            //var searchTexts = new List<(string, string)>
            //{
            //    ("Recusand non iusto velit.", /* // id: 356, original text: */"Recusandae non iusto accusantium velit."),
            //    ("velit optio sed  doloremque.", /*// id: 235356, original text: */"Voluptas velit optio sed magnam doloremque."),
            //    ("Tempore quasi in nesciunt minima.", /*// id: 335356, original text: */"Tempore quasi in perspiciatis nesciunt deserunt cumque minima."),
            //    ("minima id omnis.", /*// id: 835356, original text: */"Temporibus minima adipisci id omnis."),
            //    ("Dolores necessitatibus aut ut.", /*// id: 1035356, original text: */"Dolores necessitatibus aut ut et ipsa illum."),
            //    ("Praesentium distinctio in laudantium esse magni veritatis", /*// id: 1535356, original text: */"Praesentium distinctio in laudantium esse magni veritatis harum recusandae."),
            //    ("magnam aut eum in dolor rerum eum.", /*// id: 2535356, original text: */"Odio assumenda vel et. Quas quo qui error. Modi magnam aut eum in dolor rerum eum ratione."),
            //    ("Et voluptatem dolor. quibusdam praesentium minus dicta.", /*// id: 2935356, original text: */"Saepe omnis autem sed soluta. Et voluptatem cum pariatur ad delectus vitae dolor. Aut explicabo et molestiae. Aliquid illo totam quibusdam praesentium minus dicta."),
            //    ("Eum error ut libero voluptatem et tempora omnis. Debitis quia sit et ducimus quo ipsam sunt. Harum enim deserunt quos velit voluptas debitis dolorum.", /*// 3235356, original text: */"Eum error ut libero voluptatem et tempora omnis. Debitis quia sit et ducimus quo ipsam sunt. Harum enim deserunt quos velit voluptas debitis dolorum."),
            //    ("Illum quae et sed animi cupiditate est.", /*// id: 3735356, original text: */"Illum quae et sed id. Ducimus adipisci animi cupiditate. Eum temporibus nulla beatae voluptatem voluptas harum. Aut hic debitis praesentium maxime aliquam quia. Quia qui ea natus est."),
            //};
            //var searchTexts = new List<(string, string)>
            //{
            //    //("Upgradable high-level policy. Business-focused", /* // id: 220000006, original text: */"Upgradable high-level policy. Business-focused optimizing hub. Monitored optimizing capacity. Seamless directional definition. Assimilated motivating neural-net. Public-key intangible infrastructure. Expanded well-modulated challenge."),
            //    ("Upgradable high-level policy", /* // id: 220000006, original text: */"Upgradable high-level policy. Business-focused optimizing hub. Monitored optimizing capacity. Seamless directional definition. Assimilated motivating neural-net. Public-key intangible infrastructure. Expanded well-modulated challenge."),
            //    ("Diverse intermediate hub.", /*// id: 220000038, original text: */"Diverse intermediate hub."),
            //    ("Inverse global frame", /*// id: 220000329, original text: */"Balanced impactful emulation. Inverse global frame. Business-focused contextually-based application."),
            //    ("Up-sized optimizing orchestration.", /*// id: 220000858, original text: */"Total composite focus group. Streamlined dedicated definition. Up-sized optimizing orchestration."),
            //    ("Versatile stable encryption. Function-based 4th generation adapter.", /*// id: 220000989, original text: */"Adaptive attitude-oriented support. Networked scalable support. Open-architected scalable help-desk. Versatile stable encryption. Function-based 4th generation adapter."),
            //    ("Assimilated didactic archive.", /*// id: 1535356, original text: */"Assimilated didactic archive."),
            //    ("Enhanced disintermediate", /*// id: 220001902, original text: */"Enhanced disintermediate functionalities. Seamless contextually-based projection. Intuitive foreground process improvement. Advanced dynamic info-mediaries. Versatile needs-based open architecture. Profound clear-thinking orchestration. Integrated didactic task-force."),
            //    ("Profound fault-tolerant", /*// id: 220002667, original text: */"Cross-platform non-volatile paradigm. Profound fault-tolerant hardware. Proactive fault-tolerant portal."),
            //    ("Customer-focused discrete", /*// 3235356, original text: */"Customer-focused discrete focus group."),
            //    ("Synergistic upward-trending.", /*// id: 220002998 , original text: */"Digitized methodical matrix. Synergistic upward-trending attitude."),
            //};

            var searchTexts = new List<(string, string)>
            {
               ("Virtual radical proj","Digitized empowering adapter. Innovative multimedia firmware. Virtual radical project. Virtual system-worthy knowledge user. Decentralized secondary benchmark. Balanced uniform methodology. Exclusive tertiary capacity."),
               ("extranet multimedia Secured","Customer-focused non-volatile extranet. Fundamental hybrid alliance. Compatible multimedia intranet. Secured solution-oriented synergy. Implemented hybrid adapter. Expanded bifurcated knowledge user. Centralized composite capability."),
               ("layered incremental","Multi-layered incremental Graphic Interface."),
               ("asynchronous info-mediaries. Business","Switchable bi-directional policy. Right-sized asynchronous info-mediaries. Business-focused impactful groupware. Monitored mission-critical hub. Expanded high-level orchestration. Distributed holistic installation."),
               ("hybrid mat","Customizable hybrid matrices.")
            };

            //var searchTexts = new List<(string, string)>
            //{
            //    ("focuse time", "focuse time"),
            //    ("effective work", "effective work"),
            //    ("managed trip", "managed trip"),
            //    ("future data", "future data"),
            //    ("open source", "open source"),
            //    ("hybrid", "hybrid"),
            //    ("project", "project"),
            //    ("right solution", "right solution"),
            //    ("User-centric logistical", "User-centric logistical"),
            //    ("hour functionalities", "hour functionalities"),
            //};

            //var searchTexts = new List<(string, string)>
            //{
            //    ("culpa dolorem ","Quaerat culpa dolorem dolores nisi aut."),
            //    ("last time", "last time"),
            //    ("best company ever", "best company ever"),
            //    ("mountain trip", "mountain trip"),
            //    ("albert einstein", "albert einstein"),
            //    ("wikipedia", "wikipedia"),
            //    ("google chrome issue", "google chrome issue"),
            //    ("cryptocurrency prices", "cryptocurrency prices"),
            //    ("animal world", "animal world"),
            //    ("transactional analysis", "transactional analysis"),
            //    ("programming languages", "programming languages"),
            //};

            foreach (var text in searchTexts)
            {
                var searchTerm = $"%{text.Item1}%";
                var dbContext = new PSQLContext();
                var stopwatch = Stopwatch.StartNew();
                var messages = await dbContext.Messages                   
                    .Where(p => EF.Functions.TrigramsAreStrictWordSimilar(p.Text, searchTerm))
                    .Take(50)
                    .ToListAsync();


                #region search tests

                //var messages = await dbContext.Messages
                //   //.Where(p => p.SearchVector.Matches(text.Item1))
                //   //.Where(f => EF.Functions.FuzzyStringMatchDifference(f.Text, text.Item1) >= 4.5)
                //   //.Where(f => EF.Functions.TrigramsWordSimilarity(f.Text, text.Item1) >= 0.3)
                //   //.OrderByDescending(f => EF.Functions.FuzzyStringMatchDifference(f.Text, text.Item1))

                //   //.Where(p => EF.Functions.ILike(p.Text, searchTerm))

                //   //.Where(p => EF.Functions.TrigramsAreSimilar(p.Text, searchTerm))
                //   //.Where(p => EF.Functions.TrigramsAreWordSimilar(p.Text, searchTerm))

                //   //.Where(p => EF.Functions.TrigramsWordSimilarityDistance(p.Text, searchTerm) >= 5)

                //   //.Where(p => EF.Functions.TrigramsWordSimilarityDistance(p.Text, searchTerm) >= 5)

                //   //.Where(p => EF.Functions.TrigramsWordSimilarity(p.Text, searchTerm) >= 0.2)

                //   .Where(p => EF.Functions.TrigramsAreStrictWordSimilar(p.Text, searchTerm))

                //   //.Where(p => EF.Functions.TrigramsAreNotStrictWordSimilar(p.Text, searchTerm))

                //   //.OrderByDescending(f => EF.Functions.TrigramsAreStrictWordSimilar(f.Text, text.Item1))
                //   .Take(50)
                //   .ToListAsync();


                //var messages = await dbContext.Messages
                //    //.Where(p => p.SearchVector.Matches(text.Item1))
                //    //.Where(p => p.SearchVector.Matches(EF.Functions.PhraseToTsQuery(text.Item1)))
                //    //.Where(p => p.Text.Contains(text.Item1))
                //    //.Where(p => EF.Functions.FuzzyStringMatchLevenshtein(text.Item1, p.Text) <= 1)
                //    //.Where(p => EF.Functions.ILike(p.Text, searchTerm))
                //    // .Where(f => EF.Functions.FuzzyStringMatchDifference(f.Text, text.Item1) >= 4.8)
                //    .Where(f => EF.Functions.TrigramsWordSimilarity(f.Text, "level") >= 0.5)
                //    //.Where(f => EF.Functions.FuzzyStringMatchSoundex(f.Text) == text.Item1)
                //    //.Where(f => EF.Functions.FuzzyStringMatchDifference(f.Text, text.Item1) <= 0.1)
                //    //.OrderByDescending(f => EF.Functions.FuzzyStringMatchDifference(f.Text, text.Item1))
                //    //.OrderBy(f => EF.Functions.FuzzyStringMatchDifference(f.Text, text.Item1))
                //    .Take(5)
                //    .ToListAsync();
                #endregion




                stopwatch.Stop();
                var queryTime = stopwatch.ElapsedMilliseconds;
                Console.WriteLine("Search text: {0}", text.Item1);
                //Console.WriteLine("Original text (this text should be at the top): {0}", text.Item2);
                Console.WriteLine("Query time (milliseconds): {0}", queryTime);
                var n = messages?.Count < 10 ? messages.Count : 10; 
                for (var i = 0; i < n; i++)
                {
                    Console.WriteLine("Result[{0}].Text: {1}", i, messages[i]?.Text);
                }
                Console.WriteLine("");
                dbContext.Dispose();
            }            
        }

        private static async Task GenerateMessage()
        {
            Random r = new Random();
            var messageList = new List<Message>();
            // generate fake data
            for (var i = 0; i < 200001; i++)
            {
                messageList.Add(new Message()
                {
                    // Text = Faker.Lorem.Paragraph()
                    // Text = Faker.Company.CatchPhrase() + ". " + Faker.Company.CatchPhrase() + ". " + Faker.Company.CatchPhrase()
                    Text = GenerateMessageText(r.Next(0, 8))
                });

                //if (i % 1000000 == 0)
                {
                    var dbContext = new PSQLContext();
                    dbContext.Messages.AddRange(messageList);

                    _ = await dbContext.SaveChangesAsync();

                    dbContext.Dispose();
                    messageList = new List<Message>();
                }                
            }
        }

        private static string GenerateMessageText(int n)
        {
            string result = Faker.Company.CatchPhrase() + ".";
            for (int i = 0; i < n - 1; i++)
            {
                result += " " + Faker.Company.CatchPhrase() + ".";
            }

            return result;
        }
    }
}
