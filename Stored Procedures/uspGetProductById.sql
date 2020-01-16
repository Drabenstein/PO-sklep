USE [PO-sklep]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].uspGetProductById
	@Id_produktu INT
AS
BEGIN
	SELECT p.Id_produktu,
		p.Id_kategorii,
		p.Nazwa_produktu,
		p.Opis,
		p.Producent,
		p.Cena_netto,
		p.VAT,
		o.Id_opinii,
		o.Komentarz,
		o.Ocena,
		k.Id_klienta,
		k.Email
	FROM Produkt p
		LEFT JOIN Opinia o ON o.Id_produktu = p.Id_produktu
		LEFT JOIN Klient k ON k.Id_klienta = o.Id_klienta
	WHERE p.Id_produktu = COALESCE(@Id_produktu, p.Id_produktu);
END
GO