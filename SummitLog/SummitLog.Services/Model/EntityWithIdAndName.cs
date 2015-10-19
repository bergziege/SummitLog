using System;

namespace SummitLog.Services.Model
{
    /// <summary>
    ///     Basisklasse für Entitäten mit einem Namen
    /// </summary>
    public class EntityWithIdAndName
    {
        /// <summary>
        ///     Erstellt eine neue Instanz der ENtität mit dem übergebenen Namen
        /// </summary>
        /// <param name="name"></param>
        public EntityWithIdAndName(string name)
        {
            Name = name;
        }

        /// <summary>
        ///     Erstellt eine neue Instanz der Entität ohne Namen
        /// </summary>
        public EntityWithIdAndName()
        {
        }

        /// <summary>
        ///     Liefert die ID
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        ///     Liefert den Namnen
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Aktualisiert den Namen mit dem übergebenen Wert
        /// </summary>
        /// <param name="newName"></param>
        public void Update(string newName)
        {
            Name = newName;
        }
    }
}