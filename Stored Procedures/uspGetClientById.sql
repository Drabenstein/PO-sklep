CREATE PROCEDURE dbo.uspGetClientById
    @Id_klienta INT
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
    WHERE k.Id_klienta = @Id_klienta;
END;
GO