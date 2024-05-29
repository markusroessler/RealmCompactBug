using Realms;

namespace RealmCompactBugTest;

public class Tests
{
    [Test]
    public void Test_CompactOnLaunch()
    {
        var baseDir = Path.Combine(Environment.CurrentDirectory, $"{TestContext.CurrentContext.Test.Name}");
        if (Directory.Exists(baseDir))
            Directory.Delete(baseDir, true);
        Directory.CreateDirectory(baseDir);

        var realmConfig = new RealmConfiguration(Path.Combine(baseDir, "my.realm"))
        {
            ShouldCompactOnLaunch = (_, _) => true,
            ShouldDeleteIfMigrationNeeded = true
        };

        using (var realm = Realm.GetInstance(realmConfig))
        {
            realm.Write(() =>
            {
                realm.Add(new MyRealmObject { Id = 42, MyProperty = "foobar" });
            });
        }

        // passes
        using (var realm = Realm.GetInstance(realmConfig))
        {
            var ro = realm.Find<MyRealmObject>(42);
            Assert.That(ro, Is.Not.Null);
            Assert.That(ro.MyProperty, Is.EqualTo("foobar"));
        }

        // fails, because the realm gets cleared
        using (var realm = Realm.GetInstance(realmConfig))
        {
            var ro = realm.Find<MyRealmObject>(42);
            Assert.That(ro, Is.Not.Null);
            Assert.That(ro.MyProperty, Is.EqualTo("foobar"));
        }
    }
}