using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CloudSync
{
    [XmlRoot("DataNode")]
    public class XmlRemoteNodeInfo
    {
        [XmlElement("FileId")]
        public string FileId { get; set; } = string.Empty;

        [XmlElement("IsDeleted")]
        public bool IsDeleted { get; set; }

        [XmlElement("RelativePath")]
        public string RelativePath { get; set; } = string.Empty;

        [XmlElement("Checksum")]
        public string Checksum { get; set; } = string.Empty;
    }

    [Serializable, XmlRoot("RemoteRepository")]
    public class XmlRemoteInfo
    {
        [XmlArray("DataNodes")]
        [XmlArrayItem("DataNode")]
        public List<XmlRemoteNodeInfo> DataNodes { get; set; } = new();

        private static string GenerateXMLSchema()
        {
            const string xmlSchema = """
                <?xml version="1.0" encoding="utf-8"?>
                <xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema">
                    <xs:element name="RemoteRepository">
                        <xs:complexType>
                            <xs:element name="DataNodes">
                                <xs:complexType>
                                    <xs:sequence>
                                        <xs:element name="DataNode">
                                            <xs:complexType>
                                                <xs:sequence>
                                                    <xs:element name="FileId" type="xs:string" />
                                                    <xs:element name="IsDeleted" type="xs:boolean" />
                                                    <xs:element name="RelativePath" type="xs:string" />
                                                    <xs:element name="Checksum" type="xs:string" />
                                                </xs:sequence>
                                            </xs:complexType>
                                        </xs:element>
                                    </xs:sequence>
                                </xs:complexType>
                            </xs:element>
                        </xs:complexType>
                    </xs:element>
                </xs:schema>
                """;
            return xmlSchema;
        }

        private XmlRemoteInfo()
        {
            
        }

        public static RemoteRepository GenerateRepositoryFromXML(string xmlFilePath)
        {
            var remote = new RemoteRepository();
            var serializer = new XmlSerializer(typeof(XmlRemoteInfo));

            using var reader = new StreamReader(xmlFilePath);
            var remoteInfo = (XmlRemoteInfo?)serializer.Deserialize(reader)
                ?? throw new Exception("Invalid XML format.");

            foreach (var dataNode in remoteInfo.DataNodes)
            {
                remote.AddDataNode(new RemoteDataNode
                {
                    FileId = dataNode.FileId,
                    Checksum = dataNode.Checksum,
                    IsDeleted = dataNode.IsDeleted,
                    RelativePath = dataNode.RelativePath
                });
            }
            
            return remote;
        }

        public static void GenerateXMLFromRepository(RemoteRepository repository, string filePath)
        {
            var remote = new XmlRemoteInfo();
            foreach (var dataNode in repository.DataNodes)
            {
                remote.DataNodes.Add(new XmlRemoteNodeInfo
                {
                    Checksum = dataNode.Checksum,
                    FileId = dataNode.FileId,
                    IsDeleted = dataNode.IsDeleted,
                    RelativePath = dataNode.RelativePath
                });
            }

            var serializer = new XmlSerializer(typeof(XmlRemoteInfo));
            using var writer = new StreamWriter(filePath);
            serializer.Serialize(writer, remote);
        }
    }
}
