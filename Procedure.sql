EXEC SP_RENAME 'Receipes.Descrip' , 'Description', 'COLUMN'
GO

CREATE PROCEDURE AddReceipe @title nvarchar(50), @description nvarchar(300), @prepareTime time, @note nvarchar(350),
@ingredient nvarchar(20), @unitId int, @receipeId int, @ingredientId int, @quantity int 
AS
BEGIN
	IF NOT EXISTS(SELECT Id FROM Receipes WHERE Title = @title)
	BEGIN
		INSERT INTO Receipes (Title, [Description], PrepareTime, Note) VALUES (@title, @description, @prepareTime, @note)
		SELECT @receipeId = MAX(Id) FROM Receipes 
	END
	ELSE
	BEGIN
		SELECT @receipeId = Id FROM Receipes WHERE Title = @title
	END
	IF (@ingredientId = 0)
	BEGIN
		INSERT INTO Ingredients(Ingredient, UnitId) VALUES (@ingredient, @unitId)
		SELECT @ingredientId = Id FROM Ingredients WHERE Ingredient = @ingredient
	END
	INSERT INTO ReceipesIngredients (ReceipeId, IngredientId, Quantity) VALUES (@receipeId, @ingredientId, @quantity)
END

SELECT * FROM ReceipesIngredients WHERE ReceipeId = 2

SELECT * FROM Receipes
INSERT INTO Receipes (Title, [Description], Note, PrepareTime) VALUES (N'¿‡‡‡', N'awda', N'adawd', N'00:05:00')

SELECT * FROM Receipes
ORDER BY PrepareTime
GO

CREATE PROCEDURE GetFiltredReceipes @ingredient nvarchar(30)
AS
BEGIN
	SELECT * FROM (SELECT Receipes.Id as RecId, Title, PrepareTime, [Description], Note, ReceipeId, IngredientId, Quantity, Ingredients.Id as IngId,
	Ingredient, UnitId  FROM Receipes JOIN ReceipesIngredients
	ON Receipes.Id = ReceipesIngredients.ReceipeId JOIN Ingredients
	ON Ingredients.Id = ReceipesIngredients.IngredientId) AS rec
	WHERE rec.Ingredient IN	(N'ˇÈˆÓ')
END