using System;
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
    public class LogEntryDao:BaseDao, ILogEntryDao
    {
        /// <summary>
        /// Erstellt eine neue Instanz des DAOs
        /// </summary>
        /// <param name="graphClient"></param>
        public LogEntryDao(GraphClient graphClient): base(graphClient)
        {
        }

        /// <summary>
        ///     Liefert alle Logeinträge einer Variation
        /// </summary>
        /// <returns></returns>
        public IList<LogEntry> GetAllIn(Variation variation)
        {
            return GraphClient.Cypher.Match("(v:Variation)-[:HAS]->(le:LogEntry)")
                .Where((Variation v) => v.Id == variation.Id).Return(le => le.As<LogEntry>()).Results.ToList();
        }

        /// <summary>
        ///     Erstellt einen neuen Logeintrag einer Variation
        /// </summary>
        public void Create(Variation variation, LogEntry logEntry)
        {
            ICypherFluentQuery query = GraphClient.Cypher
                .Match("(v:Variation)")
                .Where((Variation v) => v.Id == variation.Id)
                .Create("v-[:HAS]->(logEntry:LogEntry {logEntry})")
                .WithParam("logEntry", logEntry);

            query.ExecuteWithoutResults();
        }

        /// <summary>
        /// Löscht den übergebenen Logeintrag
        /// </summary>
        /// <param name="logEntry"></param>
        public void Delete(LogEntry logEntry)
        {
            if (logEntry == null) throw new ArgumentNullException(nameof(logEntry));

            GraphClient.Cypher
                .Match("(l:LogEntry)")
                .Where((LogEntry l) => l.Id == logEntry.Id).Delete("l").ExecuteWithoutResults();
        }
    }
}