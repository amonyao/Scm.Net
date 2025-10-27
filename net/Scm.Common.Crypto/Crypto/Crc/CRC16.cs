using System;

namespace Com.Scm.Crypto.Crc
{
    /// <summary>
    /// CRC16 的摘要说明。
    /// </summary>
    public class CRC16 : ICrc
    {
        #region CRC 16 位校验表

        /// <summary>
        /// 16 位校验表 Upper 表
        /// </summary>
        public ushort[] uppercrctab = new ushort[]
        {
            0x0000,0x1231,0x2462,0x3653,0x48c4,0x5af5,0x6ca6,0x7e97,
            0x9188,0x83b9,0xb5ea,0xa7db,0xd94c,0xcb7d,0xfd2e,0xef1f
        };

        /// <summary>
        /// 16 位校验表 Lower 表
        /// </summary>
        public ushort[] lowercrctab = new ushort[]
        {
            0x0000,0x1021,0x2042,0x3063,0x4084,0x50a5,0x60c6,0x70e7,
            0x8108,0x9129,0xa14a,0xb16b,0xc18c,0xd1ad,0xe1ce,0xf1ef
        };
        #endregion

        ushort crc = 0;

        /// <summary>
        /// 校验后的结果
        /// </summary>
        public long Value
        {
            get
            {
                return crc;
            }
            set
            {
                crc = (ushort)value;
            }
        }

        /// <summary>
        /// 设置crc 初始值
        /// </summary>
        public void Reset()
        {
            crc = 0;
        }

        /// <summary>
        /// Crc16
        /// </summary>
        /// <param name="ucrc"></param>
        /// <param name="buf"></param>
        public void Crc(ushort ucrc, byte[] buf)
        {
            crc = ucrc;
            Crc(buf);
        }

        /// <summary>
        /// Crc16
        /// </summary>
        /// <param name="bval"></param>
        public void Crc(int bval)
        {
            ushort h = (ushort)(crc >> 12 & 0x0f);
            ushort l = (ushort)(crc >> 8 & 0x0f);
            ushort temp = crc;
            temp = (ushort)((temp & 0x00ff) << 8 | bval);
            temp = (ushort)(temp ^ (uppercrctab[h - 1 + 1] ^ lowercrctab[l - 1 + 1]));
            crc = temp;
        }


        /// <summary>
        /// Crc16
        /// </summary>
        /// <param name="buffer"></param>
        public void Crc(byte[] buffer)
        {
            Crc(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Crc16
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="off"></param>
        /// <param name="len"></param>
        public void Crc(byte[] buf, int off, int len)
        {
            if (buf == null)
            {
                throw new ArgumentNullException("buf");
            }

            if (off < 0 || len < 0 || off + len > buf.Length)
            {
                throw new ArgumentOutOfRangeException();
            }
            for (int i = off; i < len; i++)
            {
                Crc(buf[i]);
            }
        }
    }
}
