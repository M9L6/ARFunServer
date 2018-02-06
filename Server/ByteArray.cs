using System;
using System.Collections.Generic;
using System.Text;


namespace Server
{
    class ByteArray
    {
        private List<byte> bytes;

        public ByteArray()
        {
            this.bytes = new List<byte>();    
        }
        
        public ByteArray(byte[] bs)
        {
            this.bytes = new List<byte>();
            for (int i = 0; i < bs.Length; i++)
            {
                bytes.Add(bs[i]);
            }

        }

        public int Length
        {
            get { return bytes.Count; }
        }

        public byte[] Buffer
        {
            get { return bytes.ToArray(); }
        }

       public int Position
       {
            get;
            set;
       }


        public bool ReadBoolean()
        {
            bool result = ((int)bytes[Position] == 1) ? true : false;
            Position += 1;
            return result;
        }

        public void WriteBoolean(bool value)
        {
            byte b =(byte)( value ? 1 : 0);
            bytes.Add(b);
        }

        public int ReadInt()
        {
            int result = 0;
            for (int i = 0; i < 4; i++)
            {
                result += (int)(bytes[i + Position] << ((3 - i) * 8));
            }
            Position += 4;
            return result;
        }

        public void WriteInt(int value)
        {
            byte[] bs = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                bs[i] = (byte)(value >> ((3 - i) * 8));
            }
            bytes.AddRange(bs);
        }

        public byte ReadByte()
        {
            byte b = bytes[Position];
            Position += 1;
            return b;
        }

        public void WriteByte(byte value)
        {
            bytes.Add(value);
        }

        public void WriteBytes(byte[] values, int offset, int length)
        {
            for (int i = 0; i < length; i++)
            {
                bytes.Add(values[i + offset]);
            }

        }

        public void WriteBytes(byte[] values)
        {
            bytes.AddRange(values);
        }

        public string ReadUTFBytes(uint length)
        {
            byte[] bs = new byte[length];
            for (int i = 0; i < length; i++)
            {
                bs[i] = bytes[Position + i];
            }
            string result = Encoding.UTF8.GetString(bs);
            Position += (int)length;
            return result;
        }

        public void WriteUTFBytes(string value)
        {
            byte[] bs =  Encoding.UTF8.GetBytes(value);
            bytes.AddRange(bs);
        }

    }
}
