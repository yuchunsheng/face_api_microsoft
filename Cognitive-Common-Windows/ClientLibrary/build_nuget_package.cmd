rem NOTE NOTE Copy the signed DLL into obj\release folder before running this command
nuget pack Microsoft.ProjectOxford.Common.csproj -Build -OutputDirectory bin\release -Properties Configuration=Release;Platform=AnyCPU