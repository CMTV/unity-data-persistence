using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

namespace CMTV.DataPersistence
{
    public class IOHelper
    {
        public static string GetPath(string path)
        {
            return Application.persistentDataPath + "/" + path;
        }

        public static void Save(string path, object data)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(GetPath(path)));

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(GetPath(path), FileMode.OpenOrCreate);

            bf.Serialize(file, data);
            file.Close();
        }

        public static object Load(string path)
        {
            object data = null;

            if (DataFileExists(path))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(GetPath(path), FileMode.Open);

                try
                {
                    data = bf.Deserialize(file);
                }
                catch { }

                file.Close();
            }

            return data;
        }

        public static void Remove(string path)
        {
            if (DataFileExists(path))
            {
                File.Delete(GetPath(path));
            }
        }

        public static bool DataFileExists(string path)
        {
            return File.Exists(GetPath(path));
        }
    }
}