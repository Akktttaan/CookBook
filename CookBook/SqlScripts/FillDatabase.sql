go
use CookBookDB
INSERT INTO CuisineCategorys (Name) VALUES ('Итальянская');
INSERT INTO CuisineCategorys (Name) VALUES ('Французская');
INSERT INTO CuisineCategorys (Name) VALUES ('Японская');

INSERT INTO DishTypes (Name) VALUES ('Суп');
INSERT INTO DishTypes (Name) VALUES ('Основное блюдо');
INSERT INTO DishTypes (Name) VALUES ('Десерт');

INSERT INTO UnitOfMeasures (Name) VALUES ('грамм');
INSERT INTO UnitOfMeasures (Name) VALUES ('милилитр');
INSERT INTO UnitOfMeasures (Name) VALUES ('штук');

INSERT INTO GroceryItems (Name, UnitOfMeasureId, Price) VALUES ('Мука', 1, 50);
INSERT INTO GroceryItems (Name, UnitOfMeasureId, Price) VALUES ('Соль', 1, 10);
INSERT INTO GroceryItems (Name, UnitOfMeasureId, Price) VALUES ('Сахар', 1, 20);
INSERT INTO GroceryItems (Name, UnitOfMeasureId, Price) VALUES ('Молоко', 2, 30);
INSERT INTO GroceryItems (Name, UnitOfMeasureId, Price) VALUES ('Яйцо', 3, 5);
INSERT INTO GroceryItems (Name, UnitOfMeasureId, Price) VALUES ('Мясо', 1, 150);
INSERT INTO GroceryItems (Name, UnitOfMeasureId, Price) VALUES ('Рыба', 1, 100);
INSERT INTO GroceryItems (Name, UnitOfMeasureId, Price) VALUES ('Овощи', 1, 50);
INSERT INTO GroceryItems (Name, UnitOfMeasureId, Price) VALUES ('Фрукты', 1, 80);

INSERT INTO DishRecipes (Name, CuisineCategoryId, DishTypeId, Margin) VALUES ('Паста карбонара', 1, 2, 200);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (1, 1, 200);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (1, 6, 100);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (1, 9, 50);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (1, 5, 2);

INSERT INTO DishRecipes (Name, CuisineCategoryId, DishTypeId, Margin) VALUES ('Бульон с фрикадельками', 2, 1, 150);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (2, 7, 500);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (2, 5, 2);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (2, 2, 5);

INSERT INTO DishRecipes (Name, CuisineCategoryId, DishTypeId, Margin) VALUES ('Фруктовый тарт', 3, 3, 250);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (3, 1, 150);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (3, 9, 100);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (3, 3, 50);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (3, 5, 1);

INSERT INTO DishRecipes (Name, CuisineCategoryId, DishTypeId, Margin) VALUES ('Пицца маргарита', 1, 2, 300);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (4, 1, 200);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (4, 6, 100);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (4, 9, 50);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (4, 5, 2);

INSERT INTO DishRecipes (Name, CuisineCategoryId, DishTypeId, Margin) VALUES ('Булочки с кунжутом', 2, 3, 100);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (5, 1, 150);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (5, 2, 5);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (5, 5, 1);

INSERT INTO DishRecipes (Name, CuisineCategoryId, DishTypeId, Margin) VALUES ('Роллы с лососем', 3, 2, 350);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (6, 4, 300);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (6, 8, 200);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (6, 2, 5);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (6, 5, 1);