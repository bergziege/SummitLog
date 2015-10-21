using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4jClient;
using SummitLog.Services.Model;

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
                GraphClient.Cypher.Match("(country:Country)")
                    .Return(country => country.As<Country>())
                    .Results.ToList();
        }

        /// <summary>
        ///     Erstellt ein neues Land
        /// </summary>
        /// <param name="country"></param>
        public void Create(Country country)
        {
            GraphClient.Cypher.Create("(n:Country {country})").WithParam("country", country).ExecuteWithoutResults();
        }
    }
}