using System;
using System.IO;

namespace MyLibrary.Model.Repositories
{
    public class JsonFileUnit
    {
        /// <summary>
        /// Read and write to JSON file with access control.
        /// </summary>
        public class JsonRepository : IDisposable
        {
            private readonly string _jsonFileName;

            public JsonRepository(string jsonFileName)
            {
                _jsonFileName = jsonFileName;
            }

            /// <summary>
            /// Write data into JSON file.
            /// </summary>
            /// <param name="data"> desired dynamic data user want to write in json file </param>
            /// <exception cref="Exception"> throw exception if application doesn't have access to file  </exception>
            public void WriteJson(dynamic data)
            {
                lock (_jsonFileName)
                {
                    try
                    {
                        //string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                        using (FileStream fs = new FileStream(_jsonFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            //writer.Write(json);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"FAILED to write JSON file: {_jsonFileName}", ex);
                    }
                }
            }

            /// <summary>
            /// Read JSON file and deserialize into <typeparamref name="T"/>.
            /// </summary>
            /// <exception cref="Exception"> throw exception if application doesn't have access to file </exception>
            //public T ReadJson<T>()
            //{
            //    try
            //    {
            //        if (!File.Exists(_jsonFileName))
            //        {
            //            using (File.Create(_jsonFileName)) { }
            //            return default;
            //        }

            //        using (FileStream fs = new FileStream(_jsonFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            //        using (StreamReader reader = new StreamReader(fs))
            //        {
            //            string json = reader.ReadToEnd();
            //            //return JsonConvert.DeserializeObject<T>(json);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception($"FAILED to read JSON file: {_jsonFileName}", ex);
            //    }
            //}
            public void Dispose() { }
        }
    }
}
