@echo off

rem set the current path to the assembly .exe
cd "C:\DATA\Dev\Service-Template-MultiInstance\Service-Template-MultiInstance\bin\Debug"

if not exist c:\DATA\SVC1 mkdir c:\DATA\SVC1
if not exist c:\DATA\SVC2 mkdir c:\DATA\SVC2

rem lookup installutil.exe
c:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /servicename=Service1 Service-Template-MultiInstance.exe /InstallStateDir="C:\Data\SVC1" /LogToConsole=true
c:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /servicename=Service2 Service-Template-MultiInstance.exe /InstallStateDir="C:\Data\SVC2" /LogToConsole=true

rem this one use the default service name and default InstallStateDir
c:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe Service-Template-MultiInstance.exe /LogToConsole=true 