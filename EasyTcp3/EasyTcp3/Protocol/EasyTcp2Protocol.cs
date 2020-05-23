using System;
using System.Linq;
using EasyTcp3.Server;

namespace EasyTcp3.Protocol
{
    public class EasyTcp2Protocol : IEasyTcpProtocol
    {
        /// <summary>
        /// Create a new message from 1 or multiple byte arrays
        ///
        /// [ushort: length of data][data + data1 + data2...]
        /// </summary>
        /// <param name="data">data to send to server</param>
        /// <returns>byte array with merged data + length: [ushort: data length][data]</returns>
        /// <exception cref="ArgumentException">could not create message: Data array is empty</exception>
        public byte[] CreateMessage(params byte[][] data)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentException("Could not create message: Data array is empty");

            // Calculate length of message
            var messageLength = data.Sum(t => t?.Length ?? 0);
            if (messageLength == 0) throw new ArgumentException("Could not create message: Data array only contains empty arrays");
            byte[] message = new byte[2 + messageLength];

            // Write length of data to message
            Buffer.BlockCopy(BitConverter.GetBytes((ushort) messageLength), 0, message, 0, 2);

            // Add data to message
            int offset = 2;
            foreach (var d in data)
            {
                if (d == null) continue;
                Buffer.BlockCopy(d, 0, message, offset, d.Length);
                offset += d.Length;
            }

            return message; 
        }

        public int BufferSize { get; set; }
        
        public byte[][] DataReceive(byte[] data, int receivedBytes, EasyTcpClient client)
        {
            throw new NotImplementedException();
        }

        public bool OnClientConnect(EasyTcpClient client, EasyTcpServer server)
        {
            throw new NotImplementedException();
        }

        public void ReceiveData(byte[] data, EasyTcpClient client)
        {
            throw new NotImplementedException();
        }
    }
}