create database ong_adocao


use ong_adocao;

create table pessoa(
cpf varchar(11) not null,
nome varchar(50) not null,
sexo char(1) not null,
telefone varchar(11) not null,
endereco varchar(50),
ativa char(1) not null,
dataNascimento date not null

constraint pk_pessoa primary key(cpf)
);


alter table pessoa
add dataNascimento date not null

create table pet(
nChipPet int identity not null,
familiaPet varchar(50) not null,
racaPet varchar(20) not null,
sexoPet char(1),
NomePet varchar(50),
disponivel char(1),

constraint pk_pet primary key(nChipPet)
);

create table regAdocao(
cpf varchar(11) not null,
nChipPet int not null,

foreign key (cpf) references pessoa(cpf),
foreign key (nChipPet) references pet(nChipPet),
constraint pk_regAdocao primary key(cpf,nChipPet)
);

create table endereco(
cpf varchar(11) not null,
logradouro varchar(50) not null,
numero int not null,
bairro varchar(20) not null,
complemento varchar(20) not null,
cep varchar(8) not null,
cidade varchar(20) not null,
uf varchar(3) not null

foreign key (cpf) references pessoa(cpf)
);
