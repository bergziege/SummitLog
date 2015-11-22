using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4jClient;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence.Extensions;

namespace SummitLog.Services.Persistence.Impl
{
    /// <summary>
    ///     DAO für Länder
    /// </summary>
    public class CountryDao : BaseDao, ICountryDao
    {
        /// <summary>
        ///     Erstellt eine neue Instanz des DAOs
        /// </summary>
        /// <param name="graphClient"></param>
        public CountryDao(GraphClient graphClient) : base(graphClient)
        {
        }

        /// <summary>
        ///     Liefert alle Länder
        /// </summary>
        /// <returns></returns>
        public IList<Country> GetAll()
        {
            return
                GraphClient.Cypher.Match("".Country("c"))
                    .Return(c => c.As<Country>())
                    .Results.ToList();
        }

        /// <summary>
        ///     Erstellt ein neues Land
        /// </summary>
        /// <param name="country"></param>
        public Country Create(Country country)
        {
            return GraphClient.Cypher.Create("".CountryWithParam()).WithParam("country", country).Return(c => c.As<Country>()).Results.First();
        }
    }
}