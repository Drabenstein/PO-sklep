CREATE PROCEDURE dbo.uspAddClientByEmail
    @Email NVARCHAR(255)
AS
BEGIN
    INSERT INTO dbo.Klient
        (Imie_klienta,
        Nazwisko_klienta,
        Email
        )
    VALUES
        (SUBSTRING(LEFT(@Email, CHARINDEX('@', @Email) - 1), 1, 50), -- Imie_klienta - nvarchar
            SUBSTRING(LEFT(@Email, CHARINDEX('@', @Email) - 1), 1, 50), -- Nazwisko_klienta - nvarchar
            @Email -- Email - nvarchar
        );
    SELECT SCOPE_IDENTITY();
END;
GO