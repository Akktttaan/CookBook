go
use CookBookDB
INSERT INTO CuisineCategorys (Name) VALUES ('�����������');
INSERT INTO CuisineCategorys (Name) VALUES ('�����������');
INSERT INTO CuisineCategorys (Name) VALUES ('��������');

INSERT INTO DishTypes (Name) VALUES ('���');
INSERT INTO DishTypes (Name) VALUES ('�������� �����');
INSERT INTO DishTypes (Name) VALUES ('������');

INSERT INTO UnitOfMeasures (Name) VALUES ('�����');
INSERT INTO UnitOfMeasures (Name) VALUES ('��������');
INSERT INTO UnitOfMeasures (Name) VALUES ('����');

INSERT INTO GroceryItems (Name, UnitOfMeasureId, Price) VALUES ('����', 1, 50);
INSERT INTO GroceryItems (Name, UnitOfMeasureId, Price) VALUES ('����', 1, 10);
INSERT INTO GroceryItems (Name, UnitOfMeasureId, Price) VALUES ('�����', 1, 20);
INSERT INTO GroceryItems (Name, UnitOfMeasureId, Price) VALUES ('������', 2, 30);
INSERT INTO GroceryItems (Name, UnitOfMeasureId, Price) VALUES ('����', 3, 5);
INSERT INTO GroceryItems (Name, UnitOfMeasureId, Price) VALUES ('����', 1, 150);
INSERT INTO GroceryItems (Name, UnitOfMeasureId, Price) VALUES ('����', 1, 100);
INSERT INTO GroceryItems (Name, UnitOfMeasureId, Price) VALUES ('�����', 1, 50);
INSERT INTO GroceryItems (Name, UnitOfMeasureId, Price) VALUES ('������', 1, 80);

INSERT INTO DishRecipes (Name, CuisineCategoryId, DishTypeId, Margin) VALUES ('����� ���������', 1, 2, 200);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (1, 1, 200);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (1, 6, 100);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (1, 9, 50);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (1, 5, 2);

INSERT INTO DishRecipes (Name, CuisineCategoryId, DishTypeId, Margin) VALUES ('������ � �������������', 2, 1, 150);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (2, 7, 500);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (2, 5, 2);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (2, 2, 5);

INSERT INTO DishRecipes (Name, CuisineCategoryId, DishTypeId, Margin) VALUES ('��������� ����', 3, 3, 250);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (3, 1, 150);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (3, 9, 100);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (3, 3, 50);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (3, 5, 1);

INSERT INTO DishRecipes (Name, CuisineCategoryId, DishTypeId, Margin) VALUES ('����� ���������', 1, 2, 300);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (4, 1, 200);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (4, 6, 100);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (4, 9, 50);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (4, 5, 2);

INSERT INTO DishRecipes (Name, CuisineCategoryId, DishTypeId, Margin) VALUES ('������� � ��������', 2, 3, 100);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (5, 1, 150);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (5, 2, 5);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (5, 5, 1);

INSERT INTO DishRecipes (Name, CuisineCategoryId, DishTypeId, Margin) VALUES ('����� � �������', 3, 2, 350);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (6, 4, 300);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (6, 8, 200);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (6, 2, 5);
INSERT INTO RecipeIngredients (DishRecipeId, GroceryItemId, Quantity) VALUES (6, 5, 1);