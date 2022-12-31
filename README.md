# Desafio1

## Arquitetura
O projeto segue a arquitetura __MVC__ _(Model View Controller)_ porque acredita-se que, separando o código em camadas, torna-se mais fácil a evolução da aplicação. <br><br>
Adicionalmente, pensou-se uma camada de __Dados__ _(Data)_ para abstrair as operações sobre os dados, já pensando em futuras mudanças sobre o armazenamento dos dados. Nessa camada, os arquivos estão sob uma pasta chamada __NonPersistent__ para explicitar que a implementação feita não persiste os dados informados. <br><br>
O diagrama abaixo representa a modelagem do código em camadas com suas respectivas classes. <br><br>
__OBS:__ Algumas classes criadas apenas para organização ou redução de código não estão representadas no diagrama.
<p align="center">
  <img src="diagrama.svg" alt="diagrama">
</p>

## Organização de Arquivos
Como mostra o diagrama acima, os arquivos estão organizados de acordo com suas camadas. Cada classe está na pasta referente a sua camada:
* __Model__
* __Data__
* __Controller__
* __View__

Adicionalmente, existem duas pastas: 
* __InFiles__ : _Arquivos testes_
* __OutFiles__ : _Saída dos arquivos testes_

__OBS:__ No arquivos _launchSettings.json_ da pasta __Properties__, está configurado para que a aplicação receba __stdin__ de um arquivo teste e redirecione __stdout__ e __stderr__ para outro arquivo. Ao deletar, o campo _commandLineArgs_ o programa funcionará por console novamente.

## Funcionalidades x Camadas
A fim de auxiliar na avaliação sobre a organização do código, identifica-se, nesta seção, em qual camada cada funcionalidade requisitada foi implementada.
<div align="center">
  <img src="requisitos (1).jpeg" alt="requisitos-1">
  <img src="requisitos (2).jpeg" alt="requisitos-2">
</div>

## Nomenclatura
Os métodos, classes e variáveis foram nomeados de acordo com uma única diretriz:
__Se o método, varável ou classe está associado diretamento a uma funcionalidade ou Entidade nomeada nos requisitos, então deverá ter o nome conforme encontra-se nos requisitos, caso contrário, terá um nome em inglês.__ <br>
<br>
Por exemplo, as Entidades Pacientee e Agendamento são nomeadas no documento de requisitos, portanto, as classes serão nomeadas Paciente e Agendamento respectivamente. Assim como os campos que cada entidade possue. Já, classes auxiliares, métodos privados, variáveis locais e outros derivados de requisitos não funcionais, são nomeados em inglês.
