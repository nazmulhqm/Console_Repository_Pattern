using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml.Serialization;

namespace Repository_Source
{
    public class XMLSet<TModel> where TModel : class
    {
        private string _filename;
        private ICollection<TModel> _models;

        public XMLSet(string FileName)
        {
            this._filename = FileName;
        }

        public ICollection<TModel> Data
        {
           get
           {
               try
               {
                   if (_models == null) Load();
               }
               catch (Exception)
               {
                   _models = new List<TModel>();
               }
               return _models;
           }
           set
           {
               _models = value; Save();
           }
        }

        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<TModel>));
            StreamWriter sw = new StreamWriter(this._filename);
            serializer.Serialize(sw, this._models);
            sw.Close();
            sw.Dispose();
        }

        public void Load()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<TModel>));
            StreamReader sr = new StreamReader(this._filename);
            this._models = serializer.Deserialize(sr) as List<TModel>;
            sr.Close();
            sr.Dispose();
        }
    }
}
