using System.Data;
using System.Reflection.Metadata;
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
            _connectionString = configuration.GetConnectionString("Data");
        }

        public async Task<IEnumerable<DebitCard>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            var sql = $"SELECT * FROM \"{_tableName}\"";
            var  result = await connection.QueryAsync<DebitCard>(sql, cancellationToken);

            return result;
        }

        public async Task<DebitCard?> GetById(int id, CancellationToken cancellationToken = default)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);
            var result = await connection
                .QueryFirstOrDefaultAsync<DebitCard>($"SELECT * FROM \"{_tableName}\"" +
                                                $"WHERE \"Id\" = @Id", new{Id = id});
            return result;
        }

        public async Task<int> AddAsync(DebitCard entity, CancellationToken cancellationToken = default)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            var sql = $"INSERT INTO \"{_tableName}\" (\"FirstName\", \"LastName\", \"Number\", " +
                      $"\"SecureCode\", \"ExpirationDate\", \"CreatedAt\")" +
                      $" VALUES (@FirstName, @LastName, @Number, " +
                      $"@SecureCode, @ExpirationDate, @CreatedAt)";

            var p = new DynamicParameters();
            p.Add("FirstName", entity.FirstName, DbType.String);
            p.Add("LastName", entity.LastName, DbType.String);
            p.Add("Number", entity.Number, DbType.String);
            p.Add("SecureCode", entity.SecureCode, DbType.Int32);
            p.Add("ExpirationDate", entity.ExpirationDate, DbType.DateTimeOffset);
            p.Add("CreatedAt", entity.CreatedAt, DbType.DateTimeOffset);

            await connection.ExecuteAsync(sql, p);

            return 0;
        }

        public async Task<bool> UpdateAsync(DebitCard entity, CancellationToken cancellationToken = default)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            var sql = $"UPDATE \"{_tableName}\" SET " +
                      $"FirstName = @FirstName, FirstName = @FirstName, LastName = @LastName, Number = @Number, " +
                      $"SecureCode = @SecureCode, ExpirationDate = @ExpirationDate, CreatedAt = @CreatedAt " +
                      $"WHERE \'Id\' = @Id";

            var p = new DynamicParameters();
            p.Add("Id", entity.Id, DbType.Int32);
            p.Add("FirstName", entity.FirstName, DbType.String);
            p.Add("LastName", entity.LastName, DbType.String);
            p.Add("Number", entity.Number, DbType.String);
            p.Add("SecureCode", entity.SecureCode, DbType.Int32);
            p.Add("ExpirationDate", entity.ExpirationDate, DbType.DateTimeOffset);
            p.Add("CreatedAt", entity.CreatedAt, DbType.DateTimeOffset);

            await connection.ExecuteAsync(sql, p);

            return true;
        }

        public async Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            var sql = $"DELETE FROM \"{_tableName}\" WHERE \'Id\' = @Id";

            var p = new DynamicParameters();
            p.Add("Id", id, DbType.Int32);

            await connection.ExecuteAsync(sql, p);

            return true;
        }
    }
}
