using System.Data.SqlClient;
using Dal.Interfaces;
using Domain;

namespace Dal
{
    public class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : IBaseEntity
    {
        private readonly string _connectionString;
        private readonly string _tableName;

        public Repository()
        {
            _connectionString = ConnectionStrings.CookBookDB;
            _tableName = typeof(TEntity).Name + "s";
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var commandText = $"SELECT * FROM {_tableName}";
            var command = new SqlCommand(commandText, connection);
            var reader = await command.ExecuteReaderAsync();

            var entities = new List<TEntity>();

            while (await reader.ReadAsync())
            {
                var entity = Activator.CreateInstance<TEntity>();
                foreach (var property in typeof(TEntity).GetProperties())
                {
                    var columnValue = reader[property.Name];
                    if (columnValue != DBNull.Value)
                    {
                        property.SetValue(entity, columnValue);
                    }
                }

                entities.Add(entity);
            }

            return entities;
        }
        
        public async Task<TEntity> GetById(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var commandText = $"SELECT * FROM {_tableName} WHERE Id = @Id";
            var command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", id);
            var reader = await command.ExecuteReaderAsync();

            if (!await reader.ReadAsync()) return default; // or throw an exception if the entity was not found
            var entity = Activator.CreateInstance<TEntity>();
            foreach (var property in typeof(TEntity).GetProperties())
            {
                var columnValue = reader[property.Name];
                if (columnValue != DBNull.Value)
                {
                    property.SetValue(entity, columnValue);
                }
            }

            return entity;

        }

        public async Task Add(TEntity entity)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var properties = typeof(TEntity)
                .GetProperties()
                .Where(p => !p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
            var columns = string.Join(",", properties.Select(p => p.Name));
            var values = string.Join(",", properties.Select(p => $"@{p.Name}"));
            var commandText = $"INSERT INTO {_tableName} ({columns}) VALUES ({values})";
            var command = new SqlCommand(commandText, connection);
            foreach (var property in properties)
            {
                var parameter = new SqlParameter($"@{property.Name}", property.GetValue(entity));
                command.Parameters.Add(parameter);
            }

            await command.ExecuteNonQueryAsync();
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            foreach (var entity in entities)
            {
                var properties = typeof(TEntity)
                    .GetProperties()
                    .Where(p => !p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
                var columns = string.Join(",", properties.Select(p => p.Name));
                var values = string.Join(",", properties.Select(p => $"@{p.Name}"));
                var commandText = $"INSERT INTO {_tableName} ({columns}) VALUES ({values})";
                var command = new SqlCommand(commandText, connection);
                foreach (var property in properties)
                {
                    var parameter = new SqlParameter($"@{property.Name}", property.GetValue(entity));
                    command.Parameters.Add(parameter);
                }

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Update(TEntity entity)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var properties = typeof(TEntity)
                .GetProperties()
                .Where(p => !p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
            var sets = string.Join(",", properties.Select(p => $"{p.Name} = @{p.Name}"));
            var commandText = $"UPDATE {_tableName} SET {sets} WHERE Id = @Id";
            var command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", typeof(TEntity).GetProperty("Id")?.GetValue(entity));
            foreach (var property in properties)
            {
                var parameter = new SqlParameter($"@{property.Name}", property.GetValue(entity));
                command.Parameters.Add(parameter);
            }

            await command.ExecuteNonQueryAsync();
        }

        public async Task Delete(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var commandText = $"DELETE FROM {_tableName} WHERE Id = @Id";
            var command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", id);
            await command.ExecuteNonQueryAsync();
        }
        
        public async Task DeleteRange(IEnumerable<int> ids)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var commandText = $"DELETE FROM {_tableName} WHERE Id IN ({string.Join(",", ids)})";
            var command = new SqlCommand(commandText, connection);
            await command.ExecuteNonQueryAsync();
        }

        // реализация интерфейса IDisposable.
        public void Dispose()
        {
            // подавляем финализацию
            GC.SuppressFinalize(this);
        }
        
    }
}