Title: "Test-First Challenge - Refactoring Interlude"
Published: 01/05/2002
Tags: [Extreme Programming, TDD]
---
In [Part 2](/articles/tfc-part2.html) I developed a parser that handles integer
constants and some operators. In doing this, I built up some design debt and
I now need to refactor. In addition, [Part 3](/articles/tfc-part3.html) now
calls for formulas to handle references to other cells. This will call for
some preliminary refactoring based on some quick design thinking. On this page
I describe both refactorings and also discuss how I feel about all this change.

I'm not happy with the fact that the parser extracts tokens or with the placement
of this function in a private method. I'd rather have the parser just read
tokens. At first, I considered a separate lexical analyzer object, but then
a simpler solution came to mind: let the LexicalToken object initialize itself
by reading from a stream.

I created a public method LexicalToken::readNext() and moved
the code from `FormulaParser::readNextToken`. Where the old code did an
assignment to m_token, the new code just sets the objects fields correctly.
Then I overloaded operator `>>` so that I could use normal C++ input to read
a token from a stream. This seems right.

```c#
void LexicalToken::readNext( std::istream&amp; ist )
{
    char ch;
    static String operators( "()+*-/" );

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
    else if ( operators.find( ch ) >= 0 )
    {
        m_type = TT_OPERATOR;
        m_value = ch;
        return;
    }  
	
    return;
}

std::istream&amp; operator >>( std::istream&amp; ist, LexicalToken&amp; token )
{
    token.readNext( ist );
    return ist;
}
```

Now the lexical analyzer is clearly separated from the parser and ready
to be extended for new token types. Each of the parser routines needs to change
to use the standard C++ extraction operator a token is needed. However, some
further refactoring is needed.

For part 3, the parser needs to deal with formulas that include cell references.
This presents a bit of a problem because a cell presently has no way to access
other cells - in fact it doesn't even know its own name. In addition, dealing
with circular references calls for a broader view of the evaluation process
than a single cell can easily take. So, after a quick design session, I elected
to remove the Cell object from the solution and put all of its function back
into the Sheet. This undoes the effect of an earlier refactoring. This gives
the classes the following responsibilities:

* Sheet handles overall control, cell lookup and cell content access.
* FormulaParser evaluates cells that contain formulas for the Sheet.
* LexicalToken provides input for the FormulaParser.

I could conceivably combine Sheet and FormulaParser but the functions seem
quite separate and I'll live with their needing to reference one another.
Note that FormulaParser now gets a pointer to Sheet in its constructor. Since
it may be called recursively to parse formulas within formulas, the formula
to be parsed is passed as an argument to the eval function.

```c#
bool FormulaParser::eval( <b>const String&amp; formula, int&amp; value )
{
    std::stringstream ist( formula );</b>

    if ( ist.eof() )
        return;

    char ch;
    ist >> ch;

    if ( ch != '=' )
        return false;

    ist >> m_token;

    return evalExpression( ist, value );
}

bool FormulaParser::evalTerm( <b>std::istream&amp;,</b> int&amp; value )
{
    if ( !evalFactor( <b>ist,</b> value ) )
        return false;

    while ( m_token.isOperator() )
    {
        int op = m_token.getOperator();
        if ( op != OT_TIMES &amp;&amp; op != OT_DIVIDE )
            break;

        ist >> m_token;

        int factorValue;
        if ( !evalFactor( <b>ist</b>, factorValue ) )
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

The changes to evalFactor and evalExpression are similar. The Sheet class
remains quite simple. Here's the code.

```c#
class Sheet
{
public:
    Sheet();
    virtual ~Sheet();

    static bool isFormula( const String&amp; content );

    String get( const String&amp; cellName ); 
    String getLiteral( const String&amp; cellName );

    void put( const String&amp; cellName, const String&amp; cellValue );

private:
    std::map(String, <b>String</b>) m_content;
};

bool Sheet::isFormula( const String&amp; content )
{
    return content[0] == '=';
}

String Sheet::get( const String &amp; )
{
    const String&amp; content = m_content[cellName];

    if ( isFormula( content ) )
    {
        FormulaParser parser( this );
        int value;
        if ( parser.eval( content, value ) )
            return String::toString( value );
        else
            return "#Error";
    }
    else if ( content.isNumeric() )
        return content.trim();
    else
        return content;
}

String Sheet::getLiteral( const String&amp; cellName )
{
    return m_content[cellName];
}

void Sheet::put( const String &amp; cellName, const String&amp; cellValue )
{
    m_content[cellName] = cellValue;
}
```

Much of this looks familiar - like Sheet before I
introduced the Cell class. So, was introducing Cell a mistake? No, I needed
it when I did it. Now a new requirement leads me to move
functionality out of Cell. There isn't enough left to justify Cell, so I
am dropping it.

What about the time wasted? Well, it took minutes to add Cell, and
minutes to get rid of it. Having Cell made things simpler for me at the time.
It made writing the code faster - actually writing these pages takes lots longer
writing the code.

I hadn't really done test-first before in any systematic way. I have
generally coded in very small chunks and tested after. The up-front tests
seem to provide a better structure for my coding. I also have had a pattern
of refactoring my code frequently - although I didn't always call it that.
The tests make me even more ready to do it. All in all, I'm pretty happy
with what's coming out of this challenge. Now, on to [Part 3](/articles/tfc-part3.html).
