using System;
using System.Collections.Generic;
using System.Text;

class SqlConnectionString
{
    private static string datasource = "aida-pc";
    private static string[] database = { "EPG" };
    private static string username = "seven";
    private static string password = "root@123";
    private static string cstring = "";

    public static string Get(int i)
    {
        cstring = @"Data Source=" + datasource + ";Initial Catalog=" + database[i] + ";Persist Security Info=True;User Id=" + username + ";Password=" + password;
        return cstring;
    }
}