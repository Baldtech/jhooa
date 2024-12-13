namespace Jhooa.UI.Models;

public class UploadStream : Stream
{
    private readonly Stream _inner;
    private readonly string _tempFilePath;
    private bool _disposed;

    public UploadStream(IFormFile file)
    {
        if (!Directory.Exists("StreamFiles"))
            Directory.CreateDirectory("StreamFiles");

        _tempFilePath = Path.Combine("StreamFiles", Path.GetRandomFileName());
        _inner = new FileStream(_tempFilePath, FileMode.Create, FileAccess.ReadWrite);
        file.CopyTo(_inner);
        _inner.Position = 0;
    }

    public UploadStream(byte[] data)
    {
        if (!Directory.Exists("StreamFiles"))
            Directory.CreateDirectory("StreamFiles");

        _tempFilePath = Path.Combine("StreamFiles", Path.GetRandomFileName());
        _inner = new FileStream(_tempFilePath, FileMode.Create, FileAccess.ReadWrite);
        _inner.Write(data);
        _inner.Position = 0;
    }

    public override bool CanRead => _inner.CanRead;

    public override bool CanSeek => _inner.CanSeek;

    public override bool CanWrite => _inner.CanWrite;

    public override long Length => _inner.Length;

    public override long Position
    {
        set { _inner.Position = value; }
        get { return _inner.Position; }
    }

    public override void Flush() => _inner.Flush();

    public override int Read(byte[] buffer, int offset, int count) => _inner.Read(buffer, offset, count);

    public override long Seek(long offset, SeekOrigin origin) => _inner.Seek(offset, origin);

    public override void SetLength(long value) => _inner.SetLength(value);

    public override void Write(byte[] buffer, int offset, int count) => _inner.Write(buffer, offset, count);

    protected override void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        if (disposing)
        {
            _inner.Dispose();
            File.Delete(_tempFilePath);
        }

        base.Dispose(disposing);
    }

    protected virtual void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }

    public byte[] ToByteArray()
    {
        _inner.Position = 0;
        using var br = new BinaryReader(_inner);
        long numBytes = new FileInfo(_tempFilePath).Length;
        return br.ReadBytes((int)numBytes);
    }
}