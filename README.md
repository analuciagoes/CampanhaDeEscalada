##### Campanha de Escalada - Sistema de Comissões 

## Descrição
Sistema console desenvolvido em C# para calcular comissões de vendedores com base em dados de vendas armazenados em formato JSON.

## Tecnologias
- .NET 8
- C# 12.0
- System.Text.Json para deserialização
- Encoding UTF-8 para suporte a caracteres acentuados

## Funcionalidades
- Leitura de dados de vendas de arquivo JSON
- Cálculo automático de comissões por faixas de valor
- Agrupamento de vendas por vendedor
- Relatório completo com totais individuais e gerais
- Formatação em moeda brasileira (pt-BR)

## Regras de Comissão
| Valor da Venda		| Percentual |
|-----------------------|------------|
| < R$ 100,00		    | 0%         |
| R$ 100,00 - R$ 499,99 | 1%		 |
| ≥ R$ 500,00		    | 5%         |

## Estrutura do Arquivo JSON
O arquivo `vendas.json` deve seguir o formato:

{ "vendas": [ { "vendedor": "João Silva", "valor": 1200.50 }, 
{ "vendedor": "Maria Santos", "valor": 950.75 } ] }

## Como Executar
1. Certifique-se de ter o .NET 8 instalado
2. Coloque o arquivo `vendas.json` no diretório do executável
3. Execute o comando

## Saída do Sistema

=== RELATÓRIO DE COMISSÕES ===
Vendedor: João Silva
Quantidade de vendas: 3
Total em vendas: R$ 3.951,25
Total em comissões: R$ 197,56
TOTAL GERAL EM VENDAS: R$ 15.804,50 TOTAL GERAL EM COMISSÕES: R$ 790,23

## Requisitos
- Arquivo `vendas.json` deve estar em encoding UTF-8
- Valores monetários devem usar ponto decimal (formato americano) no JSON