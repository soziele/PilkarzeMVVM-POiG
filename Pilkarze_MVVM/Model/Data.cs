using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Pilkarze_MVVM.Model
{
    static class Data
    {
        public static void SaveToXml(List<Player> players, string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Player>));
            using (StreamWriter writer = new StreamWriter(@"../../../FootballersMVVM/Resources/Footballers.xml"))
            {
                serializer.Serialize(writer, players);
            }
        }

        public static List<Player> LoadFromXml()
        {
            try
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<Player>));
                using (StreamReader reader = new StreamReader(@"../../../FootballersMVVM/Resources/Footballers.xml"))
                {
                    return (List<Player>)deserializer.Deserialize(reader);
                }
            }
            catch
            {
                return new List<Player>();
            }
        }

        public static void SaveToTxt(List<Player> players, string fileName)
        {
            string dataToSave = string.Empty;

            foreach (var player in players)
            {
                dataToSave += $"{player.Name};{player.Surname};{player.Age};{player.Weight}{Environment.NewLine}";
            }
            File.WriteAllText(fileName, dataToSave);
        }

        public static List<Player> LoadFromTxt(string fileName)
        {
            List<Player> players = new List<Player>();

            string dataFromFile = File.ReadAllText(fileName).Trim();
            if (!string.IsNullOrEmpty(dataFromFile))
            {
                string[] txtPlayers = dataFromFile.Split('\n');
                foreach (var txtplayer in txtPlayers)
                {
                    players.Add(stringToPlayerConverter(txtplayer));
                }
            }
            return players;
        }

        private static Player stringToPlayerConverter(string playerTxt)
        {
            string[] infoAboutPlayer = playerTxt.Split(';');

            string name = infoAboutPlayer[0].Trim();
            string lastName = infoAboutPlayer[1].Trim();
            uint age = 0;
            uint weight = 0;

            try
            {
                age = uint.Parse(infoAboutPlayer[2].Trim());
                weight = uint.Parse(infoAboutPlayer[3].Trim());
            }
            catch { }

            return new Player(name, lastName, age, weight);
        }

        public static List<Player> LoadFromJson(string fileName)
        {
            try
            {
                StreamReader file = File.OpenText(fileName);
                JsonSerializer serializer = new JsonSerializer();                
                return (List<Player>)serializer.Deserialize(file, typeof(List<Player>));
            }
            catch (Exception)
            {
                return new List<Player>();
            }
        }
    }
}
