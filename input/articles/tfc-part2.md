Title: "Test-First Challenge - Basic Formulas"
Published: 01/03/2002
Tags: [Extreme Programming, TDD]
---
### Handling More Than One Cell


[Part 1](/articles/tfc-part1.html) was originally done with only one
string holding the content for all the cells. As expected, this broke on the
first test of Part 2 (testManyCells) and some refactoring was required.

> _Bill later moved this test to Part 1, but these pages reflect the original tests._

A container of some kind was needed, and there seemed to be a choice to be
made about whether to structure it as a two-dimensional array or a simple map.
Since the array would need to be sparse, it seemed complicated. So I added
a standard map from cell name to value to the Sheet class.

At this point, I considered adding a cell object but didn't. I'm keeping in my
mind the idea that the map may eventually contain Cells rather than Strings. At
this point, the code looks like this.

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
    std::map(String, String) m_content;
};

String Sheet::get( const String & )
{
    String content = m_content[cellName];

    if ( content.isNumeric() )
        return content.trim();
    else
        return content;
}

String Sheet::getLiteral( const String& cellName )
{
    return m_content[cellName];
}

void Sheet::put( const String & cellName, const String& cellValue )
{
    m_content[cellName] = cellValue;
}
```

### Adding a Cell Class

The second test (testFormulaSpec) only required adding a getFormula method with
an identical implementation to getLiteral() from the previous
section. However, the third test (testConstantFormula)
introduced a new behavior of get(). Looking ahead at the rest of the tests, I see
I'm going to be doing a some sort of parsing. For this test to pass, however,
I only need to drop the initial `=`. I decide to do a little refactoring
so that the function will be in the right place.

I feel driven toward a Cell object that knows how to interpret what's in it. I
refactor all the existing behavior of the Sheet that seems to belong to Cell.

> _By the way, don't you hate how Visual Studio wants to drop the "C" and create files named ell.cpp and ell.h? Since Microsoft classes all start with that letter, they assume you're doing the same thing.

This changes the Sheet class again, but only in its implementation. Now it delegates
everything except keeping track of cells to the Cell class. I like that better.

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
    std::map(String, Cell) m_content;
};

String Sheet::get( const String & )
{
    return m_content[cellName].get();
}

String Sheet::getLiteral( const String& cellName )
{
    return m_content[cellName].getLiteral;
}

String Sheet::getFormula( const String& cellName )
{
    return m_content[cellName].getLiteral;
}

void Sheet::put( const String & cellName, const String& cellValue )
{
    m_content[cellName].put(cellValue);
}
```

The Cell class looks has code that's actually quite similar to what was in
Sheet for [Part 1](/articles/tfc-part1.html). The main difference is
that the methods don't take the cell name as an argument. I decided that
at this point, the cell wouldn't know it's name. If I need it, I can add
it later.

I added a little function to see if the content of the cell is a formula and
another to evaluate the formula. They're only one line each but I think the
names add value to the code. I'm not entirely happy that isFormula is part
of Cell while isNumeric is in String. But it makes some degree of sense, so
I'm leaving it. There's still no parser, but I'm ready to add one and have
a place to put it.

```c++
class Cell
{
public:
    Cell();
    virtual ~Cell();

    String get();
    String getLiteral();
    String getFormula();

    void put( const String& cellValue );

private:
    String m_content;

    bool isFormula();
    String evalFormula();
};

String Cell::get()
{
    if ( m_content.isNumeric() )
        return m_content.trim();
    else
        return m_content;
}

String Cell::getLiteral()
{
    return m_content;
}

String Cell::getFormula()
{
    return m_content;
}

void Cell::put( const String& cellValue)
{
    m_content = cellValue;
}

bool Cell::isFormula()
{
    return m_content[0] == '=';
}

String Cell::evalFormula()
{
    return m_content.substr(1);
}
```

### Simple Expression Parsing

So far, we have been faking it - not really parsing the formula at all.
Begining with testParentheses, it's no longer useful to avoid  creating a
parser. But first, I need to create code to call the parser. A small
modification to Cell::evalFormula serves to do this.

