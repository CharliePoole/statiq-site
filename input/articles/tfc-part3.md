Title: "Test-First Challenge - Cell References"
Published: 01/07/2002
Tags: [Extreme Programming, TDD]
---
### Formulas with Cell References


Having built the parser in [Part 2](/articles/tfc-part2.html) and cleaned
it up in my [Refactoring Interlude](/articles/tfc-interlude1.html), I'm now
ready to handle references to cells in the formulas. First step is to have a new
token type that handles "names". I made these be any span of alphanumerics
starting with an alpha. This is actually a superset of the pattern called for
in Bill's tests, but it can be  changed later if necessary. Here's the new
version of LexicalToken.

```c++
class LexicalToken
{
public:
    LexicalToken(int type = TT_UNKNOWN, int value = OT_UNKNOWN);
    virtual ~LexicalToken();

    void readNext( std::istream& ist );

    bool isOperator();
    bool isOperator(int opType);
    bool isIntConst();
    bool isName();

    int getIntValue();
    int getOperator();
    String getName();

private:
    String m_name;
    int m_type;
    int m_value;
}

void LexicalToken::readNext( std::istream& ist )
{
    char ch;
    static String operators( "()+*-/" );

    m_name = "";
    m_type = TT_EOF;
    m_value = 0;

    do {
        if ( ist.eof() )
            return;

        ist >> ch;
    } while ( isspace( ch ) );

    if ( isdigit( ch ) )
    {
        ist.unget();
        m_type = TT_INTCONST;
        ist >> m_value;
        return;
    }
    else if ( isalpha( ch ) )
    {
        m_name += ch;

        while ( !ist.eof() && isalnum( ist.peek() )
        {
            ist >> ch;
            m_name += ch;
        }

        m_type = TT_NAME;
        m_value = ch;

        return;
    }
    else if ( operators.find( ch ) >= 0 )
    {
        m_type = TT_OPERATOR;
        m_value = ch;
        return;
    }  

    return;
}
```

Next, the parser has to do something with the cell reference when found. This
calls for a change in evalFactor.

```c++
bool FormulaParser::evalFactor( std::istream& ist, int& value )
{
    if ( m_token.isIntConst() )
    {
        value = m_token.getIntValue();
        ist >> m_token;
        return true;
    }
    else if ( m_token.IsName() )
    {
        String cellName = m_token.getName();
        String cellContent = m_sheet->getLiteral( cellName );
        if ( isFormula( cellContent ) && eval( cellContent, value ) )
        {
            ist >> m_token;
            return true;
        }
        else if ( cellContent.isNumeric() )
        {
            value = atoi( cellContent.c_str() )
            ist >> m_token;
            return true;
        }
    }
    else if ( m_token.isOperator( OT_LPAREN ) )
    {
        ist >> m_token;
        if ( evalExpression( value ) && m_token.isOperator( OT_RPAREN ) )
        {
            ist >> m_token();
            return true;
        }
    }

    return false;
}
```

Because of OAOO, the static method isFormula has been moved from Sheet,
hich now must call it as FormulaParser::isFormula. Using atoi to evaluate
a numeric cell is expedient, but not very pretty. I may change it later.

### Detecting Recursion


In my solution so far, putting a circular reference into a cell works fine - no crash.
That makes sense since I don't do anything with it in the put(). But as soon as
I try to evaluate it, I get a stack overflow. So I need to put in some code
to make sure that I stop when I get a circular reference.

I spiked a solution that involved saving the names of the current cells under
evaluation in a list. That worked but I was bothered by needing to pop the
list before exiting the evaluation function. I created a little object to do
the check and pop the list in its destructor.

```c++
class CircularityChecker
{
public:
    CircularityChecker( String cellName ) : m_ok( false )
    {
        if ( std::find( m_cells.begin(), m_cells.end(), cellName ) == m_cells.end() )
        {
            m_cells.push_back( cellName );
            m_ok = true;
        }
    }
    ~CircularityChecker()
    {
        if ( m_ok && !m_cells.empty() )
            m_cells.pop_back()
    }

    bool isOK() { return m_ok; }

private:
    static std::list&lt;String&gt; m_cells;
    bool m_ok;
}
```

I renamed my FormulaParser::eval method to evalFormula and added a new
method evalCell which does the check for circularity before calling
evalFormula. Evaluation of numeric cells moved into evalCell as well. Sheet
now calls evalCell to get things started.

```c++
bool FormulaParser::evalCell( const String& cellName )
{
    String cellContent = m_sheet.getLiteral( cellName );

    if ( isFormula( cellContent ) )
    {
        CircularityChecker check( cellName );
        if ( check.isOK() )
            return evalFormula( cellContent, value );
        else
            m_error = "#Circular";
    }
    else if ( cellContent.isNumeric() )
    {
        value = atoi( cellContent.c_str() );
        return true;
    }

    return false;
}

bool FormulaParser::evalFormula( const String& formula, int& value )
{
    std::istringstream ist( formula );

    if ( ist.eof() )
        return false;

    char ch;
    ist >> ch;

    if ( ch != '=')
        return false;

    ist >> m_token;

    return evalExpression( ist, value );     
}
```

You'll note that I gave FormulaParser a new member to allow
saving the type of error encountered. If the evaluation fails, this string
is what Sheet::get returns - default is "#Error". I'm not completely
satisfied with this but the tests all pass, so I'll wait till I need to
change it.
