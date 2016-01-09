using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using SummitLog.Services.Exceptions;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence.Extensions;

namespace SummitLog.Services.Persistence.Impl
{
    /// <summary>
    ///     DAO für Gegendem in einem Land
    /// </summary>
    public class SummitDao :BaseDao, ISummitDao
    {
        /// <summary>
        ///     Erstellt eine neue Instanz des DAOs
        /// </summary>
        /// <param name="graphClient"></param>
        public SummitDao(GraphClient graphClient):base(graphClient)
        {
        }

        /// <summary>
        ///     Liefert alle Gipfel einer Gipfelgruppe
        /// </summary>
        /// <returns></returns>
        public IList<Summit> GetAllIn(SummitGroup summitGroup)
        {
            return GraphClient.Cypher.Match("".SummitGroup("sg").Has().Summit("s"))
                .Where((SummitGroup sg) => sg.Id == summitGroup.Id).Return(s => s.As<Summit>()).Results.ToList();
        }

        /// <summary>
        ///     Erstellt einen neuen Gipfel in einer Gipfelgruppe
        /// </summary>
        public Summit Create(SummitGroup summitGroup, Summit summit)
        {
            var query = GraphClient.Cypher
                .Match("".SummitGroup("sg"))
                .Where((SummitGroup sg) => sg.Id == summitGroup.Id)
                .Create("sg".Has().SummitWithParam())
                .WithParam("summit", summit);

            return query.Return(s=>s.As<Summit>()).Results.First();
        }

        /// <summary>
        ///     Liefert ob der Gipfel verwendet wird
        /// </summary>
        /// <param name="summit"></param>
        /// <returns></returns>
        public bool IsInUse(Summit summit)
        {
            return GraphClient.Cypher.Match("".Summit("s").AnyOutboundRelationAs("usage").Route()).Where((Summit s)=>s.Id == summit.Id).Return(usage => usage.Count()).Results.First() > 0;
        }

        /// <summary>
        ///     Löscht einen Gipfel wenn diese rnicht mehr verwendet wird
        /// </summary>
        /// <param name="summit"></param>
        public void Delete(Summit summit)
        {
            if (IsInUse(summit))
            {
                throw new NodeInUseException();
            }
            GraphClient.Cypher.Match("".Summit("s").AnyInboundRelationsAs("groupAssignment").SummitGroup()).Where((Summit s)=>s.Id == summit.Id).Delete("s, groupAssignment").ExecuteWithoutResults();
        }

        /// <summary>
        ///     Speichert denm Gipfel
        /// </summary>
        /// <param name="summit"></param>
        public void Save(Summit summit)
        {
            GraphClient.Cypher.Match("".Summit("s"))
                .Where((Summit s)=>s.Id == summit.Id)
                .Set("s = {summit}").WithParam("summit", summit)
                .ExecuteWithoutResults();
        }
    }
}