namespace TodoBackend;

using MySqlConnector;

public class DB
{
    private string conString = "datasource = 127.0.0.1; port=3306;username=root;password=qweasd;database=todolist";
    private MySqlConnection con;
    public MySqlConnection MakeConnection()
    {
        con = new MySqlConnection(conString);
        con.Open();
        return con;
    }

    public void CloseConnection()
    {
        con.Close();
        con.Dispose();
    }
}