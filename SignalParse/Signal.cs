using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalParse
{
    struct Block
    {
        public int Time { get; }
        public byte Address { get; }
        public uint Data { get; }
        public byte Matrix { get; }
        public byte Control { get; }

        public Block(int time, byte address, uint data, byte matrix, byte control)
        {
            Time = time;
            Address = address;
            Data = data;
            Matrix = matrix;
            Control = control;
        }
    }

    class Signal
    {
        private const long HeaderSize = 1024;
        private const long BlockSize = 64;

        private Stream stream;

        public Signal(Stream input)
        {
            stream = input;
        }

        public IEnumerable<Block> Read()
        {
            stream.Seek(HeaderSize, SeekOrigin.Begin);
            long totalSize = stream.Length;
            //var reader = new BinaryReader(File.Open("GAMA_stop_word.scc429", FileMode.OpenOrCreate));
            var reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true);

            //header = reader.ReadChars(128);

            while (stream.Position + BlockSize < totalSize)
            {
                int time = reader.ReadInt32();
                if (!BitConverter.IsLittleEndian)
                    time = ReverseByteOrder(time);
                
                uint block = reader.ReadUInt32();
                if (BitConverter.IsLittleEndian)
                    block = ReverseByteOrder(block);
                
                byte address = (byte)(block >> 24);
                uint data = (uint)(block & 0x00FFFFFF);
                data = data >> 3;
                byte matrix = (byte)(block & 0x000007);
                matrix = (byte)(matrix >> 1);
                byte control = (byte)(block & 0x01);

                yield return new Block(time, address, data, matrix, control);
            }
        }

        private UInt32 ReverseByteOrder(UInt32 source)
        {
            return (source << 24) | ((source & 0x0000FF00) << 8) | ((source & 0x00FF0000) >> 8) | (source >> 24);
        }

        private Int32 ReverseByteOrder(Int32 source)
        {
            return (Int32)ReverseByteOrder((UInt32)source);
        }
    }
}
