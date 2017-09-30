Create database Agenda
go
use Agenda
go
create table Contatos(
id int identity primary key,
nome varchar(100),
email varchar(100),
foneCel varchar(20),
foneRes varchar(20),
foneCom varchar(20),
ender varchar(100),
bairro varchar(50),
cidade varchar(50),
uf char(2),
cep varchar(9)
)
