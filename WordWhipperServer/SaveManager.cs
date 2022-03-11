using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WordWhipperServer.Game;

namespace WordWhipperServer
{
    /// <summary>
    /// Saves and loads games 
    /// </summary>
    public static class SaveManager
    {
        private static string m_filePath;

        static SaveManager()
        {
            m_filePath = Path.Combine(Directory.GetCurrentDirectory(), "\\SaveGameData\\");
            Directory.CreateDirectory(m_filePath);
            Console.WriteLine(m_filePath);
        }

        public static void SaveGame(GameInstance game)
        {
            WriteToBinaryFile(game.GetID().ToString() + ".game", game);
        }

        public static GameInstance GetGame(string id)
        {
            if (!File.Exists(m_filePath + "id" + ".game"))
                throw new Exception("This game doesn't exist in saved data!");

            return ReadFromBinaryFile<GameInstance>(id + ".game");
        }

        /// <summary>
        /// Writes the given object instance to a binary file.
        /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the binary file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the binary file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        private static void WriteToBinaryFile<T>(string name, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(m_filePath + name, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        /// <summary>
        /// Reads an object instance from a binary file.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the binary file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
        private static T ReadFromBinaryFile<T>(string name)
        {
            using (Stream stream = File.Open(m_filePath + name, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
    }
}
