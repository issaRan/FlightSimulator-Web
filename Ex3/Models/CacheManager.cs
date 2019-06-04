using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace Ex3.Models
{
    public class CacheManager
    {
        private string path;
        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = AppDomain.CurrentDomain.BaseDirectory + @"\" + value + ".txt";
            }
        }
        public CacheManager()
        {
            string str = "flight1";
             this.Path = str;
        }
        #region
        private static CacheManager m_Instance = null;
        public static CacheManager Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new CacheManager();
                }
                return m_Instance;
            }
        }
        #endregion
        public void SavePoint(Position pos)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(this.path, true))
            {
                file.WriteLine(pos.ToString());
            }
        }

        public List<Position> ReadPositions()
        {
            List<Position> list = new List<Position>();
            string[] lines = File.ReadAllLines(this.path);
            foreach(string line in lines)
                list.Add(ToPosition(line));
            return list;
        }

        public Position ToPosition(string str)
        {
            string[] vals = str.Split(',');
            Position pos = new Position(Double.Parse(vals[0]), Double.Parse(vals[1]),
                Double.Parse(vals[2]), Double.Parse(vals[3]));
            return pos;
        }
    }
}