```c++
String Cell::evalFormula()
{
    if ( isFormula() )
    {
        FormulaParser parser( m_content );
        int value;
        if ( parser.eval( value ) )
            return String::toString( value );
    }

    return String("#Error");
}
```

Now evalFormula is for real. The FormulaParser will evaluate the formula and
return an integer. Looking ahead a tiny bit, it seems handy to make the
translation from integer to string here. The integer value is returned using
a reference argument. A more general approach would be to use a getter so
that the type of the value might be determined dynamically. I could also
use a result object for the same purpose but I'm waiting to see what happens.
I also looked ahead to pick the error string to return here.

By the way, the static function String::toString is a template function
I added to my String class in order to keep things at a relatively high level
in this code. I stole the code from CppUnit.

Anyway, Now all we need is a FormulaParser that does matches what evalFormula
expects. Here's my first cut.

```c++
class FormulaParser
{
public:
    FormulaParser( const String& );
    virtual ~FormulaParser();

    bool eval( const String& formula, int& value );
}

bool FormulaParser::eval( int& value )
{
    value = 7;
    return true;
}
```

So what do you want already? It passes all the tests. The constructor saves the
formula, but doesn't use it anywhere. Next, I add a new test
to testConstantExpression that put's "=37" into the cell. Gee, now it fails so
I have to do some work.

> _I've always done my programming in pretty small steps. To see how programming test-first feels, I want to keep the steps as small or smaller, but with tests driving things. So far, my impression is that I move along pretty fast, even with the tests. I see myself focusing better and I'm not leaving errors in the code - at least as far as the tests show._

If I'd never done any parsing, I don't know if I'd introduce lexical tokens here.
But I have, and I am. I need to be able to see the text and type of the next
token as I parse the formula. So far, I only need two types of tokens: integer
constants and operators. So that's all I'll do. First, here's the class that
holds information about a token.

```c++
class LexicalToken
{
public:
    LexicalToken(int type = TT_UNKNOWN, int value = OT_UNKNOWN)
        : m_type(type), m_value(value) {}
    ~LexicalToken();

    bool isOperator() { return m_type == TT_OPERATOR; }
    bool isOperator(int opType)
        { return m_type == TT_OPERATOR && m_value == optype; }
    bool isIntConst() { return m_type == TT_INTCONST; }

    int getIntValue(){ return m_value; }
    int getOperator(){ return m_value; }

private:
    int m_type;
    int m_value;
}

```

It's little more than a struct with some protection. It has no setters and it
can't change once it's created.
The lexical analyzer that produces these tokens could reasonably be another object.
However, I decided to just have a private method
of FormulaParser read the tokens. This turned out to be a mistake, as I'll
explain, but first I'll show the revisions to FormulaParser.

This parser needs to evaluate an expression consisting of an integer wrapped
in one or more levels of parentheses. It needs to prime the pump by reading
a token, and needs to always read a new token whenever it finishes processing
one. Here it is.</p>

```c++
class FormulaParser
{
public:
    FormulaParser( const String& );
    virtual ~FormulaParser();

    bool eval( int& value );

private:
    std::istringstream m_ist;
    LexicalToken m_token;

    void readNextToken();

    bool evalExpression( int& value );
};

bool FormulaParser::eval( int& value )
{
    if ( m_ist.eof() )
        return false;
    
    char ch;
    m_ist >> ch;

    if ( ch != '=' )
        return false;

    readNextToken();
    return evalExpression( value );
}

void FormulaParser::readNextToken()
{
    static String operators("()+*");

    while ( !m_ist.eof() )
    {
        char ch;
        m_ist >> ch;

        if ( !isspace( ch ) )
        {
            if ( isdigit( ch ) )
            {
                int value;

                m_ist.unget();
                m_ist >> value;

                m_token = LexicalToken( TT_INTCONST, value );
                return;
            )
            else if ( operators.find( ch ) >= 0 )
            {
                m_token = LexicalToken( TT_OPERATOR, ch );
                return;
            }
        }
    }

    m_token = LexicalToken( TT_EOF, 0 );
    return;
}

bool FormulaParser::evalExpression( int& value )
{
    bool itsOK = false;

    if ( m_token.isIntConst() )
    {
        value = m_token.getIntValue();
        itsOK = true;
    }
    else if ( m_token.isOperator( OT_LPAREN ) )
    {
        readNextToken();
        if ( evalExpression( value ) )
        {
            readNextToken();
            if ( m_token.isOperator( OT_RPAREN ) )
                itsOK = true;
        }
    }

    return itsOK;
}
```

