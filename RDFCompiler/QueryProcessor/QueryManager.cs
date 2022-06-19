using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using VDS.RDF.Query;
using VDS.RDF.Storage;
using VDS.RDF.Storage.Management;

namespace RDFCompiler.QueryProcessor
{
    public class QueryManager
    {
        StardogConnector _storage;
        StardogServer _server;
        IStorageProvider _database;
        string _currentName = "не установлено";
        public string DB_NAME
        {
            get => _currentName+", триплетов: "+GetCount();
        }
        public QueryManager(string dbName)
        {
            
            _server = new StardogServer("http://localhost:5820", "admin", "admin");
            if (((List<string>)_server
                .ListStores()).Contains(dbName))
            {
                _storage = new StardogConnector("http://localhost:5820", dbName, "admin", "admin");
                _currentName = dbName;
            }
                
        }
        public void SetStore(string name)
        {
            if (((List<string>)_server
    .ListStores()).Contains(name))
            {
                _database = _server.GetStore(name);
                _currentName = name;
                _storage = new StardogConnector("http://localhost:5820", name, "admin", "admin");
            }
            else
                throw new Exception("Хранилище с данным именем не существует!");
        }
        public void InsertData(List<string> triples)
        {
            string querry = "INSERT {\r\n";
            foreach (string term in triples)
            {
                querry += term+"\r\n";
            }
            querry += "\r\n} where{}";
            var q = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(querry));
            _storage.Update(q);
        }
        List<string> _namespaces = new List<string>() { "rdf-schema#", "22-rdf-syntax-ns#" };
        List<string> _types = new List<string>() { "^^XMLSchema#integer", "XMLSchema#int", "XMLSchema#string" };
        int GetCount()
        {
            SparqlResultSet serverRes = (SparqlResultSet)_storage.Query("SELECT * WHERE {?s ?p ?o}");
            return serverRes.Count;
        }
        public List<string> GetAll()
        {
            List<string> results = new List<string>();
            var serverRes = _storage.Query("SELECT * WHERE {?s ?p ?o}");
            TripleStore _trStore = new TripleStore();

            if (serverRes is SparqlResultSet)
            {
                SparqlResultSet rset = (SparqlResultSet)serverRes;

                foreach (SparqlResult res in rset)
                {
                    _trStore.Add(res.ToString());
                }
            }
            return _trStore.GetData();
        }

        public static byte[] ConvertHexStringToByteArray(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
            }

            byte[] data = new byte[hexString.Length / 2];
            for (int index = 0; index < data.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                data[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return data;
        }

        public void DeleteData(string whereClause)
        {
            string querry = "DELETE WHERE { " + whereClause + " }";
            _storage.Update(querry);
        }

        public void DeleteAll()
        {
            string querry = "DELETE WHERE { ?s ?p ?o. }";
            _storage.Update(querry);
        }

        public void CreateStore(string storeName)
        {
            if (!((List<string>)_server
                .ListStores()).Contains(storeName))
            {
                var template = _server
                    .GetDefaultTemplate(storeName);

                if (_server.CreateStore(template))
                {
                    Debug.WriteLine("Complete creation!");
                    _database = _server.GetStore(storeName);
                    _currentName = storeName;
                    _storage = new StardogConnector("http://localhost:5820", storeName, "admin", "admin");
                }
                else
                    throw new Exception("Во время создания хранилища возникла непредвиденная ошибка!");
            }
            else
                throw new Exception("Хранилище с данным именем уже существует!");

        }
        public class TripleStore
        {
            List<Triplet> _triplets = new List<Triplet>();
            string numPattern = "[-]?[0-9]+";
            public TripleStore() { }

            public void Add(string uri)
            {
                _triplets.Add(new Triplet(uri));
            }

            public List<string> GetData()
            {
                List<string> result = new List<string>();
                foreach (var trip in _triplets)
                {
                    string triplet = ":" + trip.Subject + " ";
                    triplet += (trip.Predicate.StartsWith("rdf") ? trip.Predicate : ":" + trip.Predicate) + " ";

                    triplet += _triplets.Exists(x => x.Subject == trip.Object) ?
                        ":" + trip.Object + "."
                            : Regex.IsMatch(trip.Object, numPattern) || trip.Object.Contains("rdf") || trip.Object.Contains("xsd") ?
                        trip.Object + " ."
                            : "\"" + trip.Object + "\".";

                    result.Add(triplet);
                }
                return result;
            }
        }
        public class Triplet
        {
            string _subject;
            string _predicate;
            string _object;

            public string Subject { get => _subject; }
            public string Predicate { get => _predicate; }
            public string Object { get => _object; }

            string _pattern = @"http[s]?://[\w/.]*/";

            public Triplet() { }
            public Triplet(string uriString)
            {
                string[] triplets = uriString.ToString().Split(',', StringSplitOptions.RemoveEmptyEntries);
                if (triplets.Length == 3)
                {
                    _subject = Transform(triplets[0]);
                    _predicate = Transform(triplets[1]);
                    _object = Transform(triplets[2]);
                }
                else
                {
                    throw new Exception("Число частей должно быть равно трём!");
                }
            }

            private string Transform(string tr)
            {
                string resOnly = Regex.Replace(tr.Split('=', StringSplitOptions.RemoveEmptyEntries)[1].Trim(), _pattern, String.Empty);
                resOnly = resOnly.Replace("rdf-schema#", "rdfs:");
                resOnly = resOnly.Replace("22-rdf-syntax-ns#", "rdf:");
                resOnly = resOnly.Replace("^^XMLSchema#integer", String.Empty);
                resOnly = resOnly.Replace("XMLSchema#", "xsd:");
                //для кириллицы
                if (resOnly.Contains("%"))
                {
                    var wop = resOnly.Replace("%", String.Empty);
                    if (wop.Contains("_"))
                    {
                        var parts = wop.Split('_', StringSplitOptions.RemoveEmptyEntries);
                        var conv = ConvertHexStringToByteArray(parts[0]);
                        resOnly = Encoding.Default.GetString(ConvertHexStringToByteArray(parts[0]))
                            + "_" + Encoding.Default.GetString(ConvertHexStringToByteArray(parts[1]));
                    }
                    else
                    {
                        resOnly = Encoding.Default.GetString(ConvertHexStringToByteArray(wop));
                    }
                }
                return resOnly;
            }
        }
    }
}
