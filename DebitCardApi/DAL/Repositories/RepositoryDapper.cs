using Dapper;
using DebitCardApi.DAL.Interfaces.Repositories;
using DebitCardApi.DAL.Models;
using Npgsql;

namespace DebitCardApi.DAL.Repositories
{
    public class RepositoryDapper : IRepositoryDapper
    {
        private readonly string _connectionString;
        private readonly string _tableName = "DebitCards";

        public RepositoryDapper(IConfiguration configuration, ILogger<RepositoryDapper> logger)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<IEnumerable<DebitCard>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);
            return await connection
                .QueryAsync<DebitCard>($"SELECT \"Id\", \"Content\", \"CreatedAt\", \"UpdatedAt\" FROM \"{_tableName}\"", 
                    cancellationToken);
        }

        public async Task<DebitCard?> GetById(int id, CancellationToken cancellationToken = default)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);
            var result = await connection
                .QueryFirstOrDefaultAsync<DebitCard>($"SELECT \"Id\", \"Content\", \"CreatedAt\", \"UpdatedAt\" FROM \"{_tableName}\"" +
                                                $"WHERE \"Id\" = {id}", cancellationToken);
            return result;
        }

        public async Task<int> AddAsync(DebitCard entity, CancellationToken cancellationToken = default)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);
            await connection.ExecuteAsync($"INSERT INTO \"{_tableName}\" (\"Content\", \"CreatedAt\", \"UpdatedAt\")" +
                                          $" VALUES (" +
                                          $"'{entity.CreatedAt:yyyy-MM-ddTHH\\:mm\\:ss.fffz}', ",
                cancellationToken);

            return 0;
        }

        public async Task<bool> UpdateAsync(DebitCard note, CancellationToken cancellationToken = default)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            var entity = await connection.QueryAsync($"UPDATE \"{_tableName}\" SET " +
                                                     $"\"CreatedAt\"='{note.CreatedAt:yyyy-MM-ddTHH\\:mm\\:ss.fffz}', " + 
                                                     $"WHERE \"Id\" = {note.Id}",
                cancellationToken);

            return true;
        }

        public async Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            await connection.ExecuteAsync($"DELETE FROM \"{_tableName}\"" +
                                          $"WHERE \"Id\" = {id}", 
                cancellationToken);

            return true;
        }
    }
}
