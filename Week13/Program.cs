using System.Data.SQLite;

ReadDatabase(ConnectToDatabase());
//InsertCustomer(ConnectToDatabase());
//RemoveCustomer(ConnectToDatabase());

static SQLiteConnection ConnectToDatabase()
{
    SQLiteConnection Connection = new SQLiteConnection("Data Source = mydb.db; Version = 3; New = True; Compress = True;");

    try
    {
        Connection.Open();
        //Console.WriteLine("Database found.");
    }
    catch
    {
        Console.WriteLine("Database not found.");
    }
    return Connection;
}

static void ReadDatabase(SQLiteConnection ConnectToDB)
{
    Console.Clear();
    SQLiteDataReader Reader;
    SQLiteCommand Command;

    Command = ConnectToDB.CreateCommand();
    Command.CommandText = ("SELECT rowid, * FROM Customer");

    Reader = Command.ExecuteReader();

    while (Reader.Read())
    {
        string ReadStringRowId = Reader["rowid"].ToString();
        string ReaderStringFirstName = Reader.GetString(1);
        string ReaderStringLastName = Reader.GetString(2);
        string ReaderStringDoB = Reader.GetString(3);

        Console.WriteLine($"{ReadStringRowId}. Full name: {ReaderStringFirstName} {ReaderStringLastName}, born in: {ReaderStringDoB}.");
    }
    ConnectToDB.Close();
}

static void InsertCustomer(SQLiteConnection ConnectToDB)
{
    SQLiteCommand Command;

    string FName, LName, DoB;

    Console.WriteLine("To add a customer enter the details below.");

    Console.WriteLine("Enter a first name:");
    FName = Console.ReadLine();

    Console.WriteLine("Enter a last name:");
    LName = Console.ReadLine();

    Console.WriteLine("Enter the date of birth (yyyy-mm-dd):");
    DoB = Console.ReadLine();

    Command = ConnectToDB.CreateCommand();
    Command.CommandText = ($"INSERT INTO customer (firstname, lastname, dateofbirth)\r\n" +
        $"VALUES (\"{FName}\", \"{LName}\", \"{DoB}\");");

    int RowInserted = Command.ExecuteNonQuery();
    Console.WriteLine($"{RowInserted} row(s) have been inserted.");

    ReadDatabase(ConnectToDB);
}

static void RemoveCustomer(SQLiteConnection ConnectToDB)
{
    SQLiteCommand Command;

    string DeleteCustomer;
    Console.WriteLine("Enter an Id to delete a customer:");

    DeleteCustomer = Console.ReadLine();

    Command = ConnectToDB.CreateCommand();
    Command.CommandText = ($"DELETE FROM customer WHERE rowid = {DeleteCustomer}");

    int RowRemoved = Command.ExecuteNonQuery();
    Console.WriteLine($"{RowRemoved} was deleted from the table Customer.");

    ReadDatabase(ConnectToDB);
}