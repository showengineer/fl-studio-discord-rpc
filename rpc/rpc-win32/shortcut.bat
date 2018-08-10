@echo off
ECHO Lauching FL Studio

REM Change this if FL Studio was installed elsewhere
start "" "C:\Program Files (x86)\Image-Line\FL Studio 20\FL64.exe"

ECHO FL Studio launched
ECHO.
ECHO Cancel if RPC shouldn't be enabled
TIMEOUT /T 10 /NOBREAK

rpc-win32