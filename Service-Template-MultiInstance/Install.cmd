@echo off

rem set the current path to the assembly .exe
cd "C:\DATA\Dev\Service-Template-MultiInstance\Service-Template-MultiInstance\bin\Debug"

rem lookup installutil.exe
c:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /servicename=Service1 Service-Template-MultiInstance.exe /LogToConsole=true
c:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /servicename=Service2 Service-Template-MultiInstance.exe /LogToConsole=true

rem this one use the default service name and default InstallStateDir
c:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe Service-Template-MultiInstance.exe /LogToConsole=true 