Basically, the eval routine sets things up and calls evalExpression.
If evalExpression finds an integer, it just returns the value. But  if
it finds a left parenthesis, it reads a token and calls itself recursively.
After that call succeeds, it expects the next token to be a right parenthesis.
There is probably an error in there regarding reading the next token at all the 
right places, but further tests will show that.

Getting the token reading to be correct was hard. Since I made it a private
routine, I would have to go to extra trouble to test it separately. I didn't go
to the trouble, so it took me longer than it might have. If it were a public
method of a separate Lexer object, it could have it's own test suite.

### Adding More Operators

Now that the infrastructure for parsing is in place, it's time to make more
use of it. The first test
I added was testMultiply. After it failed, I decided it would be handy to
factor create a method from the code in evalExpression that takes care of
integers. That made the code look like this:

```c++
bool FormulaParser::evalExpression( int& value )
{
    if ( m_token.isOperator( OT_LPAREN ) )
    {
        readNextToken();
        if ( evalExpression( value ) )
        {
            readNextToken();
            if ( m_token.isOperator( OT_RPAREN ) )
                itsOK = true;
        }
    }
    else
        return evalFactor();
}

bool FormulaParser::evalIntConst()
{
    if ( m_token.isIntConst() )
    {
        value = m_token.getIntValue();
        itsOK = true;
    }
    else
        return false;
}
```

Of course, testMultiply still fails, but I verified that
everything else works before proceeding. Next, I reorganized the call
relationships between the routines and implemented multiplication and
addition based on the following grammar (informally expressed):

* An expression is a sum of one or more terms.
* A term is a product of one or more factors.
* A factor is either an integer or an expression in parentheses.

So basically, I couldn't stand to keep doing it test by test. Here's the code:</p>

```c++
bool FormulaParser::evalExpression( int& value )
{
    if ( !evalTerm( value ) )
        return false;

    while ( m_token.isOperator( OT_PLUS ) )
    {
        readNextToken();
        int termValue;
        if ( !evalTerm( termValue ) )
            return false;
        value += termValue;
    }

    return true;
}

bool FormulaParser::evalTerm( int& value )
{
    if ( !evalFactor( value ) )
        return false;

    while ( m_token.isOperator( OT_TIMES ) )
    {
        readNextToken();
        int factorValue;
        if ( !evalFactor( factorValue ) )
            return false;
        value *= factorValue;
    }

    return true;
}

bool FormulaParser::evalFactor( int& value )
{
    if ( m_token.isIntConst() )
    {
        value = m_token.getIntValue();
        readNextToken();
        return true;
    }
    else if ( m_token.isOperator( OT_LPAREN ) )
    {
        readNextToken();
        if ( evalExpression( value ) && m_token.isOperator( OT_RPAREN ) )
        {
            readNextToken();
            return true;
        }
    }

    return false;
}
```

The tests showed a few errors which I proceeded to fix. The full set ran pretty
quickly. My conclusion is that it can be easier to do a group of inter-related
items all at once rather than one test at a time. Obviously, if I got into
trouble, it wouldn't be simple to fix.

Adding subtraction and division was pretty simple. Here's the code for
doing division:

```c++
bool FormulaParser::evalTerm( int& value )
{
    if ( !evalFactor( value ) )
        return false;

    while ( m_token.isOperator() )
    {
        int op = m_token.getOperator();
        if ( op != OT_TIMES && op != OT_DIVIDE )
            break;

        readNextToken();
        int factorValue;
        if ( !evalFactor( factorValue ) )
            return false;
        if ( op == OT_TIMES )
            value *= factorValue;
        else if ( factorValue == 0 )
            return false;
        else
            value /= factorValue;
    }

    return true;
}
```

Subtraction is easier since it doesn't need the divide-by-zero check.

At this point, we are able to parse and evaluate constant expressions.
To be useful, our program needs to allow cell references in formulas.
We'll do this in [Part 3](/articles/tfc-part3.html)
