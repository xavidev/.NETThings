using System;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using TDD.Api;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

public class BootStrap
{
    public BootStrap()
    {
    }

    public void Configure(HttpConfiguration config)
    {
        config.Routes.MapHttpRoute(
            name: "ApiDefault",
            routeTemplate: "{controller}/{id}",
            defaults: new
            {
                //Por convención se omite la palabra Controller. Con ella no
                //funciona.
                controller = "Authentication",
                id = RouteParameter.Optional
            }
            );

        config.Services.Replace(typeof(IHttpControllerActivator), new ControllerActivator());
    }

    public void ActivateBDD()
    {
        var connStr = ConfigurationManager.ConnectionStrings["Authentication"].ConnectionString;
        var builder = new SqlConnectionStringBuilder(connStr)
        {
            InitialCatalog = "Master"
        };

        using (var conn = new SqlConnection(builder.ConnectionString))
        {
            conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                var sqlSchema = this.GetSqlScript();

                foreach (var sql in sqlSchema.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    public void DisableBDD()
    {
        var connStr = ConfigurationManager.ConnectionStrings["Authentication"].ConnectionString;
        var builder = new SqlConnectionStringBuilder(connStr)
        {
            InitialCatalog = "Master"
        };

        using (var conn = new SqlConnection(builder.ConnectionString))
        {
            conn.Open();
            var deleteDB = @"IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'AUTHENTICATION')
                             DROP DATABASE [AUTHENTICATION];";
            using (var cmd = new SqlCommand(deleteDB, conn))
            {
                cmd.ExecuteNonQuery();

            }
        }
    }

    private string GetSqlScript()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "TDD.Api.Schema.sql";
        var result = string.Empty;

        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        using (StreamReader reader = new StreamReader(stream))
        {
            result = reader.ReadToEnd();
            return result;
        }
    }
}