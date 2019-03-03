using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeNet
{
    //byte[] 버퍼를 참조로 보관하여 pop_xxx메소드 호출 순서대로 데이터 변환을 수행한다.
    public class CPacket
    {
        public IPeer owner { get; private set; }
        public byte[] buffer { get; private set; }
        public int position { get; private set; }

        public Int16 protocol_id { get; private set; }

        //패킷 생성
        public static CPacket create(Int16 protocol_id)
        {
            CPacket packet = CPacketBufferManager.Pop();
            packet.set_protocol(protocol_id);
            return packet;
        }

        //패킷 삭제
        public static void destroy(CPacket packet)
        {
            CPacketBufferManager.push(packet);
        }

        //패킷 생성자1 (근데 왜 생성자랑 create함수를 따로 쓸까??)
        public CPacket(byte[] buffer, IPeer owner)
        {
            //참조로만 보관하여 작업한다.
            //복사가 필요하면 별도로 구현해야 한다.
            this.buffer = buffer;

            //헤더는 읽을 필요 없으니 그 이후부터 시작해야 한다.
            this.position = Defines.HEADERSIZE;

            this.owner = owner;
        }

        //패킷 생성자2
        public CPacket()
        {
            this.buffer = new byte[1024];
        }

        //프로토콜 id pop
        public Int16 pop_protocol_id()
        {
            return pop_int16();
        }

        public void copy_to(CPacket target)
        {
            target.set_protocol(this.protocol_id);
            target.overwrite(this.buffer, this.position);
        }

        public void overwrite(byte[] source, int position)
        {
            Array.Copy(source, this.buffer, source.Length); //source를 buffer에 길이만큼 복사
            this.position = position;
        }

        public byte pop_byte()
        {
            byte data = (byte)BitConverter.ToInt16(this.buffer, this.position);
            this.position += sizeof(byte);
            return data;
        }

        public Int16 pop_int16()
        {
            Int16 data = BitConverter.ToInt16(this.buffer, this.position);
            this.position += sizeof(Int16);
            return data;
        }

        public Int32 pop_int32()
        {
            Int32 data = BitConverter.ToInt32(this.buffer, this.position);
            this.position += sizeof(Int32);
            return data;
        }

        public string pop_string()
        {
            // 문자열 길이는 최대 2바이트 까지. 0 ~ 32767
            Int16 len = BitConverter.ToInt16(this.buffer, this.position);
            this.position += sizeof(Int16);

            // 인코딩은 utf8로 통일한다.
            string data = System.Text.Encoding.UTF8.GetString(this.buffer, this.position, len);
            this.position += len;

            return data;
        }

        public void set_protocol(Int16 protocol_id)
        {
            this.protocol_id = protocol_id;

            //헤더는 나중에 넣을 것이므로 데이터 부터 넣을 수 있도록 위치를 점프시켜 놓는다.
            this.position = Defines.HEADERSIZE;

            push_int16(protocol_id);
        }

        public void recore_size()
        {
            Int16 body_size = (Int16)(this.position - Defines.HEADERSIZE);
            byte[] header = BitConverter.GetBytes(body_size);
            header.CopyTo(this.buffer, 0);
        }

        public void push_int16(Int16 data)
        {
            byte[] temp_buffer = BitConverter.GetBytes(data);
            temp_buffer.CopyTo(this.buffer, this.position);
            this.position += temp_buffer.Length;
        }

        public void push(byte data)
        {
            byte[] temp_buffer = BitConverter.GetBytes(data);
            temp_buffer.CopyTo(this.buffer, this.position);
            this.position += sizeof(byte);
        }

        public void push(Int16 data)
        {
            byte[] temp_buffer = BitConverter.GetBytes(data);
            temp_buffer.CopyTo(this.buffer, this.position);
            this.position += temp_buffer.Length;
        }

        public void push(Int32 data)
        {
            byte[] temp_buffer = BitConverter.GetBytes(data);
            temp_buffer.CopyTo(this.buffer, this.position);
            this.position += temp_buffer.Length;
        }

        public void push(string data)
        {
            byte[] temp_buffer = Encoding.UTF8.GetBytes(data);

            Int16 len = (Int16)temp_buffer.Length;
            byte[] len_buffer = BitConverter.GetBytes(len);
            len_buffer.CopyTo(this.buffer, this.position);
            this.position += sizeof(Int16);

            temp_buffer.CopyTo(this.buffer, this.position);
            this.position += temp_buffer.Length;
        }





    }
}
