# Recipe Box

#### _Store Recipes, 04/01/2021_

#### By _**Tristen Everett**_

## Description

This project was an attempt at showing the skills I learned to program in C#. In this project I built a ASP.NET MVC webapp that allows the user to add recipes to the database. The user will then be able to create recipes with a dynamic number of ingredients and instructions. The user will do this by checking a box on the create or edit views that will add additional space for ingredients or instructions, then when they submit the recipe to be added to the database the program will remove any empty lines. This project was built to meet the needs of the user stories listed below.

### User Stories

* As a user, I want to add a recipe with ingredients and instructions, so I remember how to prepare my favorite dishes.
* As a user, I want to edit my recipes, so I can make improvements or corrections to my recipes.
* As a user, I want to be able to delete recipes I don't like or use, so I don't have to see them as choices.
* As a user, I want to rate my recipes, so I know which ones are the best.
* As a user, I want to list my recipes by highest rated so I can see which ones I like the best.
* As a user, I want to see all recipes that use a certain ingredient, so I can more easily find recipes for the ingredients I have.

## Setup/Installation Requirements

1. Clone this Repo
2. Run `dotnet ef database update` from within /RecipeBox to create the database
3. You may need to update the file /RecipeBox/appsettings.json to match the userID and password for the computer your using
4. Run `dotnet restore` from within /RecipeBox file location
5. Run `dotnet build` from within /RecipeBox file location
6. Run `dotnet run` from within /RecipeBox file location
7. Using your preferred web browser navigate to http://localhost:5000/

## Technologies Used

* C#
* ASP.NET Core
* Entity Framework Core
* MYSQL
* Razor Pages

### License

This software is licensed under the MIT license

Copyright (c) 2021 **_Tristen Everett_**