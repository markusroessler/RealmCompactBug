using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Realms;

namespace RealmCompactBugTest;

public partial class MyRealmObject : IRealmObject
{
    [PrimaryKey]
    public long Id { get; set; }

    public string? MyProperty { get; set; }
}
