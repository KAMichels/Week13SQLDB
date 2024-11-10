using Microsoft.VisualBasic;
using System.Data.Common;
using System.Data.SQLite;

ReadData(CreateConnection());
//AddCustomer(CreateConnection());
//RemoveCustomer(CreateConnection());

static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db; Version=3; New=True; Compress=True;");

    try
    {
        connection.Open();
        Console.WriteLine("Connection established");
    }
    catch
    {
        Console.WriteLine("DB connection failed");
    }

    return connection;
}
static void ReadData(SQLiteConnection myConnection)
{

    SQLiteDataReader read;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT * FROM customer";

    read = command.ExecuteReader();

    while (read.Read())
    {
        string fName = read.GetString(0);
        string lName = read.GetString(1);
        string dob = read.GetString(2);

        Console.WriteLine($"Full name: {fName} {lName}; Dob: {dob}");
    }

    myConnection.Close();

}

static void AddCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;

    string fName = "Kirsten";
    string lName = "Michels";
    string dob = "16-10-2003";

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth) VALUES('{fName}', '{lName}', '{dob}')";

    int rowInserted = command.ExecuteNonQuery();

    Console.Clear();
    Console.WriteLine($"Rows inserted: {rowInserted}");


    ReadData(myConnection);
    myConnection.Close();
}

static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;

    string idToDelete = "9";

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";

    int rowsDeleted = command.ExecuteNonQuery();

    Console.Clear();
    Console.WriteLine($"Rows deleted: {rowsDeleted}");

    ReadData(myConnection);
    myConnection.Close();
}
