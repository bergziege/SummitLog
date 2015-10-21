using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Cypher;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence.Impl
{
    /// <summary>
    /// DAO für Logeinträge an Variationen
    /// </summary>
    public class LogEntryDao: ILogEntryDao
    {
        private readonly GraphClient _graphClient;

        /// <summary>
        /// Erstellt eine neue Instanz des DAOs
        /// </summary>
        /// <param name="graphClient"></param>
        public LogEntryDao(GraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        /// <summary>
        ///     Liefert alle Logeinträge einer Variation
        /// </summary>
        /// <returns></returns>
        public IList<LogEntry> GetAllIn(Variation variation)
        {
            return _graphClient.Cypher.Match("(v:Variation)-[:HAS]->(le:LogEntry)")
                .Where((Variation v) => v.Id == variation.Id).Return(le => le.As<LogEntry>()).Results.ToList();
        }

        /// <summary>
        ///     Erstellt einen neuen Logeintrag einer Variation
        /// </summary>
        public void Create(Variation variation, LogEntry logEntry)
        {
            ICypherFluentQuery query = _graphClient.Cypher
                .Match("(v:Variation)")
                .Where((Variation v) => v.Id == variation.Id)
                .Create("v-[:HAS]->(logEntry:LogEntry {logEntry})")
                .WithParam("logEntry", logEntry);

            query.ExecuteWithoutResults();
        }
    }
}