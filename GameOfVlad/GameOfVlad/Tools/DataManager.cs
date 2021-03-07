using System;
using System.IO;
using System.Xml;
using GameOfVlad.Pages;

namespace GameOfVlad.Tools
{
    public class DataManager
    {
        private static readonly string fileNameScore = "SaveScore.xml";
        private static readonly string fileNameDeath = "SaveDeath.xml";

        private static readonly string[] files = new string[] { fileNameScore, fileNameDeath };
        public void CreateFile()
        {
            foreach(var file in files)
            if (!File.Exists(file))
                Load(file);
        }
        private void Load(string fileName)
        {
            var defaultValue = fileName.Equals(fileNameScore) ? "-1" : "0";
            var xmlDoc = new XmlDocument();
            var rootNode = xmlDoc.CreateElement("Levels");
            xmlDoc.AppendChild(rootNode);
            XmlNode userNode;

            for (int i = 1; i < GameLevels.LevelCount+1; i++)
            {
                userNode = xmlDoc.CreateElement("Level" + i.ToString());
                userNode.InnerText = defaultValue;
                rootNode.AppendChild(userNode);
            }
            xmlDoc.Save(fileName);
        }

        public void AddScore(string level, float time)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(fileNameScore);
            var timeFile = Convert.ToDouble(xmlDoc.SelectSingleNode("Levels/" + level).InnerText);

            if (timeFile == -1 || timeFile > time)
                xmlDoc.SelectSingleNode("Levels/" + level).InnerText = time.ToString();

            xmlDoc.Save(fileNameScore);
        }

        public string GetBestScore(string level)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(fileNameScore);
            string time = xmlDoc.SelectSingleNode("Levels/" + level).InnerText;
            xmlDoc.Save(fileNameScore);
            return time;
        }

        public bool LevelPassed(string level)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(fileNameScore);
            string time = xmlDoc.SelectSingleNode("Levels/" + level).InnerText;
            xmlDoc.Save(fileNameScore);
            return !time.Equals("-1");
        }

        public void AddDeath(string levelName)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(fileNameDeath);
            var count = Convert.ToInt32(xmlDoc.SelectSingleNode("Levels/" + levelName).InnerText);
            count++;
            xmlDoc.SelectSingleNode("Levels/" + levelName).InnerText = count.ToString();
            xmlDoc.Save(fileNameDeath);
        }

        public string GetAllDeath(string level)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(fileNameDeath);
            string deathCount = xmlDoc.SelectSingleNode("Levels/" + level).InnerText;
            xmlDoc.Save(fileNameDeath);
            return deathCount;
        }
    }
}
