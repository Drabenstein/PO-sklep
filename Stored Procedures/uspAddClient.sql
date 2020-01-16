CREATE PROCEDURE dbo.uspAddClient
    @Imie_klienta     NVARCHAR(50),
    @Nazwisko_klienta NVARCHAR(50),
    @Email            NVARCHAR(255) = NULL,
    @Adres            NVARCHAR(255) = NULL,
    @Data_urodzenia   DATETIME2(7)  = NULL
AS
BEGIN
    INSERT INTO dbo.Klient
        (Imie_klienta,
        Nazwisko_klienta,
        Email,
        Adres,
        Data_urodzenia
        )
    VALUES
        (@Imie_klienta, -- Imie_klienta - nvarchar
            @Nazwisko_klienta, -- Nazwisko_klienta - nvarchar
            @Email, -- Email - nvarchar
            @Adres, -- Adres - nvarchar
            @Data_urodzenia -- Data_urodzenia - datetime2
        );
    SELECT SCOPE_IDENTITY();
END;
GO