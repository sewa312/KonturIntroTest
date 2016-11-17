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
        private const long BlockSize = 8;

        private Stream stream;
        List<Block> list;

        public Signal(Stream input)
        {
            stream = input;
            list = new List<Block>();
            foreach (Block b in Read())
            {
                list.Add(b);
            }
        }

        public IEnumerable<Block> Read()
        {
            stream.Seek(HeaderSize, SeekOrigin.Begin);
            long totalSize = stream.Length;
            var reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true);
            while (stream.Position + BlockSize < totalSize)
            {
                int time = reader.ReadInt32();
                int block = reader.ReadInt32();

                byte address = (byte)(block >> 24);
                uint data = (uint)(block & 0x00FFFFFF);
                data = data >> 3;
                byte matrix = (byte)(block & 0x03);
                matrix = (byte)(matrix >> 1);
                byte control = (byte)(block & 0x01);

                yield return new Block(time, address, data, matrix, control);
            }

        }
    }
}
