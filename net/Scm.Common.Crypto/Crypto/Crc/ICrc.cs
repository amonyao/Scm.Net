namespace Com.Scm.Crypto.Crc
{
    public interface ICrc
    {
        long Value { get; }

        void Reset();

        void Crc(int bval);

        void Crc(byte[] buffer);

        void Crc(byte[] buf, int off, int len);
    }
}
