using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XMLSerializeDeserialize
{
    public class XmlConverter<T>
    {
        public void Serializar(T objeto, String pastaArquivo)
        {
            string nomeArquivo = ExtrairNomeArquivo(objeto.GetType());

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            var serializer = new XmlSerializer(objeto.GetType());
            if (!Directory.Exists(pastaArquivo))
                Directory.CreateDirectory(pastaArquivo);

            using (var writer = XmlWriter.Create($"{pastaArquivo}\\{nomeArquivo}.xml", settings))
            {
                serializer.Serialize(writer, objeto);
            }
        }

        public T Deserializar(string arquivo)
        {
            T objeto;
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = XmlReader.Create(arquivo))
            {
                objeto = (T)serializer.Deserialize(reader);
            }
            return objeto;
        }

        private static String ExtrairNomeArquivo(Type tipoObjeto)
        {
            String nomeClasse = tipoObjeto.Name;
            String identArquivo = DateTime.Now.ToString("ddMMyyyyHHmmss");

            return $"{nomeClasse}_{identArquivo}";
        }

    }
}
