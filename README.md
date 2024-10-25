API Creation Guide
Follow these steps to create a new API in the project:

1- create model and add it to dataContext

2- create migration for the new model

3- add model to database

4- create new service and interface for the model 

5- create dtos and enums if required

6 - create new controller


-------------------------------------------------------
product class example 


    Id --> int
    Name --> string
    Description -->string
    Price --> decimal
    ProductCategory --> Category(add new Enum ) . (e.g Electronics, HomeGoods, etc)
    ProductStatus --> Status(add new Enum ) . (e.g InStock, OutOfStock, etc)
    StockQuantity --> int
    CreatedAt --> DateTime
    UpdatedAt --> DateTime (nullable)

