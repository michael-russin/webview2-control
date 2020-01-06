dim objTest, intResult
Set objTest = WScript.CreateObject ("RemoteComObjectImpl.1")

intResult = objTest.Property
Wscript.echo "Result = " & intResult

objTest.Property = "Good Morning"
intResult = objTest.Property
Wscript.echo "Result = " & intResult