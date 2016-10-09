Imports Microsoft.VisualBasic
Imports CrystalDecisions.CrystalReports.Engine

Public Module modDeclarations
    Public Const MyBirthDate As String = "1983-01-14"
    Public Const MyMSSQLServer2000SuccessErrorNumber As Integer = 8888

    Public Const MyDefaultLogoutFile As String = "selLogout.aspx"
    Public Const MySSOSite As String = "https://kiosk.filinvest.com.ph/SSO-DEV/"
    Public Const MyTrackingCode As String = "UA-17143771-3"
    Public Const MyExceptionNotice As String = "Exception Notice : <br/>" & _
                                               "<blockquote><p>Sorry, but the event that has just occurred was not expected by this system.</p><p>&nbsp;</p>" & _
                                               "<p>An email message has already been sent to our Technical Services and Support team, notifying them of the issue. If this problem is not solved within the next two hours, please contact your system administrator to escalate its priority.</p></blockquote>"

    ' DEV Server
    ''Public Const MyDefaultFile As String = "Default.aspx"
    Public Const MyDefaultInitialFile As String = "dashboard.aspx"
    Public Const MyDefaultReportPath As String = "C:\Site\printOfDocu\Reports\"
    Public MyMailTo As String = "vinx96@yahoo.com;inquiry@filinvestinternational.com"

    Public Const MyMSSQLServer2000FREBASConnectionString As String = "Data Source=172.20.5.79; Initial Catalog=FREBAS-SIM2; User ID=o-frebas; Password=admserverfrebas; Connection Timeout=0;Min Pool Size=20;Max Pool Size=4096;Connection Lifetime=120;"
    Public Const MyMSSQLServer2000ConnectionString As String = "Data Source=localhost; Initial Catalog=Test; User ID=sa; Password=!qwerty123; Connection Timeout=0;Min Pool Size=20;Max Pool Size=4096;Connection Lifetime=120;"

    ' Public Const MyMSSQLServer2000ConnectionString As String = "Data Source=172.20.5.79; Initial Catalog=PropertyPortal-SIM2; User ID=usr-propertyportal; Password=Mvr&portal2; Connection Timeout=0;Min Pool Size=20;Max Pool Size=4096;Connection Lifetime=120;"
    'Public Const MyMSSQLServer2000ConnectionString As String = "Data Source=172.20.5.132; Initial Catalog=PropertyPortal-DEV;User ID=o-frebas; Password=admserverfrebas; Connection Timeout=0;Min Pool Size=20;Max Pool Size=4096;Connection Lifetime=120;"
    Public Const MyMSSQLServer2000ComKioskConnectionString As String = "Data Source=172.20.5.82; Initial Catalog=ComKiosk; User ID=o-comkiosk; Password=Mvr&com9; Connection Timeout=0;Min Pool Size=20;Max Pool Size=4096;Connection Lifetime=120;"

    ' DEV CAPTCHA Key
    Public MyPublicKey As String = "6LdyveUSAAAAAKVFftkBBTRrFwrbOvXkQi-d5ATy"
    Public MyPrivateKey As String = "6LdyveUSAAAAAOc9Ehi91havl1g1NKebFyS8hDii"

    Public MyMailFrom As String = "Filinvest Land Inc. <donotreply@filinvestland.com>"
    Public MyReportDocu As ReportDocument
End Module
