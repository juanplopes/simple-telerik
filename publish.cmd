@echo off
setlocal

set GC_USER=%~1
if "%GC_USER%"=="" set /p GC_USER="User: "

set GC_PASS=%~2
if "%GC_PASS%"=="" set /p GC_PASS="Password: "

call build PublishSource "/p:GCUsername=%GC_USER%" "/p:GCPassword=%GC_PASS%"
call build Publish "/p:GCUsername=%GC_USER%" "/p:GCPassword=%GC_PASS%"
endlocal
