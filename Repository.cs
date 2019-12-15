using System;
using System.IO;

public class Repository
{
    public IEnumerable<User> Users => users;
    private List<User> users;
    public Repository()
    {

    }

}
