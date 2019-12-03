#!/bin/sh

cd WebApp


dotnet aspnet-codegenerator controller -name DeliverysController -actions -m Delivery -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name DrinksController -actions -m Drink -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name DrinksInOrderController -actions -m DrinkInOrder -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name OrdersController -actions -m Order -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name PizzasController -actions -m Pizza -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name PizzasInOrderController -actions -m PizzaInOrder -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ToppingsController -actions -m Topping -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ToppingsOnPizzaController -actions -m ToppingOnPizza -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
