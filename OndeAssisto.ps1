Write-Host "Bem-Vindo! Este script vai preparar o ambiente de desenvolvimento da solução 'OndeAssisto'!"

function configureWebApi(){

	$secretProjectName = "OndeAssisto.Web.Api"
	$secretDbPassword = "OndeAssistoDatabase:Password"
	$secretKeyJwtSecurityKey = "Jwt:SecurityKey"
	$secretKeyJwtExpiresAccess = "Jwt:Expires:Access"
	$secretKeyJwtExpiresRefresh = "Jwt:Expires:Refresh"

	Write-Host "Começando pelo projeto: $($secretProjectName)"

	$keys = $secretDbPassword, $secretKeyJwtSecurityKey, $secretKeyJwtExpiresAccess, $secretKeyJwtExpiresRefresh
	$values = $secretValueDbPassword, $secretValueJwtSecurityKey, $secretValueJwtExpiresAccess, $secretValueJwtExpiresRefresh

	$index = 0
	$values[$index] = Read-Host -Prompt "Digite a senha do banco de dados de desenvolvimento do projeto"
	dotnet user-secrets -p $secretProjectName set $keys[$index] $values[$index]
	$index++

	$values[$index] = New-Guid
	Write-Host "Gerando uma nova chave de segurança para $($keys[$index])..."
	dotnet user-secrets -p $secretProjectName set $keys[$index] $values[$index]
	$index++

	$values[$index] = Read-Host -Prompt "Digite quantos segundos deseja para a expiração do Access Json Web Token"
	dotnet user-secrets -p $secretProjectName set $keys[$index] $values[$index]
	$index++

	$values[$index] = Read-Host -Prompt "Digite quantos segundos deseja para a expiração do Refresh Json Web Token"
	dotnet user-secrets -p $secretProjectName set $keys[$index] $values[$index]
	$index++

	dotnet user-secrets -p $secretProjectName list
}

configureWebApi