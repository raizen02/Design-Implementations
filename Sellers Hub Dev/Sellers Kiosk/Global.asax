<%@ Application Inherits="System.Web.HttpApplication" Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
        EO.Web.Runtime.AddLicense(
    "P8/nrqXg5/YZ8p7cwp61n1mXpM0M66Xm+8+4iVmXpLHLn1mXwPIP41nr/QEQ" +
    "vFvE6f8goVnt6QMe6KjlwbPkrWmZpMDpjEOXpLHLn1mXpM0M452X+Aob5HaZ" +
    "1/0U457E6f8goVnt6QMe6KjlwbPkrWmZpMDpjEOXpLHLn1mXpM0M452X+Aob" +
    "5HaZ2PIN0q3p7QHNn6/c9gQU7qe0psrZr1uXs8+4iVmXpLHLn1mXwPIP41nr" +
    "/QEQvFvL9vYQ1aLc+7PL9Z7p9/oa7XaZvb/boVmmwp61n1mXpLHLn1mz5fUP" +
    "n63w9PbooYbs8AUUz5re6bPL9Z7p9/oa7XaZvb/boVmmwp61n1mXpLHLn1mz" +
    "5fUPn63w9PbooXzY8PYZ45rpprEh5Kvq7QAZvFuwssHNn2i1kZvLn1mXpLHL" +
    "n3XY6PXL87Ln6c7Nwprj8PMM4qSZpAcQ8azg8//ooXKltLPLrneEjrHLn1mX" +
    "pLHLu5rb6LEf+KncwbPsyXrP2QEX7prb6QPNn6/c9gQU7qe0psrZr1uXs8+4" +
    "iVmXpLHLn1mXwPIP41nr/QEQvFu77fIX7qCZpAcQ8azg8//ooXKltLPLrneE" +
    "jrHLn1mXpLHLu5rb6LEf+KncwbP+76Xg+AUQ8VuX+vYd8qLm8s7NuGenprHa" +
    "vUaBpLHLn1mXpLHn4J3bpAUk7560pt4M8qTc6NYP6K2ZpAcQ8azg8//ooXKl" +
    "tLPLrneEjrHLn1mXpLHLu5rb6LEf+KncwbPy8aLbprEh5Kvq7QAZvFuwssHN" +
    "n2i1kZvLn1mXpLHLn3XY6PXL87Ln6c7Nwqjj8wP76Jzi6QPNn6/c9gQU7qe0" +
    "psrZr1uXs8+4iVmXpLHLn1mXwPIP41nr/QEQvFvK9PYX63zf6fQW5KuZpAcQ" +
    "8azg8//ooXKltLPLrneEjrHLn1mXpLHLu5rb6LEf+KncwbPw46Lr8wPNn6/c" +
    "9gQU7qe0psrZr1uXs8+4iVmXpLHLn1mXwPIP41nr/QEQvFvA8fIS5JPm8/7N" +
    "n6/c9gQU7qe0psrZr1uXs8+4iVmXpLHLn1mXwPIP41nr/QEQvFu78wgZ66jY" +
    "6PYdoVnt6QMe6KjlwbPkrWmZpMDpjEOXpLHLn1mXpM0M452X+Aob5HaZyv0a" +
    "4K3c9rPL9Z7p9/oa7XaZvb/boVmmwp61n1mXpLHLn1mz5fUPn63w9PbooYzj" +
    "7fUQoVnt6QMe6KjlwbPkrWmZpMDpjEOXpLHLn1mXpM0M452X+Aob5HaZyv0k" +
    "7q7rprEh5Kvq7QAZvFuwssHNn2i1kZvLn1mXpLHLn3XY6PXL87Ln6c7NxJ3g" +
    "+PIN657D5fMQ61uX+vYd8qLm8s7NuGenprHavUaBpLHLn3Wm5f0X7rC1kZvL" +
    "n1mXwAAd457pzf8R7lnb5QUQvFuws8LfrmuntcHNn6/c9gQU7qe0psnNn2i1" +
    "kZvLn1mXwAQU5qfY+AYd5HfB7tQk7H2u8An/qo+nzcYDs6Gr3fb8vHazswQU" +
    "5qfY+AYd5HeEjs3a66La6f8e5HeEjnXj7fQQ7azcwp61n1mXpM0X6Jzc8gQQ" +
    "yJ21t8PbtW+ousHbr3Wm8PoO5Kfq6doPvUaBpLHLn3Xj7fQQ7azc6Q==")

    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error
        'Dim exc As Exception = Server.GetLastError

        'If (exc.GetType Is GetType(HttpException)) Then
        '    Dim ex As HttpException = CType(exc, HttpException)
        '    Dim httpCode As Decimal = ex.GetHttpCode

        '    If exc.Message.Contains("NoCatch") Or exc.Message.Contains("maxUrlLength") Then
        '        Return
        '    End If

        '    If httpCode >= 401 And httpCode <= 403 Then
        '        Response.Redirect("~/errorPage.aspx?aspxerrortype=401&aspxerrorpath=" & Request.Path)
        '    ElseIf httpCode >= 404 And httpCode < 405 Then
        '        Response.Redirect("~/errorPage.aspx?aspxerrortype=404&aspxerrorpath=" & Request.Path)
        '    Else
        '        Response.Redirect("~/errorPage.aspx?aspxerrortype=500&aspxerrorpath=" & Request.Path)
        '    End If
        'Else
        '    Response.Redirect("~/errorPage.aspx?aspxerrortype=500&aspxerrorpath=" & Request.Path)
        'End If
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
</script>