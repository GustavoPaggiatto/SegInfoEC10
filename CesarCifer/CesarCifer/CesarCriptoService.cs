using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CesarCifer
{
    public sealed class CesarCriptoService
    {
        private readonly byte _key;
        private readonly int _blockLength;

        public CesarCriptoService()
        {
            _key = 18;
            _blockLength = 1000;
        }
        
        public async Task CriptoAsync(string filePath, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var reader = this.GetStream(filePath, FileMode.Open);
            var writer = this.GetStream($"{filePath.Replace(".txt","")}_encripted.txt", FileMode.Create);

            try
            {
                await TransferContentAsync(reader, writer, true, cancellationToken);
            }
            catch
            {
                throw;
            }
            finally
            {
                await reader.FlushAsync(cancellationToken);
                await writer.FlushAsync(cancellationToken);

                await reader.DisposeAsync();
                await writer.DisposeAsync();
            }

            File.Delete(filePath);
        }

        public async Task DecriptAsync(string filePath, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var reader = this.GetStream(filePath, FileMode.Open);
            var writer = this.GetStream($"{filePath.Replace("_encripted.txt",".txt")}", FileMode.Create);
            
            try
            {
                await TransferContentAsync(reader, writer, false, cancellationToken);
            }
            catch
            {
                throw;
            }
            finally
            {
                await reader.FlushAsync(cancellationToken);
                await writer.FlushAsync(cancellationToken);

                await reader.DisposeAsync();
                await writer.DisposeAsync();
            }

            File.Delete(filePath);
        }

        private FileStream GetStream(string filePath, FileMode fileMode = FileMode.Append)
        {
            return new FileStream(filePath, fileMode);
        }

        private async Task TransferContentAsync(FileStream reader, FileStream writer, bool encript, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            while (reader.Position < reader.Length)
            {
                var buffer = new byte[_blockLength];
                
                await reader.ReadAsync(
                    buffer,
                    (int) reader.Position,
                    _blockLength,
                    cancellationToken);

                buffer = buffer.Select(b => (byte)(encript ? (b + _key) : (b - _key))).ToArray();

                if (_blockLength > reader.Position)
                    buffer = buffer.Take((int)reader.Length).ToArray();
                
                await writer.WriteAsync(buffer, cancellationToken);
            }
        }
    }
}