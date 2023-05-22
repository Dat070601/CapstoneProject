FROM debian:bullseye-slim

RUN apt update && \
    apt install -y curl wget git && \
    apt-get install libicu-dev -y

RUN mkdir /root/.dotnet && \
    curl -L https://download.visualstudio.microsoft.com/download/pr/868b2f38-62ca-4fd8-93ea-e640cf4d2c5b/1e615b6044c0cf99806b8f6e19c97e03/dotnet-sdk-6.0.407-linux-x64.tar.gz \
    -o /root/.dotnet/dotnet6.tar.gz && \
    tar -xzf /root/.dotnet/dotnet6.tar.gz -C /root/.dotnet && \
    ln -s /root/.dotnet/dotnet /usr/bin/dotnet

RUN git clone https://github.com/Dat070601/CapstoneProject.git /tmp/CapstoneProject

WORKDIR /tmp/CapstoneProject/BookStore

RUN dotnet build

CMD ["dotnet", "run"]