Title: "Test-First Challenge - Table Model"
Published: 01/10/2002
Tags: [Extreme Programming, TDD]
---
So we're evaluating expressions. Now we are to create a two-dimensional adapter
for Sheet so cells can be referenced by row and column. Since this specific
interface is aimed at a Java-based GUI, I'm not really sure if I need all of it.
But I've put it together quickly using the tests Bill supplied.

```c#
class SheetTableModel
{
public:
    SheetTableModel( Sheet * sheet, int rows=0, int cols=0 );
    virtual ~SheetTableModel();

    int getColumnCount() { return m_cols; }
    int getRowCount() { return m_rows; }

    String getColumnName( int column );
    String getCellName( int row, int column );

    String getValueAt( int row, int column );
    void setValueAt( int row, int column );

private:
    Sheet * m_sheet;

    int m_rows;
    int m_cols;
}

SheetTableModel::SheetTableModel( Sheet * sheet, int rows, int cols )
    : m_sheet( sheet )
{
    m_rows = rows ? rows : LAST_ROW_INDEX + 1;
    m_cols = cols ? cols : LAST_COLUMN_INDEX + 1;
}

String SheetTableModel::getColumnName( int row, int column )
{
    static String alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    if ( column &lt;= 0 || column &gt;= m_cols )
        return "";

    String name = "";
    while ( column > 0 )
    {
        String digit;
        digit += alphabet[ ( column - 1 ) % 26 ];
        name = digit + name;
        column = ( column - 1 ) / 26;
    }

    return name;
}

String SheetTableModel::getCellName( int row, int column )
{
    if ( row &lt; 0 || row &gt; LAST_ROW_INDEX ) return "";
    if ( column &lt;= 0 || column > LAST_COLUMN_INDEX ) return "";

    return getColumnName( column ) + String::toString( row + 1 );
}

String SheetTableModel::getValueAt( int row, int column )
{
    if ( row &lt; 0 || column &lt; 0 ) return "";
    if ( column == 0 ) return String::toString( row + 1 );

    return m_sheet->get( getCellName( row, column ) );
}

void SheetTableModel setValueAt( const String&amp; contents, int row, int column )
{
    m_sheet->put( getCellName( row, column ), contents );
}
```

I made getCellName a separate function for now. It's public so it can be tested.
I ignored the notification of listeners for now since I don't know how I'll
eventually be doing the GUI part of this test.

Even if I don't get much use out of the adapter, it was interesting to do it.
In this case, I worked entirely from the tests without any knowledge of the
particular interface or why things had to work the way they do. This isn't how
I'd like to work all the time, but it's surprising how it came together anyway.

I'm considering how - or whether - to do Part 5 now.
