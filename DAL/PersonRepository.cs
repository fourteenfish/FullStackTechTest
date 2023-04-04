using System.Data;
using System.Reflection.PortableExecutable;
using System.Text;
using Models;
using MySql.Data.MySqlClient;

namespace DAL;

public class PersonRepository : IPersonRepository
{
    public async Task<List<Person>> ListAllAsync()
    {
        var peopleList = new List<Person>();
        
        var sql = new StringBuilder();
        sql.AppendLine("SELECT * FROM people");

        await using (var connection = new MySqlConnection(Config.DbConnectionString))
        {
            await connection.OpenAsync();
            
            var command = new MySqlCommand(sql.ToString(), connection);
            
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                peopleList.Add(PopulatePerson(reader));
            }
        }

        return peopleList;
    }

    public async Task<Person> GetByIdAsync(int personId)
    {
        var person = new Person();
        
        var sql = new StringBuilder();
        sql.AppendLine("SELECT * FROM people");
        sql.AppendLine("WHERE Id = @personId");

        await using (var connection = new MySqlConnection(Config.DbConnectionString))
        {
            await connection.OpenAsync();

            var command = new MySqlCommand(sql.ToString(), connection);
            command.Parameters.AddWithValue("personId", personId);

            var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                person = PopulatePerson(reader);
            }
        }

        return person;
    }

    public async Task<Person> GetByGMCAsync(int GMC)
    {
        var person = new Person();

        var sql = new StringBuilder();
        sql.AppendLine("SELECT * FROM people");
        sql.AppendLine("WHERE GMC = @GMC");

        await using (var connection = new MySqlConnection(Config.DbConnectionString))
        {
            await connection.OpenAsync();

            var command = new MySqlCommand(sql.ToString(), connection);
            command.Parameters.AddWithValue("GMC", GMC);

            var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                person = PopulatePerson(reader);
            }
        }

        return person;
    }


    public async Task SaveAsync(Person person)
    {
        var sql = new StringBuilder();
        sql.AppendLine("UPDATE people SET");
        sql.AppendLine("FirstName = @firstName,");
        sql.AppendLine("LastName = @lastName,");
        sql.AppendLine("GMC = @gmc");
        sql.AppendLine("WHERE Id = @personId");
        
        await using (var connection = new MySqlConnection(Config.DbConnectionString))
        {
            await connection.OpenAsync();

            var command = new MySqlCommand(sql.ToString(), connection);
            command.Parameters.AddWithValue("firstName", person.FirstName);
            command.Parameters.AddWithValue("lastName", person.LastName);
            command.Parameters.AddWithValue("gmc", person.GMC);
            command.Parameters.AddWithValue("personId", person.Id);

            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task<int> InsertAsync(Person person)
    {
        var sql = new StringBuilder();
        sql.AppendLine("Insert INTO people (`FirstName`,`LastName`,`GMC`) VALUES (");
        sql.AppendLine("@firstName,");
        sql.AppendLine("@lastName,");
        sql.AppendLine("@gmc");
        sql.AppendLine(")");

        await using (var connection = new MySqlConnection(Config.DbConnectionString))
        {
            await connection.OpenAsync();

            var command = new MySqlCommand(sql.ToString(), connection);
            command.Parameters.AddWithValue("firstName", person.FirstName);
            command.Parameters.AddWithValue("lastName", person.LastName);
            command.Parameters.AddWithValue("gmc", person.GMC);

            await command.ExecuteNonQueryAsync();

            //return the new ID
            return (int)command.LastInsertedId;
        }
    }

    private Person PopulatePerson(IDataRecord data)
    {
        var person = new Person
        {
            Id = int.Parse(data["Id"].ToString()),
            FirstName = data["FirstName"].ToString(),
            LastName = data["LastName"].ToString(),
            GMC = int.Parse(data["GMC"].ToString())
        };
        return person;
    }
}