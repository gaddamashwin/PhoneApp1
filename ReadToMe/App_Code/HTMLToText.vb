Imports System.Text
Imports System.Web
Imports System.Collections.Generic

''' <summary>
''' Converts HTML to plain text.
''' </summary>
Public Class HtmlToText
    ' Static data tables
    Protected Shared _tags As Dictionary(Of String, String)
    Protected Shared _ignoreTags As HashSet(Of String)

    ' Instance variables
    Protected _text As TextBuilder
    Protected _html As String
    Protected _pos As Integer

    ' Static constructor (one time only)
    Shared Sub New()
        _tags = New Dictionary(Of String, String)()
        _tags.Add("address", vbLf)
        _tags.Add("blockquote", vbLf)
        _tags.Add("div", vbLf)
        _tags.Add("dl", vbLf)
        _tags.Add("fieldset", vbLf)
        _tags.Add("form", vbLf)
        _tags.Add("h1", vbLf)
        _tags.Add("/h1", vbLf)
        _tags.Add("h2", vbLf)
        _tags.Add("/h2", vbLf)
        _tags.Add("h3", vbLf)
        _tags.Add("/h3", vbLf)
        _tags.Add("h4", vbLf)
        _tags.Add("/h4", vbLf)
        _tags.Add("h5", vbLf)
        _tags.Add("/h5", vbLf)
        _tags.Add("h6", vbLf)
        _tags.Add("/h6", vbLf)
        _tags.Add("p", vbLf)
        _tags.Add("/p", vbLf)
        _tags.Add("table", vbLf)
        _tags.Add("/table", vbLf)
        _tags.Add("ul", vbLf)
        _tags.Add("/ul", vbLf)
        _tags.Add("ol", vbLf)
        _tags.Add("/ol", vbLf)
        _tags.Add("/li", vbLf)
        _tags.Add("br", vbLf)
        _tags.Add("/td", vbTab)
        _tags.Add("/tr", vbLf)
        _tags.Add("/pre", vbLf)

        _ignoreTags = New HashSet(Of String)()
        _ignoreTags.Add("script")
        _ignoreTags.Add("noscript")
        _ignoreTags.Add("style")
        _ignoreTags.Add("object")
    End Sub

    ''' <summary>
    ''' Converts the given HTML to plain text and returns the result.
    ''' </summary>
    ''' <param name="html">HTML to be converted</param>
    ''' <returns>Resulting plain text</returns>
    Public Function Convert(ByVal html As String) As String
        ' Initialize state variables
        _text = New TextBuilder()
        _html = html
        _pos = 0

        ' Process input
        While Not EndOfText
            If Peek() = "<"c Then
                ' HTML tag
                Dim selfClosing As Boolean
                Dim tag As String = ParseTag(selfClosing)

                ' Handle special tag cases
                If tag = "body" Then
                    ' Discard content before <body>
                    _text.Clear()
                ElseIf tag = "/body" Then
                    ' Discard content after </body>
                    _pos = _html.Length
                ElseIf tag = "pre" Then
                    ' Enter preformatted mode
                    _text.Preformatted = True
                    EatWhitespaceToNextLine()
                ElseIf tag = "/pre" Then
                    ' Exit preformatted mode
                    _text.Preformatted = False
                End If

                Dim value As String
                If _tags.TryGetValue(tag, value) Then
                    _text.Write(value)
                End If

                If _ignoreTags.Contains(tag) Then
                    EatInnerContent(tag)
                End If
            ElseIf [Char].IsWhiteSpace(Peek()) Then
                ' Whitespace (treat all as space)
                _text.Write(If(_text.Preformatted, Peek(), " "c))
                MoveAhead()
            Else
                ' Other text
                _text.Write(Peek())
                MoveAhead()
            End If
        End While
        ' Return result
        Return HttpUtility.HtmlDecode(_text.ToString())
    End Function

    ' Eats all characters that are part of the current tag
    ' and returns information about that tag
    Protected Function ParseTag(ByRef selfClosing As Boolean) As String
        Dim tag As String = [String].Empty
        selfClosing = False

        If Peek() = "<"c Then
            MoveAhead()

            ' Parse tag name
            EatWhitespace()
            Dim start As Integer = _pos
            If Peek() = "/"c Then
                MoveAhead()
            End If
            While Not EndOfText AndAlso Not [Char].IsWhiteSpace(Peek()) AndAlso Peek() <> "/"c AndAlso Peek() <> ">"c
                MoveAhead()
            End While
            tag = _html.Substring(start, _pos - start).ToLower()

            ' Parse rest of tag
            While Not EndOfText AndAlso Peek() <> ">"c
                If Peek() = """"c OrElse Peek() = "'"c Then
                    EatQuotedValue()
                Else
                    If Peek() = "/"c Then
                        selfClosing = True
                    End If
                    MoveAhead()
                End If
            End While

            ''In this case it is javascirpt within a tag.
            If tag.ToLower = "a" AndAlso _html.Substring(start, _pos - start).Contains("javascript") Then tag = "ascript"

            MoveAhead()
        End If
        Return tag
    End Function

    ' Consumes inner content from the current tag
    Protected Function EatInnerContent(ByVal tag As String) As Boolean
        Dim endTag As String = "/" & tag

        While Not EndOfText
            If Peek() = "<"c Then
                ' Consume a tag
                Dim selfClosing As Boolean
                If ParseTag(selfClosing) = endTag Then
                    Return True
                End If
                ' Use recursion to consume nested tags
                If Not selfClosing AndAlso Not tag.StartsWith("/") Then
                    EatInnerContent(tag)
                    Return False
                End If
            Else
                MoveAhead()
            End If
        End While
        Return False
    End Function

    ' Returns true if the current position is at the end of
    ' the string
    Protected ReadOnly Property EndOfText() As Boolean
        Get
            Return (_pos >= _html.Length)
        End Get
    End Property

    ' Safely returns the character at the current position
    Protected Function Peek() As Char
        Return If((_pos < _html.Length), _html(_pos), ChrW(0))
    End Function

    ' Safely advances to current position to the next character
    Protected Sub MoveAhead()
        _pos = Math.Min(_pos + 1, _html.Length)
    End Sub

    ' Moves the current position to the next non-whitespace
    ' character.
    Protected Sub EatWhitespace()
        While [Char].IsWhiteSpace(Peek())
            MoveAhead()
        End While
    End Sub

    ' Moves the current position to the next non-whitespace
    ' character or the start of the next line, whichever
    ' comes first
    Protected Sub EatWhitespaceToNextLine()
        While [Char].IsWhiteSpace(Peek())
            Dim c As Char = Peek()
            MoveAhead()
            If c = ControlChars.Lf Then
                Exit While
            End If
        End While
    End Sub

    ' Moves the current position past a quoted value
    Protected Sub EatQuotedValue()
        Dim c As Char = Peek()
        If c = """"c OrElse c = "'"c Then
            ' Opening quote
            MoveAhead()
            ' Find end of value
            Dim start As Integer = _pos
            _pos = _html.IndexOfAny(New Char() {c, ControlChars.Cr, ControlChars.Lf}, _pos)
            If _pos < 0 Then
                _pos = _html.Length
            Else
                MoveAhead()
                ' Closing quote
            End If
        End If
    End Sub

    ''' <summary>
    ''' A StringBuilder class that helps eliminate excess whitespace.
    ''' </summary>
    Protected Class TextBuilder
        Private _text As StringBuilder
        Private _currLine As StringBuilder
        Private _emptyLines As Integer
        Private _preformatted As Boolean

        ' Construction
        Public Sub New()
            _text = New StringBuilder()
            _currLine = New StringBuilder()
            _emptyLines = 0
            _preformatted = False
        End Sub

        ''' <summary>
        ''' Normally, extra whitespace characters are discarded.
        ''' If this property is set to true, they are passed
        ''' through unchanged.
        ''' </summary>
        Public Property Preformatted() As Boolean
            Get
                Return _preformatted
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    ' Clear line buffer if changing to
                    ' preformatted mode
                    If _currLine.Length > 0 Then
                        FlushCurrLine()
                    End If
                    _emptyLines = 0
                End If
                _preformatted = value
            End Set
        End Property

        ''' <summary>
        ''' Clears all current text.
        ''' </summary>
        Public Sub Clear()
            _text.Length = 0
            _currLine.Length = 0
            _emptyLines = 0
        End Sub

        ''' <summary>
        ''' Writes the given string to the output buffer.
        ''' </summary>
        ''' <param name="s"></param>
        Public Sub Write(ByVal s As String)
            For Each c As Char In s
                Write(c)
            Next
        End Sub

        ''' <summary>
        ''' Writes the given character to the output buffer.
        ''' </summary>
        ''' <param name="c">Character to write</param>
        Public Sub Write(ByVal c As Char)
            If _preformatted Then
                ' Write preformatted character
                _text.Append(c)
            Else
                ' Ignore carriage returns. We'll process
                ' '\n' if it comes next
                If c = ControlChars.Cr Then
                ElseIf c = ControlChars.Lf Then
                    ' Flush current line
                    FlushCurrLine()
                ElseIf [Char].IsWhiteSpace(c) Then
                    ' Write single space character
                    Dim len As Integer = _currLine.Length
                    If len = 0 OrElse Not [Char].IsWhiteSpace(_currLine(len - 1)) Then
                        _currLine.Append(" "c)
                    End If
                Else
                    ' Add character to current line
                    _currLine.Append(c)
                End If
            End If
        End Sub

        ' Appends the current line to output buffer
        Protected Sub FlushCurrLine()
            ' Get current line
            Dim line As String = _currLine.ToString().Trim()

            ' Determine if line contains non-space characters
            Dim tmp As String = line.Replace("&nbsp;", [String].Empty)
            If tmp.Length = 0 Then
                ' An empty line
                _emptyLines += 1
                If _emptyLines < 2 AndAlso _text.Length > 0 Then
                    _text.AppendLine(line)
                End If
            Else
                ' A non-empty line
                _emptyLines = 0
                _text.AppendLine(line)
            End If

            ' Reset current line
            _currLine.Length = 0
        End Sub

        ''' <summary>
        ''' Returns the current output as a string.
        ''' </summary>
        Public Overrides Function ToString() As String
            If _currLine.Length > 0 Then
                FlushCurrLine()
            End If
            Return _text.ToString()
        End Function
    End Class
End Class
