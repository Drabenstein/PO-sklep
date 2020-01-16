CREATE TYPE dbo.CheckoutItems AS TABLE
(Id_produktu INT NOT NULL,
    Ilosc INT NOT NULL
);
GO

CREATE PROCEDURE dbo.uspSubmitOrder
    @Id_klienta         INT,
    @Id_sposobu_dostawy INT,
    @Id_typu_platnosci  INT               = NULL,
    @Lista              dbo.CheckoutItems READONLY
AS
BEGIN
    IF NOT EXISTS
        (
            SELECT 1
    FROM dbo.Klient k
    WHERE k.Id_klienta = @Id_klienta
        )
            BEGIN
        RAISERROR('Invalid parameter: Client with id @Id_klienta does not exist', 18, 0);
        RETURN;
    END;
    IF NOT EXISTS
        (
            SELECT 1
    FROM Sposob_dostawy
    WHERE Id_sposobu_dostawy = @Id_sposobu_dostawy
        )
            BEGIN
        RAISERROR('Invalid parameter: @Id_sposobu_dostawy does not exist', 18, 0);
        RETURN;
    END;
    IF NOT EXISTS
        (
            SELECT 1
    FROM Typ_platnosci
    WHERE @Id_typu_platnosci IS NULL
        OR Id_typu_platnosci = @Id_typu_platnosci
        )
            BEGIN
        RAISERROR('Invalid parameter: @Id_typu_platnosci is not null and is not a recognized payment type', 18, 0);
        RETURN;
    END;
    IF EXISTS
        (
            SELECT 1
    FROM @Lista l
    WHERE l.Ilosc <= 0
        OR NOT EXISTS
            (
                SELECT 1
        FROM Produkt
        WHERE Id_produktu = l.Id_produktu
            )
        )
            BEGIN
        RAISERROR('Invalid parameter: @Lista contains either not existent product or non-positive product count', 18, 0);
        RETURN;
    END;
    BEGIN TRANSACTION;
    BEGIN TRY
            DECLARE @Id_platnosci INT= NULL;
            IF @Id_typu_platnosci IS NOT NULL
                BEGIN
        INSERT INTO dbo.Platnosc
            (Data_platnosci,
            Id_typu_platnosci
            )
        VALUES
            (SYSDATETIME(),
                @Id_typu_platnosci
                    );
        SET @Id_platnosci = SCOPE_IDENTITY();
    END;
            DECLARE @Id_statusu_zamowienia INT;
            SELECT @Id_statusu_zamowienia = Id_statusu
    FROM dbo.Status_zamowienia
    WHERE Nazwa_statusu = IIF(@Id_typu_platnosci IS NULL, N'Oczekujące na płatność', N'W realizacji');
            INSERT INTO dbo.Zamowienie
        (Id_statusu,
        Id_platnosci,
        Id_sposobu_dostawy,
        Id_klienta
        )
    VALUES
        (@Id_statusu_zamowienia, -- Id_statusu - int
            @Id_platnosci, -- Id_platnosci
            @Id_sposobu_dostawy, -- Id_sposobu_dostawy - int
            @Id_klienta -- Id_klienta - int
            );
            DECLARE @Id_zamowienia INT= SCOPE_IDENTITY();
            INSERT INTO dbo.Zamowienie_Produkt
        (Id_produktu,
        Id_zamowienia,
        Ilosc
        )
    SELECT l.Id_produktu,
        @Id_zamowienia,
        l.Ilosc
    FROM @Lista l;
            COMMIT;
            SELECT @Id_zamowienia;
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0
                BEGIN
        ROLLBACK TRANSACTION;
        THROW;
    END;
        END CATCH;
END;
GO