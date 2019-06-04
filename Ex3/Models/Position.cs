using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Ex3.Models
{
    public class Position
    {
        public Position(double lon, double lat, double throttle, double rudder)
        {
            this.lon = lon;
            this.lat = lat;
            this.throttle = throttle;
            this.rudder = rudder;
        }
        private double lon;
        private double lat;
        private double rudder;
        private double throttle;
        public double Lon
        {
            get { return this.lon; }
            set { this.lon = value; }
        }
        public double Lat
        {
            get { return this.lon;}
            set { this.lon = value; }
        }
        public double Rudder
        {
            get { return this.rudder; }
            set { this.rudder = value; }
        }
        public double Throttle
        {
            get { return this.throttle; }
            set { this.throttle = value; }
        }
        public override string ToString()
        {
            string str = this.lon.ToString() + "," + this.lat.ToString()
                + "," + this.throttle.ToString() + "," + this.rudder.ToString();
            return str;
        }
        public void ToXml(XmlWriter writer)
        {
            writer.WriteStartElement("Position");
            writer.WriteElementString("lon", this.lon.ToString());
            writer.WriteElementString("lat", this.lat.ToString());
            writer.WriteElementString("throttle", this.throttle.ToString());
            writer.WriteElementString("rudder", this.rudder.ToString());
            writer.WriteEndElement();
        }
    }
}