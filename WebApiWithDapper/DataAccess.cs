using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;
using WebApiWithDapper.Models;

namespace WebApiWithDapper
{
    public class DataAccess
    {
        string conStr = ConfigurationManager.ConnectionStrings["DBDapperDemo"].ConnectionString;
        public List<Person> GetAllPeople()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(conStr))
            {
                return connection.Query<Person>("GetAllPeople").ToList();
            }
        }
        public Person GetPersonById(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(conStr))
            {
                return connection.Query<Person>("GetPersonById @Id", new { Id = id }).ToList()[0];
            }
        }

        public void InsertPerson(Person person)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(conStr))
            {
                connection.Execute("InsertPerson @firstName, @lastName, @email", new { firstName = person.FirstName, lastName = person.LastName, email = person.Email });
            }
        }

        public void UpdateUser(Person person)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(conStr))
            {
                connection.Execute("UpdatePerson @id, @firstName, @lastName, @email", new { id = person.ID, firstName = person.FirstName, lastName = person.LastName, email = person.Email });
            }
        }

        public void DeletePerson(int ID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(conStr))
            {
                connection.Execute("DeletePerson @id", new { id = ID });
            }
        }
    }
}