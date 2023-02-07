## Executando

### Conexão com o DB
Para conectar-se a um banco de dados, crie um arquivo __.env__ em um dos seguintes diretórios:
* __ResidenciaDesafio3/Desafio1__ _(este diretório)_
* __ResidenciaDesafio3/Desafio1/Desafio1__

O arquivo deverá ter as seguintes variáveis:

```bash
HOST=[host]
DATABASE=[database]
USERNAME=[username]
PASSWORD=[password]
PORT=[port]
```
### Executando DB Localmente

Crie um arquivo __.env__ no diretório __ResidenciaDesafio3/Desafio1__ _(este diretório)_ da seguinte forma e execute o banco de dados via __docker compose__.

```bash
echo "HOST=localhost
DATABASE=[database]
USERNAME=[username]
PASSWORD=[password]
PORT=[port]" > .env
docker compose up -d db
```

#### Primeiro Uso: Atualizando as migrações no DB
```
dotnet ef database update
```

### Execute a aplicação.....