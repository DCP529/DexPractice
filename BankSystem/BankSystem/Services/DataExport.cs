using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

namespace BankSystem.Services
{
    public class DataExport
    {
        public void AddDataToFile<T>(T obj, string path)
        {
            var type = obj.GetType();
            var properties = type.GetProperties();

            using (FileStream fs = new FileStream(path, FileMode.Append))
            {
                var serializeProp = JsonConvert.SerializeObject(properties);

                byte[] dataArray = Encoding.Default.GetBytes(serializeProp);
                fs.Write(dataArray);
            }
        }
    }
}
