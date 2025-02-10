FROM mcr.microsoft.com/dotnet/aspnet:8.0	

COPY /app /app

WORKDIR /app

CMD ["dotnet", "Server.dll"] 