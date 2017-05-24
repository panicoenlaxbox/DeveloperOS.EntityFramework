using System;
using System.Data.Entity.Migrations.Infrastructure;
using System.IO;
using System.IO.Compression;
using MigrationsTeamEnviroment.Migrations;

namespace MigrationsTeamEnviroment
{
    class Program
    {
        static void Main(string[] args)
        {
            SaveEdmxFile();
        }

        private static void SaveEdmxFile()
        {
            var migration = new LastName();
            var metadata = (IMigrationMetadata)migration;
            var compressedBytes = Convert.FromBase64String(metadata.Target);
            var memoryStream = new MemoryStream(compressedBytes);
            var gzip = new GZipStream(memoryStream, CompressionMode.Decompress);
            var reader = new StreamReader(gzip);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "MigrationsTeamEnvironment.edmx");
            File.WriteAllText(path, reader.ReadToEnd());
        }
    }
}
