set mydir=%~dp0

Powershell -Command "& { Start-Process \"%mydir%Register_ebCrypt.bat\" -verb RunAs}"