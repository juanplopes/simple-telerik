@echo off
setlocal

set GC_USER=%~1
if "%GC_USER%"=="" set /p GC_USER="User: "

set GC_PASS=%~2
if "%GC_PASS%"=="" set /p GC_PASS="Password: "

call build Publish "/property:BuildTarget=MVC1" "/property:GCUsername=%GC_USER%" "/property:GCPassword=%GC_PASS%"
call build Publish "/property:BuildTarget=MVC2" "/property:GCUsername=%GC_USER%" "/property:GCPassword=%GC_PASS%"
endlocal
