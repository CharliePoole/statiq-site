Title: "Test-First Challenge - Getting Started"
Published: 01/01/2002
Tags: [Extreme Programming, TDD]
---
Bill Wake created the [Test-first Challenge](http://xp123.com/xplor/xp0201/)
as a way for people to practice [test-first](http://www.c2.com/cgi/wiki?TestFirst).
He provided a set of tests that drive the development of a simple spreadsheet
application. A number of people who frequent the extreme programming list wrote
code in response to Bill's tests, including me.

I'm using C++, so first off I had to spend some time initially getting CppUnit
to work correctly on my system. It's a bit annoying how the assertEquals requires
two objects of the same type. At least in VC 6.0, you can't use string constants
to the comparison with string objects. I think it's because assertEquals is a 
template, and VC is not doing the matching 100% correctly. For now, I'm not trying
to fix it, so I have to use String("Some string") in various places.

Even though I know Bill regrets using only one cell for all the tests, I didn't
deal with it in part 1. My initial version for part 1 looks pretty
much like everyone else's.

```c++
class Sheet
{
public:
    Sheet();
    virtual ~Sheet();

    String get( const String& cellName ); 
    String getLiteral( const String& cellName );

    void put( const String& cellName, const String& cellValue );
private:
    String m_content;
};

String Sheet::get( const String & )
{
    if ( m_content.isNumeric() )
        return m_content.trim();
    else
        return m_content;
}

String Sheet::getLiteral( const String& cellName )
{
    return m_content
}

void Sheet::put( const String & cellName, const String& cellValue )
{
    m_content = cellValue;
}
```

The isNumeric and trim functions were originally part of Sheet with slightly
different names. All the functions used std::string directly. After all the
tests passed, I decided to refactor by deriving a String class from std::string
and making isNumeric and trim members. I confess that I didn't write tests
for the String class but I think the existing tests cover it well enough.

Things get a bit more interesting in [Part 2](/articles/tfc-part2.html)
