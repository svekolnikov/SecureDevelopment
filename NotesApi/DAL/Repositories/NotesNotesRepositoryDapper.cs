using NotesApi.DAL.Interfaces.Repositories;
using Dapper;
using Lesson1.DAL.Models;
using Npgsql;

namespace NotesApi.DAL.Repositories
{
    public class NotesNotesRepositoryDapper : INotesRepositoryDapper
    {
        private readonly string _connectionString;
        private readonly string _tableName = "Notes";

        public NotesNotesRepositoryDapper(IConfiguration configuration, ILogger<NotesNotesRepositoryDapper> logger)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<IEnumerable<Note>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);
            return await connection
                .QueryAsync<Note>($"SELECT \"Id\", \"Content\", \"CreatedAt\", \"UpdatedAt\" FROM \"{_tableName}\"", 
                    cancellationToken);
        }

        public async Task<Note?> GetById(int id, CancellationToken cancellationToken = default)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);
            var result = await connection
                .QueryFirstOrDefaultAsync<Note>($"SELECT \"Id\", \"Content\", \"CreatedAt\", \"UpdatedAt\" FROM \"{_tableName}\"" +
                                                $"WHERE \"Id\" = {id}", cancellationToken);
            return result;
        }

        public async Task<int> AddAsync(Note note, CancellationToken cancellationToken = default)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);
            await connection.ExecuteAsync($"INSERT INTO \"{_tableName}\" (\"Content\", \"CreatedAt\", \"UpdatedAt\")" +
                                          $" VALUES (" +
                                          $"'{note.Content}', " +
                                          $"'{note.CreatedAt:yyyy-MM-ddTHH\\:mm\\:ss.fffz}', " +
                                          $"'{note.UpdatedAt:yyyy-MM-ddTHH\\:mm\\:ss.fffz}')",
                cancellationToken);

            return 0;
        }

        public async Task<bool> UpdateAsync(Note note, CancellationToken cancellationToken = default)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            var entity = await connection.QueryAsync($"UPDATE \"{_tableName}\" SET " +
                                                     $"\"Content\"='{note.Content}', " +
                                                     $"\"CreatedAt\"='{note.CreatedAt:yyyy-MM-ddTHH\\:mm\\:ss.fffz}', " +
                                                     $"\"UpdatedAt\"='{note.UpdatedAt:yyyy-MM-ddTHH\\:mm\\:ss.fffz}'" + 
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
