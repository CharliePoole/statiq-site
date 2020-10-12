Title: "NUnit's Generic Test Fixtures"
Published: 03 Jul 2008
Tags: [NUnit,It's the Tests]
---
One recent addition to NUnit 2.5 is the ability to define generic test fixtures, allowing the same fixture to be reused for multiple types that implement the same interface or even just having common method signatures. For example, the following code tests multiple implementations of IList.

```csharp
[TestFixture(typeof(ArrayList))]
[TestFixture(typeof(List&lt;int&gt;))]
public class IList_Tests&lt;TList&gt; where TList : IList, new()
{
  private IList list;

  [SetUp]
  public void CreateList()
  {
    this.list = new TList();
  }

  [Test]
  public void CanAddToList()
  {
    list.Add(1); list.Add(2); list.Add(3);
    Assert.AreEqual(3, list.Count);
  }
}
```

NUnit will create an tree branch containing two fixtures, one that uses ArrayList and another using List<int>. Combined with features like TestCase, this can be quite powerful.

This feature is available in the current source code and will be included in the Alpha-3 release.

---

### Comments

---

[...] NUnit’s Generic Test Fixtures - Charlie Poole highlights a newly added feature in NHibernate 2.5. This Generic Test Fixture functionality will be included in the forthcoming alpha 3 release. [...]
>Reflective Perspective - Chris Alcock &raquo; The Morning Brew #128, Wednesday, July 2, 2008
