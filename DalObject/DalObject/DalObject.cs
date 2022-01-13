using System.Collections.Generic;
using System.Xml.Linq;

namespace Dal
{
    internal sealed partial class DalObject : DalApi.IDal
    {
        private static readonly DalApi.IDal instance = new DalObject();
        public static DalApi.IDal Instance { get => instance; }

        private DalObject()
        {
            DataSource.Initialize();
            /* saveListToXml(DataSource.BaseStations);
             saveListToXml(DataSource.Charges);
             saveListToXml(DataSource.Customers);
             saveListToXml(DataSource.Drones);
             saveListToXml(DataSource.Parcels);*/
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
    }
}
