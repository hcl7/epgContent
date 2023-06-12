using System;
using System.Collections.Generic;
using System.Text;

class SqlConnectionString
{
    private static string datasource = "";
    private static string[] database = { "" };
    private static string username = "";
    private static string password = "";
    private static string cstring = "";

    public static string Get(int i)
    {
        cstring = @"Data Source=" + datasource + ";Initial Catalog=" + database[i] + ";Persist Security Info=True;User Id=" + username + ";Password=" + password;
        return cstring;
    }
}
