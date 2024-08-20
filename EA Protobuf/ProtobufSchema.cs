using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.Compiler;
using Google.Protobuf;

namespace EA_Protobuf
{
    public class ProtobufSchema
    {
        public ProtobufSchema() { }
        public static void GenerateProtoFile(string filePath)
        {

            // Define message structure
            var fileDescriptor = new FileDescriptorProto();
            fileDescriptor.Name = "test.proto";
            fileDescriptor.Syntax = "proto3";
            var message = new DescriptorProto();
            message.Name = "TestMessage";
            var field1 = new FieldDescriptorProto();
            field1.Name = "id";
            field1.Type = FieldDescriptorProto.Types.Type.Int32;
            //add field to message
            message.Field.Add(field1);
            //add message to file
            fileDescriptor.MessageType.Add(message);
            fileDescriptor.WriteTo(new FileStream(filePath, FileMode.OpenOrCreate));  
            var protoString = fileDescriptor.ToString();

        }
    }
}
