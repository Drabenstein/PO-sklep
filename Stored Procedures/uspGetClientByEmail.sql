CREATE PROCEDURE dbo.uspGetClientByEmail
    @Email NVARCHAR(255)
AS
BEGIN
    SELECT k.Id_klienta,
        k.Imie_klienta,
        k.Nazwisko_klienta,
        k.Email,
        k.Adres,
        k.Data_urodzenia,
        k.Id_konta
    FROM dbo.Klient k
    WHERE k.Email = @Email;
END;
GO