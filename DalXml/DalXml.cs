using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Reflection;

namespace Dal
{
    sealed partial class DalXml :DalApi.IDal
    {
        static readonly DalApi.IDal instance = new DalXml();
        public static DalApi.IDal Instance { get => instance; }

        DalXml()
        {
        }

        internal class Config
        {
            //Precent To KM
            public static double Free
            { 
                get 
                {
                    XElement configRoot = XElement.Load(@"xml\Config.xml");
                    return double.Parse(configRoot.Element("Free").Value);
                }
            }
            public static double LightParcel
            {
                get
                {
                    XElement configRoot = XElement.Load(@"xml\Config.xml");
                    return double.Parse(configRoot.Element("LightParcel").Value);
                }
            }
            public static double MediumParcel
            {
                get
                {
                    XElement configRoot = XElement.Load(@"xml\Config.xml");
                    return double.Parse(configRoot.Element("MediumParcel").Value);
                }
            }
            public static double HeavyParcel
            {
                get
                {
                    XElement configRoot = XElement.Load(@"xml\Config.xml");
                    return double.Parse(configRoot.Element("HeavyParcel").Value);
                }
            }
            public static double ChargingRate
            { //precent to mintue
                get
                {
                    XElement configRoot = XElement.Load(@"xml\Config.xml");
                    return double.Parse(configRoot.Element("ChargingRate").Value);
                }
            }

            public static int ParcelId {
                //return the ParcelID and update it to next number.
                get
                {
                    XElement configRoot = XElement.Load(@"xml\Config.xml");
                    int parcelId = int.Parse(configRoot.Element("ParcelId").Value);
                    configRoot.Element("ParcelId").Value = (parcelId + 1).ToString();
                    configRoot.Save(@"xml\Config.xml");
                    return parcelId;
                }
            }
        }


        private void saveListToXml<T>(List<T> myList)
        {
            XElement root = new(typeof(T).Name + "s");
            foreach (var item in myList)
            {
                XElement station = new(item.GetType().Name);
                var properties = item.GetType().GetProperties();
                foreach (var pro in properties)
                {
                    station.Add(new XElement(pro.Name, pro.GetValue(item)));
                }
                root.Add(station);
            }
            root.Save(@$"xml\{root.Name}.xml");
        }
        private List<T> loadXmlToList<T>() where T : new()
        {
            List<T> myList = new();
            XElement a = XElement.Load(@$"xml\{typeof(T).Name}s.xml");
            foreach (XElement item in a.Elements())
            {
                object x = new T();

                foreach (PropertyInfo pro in x.GetType().GetProperties())
                {
                    if (pro.PropertyType == typeof(int))
                        pro.SetValue(x, int.Parse(item.Element(pro.Name).Value));
                    if (pro.PropertyType == typeof(double))
                        pro.SetValue(x, double.Parse(item.Element(pro.Name).Value));
                    if (pro.PropertyType == typeof(string))
                        pro.SetValue(x, item.Element(pro.Name).Value);
                    if (pro.PropertyType == typeof(bool))
                        pro.SetValue(x, bool.Parse(item.Element(pro.Name).Value));
                    if (pro.PropertyType == typeof(DateTime?))
                        if (item.Element(pro.Name).Value != "")
                            pro.SetValue(x, (DateTime?)item.Element(pro.Name));
                        else
                            pro.SetValue(x, null);
                    if (pro.PropertyType == typeof(DO.Permissions))
                        pro.SetValue(x, Enum.Parse(typeof(DO.Permissions), item.Element(pro.Name).Value));
                    if (pro.PropertyType == typeof(DO.Priorities))
                        pro.SetValue(x, Enum.Parse(typeof(DO.Priorities), item.Element(pro.Name).Value));
                    if (pro.PropertyType == typeof(DO.WeightCategories))
                        pro.SetValue(x, Enum.Parse(typeof(DO.WeightCategories), item.Element(pro.Name).Value));
                }
                myList.Add((T)x);
            }
            return myList;
        }


    }
}
