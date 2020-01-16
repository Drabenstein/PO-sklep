USE [PO-sklep]
GO
/****** Object:  StoredProcedure [dbo].[uspGetAllProducts]    Script Date: 14.01.2020 13:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].uspAddReview
	@Id_produktu INT,
	@Ocena TINYINT,
	@Komentarz NVARCHAR(1024) NULL = NULL,
	@Czy_potwierdzona_zakupem BIT = 0,
	@Email NVARCHAR(255)
AS
BEGIN
	IF NOT EXISTS(SELECT 1
	FROM dbo.Produkt p
	WHERE p.Id_produktu = @Id_produktu)
	BEGIN
		RAISERROR('Product with specified @Id_produktu does not exist', 18, 0);
		RETURN;
	END;

	IF (ISNULL(@Ocena, 0) = 0)
	BEGIN
		RAISERROR('Invalid parameter: @Ocena cannot be NULL or zero', 18, 0);
		RETURN;
	END;

	IF (ISNULL(@Czy_potwierdzona_zakupem, -1) = -1)
	BEGIN
		SET @Czy_potwierdzona_zakupem = 0;
	END;

	DECLARE @Klient_id INT;

	IF NOT EXISTS(SELECT 1
	FROM dbo.Klient k
	WHERE Email = @Email)
	BEGIN
		INSERT INTO dbo.Klient
			(
			Imie_klienta,
			Nazwisko_klienta,
			Email
			)
		VALUES
			(
				SUBSTRING(LEFT(@Email, CHARINDEX('@', @Email) - 1), 1, 50), -- Imie_klienta - nvarchar
				SUBSTRING(LEFT(@Email, CHARINDEX('@', @Email) - 1), 1, 50), -- Nazwisko_klienta - nvarchar
				@Email -- Email - nvarchar
		)
		SET @Klient_id = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		SELECT @Klient_id = Id_klienta
		FROM dbo.Klient k
		WHERE Email = @Email;
	END;

	INSERT INTO dbo.Opinia
		(
		Ocena,
		Komentarz,
		Czy_potwierdzona_zakupem,
		Id_produktu,
		Id_klienta
		)
	VALUES
		(
			@Ocena, -- Ocena - tinyint
			@Komentarz, -- Komentarz - nvarchar
			@Czy_potwierdzona_zakupem, -- Czy_potwierdzona_zakupem - bit
			@Id_produktu, -- Id_produktu - int
			@Klient_id -- Id_klienta - int
	);

	RETURN SCOPE_IDENTITY();
END
GO