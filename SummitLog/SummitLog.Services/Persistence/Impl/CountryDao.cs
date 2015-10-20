using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence.Impl
{
    /// <summary>
    ///     DAO für Länder
    /// </summary>
    public class CountryDao : ICountryDao
    {
        private readonly GraphClient _graphClient;

        /// <summary>
        ///     Erstellt eine neue Instanz des DAOs
        /// </summary>
        /// <param name="graphClient"></param>
        /// <param name="nodeLabels"></param>
        public CountryDao(GraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        /// <summary>
        ///     Liefert alle Länder
        /// </summary>
        /// <returns></returns>
        public IList<Country> GetAll()
        {
            return
                _graphClient.Cypher.Match("(country:Country)")
                    .Return(country => country.As<Country>())
                    .Results.ToList();
        }

        /// <summary>
        ///     Erstellt ein neues Land
        /// </summary>
        /// <param name="country"></param>
        public void Create(Country country)
        {
            _graphClient.Cypher.Create("(n:Country {Name:'" + country.Name +"'})").ExecuteWithoutResults();
        }
    }
}