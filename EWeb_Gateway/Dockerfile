
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

#solution altındaki proje dosyalarını kopyala
COPY *.csproj ./
#projeyi geri yükleme işlemini başlat
RUN dotnet restore

#projenin tüm dosyalarını kopyala
COPY . .
#projeyi yayınlayabilir hale getir
RUN dotnet publish -c Release -o out

#çalışma zamanında gerekli olan sdk runtime paketi yükle
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime

WORKDIR /app

#
COPY --from=build /app/out ./

EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "EWeb_Gateway.dll"]

#docker build -t e_web_gateway .
#docker run -d -p 6000:80 --name  e_web_gateway e_web_gateway