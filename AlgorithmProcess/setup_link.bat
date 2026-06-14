@echo off
:: 自動取得批次檔所在的目前目錄，並建立 C 槽連結
mklink /d "C:\AlgorithmRecipe" "%~dp0AlgorithmRecipe"
pause