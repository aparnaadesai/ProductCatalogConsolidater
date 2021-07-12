# ProductCatalogConsolidater
A console application to merge different Product Catalogs into one

##Problem Statement
https://github.com/tosumitagrawal/codingskills

##Assumptions
Assumed that when two products have the same barcode the source of the product would be the based on the first source name in ascending order, e.g. for two same products in catalog with source A and B, the output CSV would have one single product with source as A 

## Pre-requisites
- .NET 5 should be setup on the machine
- Visual Studio 2019

## How to run the project?

1. Clone the repository on your machine
2. Open the solution in Visual Studio
3. Populate input folder with barcodesA.csv, barcodesB.csv, catalogA.csv, catalogB.csv, suppliersA.csv, suppliersB.csv if you wish to replace the existing csv files
4. Run the project by pressing F5. This should create an output directory with result_output.csv





