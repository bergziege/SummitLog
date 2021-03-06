﻿using System;
using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Cypher;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence.Extensions;

namespace SummitLog.Services.Persistence.Impl
{
    /// <summary>
    /// DAO für Logeinträge an Variationen
    /// </summary>
    public class LogEntryDao:BaseDao, ILogEntryDao
    {
        
        /// <summary>
        ///     Liefert alle Logeinträge einer Variation
        /// </summary>
        /// <returns></returns>
        public IList<LogEntry> GetAllIn(Variation variation)
        {
            return GraphClient.Cypher.Match("".Variation("v").Has().LogEntry("le"))
                .Where((Variation v) => v.Id == variation.Id).Return(le => le.As<LogEntry>()).Results.ToList();
        }

        /// <summary>
        ///     Erstellt einen neuen Logeintrag einer Variation
        /// </summary>
        public LogEntry Create(Variation variation, LogEntry logEntry)
        {
            ICypherFluentQuery query = GraphClient.Cypher
                .Match("".Variation("v"))
                .Where((Variation v) => v.Id == variation.Id)
                .Create("".Node("v").Has().LogEntryWithParam())
                .WithParam("logEntry", logEntry);

            return query.Return(le=>le.As<LogEntry>()).Results.First();
        }

        /// <summary>
        /// Löscht den übergebenen Logeintrag
        /// </summary>
        /// <param name="logEntry"></param>
        public void Delete(LogEntry logEntry)
        {
            if (logEntry == null) throw new ArgumentNullException(nameof(logEntry));

            GraphClient.Cypher
                .Match("".LogEntry("le").AnyInboundRelationsAs("r").Node(""))
                .Where((LogEntry le) => le.Id == logEntry.Id).Delete("le, r").ExecuteWithoutResults();
        }

        /// <summary>
        ///     Speichert den Logeintrag
        /// </summary>
        /// <param name="logEntry"></param>
        public void Save(LogEntry logEntry)
        {
            if (logEntry == null) throw new ArgumentNullException(nameof(logEntry));
            GraphClient.Cypher.Match("".LogEntry("l"))
                .Where((LogEntry l) => l.Id == logEntry.Id)
                .Set("l.Memo={Memo}, l.DateTime={Date}")
                .WithParams(new Dictionary<string, object> {{"Memo", logEntry.Memo}, {"Date", logEntry.DateTime}})
                .ExecuteWithoutResults();
        }
    }
}