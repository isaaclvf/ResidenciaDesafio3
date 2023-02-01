## Executar DB Localmente (Docker Compose)

### Executando o DB
Substituir os campos entre "[]" pelos valores desejado

```bash
echo "HOST=localhost
DATABASE=[database]
USERNAME=[username]
PASSWORD=[password]
PORT=[port]" > .env
docker compose up -d db
```

#### Primeiro Uso: Atualizando as migrações no DB
```bash
dotnet ef database update
```

#### Acessando o postgres via psql
```bash
docker exec -it postgres-container psql -U [username] -d [database]
```
#### Ver todas as tabelas
```
\dt
```
#### Comandos SQL são aceitos no PSQL
```sql
select * from [table];
``` 

### Executando o DB com PGAdimin
Substituir os campos entre "[]" pelos valores desejado <br>
**Obs:** O email não precisa ser válido.

```bash
echo "HOST=localhost
DATABASE=[database]
USERNAME=[username]
PASSWORD=[password]
PORT=[port]
PGADMIN_PORT=[other_port]
PGADMIN_PASSWORD=[password]
PGADMIN_EMAIL=[email]" > .env
docker compose up -d
```

Acessar no browser __localhost:[PGADMIN_PORT]__, logar com __[PGADMIN_EMAIL]__ e __[PGADMIN_PASSWORD]__

