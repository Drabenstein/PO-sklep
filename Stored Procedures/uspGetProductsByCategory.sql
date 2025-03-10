USE [PO-sklep]
GO
/****** Object:  StoredProcedure [dbo].[uspGetAllProducts]    Script Date: 14.01.2020 13:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].uspGetProductsByCategory
@Id_kategorii INT
AS
BEGIN
	SELECT	p.Id_produktu, 
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
	WHERE p.Id_kategorii = COALESCE(@Id_kategorii, p.Id_kategorii);
END
GO